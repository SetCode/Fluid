using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Algo;
using System.Diagnostics;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Sensors.Heater;
using System.Windows.Forms;
using static Anda.Fluid.Domain.Conveyor.Flag.UILevel;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive.MachineStates;
using Anda.Fluid.Domain.SoftFunction.LotControl;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Domain.Data;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Domain.SoftFunction.PatternWeight;
using Anda.Fluid.Drive.Vision;
using Anda.Fluid.Domain.DataStatistics.DownTime;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Domain.RTV;
using Anda.Fluid.Domain.Custom;
using Anda.Fluid.Drive.Motion.Command;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.SoftFunction;

namespace Anda.Fluid.Domain.FluProgram
{
    /// <summary>
    /// 脚本执行器，支持 运行、单步运行、暂停、恢复、终止 操作
    /// </summary>
    public class Executor : IMsgSender, IAlarmSenderable, IAlarmDiObservable, IMsgI18N
    {
        #region 运行状态

        public enum WorkState
        {
            Idle,
            Preparing,
            WaitingForBoard,
            Programing,
            Stopping
        }

        /// <summary>
        /// 程序运行状态(内部)
        /// </summary>
        private enum ProgramInnerState
        {
            /// <summary>
            /// 空闲状态-未运行
            /// </summary>
            IDLE,
            /// <summary>
            /// 正在运行中状态
            /// </summary>
            RUNNING,
            /// <summary>
            /// 暂停状态
            /// </summary>
            PAUSED,
            /// <summary>
            /// 中止状态
            /// </summary>
            ABORTED
        }
        /// <summary>
        /// 程序运行状态(外部)
        /// </summary>
        public enum ProgramOuterState
        {
            /// <summary>
            /// 空闲状态-未运行
            /// </summary>
            IDLE,
            /// <summary>
            /// 运行中
            /// </summary>
            RUNNING,
            /// <summary>
            /// 用户执行了 暂停 操作，正在等待当前指令执行完成后暂停运行
            /// </summary>
            PAUSING,
            /// <summary>
            /// 已暂停
            /// </summary>
            PAUSED,
            /// <summary>
            /// 用户执行了 中止 操作，正在等待当前指令执行完成后中止运行
            /// </summary>
            ABORTING,
            /// <summary>
            /// 已中止
            /// </summary>
            ABORTED
        }

        /// <summary>
        /// 当前工作状态
        /// </summary>
        public WorkState CurrWorkState { get; private set; } = WorkState.Idle;

        /// <summary>
        /// 程序运行状态，由内部状态计算外部状态
        /// </summary>
        public ProgramOuterState CurrProgramState
        {
            get { return calculateOuterState(runningState, pendingState); }
        }

        /// <summary>
        /// 当前运行状态(内部)
        /// </summary>
        private volatile ProgramInnerState runningState = ProgramInnerState.IDLE;

        /// <summary>
        /// 即将到来的运行状态：
        /// 当用户执行 单步执行/暂停/停止 操作后，需要等待当前指令执行完成；
        /// 在等待期间，当前状态仍然是RUNNING，但是执行完当前指令后，状态将切换为pendingState值
        /// </summary>
        private volatile ProgramInnerState pendingState = ProgramInnerState.IDLE;
        // 注：以上两个变量虽然会在不同的线程中被操作，但是由于操作所处的运行状态是单一的，运行状态之间是互斥的，所以不需要加锁

        private bool isOffline = false;
        /// <summary>
        /// 用于在判断当前程序是离线(编程模式还是生产模式)
        /// </summary>
        public bool IsOffline
        {
            get { return isOffline; }
        }


        #endregion


        #region Internal Vars

        private const string TAG = "Executor";
        // 运行线程
        private Thread runningThread;
        // 根据mark点拍照实际位置，对轨迹命令的坐标进行校正
        private CoordinateCorrector coordinateCorrector;
        // 秒表，计算CycleTime
        private Stopwatch stopWatch = new Stopwatch();
        // 当程序暂停运行时，下一个待执行的命令
        private volatile SupportDirectiveCmd currPausedCmd;
        // 停止中标志位
        private bool stopping = false;
        private bool isSingleStep = false;
        /// <summary>
        /// 判断是否有pattern是双阀模式（双阀时使用）
        /// </summary>
        private bool hasSimulPattern = false;
        // lot control 功能对象，提供相关方法和数据记录
        private LotController lotController;
        // Workpiece数据库对象
        private workpiece dbWorkpiece;
        // 使用全局NozzleCheck 时用于
        private bool nozzleCheckIsFinish = false;

        private List<NozzleCheckCmd> finishNozzleCheckCmds = new List<NozzleCheckCmd>();

        private Custom.ICustomary customary;

        #endregion


        #region 运行时临时变量

        /// <summary>
        /// 单次运行开始时间
        /// </summary>
        public DateTime ProgramRunTime;

        /// <summary>
        /// 单次运行结束时间
        /// </summary>
        public DateTime ProgramEndTime;

        /// <summary>
        /// 无效数据
        /// </summary>
        private const int VALUE_INVALID = -1;

        /// <summary>
        /// 上一次运行步数（只对需要外部去执行的命令计数）
        /// </summary>
        private volatile int lastFinishedSteps = 0;

        /// <summary>
        /// 当前已经运行的步数（只对需要外部去执行的命令计数）
        /// </summary>
        private volatile int currFinishedSteps = 0;

        /// <summary>
        /// 运行完指定的步数后暂停运行
        /// </summary>
        private volatile int pausedAtStep = VALUE_INVALID;

        /// <summary>
        /// 程序未开始运行时指定程序将在指定的步数后暂停运行
        /// </summary>
        //private volatile int pendingPausedAtStep = VALUE_INVALID;
        // 注：以上三个变量虽然会在不同的线程中被操作，但是由于操作所处的运行状态是单一的，运行状态之间是互斥的，所以不需要加锁

        /// <summary>
        /// 是否允许运行：当程序暂停时，通过该同步工具阻塞运行线程
        /// </summary>
        private ManualResetEventSlim permitRunning = new ManualResetEventSlim(false);

        /// <summary>
        /// 记录每个 DoMultipassCmd 的各个 pass block 的 Timer 延时截止时间戳
        /// </summary>
        private Dictionary<DoMultipassCmd, Dictionary<int, long>> doMultipassPassBlockDelayTimes = new Dictionary<DoMultipassCmd, Dictionary<int, long>>();

        /// <summary>
        /// 记录每个 DoMultipassCmd 里面的 pass block 序号列表
        /// </summary>
        private Dictionary<DoMultipassCmd, List<int>> doMultipassPassBlockIndexes = new Dictionary<DoMultipassCmd, List<int>>();

        /// <summary>
        /// 记录每个cycle中mes或其他方式获取到的需要跳过的穴位号
        /// </summary>
        public List<int> SkipBoardsNo = new List<int>();
        #endregion


        #region 回调接口定义

        // 程序状态发生变化
        public delegate void OnStateChangedEventHandler(ProgramOuterState oldState, ProgramOuterState newState);
        public event OnStateChangedEventHandler OnStateChanged;

        #endregion


        #region 单例模式

        private readonly static Executor instance = new Executor();
        private Executor() { this.ReadMsgLanguageResource(); }
        public static Executor Instance => instance;

        #endregion


        #region Properties

        /// <summary>
        /// 拍mark失败，等待手动操作Event
        /// </summary>
        public ManualResetEvent WaitMarkManualDone { get; private set; } = new ManualResetEvent(false);

        public ManualResetEvent WaitBlobsManualDone { get; private set; } = new ManualResetEvent(false);

        /// <summary>
        /// 拍mark失败，存储手动操作窗口结果
        /// 注：手动窗口必须在UI线程创建
        /// </summary>
        public DialogResult FindMarkDialogResult = DialogResult.OK;

        public DialogResult FindBlobsDialogResult = DialogResult.OK;

        /// <summary>
        /// 脚本程序
        /// </summary>
        public FluidProgram Program { get; private set; }

        /// <summary>
        /// 轨道1点胶脚本程序
        /// </summary>
        public FluidProgram Conveyor1Program { get; set; }

        /// <summary>
        /// 轨道2点胶脚本程序
        /// </summary>
        public FluidProgram Conveyor2Program { get; set; }

        /// <summary>
        /// 当程序暂停运行时，下一个待执行的命令
        /// </summary>
        public SupportDirectiveCmd CurrPausedCmd => currPausedCmd;

        /// <summary>
        /// 循环次数
        /// </summary>
        public int Cycle { get; set; } = 0;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDateTime { get; private set; }

        /// <summary>
        /// 完成板数
        /// </summary>
        public int FinishedBoardCount { get; private set; } = 0;

        /// <summary>
        /// 异常板数
        /// </summary>
        public int FailedCount { get; set; } = 0;

        /// <summary>
        /// 秒表时间，单位秒
        /// </summary>
        public double WatchTime => (double)this.stopWatch.ElapsedMilliseconds / 1000.0;

        /// <summary>
        /// 运行周期，单位秒
        /// </summary>
        public double CycleTime { get; private set; }

        public object Obj => this;

        public string Name => this.ToString();

        public bool IsContinueAfterAbort = false;

        /// <summary>
        /// 当前开始生产的产品条码
        /// </summary>
        private string CurProductionBarcode = "";

        public bool IsClearAllCount { get; set; } = false;


        /// <summary>
        /// 设置点胶程序
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        public Executor SetProgram(FluidProgram program)
        {
            if (program == null)
            {
                throw new Exception("program is null.");
            }
            this.Program = program;
            return this;
        }

        #endregion

        #region 弹窗提示语言文本
        /// <summary>
        /// 0  "运动到位异常停机",
        /// 1  "气压检测异常停机",
        /// 2  "非法飞拍mark偏移值",
        /// 3  "是否忽略?",
        /// 4  "校准mark偏移量错误,请确定mark参数是否正确",
        /// 5  "U轴默认速度不能为0",
        /// 6  "U轴运行速度不能为0",
        /// 7  "U轴运行加速度不能为0",
        /// 8  "阀1清洗失败!",
        /// 9  "阀2清洗失败!",
        /// 10  "阀1称重失败!",
        /// 11  "阀2称重失败!",
        /// 12  "飞拍mark图像处理超时!",
        /// 13  "获取轨道条码失败!",
        /// 14  "获取轨道条码失败!",
        /// </summary>
        private string[] msgText = new string[] 
        {
            "运动到位异常停机",/* 0 */
            "气压检测异常停机",/* 1 */
            "非法飞拍mark偏移值",/* 2 */
            "是否忽略?",/* 3 */
            "校准mark偏移量错误,请确定mark参数是否正确",/* 4 */
            "U轴默认速度不能为0",/* 5 */
            "U轴运行速度不能为0",/* 6 */
            "U轴运行加速度不能为0",/* 7 */
            "阀1清洗失败!",/* 8 */
            "阀2清洗失败!",/* 9 */
            "阀1称重失败!",/* 10 */
            "阀2称重失败!",/* 11 */
            "飞拍mark图像处理超时!",/* 12 */
            "获取轨道1条码失败!",/* 13 */
            "获取轨道2条码失败!",/* 14 */
        };
        #endregion


        #region 用户操作：运行 单步运行 暂停 恢复 终止

        public event Action<WorkState> OnWorkStateChanged;
        public event Action OnWorkRunning;
        public event Action OnWorkDone;
        public event Action OnWorkStop;
        public event Action OnProgramRunning;
        public event Action OnProgramDone;
        public event Action OnProgramAborting;
        public event Action OnProgramAborted;
        public event Action OnProgramPausing;
        public event Action OnProgramPaused;
        public event Action OnProgramResuming;
        public event Action<long, int> OnTimerSleeping;
        public event Action<int> OnOfflineCycleChanged;

        private void setWorkState(WorkState workState)
        {
            this.CurrWorkState = workState;
            this.OnWorkStateChanged?.Invoke(workState);
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, string.Format("work state has changed: {0}", workState));
        }

        /// <summary>
        /// 停止，执行完当前cycle
        /// </summary>
        public void Stop()
        {
            //ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.停止按钮按下);
            if (this.CurrWorkState == WorkState.WaitingForBoard || this.CurrWorkState == WorkState.Programing)
            {
                this.stopping = true;
                this.setWorkState(WorkState.Stopping);
                return;
            }
        }

        /// <summary>
        /// 运行（外部触发）
        /// </summary>
        public void Run()
        {
            if (FluidProgram.Current == null)
            {
                return;
            }            
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Start Button clicked");
            if (!Machine.Instance.IsAllInitDone)
            {
                if (MessageBox.Show(Machine.Instance.HardwareErrorString, msgText[3], MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                {
                    Logger.DEFAULT.Warn(LogCategory.RUNNING, TAG, Machine.Instance.HardwareErrorString);
                    return;
                }
            }

            //如果是双轨双程序的情况
            if (Machine.Instance.Setting.ConveyorSelect == ConveyorSelection.双轨 && Machine.Instance.Setting.DoubleProgram)
            {
                //检查飞拍参数和阀速度放到切换程序时执行
            }
            else
            {
                this.CheckFlyMarkAndWriteValveSpeed(FluidProgram.Current);
            }

            //先加载当前程序，双轨双程序在运行时切换
            if (this.CurrProgramState == ProgramOuterState.IDLE
                  || this.CurrProgramState == ProgramOuterState.ABORTED)
            {
                this.SetProgram(FluidProgram.Current);
                this.run2PausedStep(VALUE_INVALID);
            }
        }

        /// <summary>
        /// 单步运行
        /// </summary>
        public void SingleStep()
        {
            if (FluidProgram.Current == null)
            {
                return;
            }
            if (FluidProgram.Current.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.连续)
            {
                return;
            }
            this.isSingleStep = true;           
            Logger.DEFAULT.Info(LogCategory.MANUAL, TAG, "RunSingleStep()");

            // 只有当程序处在未运行、暂停状态下，才允许执行单步运行操作
            // 程序未运行：启动线程开始运行，并在第一个命令执行完成后暂停
            if (CurrProgramState == ProgramOuterState.IDLE || CurrProgramState == ProgramOuterState.ABORTED)
            {
                this.SetProgram(FluidProgram.Current);
                this.run2PausedStep(1);
            }
            // 程序处在暂停状态：在下一个命令执行完后暂停
            else if (CurrProgramState == ProgramOuterState.PAUSED)
            {
                this.resume2PausedStep(currFinishedSteps + 1);
            }
            else
            {
                Log.Print(TAG, "Refused: curr state = " + CurrProgramState);
            }
        }

        /// <summary>
        /// 从初始状态运行，run/step
        /// </summary>
        /// <param name="pausedAtStep">单步指定暂停步数为1</param>
        /// <returns></returns>
        private Result run2PausedStep(int pausedAtStep)
        {            
            Logger.DEFAULT.Info(LogCategory.RUNNING,TAG, "Run()");
            if (Program.ParseResult == null)
            {                
                Logger.DEFAULT.Warn(LogCategory.RUNNING, TAG, "Refused: program is parsing...");
                return new Result(false, null, "Program is parsing...");
            }
            if (!Program.ParseResult.IsOk)
            {
                Logger.DEFAULT.Warn(LogCategory.RUNNING, TAG, "Refused: program has grammar error");
                return new Result(false, null, "Program has grammar error.");
            }
            if (CurrProgramState != ProgramOuterState.IDLE && CurrProgramState != ProgramOuterState.ABORTED)
            {
                Logger.DEFAULT.Warn(LogCategory.RUNNING, TAG, "Refused: curr state = " + CurrProgramState);
                return new Result(false, null, "Program is already running.");
            }
            this.currFinishedSteps = 0;
            this.pausedAtStep = pausedAtStep;
            this.startRunningThread();
            return Result.OK;
        }

        /// <summary>
        /// 启动线程执行脚本程序
        /// </summary>
        private void startRunningThread()
        {            
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "start running thread...");
            // 启动线程执行脚本程序
            runningThread = new Thread(startRunning);
            runningThread.Priority = ThreadPriority.Highest;
            runningThread.Start();
        }

        /// <summary>
        /// 开始运行程序
        /// </summary>
        private void startRunning()
        {
            Logger.DEFAULT.Debug("Start Running ");
            this.StartDateTime = DateTime.Now;
            this.stopping = false;
            if (this.IsClearAllCount)
            {
                this.FinishedBoardCount = 0;
                this.FailedCount = 0;
                this.IsClearAllCount = false;
            }
            
            this.OnWorkRunning?.Invoke();
            Logger.DEFAULT.Debug("操作UI控件");
            this.setWorkState(WorkState.Preparing);

            Machine.Instance.IsProducting = true;

            Logger.DEFAULT.Debug("写入数据库前");
            //记录总运行  breakDown时间            
            TimeRecorderMgr.Instance.Running.SetStartTime();
            TimeRecorderMgr.Instance.BreakDown.SetEndTime();
            Logger.DEFAULT.Debug("写入数据库后");
            Machine.Instance.IsAborted = false;
            Logger.DEFAULT.Debug("更改机台状态前");
            Machine.Instance.FSM.ChangeProductionState(ProductionState.Normal);
            Logger.DEFAULT.Debug("更改机台状态后");

            Logger.DEFAULT.Debug("ConveyorMsgCenter.Instance.Program.IsOffline()  before");
            if (!ConveyorMsgCenter.Instance.Program.IsOffline().Item1)
            {
                this.isOffline = false;
                Machine.Instance.IsOffline = false;
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "cycle run");
                this.cycleRun();
            }
            else
            {
                this.isOffline = true;
                Machine.Instance.IsOffline = true;
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "offline run");
                this.offlineRun();
            }
            this.stopProgram();
            Logger.DEFAULT.Debug("ConveyorMsgCenter.Instance.Program.IsOffline()  end");
            Machine.Instance.IsProducting = false;
            this.OnWorkDone?.Invoke();
            this.setWorkState(WorkState.Idle);
        }

        /// <summary>
        /// 离线单次运行
        /// </summary>
        private void offlineRun()
        {
            for (int i = 0; i < this.Program.RuntimeSettings.OfflineCycle; i++)
            {
                this.OnOfflineCycleChanged?.Invoke(this.Program.RuntimeSettings.OfflineCycle - i);
                if (!this.doPrepare().IsOk)
                {
                    //stopProgram();
                    return;
                }
                this.setWorkState(WorkState.Programing);
                if (!this.runProgram().IsOk)
                {
                    stopProgram(true);
                    return;
                }
                if (Machine.Instance.IsErrorStop())
                {
                    this.stopProgram(true);
                    return;
                }
            }
            this.OnOfflineCycleChanged?.Invoke(0);
        }

        /// <summary>
        /// 生产连续运行
        /// </summary>
        private void cycleRun()
        {
            ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.启动按钮按下);
            ConveyorMsgCenter.Instance.Program.SendBoardCount(Executor.Instance.Cycle);
            ConveyorMsgCenter.Instance.Program.SendConveyorWidth(FluidProgram.Current.ConveyorWidth);

            //注册使之可响应运行过程中刷出来的Di报警
            AlarmServer.Instance.Register(this);

            //如果是双轨双程序，则在等待板
            if (Machine.Instance.Setting.ConveyorSelect == ConveyorSelection.双轨 && Machine.Instance.Setting.DoubleProgram)
            {

                while (true)
                {
                    this.setWorkState(WorkState.WaitingForBoard);
                    //等待进板过程中点击停止按钮，直接返回
                    if (this.stopping)
                    {
                        return;
                    }
                    //自动模式等待板到位
                    Tuple<bool, bool> isBoardIn = ConveyorMsgCenter.Instance.Program.IsBoardIn();
                    if (isBoardIn.Item1)
                    {
                        //设置程序
                        this.SetProgram(this.Conveyor1Program);
                        //解析程序
                        this.Program.Parse();
                        //检查参数写入是否成功
                        this.CheckFlyMarkAndWriteValveSpeed(this.Program);
                        break;
                    }
                    else if (isBoardIn.Item2)
                    {
                        this.SetProgram(this.Conveyor2Program);
                        //解析程序
                        this.Program.Parse();
                        //检查参数写入是否成功
                        this.CheckFlyMarkAndWriteValveSpeed(this.Program);
                        break;
                    }
                    Thread.Sleep(10);
                }
            }

            this.Program.lastPurgeCount = 0;
            this.Program.lastScaleCount = 0;
            this.Program.lastPurgeTime = DateTime.MinValue;
            this.Program.lastScaleTime = DateTime.MinValue;
            //避免一个板都没生产
            this.soakRecord = DateTime.Now;
            this.isSoaked = false;
            //运行前 设置Lot Control 
            if (this.Program.LotControlEnable)
            {
                this.lotController = new LotController(this.Program);
                //弹窗处理设置LotControl相关数据
                if (this.Program.RuntimeSettings.IsStartLotById)
                {
                    new LotControlSettingForm(this.Program).Show();
                }
                this.Program.RuntimeSettings.LotControlStart = true;
            }
            //todo 向轨道绑定进板等待处理事件

            Result result = this.doPrepare();
            if (!result.IsOk)
            {
                stopCycle(true);
                return;
            }

            int ret = 0;

            if (this.Cycle <= 0)
            {
                int i = 0;
                while (true)
                {
                    ret = this.oneCycleRun(i);
                    if (ConveyorMsgCenter.Instance.Program.isConveyorScanBarcode())
                    {
                        this.CurProductionBarcode = "";
                    }
                    if (ret == 0)
                    {
                        //通知消息中心处理报警
                        AlarmServer.Instance.HandleDelayAlarm(new Action(() => this.stopCycle(true)));
                        i++;
                    }
                    else if (ret < 0)
                    {
                        stopCycle(true);
                        return;
                    }
                    else
                    {
                        stopCycle();
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.Cycle; i++)
                {
                    ret = this.oneCycleRun(i);
                    if (ConveyorMsgCenter.Instance.Program.isConveyorScanBarcode())
                    {
                        this.CurProductionBarcode = "";
                    }
                    if (ret == 0)
                    {
                        //通知消息中心处理报警
                        AlarmServer.Instance.HandleDelayAlarm(new Action(() => this.stopCycle(true)));
                        if (Machine.Instance.IsErrorStop())
                        {
                            this.stopCycle(true);
                            return;
                        }
                    }
                    if (ret < 0)
                    {
                        stopCycle(true);
                        return;
                    }
                    else if (ret > 0)
                    {
                        stopCycle();
                        return;
                    }
                }
            }
        }
        private DateTime soakRecord = DateTime.MaxValue;
        private bool isSoaked = false;
        /// <summary>
        /// 单次循环运行
        /// </summary>
        /// <param name="i">循环索引</param>
        /// <returns>0：成功，-1：异常停止；1：正常停止</returns>
        private int oneCycleRun(int i)
        {
            //移动到workpiece起点
            if (this.Program.RuntimeSettings.Back2WorkpieceOrigin)
            {
                if (!Machine.Instance.Robot.MoveSafeZAndReply().IsOk)
                {
                    return -1;
                }
                if (!this.moveToWorkpieceOrigin().IsOk)
                {
                    Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "moveToWorkpieceOrigine - Start");
                    return -1;
                }
            }


            this.setWorkState(WorkState.WaitingForBoard);
            TimeRecorderMgr.Instance.WaitForBoard.SetStartTime();
            while (true)
            {

                if (Machine.Instance.IsErrorStop())
                {
                    this.stopCycle(true);
                    return -1;
                }
                //等待进板过程中点击停止按钮，直接返回
                if (this.stopping)
                {
                    return 1;
                }
                bool startRun = false;
                if (Machine.Instance.IsNoConveyor())
                {
                    startRun = MachineServer.Instance.OnRun;
                }
                else
                {
                    startRun = true;
                }
                if (startRun)
                {
                    //等待进板过程中如果清洗间隔时间到了，则执行清洗
                    if (Program.RuntimeSettings.IsAutoPurgeSpan && (DateTime.Now - this.Program.lastPurgeTime >= Program.RuntimeSettings.AutoPurgeSpan))
                    {
                        if (!this.doPurge().IsOk)
                        {
                            return -1;
                        }
                        this.Program.lastPurgeCount = i;
                    }

                    //等待进板过程中如果称重间隔时间到了，则执行称重
                    if (Program.RuntimeSettings.IsAutoScaleSpan && (DateTime.Now - this.Program.lastScaleTime >= Program.RuntimeSettings.AutoScaleSpan))
                    {
                        if (!this.doScale().IsOk)
                        {
                            return -1;
                        }
                        this.Program.lastScaleCount = i;
                    }

                    if (!Machine.Instance.IsNoConveyor())
                    {
                        // 等待板的过程中，如果到了时间就浸泡
                        if (this.Program.RuntimeSettings.IsAutoSoakSpan && (DateTime.Now - this.soakRecord) >= this.Program.RuntimeSettings.AutoSoakSpan)
                        {
                            if (!this.isSoaked)
                            {
                                if (!this.doSoak().IsOk)
                                {
                                    return -1;
                                }
                                this.isSoaked = true;
                            }
                        }
                        //自动模式等待板到位
                        Tuple<bool, bool> isBoardIn = ConveyorMsgCenter.Instance.Program.IsBoardIn();
                        if (isBoardIn.Item1)
                        {
                            this.Program.ExecutantOriginOffset = new PointD(0, 0);
                            ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道1点胶动作开始);
                            // 启用轨道扫码，获取当前工作站条码
                            if (ConveyorPrmMgr.Instance.FindBy(0).ConveyorScan)
                            {
                                bool barcodeExist = ConveyorMsgCenter.Instance.Program.GetWorkingSiteBarcode().Item1.Item1;
                                if (!barcodeExist)
                                {
                                    MessageBox.Show(msgText[13]);
                                    AlarmServer.Instance.Fire(this, AlarmInfoDomain.GetBarcodeError);
                                    return -1;
                                }
                                else
                                {
                                    this.CurProductionBarcode = ConveyorMsgCenter.Instance.Program.GetWorkingSiteBarcode().Item1.Item2;
                                }
                            }
                            break;
                        }
                        else if (isBoardIn.Item2)
                        {
                            this.Program.ExecutantOriginOffset = this.Program.Conveyor2OriginOffset;
                            ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道2点胶动作开始);
                            if (ConveyorPrmMgr.Instance.FindBy(1).ConveyorScan)
                            {
                                bool barcodeExist = ConveyorMsgCenter.Instance.Program.GetWorkingSiteBarcode().Item2.Item1;
                                if (!barcodeExist)
                                {
                                    MessageBox.Show(msgText[14]);
                                    AlarmServer.Instance.Fire(this, AlarmInfoDomain.GetBarcodeError);
                                    return -1;
                                }
                                else
                                {
                                    this.CurProductionBarcode = ConveyorMsgCenter.Instance.Program.GetWorkingSiteBarcode().Item2.Item2;
                                }
                            }
                            break;
                        }
                        // todo 工作站没有产品，检查预热站是否有产品且扫到了条码
                        if (ConveyorPrmMgr.Instance.FindBy(0).ConveyorScan || ConveyorPrmMgr.Instance.FindBy(1).ConveyorScan)
                        {
                            Tuple<bool, bool> preHeaterBarcodeExist = ConveyorMsgCenter.Instance.Program.GetPreSiteBarcodeError();
                            if (preHeaterBarcodeExist.Item1)
                            {
                                MessageBox.Show(msgText[13]);
                                AlarmServer.Instance.Fire(this, AlarmInfoDomain.GetBarcodeError);
                                return -1;
                            }
                            else
                            {
                                MessageBox.Show(msgText[14]);
                                AlarmServer.Instance.Fire(this, AlarmInfoDomain.GetBarcodeError);
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        // 无轨道收到启动按键直接运行
                        this.Program.ExecutantOriginOffset = new PointD(0, 0);
                        break;
                    }
                }
                Thread.Sleep(10);
            }
            //离开浸泡位置,当浸泡功能启用并且浸泡过后
            if (this.Program.RuntimeSettings.IsAutoSoakSpan && this.isSoaked)
            {
                if (!this.OutSoak().IsOk)
                {
                    return -1;
                }
                this.isSoaked = false;
            }
            TimeRecorderMgr.Instance.WaitForBoard.SetEndTime();
            //todo 生产前判断并记录产品ID信息
            this.dbWorkpiece = new Anda.Fluid.Domain.Data.workpiece();
            this.SaveDBWorkpieceInitData(dbWorkpiece, this.Program);

            //program运行
            this.setWorkState(WorkState.Programing);
            if (this.runProgram().IsOk)
            {
                this.FinishedBoardCount++;
                string msg = string.Format("Number of products already producted is {0}", this.FinishedBoardCount);
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, msg);
            }
            else
            {
                this.FailedCount++;
                string msg = string.Format("Number of Failed product is {0}", this.FailedCount);
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, msg);
                stopProgram(true);
                return -1;
            }

            if (this.stopping)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 停止生产循环
        /// </summary>
        /// <param name="aborted">异常停止标志</param>
        private void stopCycle(bool aborted = false)
        {
            if (Machine.Instance.IsMotionErrStop)
            {
                MessageBox.Show(msgText[0]);
                Machine.Instance.IsMotionErrStop = false;
            }
            if (Machine.Instance.IsAirPressureErrStop)
            {
                MessageBox.Show(msgText[1]);
                Machine.Instance.IsAirPressureErrStop = false;
            }
            if (aborted)
            {
                Machine.Instance.IsAborted = true;
                //Machine.Instance.FSM.ChangeProductionState(ProductionState.Alarm);
                Logger.DEFAULT.Error(LogCategory.RUNNING, TAG, "Cycle aborted");
            }
            this.setRunningState(ProgramInnerState.IDLE, ProgramInnerState.IDLE);
            this.setWorkState(WorkState.Idle);
            TimeRecorderMgr.Instance.Running.SetEndTime();
            TimeRecorderMgr.Instance.BreakDown.SetStartTime();
            //this.OnWorkStop?.Invoke();
            
            ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.停止按钮按下);
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Cycle stopped");
        }

        /// <summary>
        /// 暂停/继续
        /// </summary>
        public void PauseResume()
        {
           
            switch (this.CurrProgramState)
            {
                case ProgramOuterState.RUNNING:
                    this.pause();
                    break;
                case ProgramOuterState.PAUSED:
                    this.resume();
                    break;
            }
        }

        /// <summary>
        /// 暂停(外部触发)
        /// </summary>
        private void pause()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "pause button clicked");
            if (CurrProgramState != ProgramOuterState.RUNNING)
            {
                //Log.Print(TAG, "Refused: curr state = " + CurrProgramState);
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Refused: curr state = " + CurrProgramState);
                return;
            }
            if (Program.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.普通)
            {
                setRunningState(this.runningState, ProgramInnerState.PAUSED);
                permitRunning.Reset();
                this.OnProgramPausing?.Invoke();
            }
            else
            {
                Machine.Instance.Robot.Pause();
                setRunningState(ProgramInnerState.PAUSED, ProgramInnerState.PAUSED);
                this.OnProgramPaused?.Invoke();
            }
        }

        /// <summary>
        /// 恢复运（外部触发）
        /// </summary>
        private void resume()
        {
            if (Program.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.普通)
            {
                this.resume2PausedStep(VALUE_INVALID);
            }
            else
            {
                Machine.Instance.Robot.Resume();
                setRunningState(ProgramInnerState.RUNNING, ProgramInnerState.RUNNING);
                this.OnProgramResuming?.Invoke();
            }
        }

        /// <summary>
        /// 从暂停状态运行，run/step
        /// </summary>
        /// <param name="pausedAtStep">单步指定下一步暂停</param>
        private void resume2PausedStep(int pausedAtStep)
        {
            //Log.Print(TAG, "Resume()");
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Resume and run");
            if (CurrProgramState != ProgramOuterState.PAUSED)
            {
                //Log.Print(TAG, "Refused: curr state = " + CurrProgramState);
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Refused: curr state = " + CurrProgramState);
                return;
            }
            this.pausedAtStep = pausedAtStep;
            setRunningState(ProgramInnerState.RUNNING, ProgramInnerState.RUNNING);
            permitRunning.Set();
            this.OnProgramResuming?.Invoke();
        }

        /// <summary>
        /// 中止（外部触发）
        /// </summary>
        public void Abort()
        {
            //Log.Print(TAG, "Abort()");
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Abort button clicked");
            if ((int)this.CurrWorkState<=(int)WorkState.WaitingForBoard)
            {
                this.Stop();
            }
            if (Program == null) return;
            if (Program.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.普通)
            {
                if (CurrProgramState != ProgramOuterState.RUNNING && CurrProgramState != ProgramOuterState.PAUSED)
                {
                    //Log.Print(TAG, "Refused: curr state = " + CurrProgramState);
                    Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Refused: curr state = " + CurrProgramState);
                    return;
                }
                setRunningState(this.runningState, ProgramInnerState.ABORTED);
                permitRunning.Set();
                this.OnProgramAborting?.Invoke();
            }
            else
            {
                Machine.Instance.Robot.Stop();
                this.stopProgram(true);
            }
            if (!ConveyorMsgCenter.Instance.Program.IsOffline().Item1)
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.停止按钮按下);
            }
        }

     
        /// <summary>
        /// 检测是否需要暂停运行，如果需要暂停运行，则阻塞线程暂停运行
        /// </summary>
        private void checkPaused(SupportDirectiveCmd nextCmd)
        {
            // 在指定步数暂停运行
            bool pausedAtCurrStep = currFinishedSteps == pausedAtStep;
            if (pausedAtCurrStep)
            {
                pausedAtStep = VALUE_INVALID;
                permitRunning.Reset();
            }
            // 用户请求暂停运行
            bool requestPaused = runningState != this.pendingState && this.pendingState == ProgramInnerState.PAUSED;
            if (pausedAtCurrStep || requestPaused)
            {
                currPausedCmd = nextCmd;
                int line = currPausedCmd.RunnableModule.CommandsModule.FindCmdLineIndex(currPausedCmd.CmdLine) + 1;
                Log.Print("Current pause at pattern:" + currPausedCmd.RunnableModule.CommandsModule.Name + ", line:" + line);
                setRunningState(ProgramInnerState.PAUSED, ProgramInnerState.PAUSED);
                this.OnProgramPaused?.Invoke();
                permitRunning.Wait();
                currPausedCmd = null;
            }
        }

        #endregion


        #region 数据保存
        private void SaveDBWorkpieceInitData(workpiece workpiece, FluidProgram program)
        {
            workpiece.Name = program.Name;
            workpiece.Operator = AccountMgr.Instance.CurrentAccount.NameId;
            workpiece.RunMode = (int)Machine.Instance.Valve1.RunMode;
            workpiece.StartTime = DateTime.Now;
            workpiece.LotNum = program.RuntimeSettings.LotId;
            workpiece.Barcode = program.RuntimeSettings.ProductionId;

            workpiecesetting setting = new workpiecesetting();
            setting.GluePressure1 = Machine.Instance.Proportioner1.Proportional.CurrentValue;
            setting.GluePressure2 = Machine.Instance.Proportioner2.Proportional.CurrentValue;
            setting.SingleDotWt = program.RuntimeSettings.SingleDropWeight;
            //setting.Valve1Temperature = program.RuntimeSettings.Valve1Temperature;
            //setting.Valve2Temperature = program.RuntimeSettings.Valve2Temperature;
            setting.VelXY = this.Program.MotionSettings.VelXY;
            setting.VelZ = this.Program.MotionSettings.VelZ;
            setting.AccXY = this.Program.MotionSettings.AccXY;
            setting.AccZ = this.Program.MotionSettings.AccZ;
            setting.WorkpieceId = workpiece.Id;
            setting.workpiece = workpiece;
            workpiece.workpiecesetting.Add(setting);
        }
        #endregion


        #region 程序运行实现逻辑

        public Custom.ICustomary GetCustom()
        {
            if (this.customary == null)
            {
                //this.customary = Custom.CustomFactory.GetCustom(this.Program.RuntimeSettings.Custom); ;
                this.customary = Custom.CustomFactory.GetCustom(Machine.Instance.Setting.MachineSelect); 
            }
            else
            {
                //if (this.customary.Custom != this.Program.RuntimeSettings.Custom)
                //{
                //    this.customary = Custom.CustomFactory.GetCustom(this.Program.RuntimeSettings.Custom); 
                //}
                if (this.customary.MachineSelection!=Machine.Instance.Setting.MachineSelect)
                {
                    this.customary = Custom.CustomFactory.GetCustom(Machine.Instance.Setting.MachineSelect);
                }
            }
            return this.customary;
        }

        /// <summary>
        /// 运行生产程序
        /// </summary>
        /// <returns></returns>
        private Result runProgram()
            {
            this.ProgramRunTime = DateTime.Now;

            this.OnProgramRunning?.Invoke();
            setRunningState(ProgramInnerState.RUNNING, ProgramInnerState.RUNNING);
            permitRunning.Set();
            this.stopWatch.Restart();

            this.nozzleCheckIsFinish = false;
            this.SkipBoardsNo?.Clear();
            this.finishNozzleCheckCmds.Clear();
            doMultipassPassBlockDelayTimes.Clear();
            doMultipassPassBlockIndexes.Clear();
            this.hasSimulPattern = false;
            coordinateCorrector = new CoordinateCorrector(Program.ModuleStructure);

            //设置硬件参数
            Program.InitHardware();
            // 勾选称重参数同步选项，自动同步最近一次称重参数
            if (Program.RuntimeSettings.IsSyncSingleDropWeight)
            {
                Program.RuntimeSettings.SingleDropWeight = Machine.Instance.Valve1.weightPrm.SingleDotWeight;
            }

            // 设置运行时参数默认值
            Program.ModuleStructure.ProgramModule.MeasuredHt = Machine.Instance.Robot.CalibPrm.StandardHeight;
            Log.Dprint(string.Format("Program Ht:{0}", Program.ModuleStructure.ProgramModule.MeasuredHt));
            DirUtils.CreateDir(string.Format("log\\{0}", DateTime.Today.ToString("yyyyMMdd")));
            Program.RuntimeSettings.FilePathInspectDot =
                string.Format("log\\{0}\\{1}_{2}_InspectDot.csv",
                DateTime.Today.ToString("yyyyMMdd"),
                Program.Name,
                DateTime.Now.ToString("HHmmss"));
            Program.RuntimeSettings.FilePathInspectRect =
                string.Format("log\\{0}\\{1}_{2}_InspectRect.csv",
                DateTime.Today.ToString("yyyyMMdd"),
                Program.Name,
                DateTime.Now.ToString("HHmmss"));


            // 初始化所有RunnableModule的运行模式：轨迹中事先保存的类型
            foreach (var item in Program.ModuleStructure.GetAllRunnableModules())
            {
                item.Mode = item.SaveMode;
            }

            Result result = Result.OK;

            result = executeBarcodeCmds();
            if (!result.IsOk)
            {
                return result;
            }
            // 同一跳过所有指定的穴位拼版
            Program.ModuleStructure.SkipRunnableModuleByBoardNo(this.SkipBoardsNo);

            //Mark执行
            if (!Program.RuntimeSettings.isFlyMarks)
            {
                // mark定拍
                result = executeMarkCmds();
            }
            else
            {
                result = executeFlyMarkCmds();
                if (Machine.Instance.Valve1.RunMode == ValveRunMode.Look && Program.RuntimeSettings.isAdjustFlyOffset)
                {
                    if (!result.IsOk)
                    {
                        MessageBox.Show(msgText[4]);
                    }
                    else
                    {
                        //校准成功后关闭校准选项
                        Program.RuntimeSettings.isAdjustFlyOffset = false;
                        Program.RuntimeSettings.FlyOffsetIsValid = true;
                        Program.HasChanged = true;
                        stopProgram();
                    }
                    return result;
                }
            }
            if (!result.IsOk)
            {
                return result;
            }
                        

            // badmark拍摄
            result = executeBadMarkCmds();
            if (!result.IsOk)
            {
                return result;
            }

            // cv模式不测高
            if (Machine.Instance.Valve1.RunMode != ValveRunMode.Look)
            {
                //如果是RTV
                Machine.Instance.MeasureHeightBefore();
                // 测高点读数
                result = executeMeasureHeightCmds();
                if (!result.IsOk)
                {
                    return result;
                }
                //测胶高度集中处理
                result=executeMeasureHeightCmdsInMeasureCmd();
                if (!result.IsOk)
                {
                    return result;
                }
                //如果是RTV
                Machine.Instance.MeasureHeightAfter();
            }

            //双阀生产时自动对所有相同的pattern进行配对(仅workpiece的子节点这一层级，即level=2)
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && Machine.Instance.Setting.DualValveMode == DualValveMode.异步)
            {
                ModulesAutoPairing(Program.ModuleStructure.GetChildrenModule(Program.ModuleStructure.WorkpieceModule));
            }
            else if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                // 单阀模式统一默认使用主阀
                foreach (var item in Program.ModuleStructure.GetAllRunnableModules())
                {
                    if (item.Mode == ModuleMode.SkipMode)
                    {
                        continue;
                    }
                    item.Mode = ModuleMode.AssignMode1;
                }
            }

            // Z轴回安全高度
            result = Machine.Instance.Robot.MoveSafeZAndReply();
            if (!result.IsOk)
            {
                return result;
            }

            #region 保存workpiece的Mark信息
            if (!IsOffline)
            {
                List<MarkCmd> workpieceMark = coordinateCorrector.GetRunnableModuleMark(Program.ModuleStructure.WorkpieceModule);
                if (workpieceMark.Count > 0)
                {
                    int workpieceMarkIndex = 0;
                    foreach (MarkCmd item in workpieceMark)
                    {
                        PointD markPos = coordinateCorrector.GetMarkRealPosition(item);
                        if (workpieceMarkIndex == 0)
                        {
                            this.dbWorkpiece.Mark1X = markPos.X;
                            this.dbWorkpiece.Mark1Y = markPos.Y;
                        }
                        else
                        {
                            this.dbWorkpiece.Mark2X = markPos.X;
                            this.dbWorkpiece.Mark2Y = markPos.Y;
                        }
                        workpieceMarkIndex++;
                    }
                }
            }

            #endregion

            #region YBSX客户进行手动示教修改点
            if (this.GetCustom() is Custom银宝山)
            {
                List<SymbolLinesCmd> list = Program.ModuleStructure.GetAllSymbolLines();
                //将数据传入，内部会判断是否需要打开窗体
                new SymbolLineFineTune().SetData(list, this.coordinateCorrector);
            }
            #endregion

            TimeRecorderMgr.Instance.Spray.SetStartTime();
            this.GetCustom().ClearRecord();
            this.GetCustom().Production();
            Machine.Instance.Robot.BufFluidItems.Clear();
            result = execute(Program.ModuleStructure.ProgramModule.CmdList, DateUtils.CurrTimeInMills);
            if (!result.IsOk)
            {
                TimeRecorderMgr.Instance.Spray.SetEndTime();
                //this.FailedCount++;
                //string msg = string.Format("Number of Failed product is {0}", this.FailedCount);
                //Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, msg);
                return result;
            }
            //连续前瞻模式打胶
            if (Program.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.连续)
            {
                InitLook lookAheadPrm = new InitLook()
                {
                    crd = 1,
                    fifo = 0,
                    Ltime = Program.RuntimeSettings.LookTime,
                    Lmax = Program.RuntimeSettings.LookAccMax,
                    n = (short)Program.RuntimeSettings.LookCount
                };
                result = Machine.Instance.StartBufFluid(lookAheadPrm);
                if (!result.IsOk)
                {
                    TimeRecorderMgr.Instance.Spray.SetEndTime();
                    //this.FailedCount++;
                    //string msg = string.Format("Number of Failed product is {0}", this.FailedCount);
                    //Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, msg);
                    return result;
                }
            }
            //执行Blobs检测指令
            result = this.executeBlobsCmds();
            if(!result.IsOk)
            {
                stopProgram(true);
                return result;
            }

            //有需要检测的喷嘴检测指令
            if (this.finishNozzleCheckCmds.Count > 0)
            {
                foreach (NozzleCheckCmd item in this.finishNozzleCheckCmds)
                {
                    Log.Print("execute directive : NozzleCheck");
                    Result nozzleCheckResult = item.SpecialExcute(coordinateCorrector);
                    if (!nozzleCheckResult.IsOk)
                    {
                        stopProgram(true);
                        Log.Print("execute directive ERROR : " + result.ErrMsg + ", stop running!");
                        return nozzleCheckResult;
                    }
                }
            }

            this.GetCustom().ProductionAfter();

            TimeRecorderMgr.Instance.Spray.SetEndTime();

            //单步模式最后一个轨迹暂停
            if (isSingleStep)
            {
                bool pausedAtCurrStep = currFinishedSteps == pausedAtStep;
                if (pausedAtCurrStep)
                {
                    pausedAtStep = VALUE_INVALID;
                    permitRunning.Reset();
                }
                // 用户请求暂停运行
                bool requestPaused = runningState != this.pendingState && this.pendingState == ProgramInnerState.PAUSED;
                if (pausedAtCurrStep || requestPaused)
                {
                    //Log.Print("Current pause at end");
                    Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Current pause at end");
                    setRunningState(ProgramInnerState.PAUSED, ProgramInnerState.PAUSED);
                    this.OnProgramPaused?.Invoke();
                    permitRunning.Wait();
                }
                this.isSingleStep = false;
            }

            this.ProgramEndTime = DateTime.Now;

            if (this.GetCustom() is CustomRTV)
            {
                //如果有测宽没通过，弹窗提示
                result = this.ShowRtvResultForm();
                //保存条码  线宽 生产后
                this.GetCustom().SaveData(Program.RuntimeSettings.CustomParam.RTVParam.DataMesPathDir);
                if (!result.IsOk)
                {
                    Machine.Instance.Robot.MovePosZAndReply(0);
                    DoType.工作阻挡.Set(false);
                    DoType.工作顶升.Set(false);
                    return result;
                }
            }
            this.OnProgramDone?.Invoke();
            
            //stopProgram();
            return Result.OK;
        }

        /// <summary>
        /// 停止生产程序（异常中止，abort = true）
        /// </summary>
        /// <param name="aborted">是否Abort</param>
        private void stopProgram(bool aborted = false)
        {
            if (aborted)
            {
                if (Machine.Instance.IsMotionErrStop)
                {
                    MessageBox.Show(msgText[0]);
                    Machine.Instance.IsMotionErrStop = false;
                }
                if (Machine.Instance.IsAirPressureErrStop)
                {
                    MessageBox.Show(msgText[1]);
                    Machine.Instance.IsAirPressureErrStop = false;
                }
                Machine.Instance.IsAborted = true;
                //Machine.Instance.FSM.ChangeProductionState(ProductionState.Alarm);
                this._stopProgram(ProgramInnerState.ABORTED, ProgramInnerState.ABORTED);
                Logger.DEFAULT.Info(LogCategory.RUNNING, this.GetType().Name, "Program aborted");
                this.OnProgramAborted?.Invoke();
            }
            else
            {
                this._stopProgram(ProgramInnerState.IDLE, ProgramInnerState.IDLE, true);
                Logger.DEFAULT.Info(LogCategory.RUNNING, this.GetType().Name, "Program stoped");
                //程序执行完成浸泡开始计时
                this.soakRecord = DateTime.Now;
                this.OnProgramDone?.Invoke();
            }

        }

        private Result _stopProgram(ProgramInnerState runningState, ProgramInnerState pendingState, bool toOrigin = false)
        {
            Result ret = Result.OK;

            if (toOrigin && this.Program.RuntimeSettings.Back2WorkpieceOrigin)
            {
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
                if (!ret.IsOk)
                {
                    return ret;
                }
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "moveToWorkpieceOrigine - End");
                ret = moveToWorkpieceOrigin();
                if (!ret.IsOk)
                {
                    return ret;
                }
            }
            //如果end前的指令是move to loc，并且选择不回原点，则保持位置不变（edit by 肖旭）
            else if (toOrigin && !this.Program.RuntimeSettings.Back2WorkpieceOrigin)
            {
                //得到倒数第二条指令的索引
                int index = this.Program.Workpiece.CmdLineList.Count - 2;
                if (this.Program.Workpiece.CmdLineList[index] is MoveToLocationCmdLine)
                {

                }
                else
                {
                    ret = Machine.Instance.Robot.MoveSafeZAndReply();
                    if (!ret.IsOk)
                    {
                        return ret;
                    }
                }
            }
            else
            {
                ret = Machine.Instance.Robot.MoveSafeZAndReply();
                if (!ret.IsOk)
                {
                    return ret;
                }
            }

            // 更新program的状态
            this.lastFinishedSteps = this.currFinishedSteps;
            this.stopWatch.Stop();
            this.CycleTime = (double)this.stopWatch.ElapsedMilliseconds / 1000.0;
            setRunningState(runningState, pendingState);

            // 复位pattern中LineAdjusted
            this.setPatternAdjust(false);

            return ret;
        }

        private void setPatternAdjust(bool flag)
        {
            //this.Program.Workpiece.LineAdjusted = flag;
            foreach (var p in this.Program.Patterns)
            {
                p.LineAdjusted = flag;
                Pattern pattern = p.ReversePattern();
                if (pattern != null)
                    pattern.LineAdjusted = flag;
            }
        }
        /// <summary>
        /// 运行前检查必要参数
        /// </summary>
        /// <returns></returns>
        private Result CheckParam()
        {
            if (Math.Round(Machine.Instance.Robot.DefaultPrm.VelU,3) == 0)
            {
                MessageBox.Show(msgText[5]);
                return Result.FAILED;
            }
            if (Math.Round(FluidProgram.Current.MotionSettings.VelU,3) == 0)
            {
                MessageBox.Show(msgText[6]);
                return Result.FAILED;
            }
            if (Math.Round(FluidProgram.Current.MotionSettings.AccU,3) == 0)
            {
                MessageBox.Show(msgText[7]);
                return Result.FAILED;
            }
            return Result.OK;
        }

        /// <summary>
        /// 运行前准备
        /// </summary>
        /// <returns></returns>
        private Result doPrepare()
        {
            // 点击运行后开始称重也应当进入运行状态，屏蔽运行等按键
            this.OnProgramRunning?.Invoke();
            setRunningState(ProgramInnerState.RUNNING, ProgramInnerState.RUNNING);
            Result ret = Result.OK;
            // 清洗阀
            if (Program.RuntimeSettings.PurgeBeforeStart)
            {
                ret = this.doPurge();
                if (!ret.IsOk)
                {
                    Logger.DEFAULT.Error(LogCategory.RUNNING, TAG, "doPurge failed");
                    return ret;
                }
            }
            // 称重
            if (Program.RuntimeSettings.ScaleBeforeStart)
            {
                ret = this.doScale();
                if (!ret.IsOk)
                {
                    Logger.DEFAULT.Error(LogCategory.RUNNING, TAG, "doScale failed");
                    return ret;
                }
            }
            // 判断单点重量
            if (Program.RuntimeSettings.SingleDropWeight <= 0)
            {
                Logger.DEFAULT.Fatal(LogCategory.RUNNING, TAG, "Single dot weight <= 0");
                //MessageBox.Show("Single dot weight <= 0");
                //MessageBox.Show("胶单点重量<= 0");
                AlarmServer.Instance.Fire(this, AlarmInfoDomain.SingleDotWeight);
                MsgCenter.Broadcast(Domain.MsgType.MSG_SINGLE_DOT_SETTING, this, null);                
                //return Result.FAILED;
            }
       
            // 如果是U轴机台,运行前胶阀重置
            if (Machine.Instance.Robot.RobotIsXYZU || Machine.Instance.Robot.RobotIsXYZUV)
            {
                Machine.Instance.Valve1.ResetValveTilt(FluidProgram.Current.MotionSettings.VelU,FluidProgram.Current.MotionSettings.AccU);
            }
            return ret;
        }

        /// <summary>
        /// 执行清洗操作
        /// </summary>
        /// <returns></returns>
        private Result doPurge()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "do purge begin");
            Result ret = Machine.Instance.Valve1.DoPurgeAndPrime();
            if (!ret.IsOk)
            {
                MessageBox.Show(msgText[8]);
                return ret;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                ret = Machine.Instance.Valve2.DoPurgeAndPrime();
                if (!ret.IsOk)
                {
                    MessageBox.Show(msgText[9]);
                    return ret;
                }
            }
            this.Program.lastPurgeTime = DateTime.Now;
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "do purge end");
            return ret;
        }

        /// <summary>
        /// 执行称重操作
        /// </summary>
        /// <returns></returns>
        private Result doScale()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "do scale begin");
            Result ret = Machine.Instance.Valve1.DoWeight();
            if (!ret.IsOk)
            {
                Program.RuntimeSettings.SingleDropWeight = 0;
                MessageBox.Show(msgText[10]);
                return ret;
            }
            Program.RuntimeSettings.SingleDropWeight = Machine.Instance.Valve1.weightPrm.SingleDotWeight;
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                ret = Machine.Instance.Valve2.DoWeight();
                if (!ret.IsOk)
                {
                    MessageBox.Show(msgText[11]);
                    return ret;
                }
            }
            this.Program.lastScaleTime = DateTime.Now;
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "do scale end");
            return ret;
        }
        /// <summary>
        /// 浸泡
        /// </summary>
        /// <returns></returns>
        private Result doSoak()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "do soak begin");
            Result ret = Machine.Instance.Valve1.DoSoak();
            if (!ret.IsOk)
            {
                return ret;
            }
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "do soak end");
            return Result.OK;
        }
        private Result OutSoak()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "do outSoak begin");
            Result ret = Machine.Instance.Valve1.OutSoak();
            if (!ret.IsOk)
            {
                return ret;
            }
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "do outSoak end");
            return Result.OK;
        }

        /// <summary>
        /// 移动到workpiece起点
        /// </summary>
        /// <returns></returns>
        private Result moveToWorkpieceOrigin()
        {
            return Machine.Instance.Robot.MovePosXYAndReply(this.Program.Workpiece.OriginPos, 
                this.Program.MotionSettings.VelXY,
                this.Program.MotionSettings.AccXY);           
        }

        /// <summary>
        /// 集中处理badmark
        /// </summary>
        /// <returns></returns>
        private Result executeBadMarkCmds()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Begin to execute bad marks");
            if (Program.ModuleStructure.BadMarkMap.Count <= 0)
            {
                return Result.OK;
            }
            // 执行badmark点拍摄
            Result ret = Result.OK;
            foreach (RunnableModule module in Program.ModuleStructure.BadMarkMap.Keys)
            {
                List<BadMarkCmd> badMarkCmds = Program.ModuleStructure.BadMarkMap[module];
                //已经跳过的拼板不拍badmark
                if (module.Mode == ModuleMode.SkipMode)
                {
                    continue;
                }
                for (int i = 0; i < badMarkCmds.Count; i++)
                {
                    // 检测是否应该暂停的操作应该放在下一次外部命令被执行之前，当后续没有外部命令的时候可以直接退出运行
                    checkPaused(badMarkCmds[i] as SupportDirectiveCmd);
                    // 暂停状态下中止，直接退出
                    if (this.pendingState == ProgramInnerState.ABORTED)
                    {
                        return Result.FAILED;
                    }
                    BadMark badmark = (BadMark)badMarkCmds[i].ToDirective(coordinateCorrector);
                    ret = onExecuteDirective(badmark);

                    //// 屏蔽原因：badMark的绑定不再以拼版为单位，而是管辖范围内的轨迹组
                    //// 根据badmark结果确定是否跳过关联RunnableModule,判定依据待修改
                    //if (ret.IsOk == badMarkCmds[i].IsOkSkip)
                    //{
                    //    badmark.RunnableModule.Mode = ModuleMode.SkipMode;
                    //    List<RunnableModule> childModules = Program.ModuleStructure.GetChildrenModule(module);
                    //    if (childModules != null)
                    //    {
                    //        foreach (RunnableModule childModule in childModules)
                    //        {
                    //            childModule.Mode = ModuleMode.SkipMode;
                    //            Program.ModuleStructure.SetAllChildModuleMode(module, ModuleMode.SkipMode);
                    //        }
                    //    }
                    //}
                    badMarkCmds[i].ResultIsSkip = ret.IsOk == badMarkCmds[i].IsOkSkip;
                    //badmark
                    
                    currFinishedSteps++;
                }
            }
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Execute bad marks end");
            return Result.OK;
        }

        /// <summary>
        /// 集中处理barcode
        /// </summary>
        /// <returns></returns>
        private Result executeBarcodeCmds()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Begin to execute barcode");
            if (ConveyorMsgCenter.Instance.Program.isConveyorScanBarcode())
            {
                // 条码识别成功，解析处理
                if (!this.GetCustom().ParseBarcode(this.CurProductionBarcode))
                {
                    return Result.FAILED;
                }

                // 获取跳过的产品穴位
                this.GetCustom().SkipBoard(this.SkipBoardsNo);
            }
            else
            {
                if (Program.ModuleStructure.BarcodeMap.Count <= 0)
                {
                    return Result.OK;
                }
                // 执行badmark点拍摄
                Result ret = Result.OK;
                foreach (RunnableModule module in Program.ModuleStructure.BarcodeMap.Keys)
                {
                    List<BarcodeCmd> barcodeCmds = Program.ModuleStructure.BarcodeMap[module];
                    //已经跳过的拼板不拍barcode
                    if (module.Mode == ModuleMode.SkipMode)
                    {
                        continue;
                    }
                    for (int i = 0; i < barcodeCmds.Count; i++)
                    {
                        // 检测是否应该暂停的操作应该放在下一次外部命令被执行之前，当后续没有外部命令的时候可以直接退出运行
                        checkPaused(barcodeCmds[i] as SupportDirectiveCmd);
                        // 暂停状态下中止，直接退出
                        if (this.pendingState == ProgramInnerState.ABORTED)
                        {
                            return Result.FAILED;
                        }
                        Barcode barcode = (Barcode)barcodeCmds[i].ToDirective(coordinateCorrector);
                        if (this.findBarcode(barcode) < 0)
                        {
                            return Result.FAILED;
                        }

                        // 条码识别成功，解析处理
                        if (!this.GetCustom().ParseBarcode(barcode.BarcodePrm.Text))
                        {
                            return Result.FAILED;
                        }

                        // 获取跳过的产品穴位
                        this.GetCustom().SkipBoard(this.SkipBoardsNo);

                        currFinishedSteps++;
                    }
                }
            }

            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Execute barcode end");
            return Result.OK;
        }

        /// <summary>
        /// 相机识别条码，并弹窗提示
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private int findBarcode(Barcode barcode)
        {
            Result ret = onExecuteDirective(barcode);
            if (ret.IsOk)
            {
                return 0;
            }
            Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
            dic.Add(DialogResult.Abort, new Action(() => { }));
            dic.Add(DialogResult.Retry, new Action(() => { }));
            dic.Add(DialogResult.Ignore, new Action(() =>
            {
                barcode.RunnableModule.Mode = ModuleMode.SkipMode;
                Program.ModuleStructure.SetAllChildModuleMode(barcode.RunnableModule, ModuleMode.SkipMode);
            }));
            dic.Add(DialogResult.Cancel, new Action(() =>
            {
                Machine.Instance.IsProducting = false;
                this.WaitMarkManualDone.Reset();
                MsgCenter.Broadcast(MsgType.MSG_FIND_BARCODE_FIALED, this, barcode.BarcodeCmd.RunnableModule.CommandsModule as Pattern, barcode);
                this.WaitMarkManualDone.WaitOne();
                Machine.Instance.IsProducting = true;
            }));

            DialogResult? dr = AlarmServer.Instance.Fire(this, AlarmInfoVision.WarnFindBarcodeFailed, dic);
            switch (dr)
            {
                case DialogResult.Cancel:
                    if (this.FindMarkDialogResult == DialogResult.OK)
                    {
                        
                    }
                    break;
                case DialogResult.Abort:
                    return -1;
                case DialogResult.Retry:
                    return this.findBarcode(barcode);
                case DialogResult.Ignore:
                    return 1;
            }
            return 0;
        }

        /// <summary>
        /// 轨道条码枪识别条码，并弹窗提示
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private int findConveryorBarcode(ConveyorBarcode conveyorBarcode)
        {
            Result ret = onExecuteDirective(conveyorBarcode);
            if (ret.IsOk)
            {
                MsgCenter.Broadcast(LngMsg.MSG_Barcode_Info, this, conveyorBarcode.Text);
                if(this.GetCustom() is CustomRTV)
                {
                    CustomRTV rtv = this.GetCustom() as CustomRTV;
                    rtv.Barcode = conveyorBarcode.Text;
                }
                return 0;
            }
            Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
            dic.Add(DialogResult.Abort, new Action(() => { }));
            dic.Add(DialogResult.Retry, new Action(() => { }));
            dic.Add(DialogResult.Cancel, new Action(() =>
            {
                Machine.Instance.IsProducting = false;
                this.WaitMarkManualDone.Reset();
                MsgCenter.Broadcast(MsgType.MSG_FIND_BARCODE_FIALED, this, conveyorBarcode.ConveyorBarcodeCmd.RunnableModule.CommandsModule as Pattern, conveyorBarcode);
                this.WaitMarkManualDone.WaitOne();
                Machine.Instance.IsProducting = true;
            }));

            DialogResult? dr = AlarmServer.Instance.Fire(this, AlarmInfoVision.WarnFindBarcodeFailed, dic);
            switch (dr)
            {
                case DialogResult.Cancel:
                    if (this.FindMarkDialogResult == DialogResult.OK)
                    {

                    }
                    break;
                case DialogResult.Abort:
                    return -1;
                case DialogResult.Retry:
                    return this.findConveryorBarcode(conveyorBarcode);
            }
            return 0;
        }

        /// <summary>
        /// 集中处理Mark点命令，并进行坐标校正，转换
        /// </summary>
        /// <returns>0：成功，-1：停止，1：跳过</returns>
        private Result executeMarkCmds()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, this.GetType().Name, "Begin to execute normal marks ");

            List<MarkCmd> markList = null;
            if (Program.ModuleStructure.MarksSorted == null)
            {
                Program.ModuleStructure.GetAllMarkPoints();
                if (markList.Count <= 0)
                {
                    return Result.OK;
                }
            }
            else
            {
                markList = Program.ModuleStructure.MarksSorted;
            }
               
            
            // 执行mark点拍摄
            RunnableModule currentModule = null;
            int ret = 0;
            foreach (MarkCmd markCmd in markList)
            {
                //当执行到不同的RunnableModule时，将上一个RunnableModule执行的Mark生成坐标校正器
                if (currentModule != markCmd.RunnableModule)
                {
                    if (currentModule != null && currentModule.Mode != ModuleMode.SkipMode)
                    {
                        coordinateCorrector.SetRunnableModuleTransformer(currentModule);
                        if (currentModule.CommandsModule is Workpiece)
                        {
                            //得到对应的mark 两个  拍照的位置
                            List<MarkCmd> workpieceMarks = coordinateCorrector.GetRunnableModuleMark(Program.ModuleStructure.WorkpieceModule);
                            if (this.GetCustom()!=null)
                            {
                                bool transResult = this.GetCustom().TransPoint(workpieceMarks);
                                if (!transResult)
                                {
                                    return Result.FAILED;
                                }
                            }
                        }
                    }
                    currentModule = markCmd.RunnableModule;
                }
                
                // 检测是否应该暂停的操作应该放在下一次外部命令被执行之前，当后续没有外部命令的时候可以直接退出运行
                checkPaused(markCmd as SupportDirectiveCmd);
                // 暂停状态下中止，直接退出
                if (this.pendingState == ProgramInnerState.ABORTED)
                {
                    return Result.FAILED;
                }
                //生成执行层mark之前必须先生成上一层的坐标校准器，否则可能跑偏
                Mark mark = (Mark)markCmd.ToDirective(coordinateCorrector);
                // 判断父级Pattern是否跳过
                RunnableModule parentModule = Program.ModuleStructure.GetParentModule(mark.RunnableModule);
                if (parentModule != null && parentModule.Mode == ModuleMode.SkipMode)
                {
                    continue;
                }
                // 同级Pattern第一个Mark跳过，第二个也不执行
                if (markCmd.RunnableModule.Mode == ModuleMode.SkipMode)
                {
                    continue;
                }
                // 执行拍Mark
                ret = this.findMark(mark);
                if (ret < 0)
                {
                    return Result.FAILED;
                }
                // 设置mark点实际位置
                coordinateCorrector.SetMarkRealPosition(markCmd, mark.ModelFindPrm.TargetInMachine);
                currFinishedSteps++;
                if (mark.ModelFindPrm.TargetInMachine != null)
                {
                    string msg = string.Format("Target position is [{0}, {1}] in machine", mark.ModelFindPrm.TargetInMachine.X, mark.ModelFindPrm.TargetInMachine.Y);
                    Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, msg);
                }
            }
            //对最后一次执行Mark的RunnableModule生成坐标校正器
            coordinateCorrector.SetRunnableModuleTransformer(currentModule);
            Logger.DEFAULT.Info(LogCategory.RUNNING, this.GetType().Name, "Execute normal marks end");
            return Result.OK;
        }

        /// <summary>
        /// 处理飞拍Mark命令
        /// </summary>
        /// <returns></returns>
        private Result executeFlyMarkCmds()
        {
            Result result = Result.OK;
            //Look模式且启用了飞拍校准执行飞拍校准流程
            if (Machine.Instance.Valve1.RunMode == ValveRunMode.Look && Program.RuntimeSettings.isAdjustFlyOffset)
            {
                result = FlyMarkOffsetAdjust();
                return result;
            }
            else
            {
                //Mark飞拍
                result = executeFlyMarks();
                if (!result.IsOk)
                {
                    return result;
                }
                //飞拍NG Mark 补拍逻辑
                if (Program.RuntimeSettings.IsNGReshoot)
                {
                    foreach (RunnableModule item in Program.ModuleStructure.GetAllRunnableModules())
                    {
                        if (item.Mode == ModuleMode.SkipMode)
                        {
                            List<MarkCmd> marks = coordinateCorrector.GetRunnableModuleMark(item);
                            if (marks.Count < 1)
                            {
                                continue;
                            }
                            else
                            {
                                //重置为默认值
                                item.Mode = item.SaveMode;
                                foreach (MarkCmd ngMarkCmd in marks)
                                {
                                    Mark ngMark = (Mark)ngMarkCmd.ToDirective(coordinateCorrector);
                                    // 执行拍Mark
                                    int ret = this.findMark(ngMark);
                                    if (ret < 0)
                                    {
                                        return Result.FAILED;
                                    }
                                    // 设置mark点实际位置
                                    coordinateCorrector.SetMarkRealPosition(ngMarkCmd, ngMark.ModelFindPrm.TargetInMachine);
                                }
                            }
                        }
                    }
                }
                //遍历生成对应轨迹的坐标校正器
                coordinateCorrector.OnAllMarkCmdsExecuted();
            }
            return result;
        }

        /// <summary>
        /// 查找Mark，并弹窗进行提示处理
        /// </summary>
        /// <param name="mark">mark对象</param>
        /// <returns>0：成功，-1：中止，1：忽略</returns>
        private int findMark(Mark mark)
        {
            Result ret = onExecuteDirective(mark);
            if (ret.IsOk)
            {
                if (Machine.Instance.Setting.MachineSelect == MachineSelection.AFN)
                {
                    if (mark.ModelFindPrm.TargetInMachine.X > 5999 || mark.ModelFindPrm.TargetInMachine.Y > 5999)
                    {
                        mark.RunnableModule.Mode = ModuleMode.SkipMode;
                        Program.ModuleStructure.SetAllChildModuleMode(mark.RunnableModule, ModuleMode.SkipMode);
                        return 1;
                    }
                }
                return 0;
            }
            
            if (Program.RuntimeSettings.AutoSkipNgMarks)
            {
                mark.RunnableModule.Mode = ModuleMode.SkipMode;
                Program.ModuleStructure.SetAllChildModuleMode(mark.RunnableModule, ModuleMode.SkipMode);
                return 1;
            }
            else
            {
                //Machine.Instance.FSM.ChangeProductionState(ProductionState.Alarm);
                //string errMsg = "Find Mark Failed!";
                //AbortIgnoreRetryManualForm form = new AbortIgnoreRetryManualForm(errMsg);
                //DialogResult dr = form.ShowDialog();
                Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
                dic.Add(DialogResult.Abort, new Action(() => { }));
                dic.Add(DialogResult.Retry, new Action(() => { }));
                dic.Add(DialogResult.Ignore, new Action(() =>
                {
                    mark.RunnableModule.Mode = ModuleMode.SkipMode;
                    Program.ModuleStructure.SetAllChildModuleMode(mark.RunnableModule, ModuleMode.SkipMode);
                }));
                dic.Add(DialogResult.Cancel, new Action(() =>
                {
                    Machine.Instance.IsProducting = false;
                    this.WaitMarkManualDone.Reset();
                    MsgCenter.Broadcast(MsgType.MSG_FIND_MARK_FIALED, this, mark.MarkCmd.RunnableModule.CommandsModule as Pattern, mark);
                    this.WaitMarkManualDone.WaitOne();
                    Machine.Instance.IsProducting = true;
                }));

                DialogResult? dr = AlarmServer.Instance.Fire(this, AlarmInfoVision.WarnFindMarkFailed, dic);
                switch (dr)
                {
                    case DialogResult.Cancel:
                        if (this.FindMarkDialogResult == DialogResult.OK)
                        {
                            // 更新mark目标位置为用户手动指定位置
                            mark.ModelFindPrm.TargetInMachine.X = Machine.Instance.Robot.PosX;
                            mark.ModelFindPrm.TargetInMachine.Y = Machine.Instance.Robot.PosY;
                        }
                        else
                        {
                            // 用户取消，自动查找一次
                            return this.findMark(mark);
                        }
                        break;
                    case DialogResult.Abort:
                        return -1;
                    case DialogResult.Retry:
                        return this.findMark(mark);                        
                    case DialogResult.Ignore:  //相当于旧软件的跳过
                        return 1;
                }
                return 0;
            }
        }

        /// <summary>
        /// 飞拍指令生成、执行函数
        /// </summary>
        /// <returns></returns>
        private Result executeFlyMarks()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, this.GetType().Name, "Begin to execute flying marks ");
            List<MarkCmd> markList = Program.ModuleStructure.GetAllMarkPoints();
            //1.Mark数量太少不启动飞拍
            //2.所有Mark不在同一层级不启动飞拍
            //3.不执行飞拍则使用常规Mark拍照
            if (markList.Count < 4 || !Program.ModuleStructure.AllMarkIsSameLevel())
            {
                string msg = string.Format("The number of mark is {0},or all mark is not in the same level, execute the normal Marks", markList.Count);
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, msg);
                return this.executeMarkCmds();
            }
            FlyMarks flyMarks = new FlyMarks(markList, coordinateCorrector);
            Result resultTemp = flyMarks.Execute();
            if (flyMarks.Imageflag == -1)
            {
                //string msg = "Fly mark image dispose time out!";
                string msg = "飞拍mark图像处理超时!";
                MessageBox.Show(msgText[12]);
            }
            Logger.DEFAULT.Info(LogCategory.RUNNING, this.GetType().Name, "Execute flying marks end");
            return resultTemp;
        }

        /// <summary>
        /// Description：飞拍校准函数
        /// 用于自动计算飞拍坐标校准值
        /// Author：liyi
        /// Date：2019/06/22
        /// </summary>
        /// <returns></returns>
        private Result FlyMarkOffsetAdjust()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, this.GetType().Name, "Execute normal marks before begin to execute flying marks ");
            //执行一遍定拍
            Result result = this.executeMarkCmds();
            if (!result.IsOk)
            {
                return result;
            }
            //获取所有定拍点位
            List<MarkCmd> markList = Program.ModuleStructure.GetAllMarkPoints();
            //1.Mark数量太少不启动飞拍
            //2.所有Mark不在同一层级不启动飞拍
            if (markList.Count < 4 || !Program.ModuleStructure.AllMarkIsSameLevel())
            {
                return Result.FAILED;
            }
            Dictionary<MarkCmd, PointD> staticMarkPos = new Dictionary<MarkCmd, PointD>();
            PointD realPoint;
            foreach (MarkCmd item in markList)
            {
                realPoint = coordinateCorrector.GetMarkRealPosition(item);
                staticMarkPos.Add(item, realPoint);
            }
            //重置所有拼版状态，确定飞拍中是否有定位失败
            foreach (var item in Program.ModuleStructure.GetAllRunnableModules())
            {
                item.Mode = item.SaveMode;
            }
            //执行一次飞拍
            FlyMarks flyMarks = new FlyMarks(markList, coordinateCorrector);
            result = flyMarks.Execute();
            if (!result.IsOk)
            {
                return result;
            }
            //
            foreach (MarkCmd item in markList)
            {
                //飞拍过程中有Mark定位失败
                if (item.RunnableModule.Mode == ModuleMode.SkipMode)
                {
                    return Result.FAILED;
                }
                realPoint = coordinateCorrector.GetMarkRealPosition(item);
                item.FlyOffset = staticMarkPos[item] - realPoint;
            }
            Program.RuntimeSettings.FlyOffsetList.Clear();
            //确定全部都校准正常后保存到对应变量中，用于保存到文件中
            foreach (MarkCmd item in markList)
            {
                Program.RuntimeSettings.FlyOffsetList.Add(new VectorD(item.FlyOffset.X, item.FlyOffset.Y));
            }
            Logger.DEFAULT.Info(LogCategory.RUNNING, this.GetType().Name, "End of  flying marks execution");
            return Result.OK;
        }

        /// <summary>
        /// 集中处理Blobs命令
        /// </summary>
        /// <returns></returns>
        private Result executeBlobsCmds()
        {
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Begin to execute blobs");
            if (Program.ModuleStructure.BlobsMap.Count <= 0)
            {
                return Result.OK;
            }
            Result ret = Result.OK;
            foreach (RunnableModule module in Program.ModuleStructure.BlobsMap.Keys)
            {
                List<BlobsCmd> blobsCmds = Program.ModuleStructure.BlobsMap[module];
                //已经跳过的拼板不执行
                if (module.Mode == ModuleMode.SkipMode)
                {
                    continue;
                }
                for (int i = 0; i < blobsCmds.Count; i++)
                {
                    // 检测是否应该暂停的操作应该放在下一次外部命令被执行之前，当后续没有外部命令的时候可以直接退出运行
                    checkPaused(blobsCmds[i] as SupportDirectiveCmd);
                    // 暂停状态下中止，直接退出
                    if (this.pendingState == ProgramInnerState.ABORTED)
                    {
                        return Result.FAILED;
                    }
                    Blobs blobs = (Blobs)blobsCmds[i].ToDirective(coordinateCorrector);
                    if(this.findBlobs(blobs) < 0)
                    {
                        return Result.FAILED;
                    }
                    currFinishedSteps++;
                }
            }
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Execute blobs end");
            return Result.OK;
        }

        private int findBlobs(Blobs blobs)
        {
            Result ret = onExecuteDirective(blobs);
            if (ret.IsOk)
            {
                return 0;
            }
            this.WaitBlobsManualDone.Reset();
            MsgCenter.Broadcast(MsgType.MSG_BLOBS_FIALED, this, blobs);
            this.WaitBlobsManualDone.WaitOne();
            if(this.FindBlobsDialogResult == DialogResult.Abort)
            {
                return -1;
            }
            else if (this.FindBlobsDialogResult == DialogResult.Ignore)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 执行命令列表
        /// </summary>
        /// <param name="cmdList">命令列表</param>
        /// <param name="mostEarlyExecTime">最早开始执行时间，不得早于这个时间点</param>
        /// <param name="doMultipassCmd">如果命令列表属于DoMultipassCmd关联的pattern里面的，则该参数不为null.</param>
        /// <param name="passBlockIndex">如果命令列表在pass block里面，则该参数为pass block分组序号</param>
        private Result execute(IReadOnlyList<Command> cmdList, long mostEarlyExecTime, DoMultipassCmd doMultipassCmd = null, int passBlockIndex = VALUE_INVALID)
        {
            if (cmdList == null || cmdList.Count <= 0)
            {
                return Result.OK;
            }
            long waitInMills = mostEarlyExecTime - DateUtils.CurrTimeInMills;
            if (waitInMills > 0)
            {
                if (this.Program.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.普通)
                {
                    this.OnTimerSleeping?.Invoke(DateUtils.CurrTimeInMills, (int)waitInMills);
                    Thread.Sleep((int)waitInMills);
                }
                else
                {
                    Machine.Instance.Robot.BufDelay((int)waitInMills);
                }
            }
            Result ret = Result.OK;
            // 当前BadMark是否跳过
            bool curBadMarkIsSkip = false;
            foreach (Command cmd in cmdList)
            {
                if (cmd.RunnableModule.Mode == ModuleMode.SkipMode)
                {
                    return Result.OK;
                }
                // 检测是否中止运行
                if (this.pendingState == ProgramInnerState.ABORTED)
                {
                    return Result.FAILED;
                }
                // Mark点命令是在运行开始时集中执行的，此处不再重复执行
                if (cmd is MarkCmd)
                {
                    continue;
                }
                if (cmd is BadMarkCmd)
                {
                    BadMarkCmd badMarkCmd = cmd as BadMarkCmd;
                    curBadMarkIsSkip = badMarkCmd.ResultIsSkip;
                    continue;
                }              
                if (cmd is BarcodeCmd)
                {
                    continue;
                }
                // 测高点命令是在运行开始时集中执行的，此处不再重复执行
                if (cmd is MeasureHeightCmd)
                {
                    continue;
                }
                if (cmd is BlobsCmd)
                {
                    continue;
                }
                // 轨道扫描条码,需要弹出提示窗
                if (cmd is ConveyorBarcodeCmd)
                {
                    ConveyorBarcode conveyorBarcode = (ConveyorBarcode)(cmd as ConveyorBarcodeCmd).ToDirective(coordinateCorrector);
                    if (this.findConveryorBarcode(conveyorBarcode) < 0)
                    {
                        conveyorBarcode.RecordBarcode();
                        setRunningState(ProgramInnerState.ABORTED, ProgramInnerState.ABORTED);
                        continue;
                    }
                    conveyorBarcode.RecordBarcode();
                }
                // 需要交给外部去执行的命令
                else if (cmd is SupportDirectiveCmd)
                {
                    // 不是倾斜又使用倾斜指令，跳过处理
                    if (!Machine.Instance.Robot.RobotIsXYZU && !Machine.Instance.Robot.RobotIsXYZUV)
                    {
                        if ((int)(cmd as SupportDirectiveCmd).Tilt != 0)
                        {
                            continue;
                        }
                    }
                    else if (Machine.Instance.Robot.RobotIsXYZU)
                    {
                        // 只有左右倾斜的跳过前后倾斜的逻辑
                        if ((int)(cmd as SupportDirectiveCmd).Tilt > 2)
                        {
                            continue;
                        }
                    }
                    
                    // 如果当前拼版最近的一个BadMark为跳过结果则跳过轨迹
                    if (curBadMarkIsSkip)
                    {
                        continue;
                    }
                    // 判断当前RunnableModule是否跳过
                    if (cmd.RunnableModule.Mode == ModuleMode.SkipMode)
                    {
                        continue;
                    }
                    // 检测是否应该暂停的操作应该放在下一次外部命令被执行之前，当后续没有外部命令的时候可以直接退出运行
                    checkPaused(cmd as SupportDirectiveCmd);
                    // 暂停状态下中止，直接退出
                    if (this.pendingState == ProgramInnerState.ABORTED)
                    {
                        return Result.FAILED;
                    }
                    //出胶检测指令特殊处理
                    if (cmd is NozzleCheckCmd)
                    {
                        NozzleCheckCmd nozzleCheckCmd = cmd as NozzleCheckCmd;
                        if (nozzleCheckCmd.isGlobal)
                        {
                            //全局模式下，如果已经有检测点位执行，跳过处理
                            if (this.nozzleCheckIsFinish)
                            {
                                currFinishedSteps++;
                                continue;
                            }
                            //双阀的全局模式下，单拼版不执行喷嘴检测点位，只在同步拼版执行（保证副阀可以检测）
                            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && this.hasSimulPattern)
                            {
                                if (nozzleCheckCmd.RunnableModule.Mode == ModuleMode.AssignMode1 || nozzleCheckCmd.RunnableModule.Mode == ModuleMode.AssignMode2)
                                {
                                    currFinishedSteps++;
                                    continue;
                                }
                            }
                            this.nozzleCheckIsFinish = true;
                        }
                        //记录执行的喷嘴检测指令，用于点胶完成后检测处理
                        this.finishNozzleCheckCmds.Add(nozzleCheckCmd);
                    }
                    if (Machine.Instance.IsAirPressureErrStop)
                    {
                        //如果点胶动作之前气压报警，直接停止程序
                        setRunningState(ProgramInnerState.ABORTED, ProgramInnerState.ABORTED);
                        return Result.FAILED;
                    }
                    ret = onExecuteDirective((cmd as SupportDirectiveCmd).ToDirective(coordinateCorrector));
                    if (!ret.IsOk)
                    {
                        Machine.Instance.IsMotionErrStop = true;
                        setRunningState(ProgramInnerState.ABORTED, ProgramInnerState.ABORTED);
                        currFinishedSteps++;
                        return ret;
                    }
                    currFinishedSteps++;
                }
                // 内部处理的命令：
                // 1. 程序逻辑控制命令： DoCmd DoMultipassCmd LoopBlockCmd PassBlockCmd
                // 2. 其他命令： SetHeightSenseModeCmd TimerCmd
                else
                {
                    if (cmd is SetHeightSenseModeCmd)
                    {
                        // TODO
                    }
                    else if (cmd is DoCmd)
                    {
                        // 如果当前拼版最近的一个BadMark为跳过结果则跳过轨迹
                        if (curBadMarkIsSkip)
                        {
                            continue;
                        }
                        DoCmd doCmd = cmd as DoCmd;
                        MsgCenter.Broadcast(MsgType.MSG_CURRENT_HEIGHT, this, doCmd.AssociatedMeasureHeightCmd?.RealHtValue);
                        RunnableModule rm = doCmd.AssociatedRunnableModule;
                        bool isWorkPiece = rm.CommandsModule.Name.Equals(Workpiece.WORKPIECE_NAME);
                        pattern curDBPattern = new pattern();
                        if (!IsOffline)
                        {
                            //数据记录
                            if (!isWorkPiece)
                            {
                                curDBPattern.Name = rm.CommandsModule.Name;
                                curDBPattern.LotNum = Program.RuntimeSettings.LotId;
                                curDBPattern.Barcode = Program.RuntimeSettings.ProductionId;
                                curDBPattern.Operator = AccountMgr.Instance.CurrentAccount.NameId;
                                List<MarkCmd> rmMarks = coordinateCorrector.GetRunnableModuleMark(rm);
                                if (rmMarks.Count > 0)
                                {
                                    PointD realMark1Pos = coordinateCorrector.GetMarkRealPosition(rmMarks[0]);
                                    if (realMark1Pos != null)
                                    {
                                        if (realMark1Pos.X != 0 && realMark1Pos.Y != 0)
                                        {
                                            curDBPattern.Mark1X = realMark1Pos.X;
                                            curDBPattern.Mark1Y = realMark1Pos.Y;
                                        }
                                        if (rmMarks.Count > 1)
                                        {
                                            PointD realMark2Pos = coordinateCorrector.GetMarkRealPosition(rmMarks[1]);
                                            if (realMark2Pos.X != 0 && realMark2Pos.Y != 0)
                                            {
                                                curDBPattern.Mark2X = realMark2Pos.X;
                                                curDBPattern.Mark2Y = realMark2Pos.Y;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine && rm.CommandsModule.LineAdjusted)
                        {
                            continue;
                        }
                        //如何是自动分配的副阀pattern，跳过，由主阀生产时同步点胶
                        if (rm.Mode == ModuleMode.SimulMode || rm.Mode == ModuleMode.SkipMode)
                        {
                            if (!IsOffline)
                            {
                                //数据记录
                                if (isWorkPiece)
                                {
                                    this.dbWorkpiece.Result = 0;
                                }
                                else
                                {
                                    curDBPattern.Result = 0;
                                    this.dbWorkpiece.pattern.Add(curDBPattern);
                                }
                            }
                            continue;
                        }
                        //初始测高值：父Module测高值
                        if (doCmd.AssociatedMeasureHeightCmd != null)
                        {
                            rm.MeasuredHt = doCmd.AssociatedMeasureHeightCmd.RealHtValue;
                        }
                        else
                        {
                            RunnableModule parent = Program.ModuleStructure.GetParentModule(rm);
                            if (parent != null)
                            {
                                rm.MeasuredHt = parent.MeasuredHt;
                            }
                        }

                        //数据记录
                        if (!IsOffline)
                        {
                            if (isWorkPiece)
                            {
                                this.dbWorkpiece.Height = rm.MeasuredHt;
                            }
                            else
                            {
                                curDBPattern.Height = rm.MeasuredHt;
                                curDBPattern.StartTime = DateTime.Now;
                            }
                        }

                        //Log.Dprint(string.Format("Pattern-{0} Ht:{1}", rm.CommandsModule.Name, rm.MeasuredHt));
                        string msg = string.Format("Pattern-{0} Ht:{1}", rm.CommandsModule.Name, rm.MeasuredHt);
                        Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, msg);
                        //Program.Patterns
                        //if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine && Program.Patterns.Contains(rm.CommandsModule as Pattern))
                        if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine && rm.CommandsModule is Pattern
                            && !(rm.CommandsModule is Workpiece))
                        {
                            rm.CommandsModule.LineAdjusted = true;
                        }
                        ret = execute(rm.CmdList, DateUtils.CurrTimeInMills);
                        if (!ret.IsOk)
                        {
                            if (!IsOffline)
                            {
                                if (isWorkPiece && !IsOffline)
                                {
                                    this.dbWorkpiece.Result = 0;
                                }
                                else
                                {
                                    curDBPattern.Result = 0;
                                    this.dbWorkpiece.pattern.Add(curDBPattern);
                                }
                            }
                            return ret;
                        }
                        if (!isWorkPiece && !IsOffline)
                        {
                            this.dbWorkpiece.pattern.Add(curDBPattern);
                        }
                    }
                    // 由于 DoMultipassCmd 包含在 LoopBlockCmd 指令中，所以不单独处理 DoMultipassCmd
                    else if (cmd is LoopBlockCmd)
                    {
                        // 如果当前拼版最近的一个BadMark为跳过结果则跳过轨迹
                        if (curBadMarkIsSkip)
                        {
                            continue;
                        }
                        LoopBlockCmd loopBlockCmd = cmd as LoopBlockCmd;
                        for (int i = loopBlockCmd.Start; i <= loopBlockCmd.End; i++)
                        {
                            foreach (DoMultipassCmd doMultiCmd in loopBlockCmd.DoMultipassCmdList)
                            {
                                RunnableModule rm = doMultiCmd.AssociatedRunnableModule;

                                if (Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine)
                                {
                                    if (!rm.CommandsModule.LineAdjusted)
                                    {
                                        rm.AdjustBegin = true;
                                    }
                                    else
                                    {
                                        if (!rm.AdjustBegin)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                //副阀RunnableModule已在主阀中同步生产
                                if (rm.Mode == ModuleMode.SimulMode || rm.Mode == ModuleMode.SkipMode)
                                {
                                    continue;
                                }
                                if (doMultiCmd.AssociatedMeasureheightCmd != null)
                                {
                                    Log.Dprint("this runnable " + rm.CommandsModule.Name + " associatedHeightCmd = " + doMultiCmd.AssociatedMeasureheightCmd.RealHtValue.ToString());
                                    rm.MeasuredHt = doMultiCmd.AssociatedMeasureheightCmd.RealHtValue;
                                }
                                else
                                {
                                    Log.Dprint("this runnable " + rm.CommandsModule.Name + " associatedHeightCmd = ");
                                    rm.MeasuredHt = Program.ModuleStructure.GetParentModule(rm).MeasuredHt;
                                }
                                Log.Dprint(string.Format("Pattern-{0} Ht:{1}", rm.CommandsModule.Name, rm.MeasuredHt));
                                if ((Machine.Instance.Valve1.RunMode == ValveRunMode.AdjustLine) && Program.Patterns.Contains(rm.CommandsModule as Pattern))
                                {
                                    rm.CommandsModule.LineAdjusted = true;
                                }
                                ret = execute(rm.CmdList, getPassBlockExecMostEarlyTime(doMultiCmd, i), doMultiCmd, i);
                                if (!ret.IsOk)
                                {
                                    return ret;
                                }
                            }//foreach end
                        }
                    }
                    else if (cmd is PassBlockCmd && (cmd as PassBlockCmd).Index == passBlockIndex)
                    {
                        // 如果当前拼版最近的一个BadMark为跳过结果则跳过轨迹
                        if (curBadMarkIsSkip)
                        {
                            continue;
                        }
                        ret = execute((cmd as PassBlockCmd).CmdList, DateUtils.CurrTimeInMills, doMultipassCmd, passBlockIndex);
                        if (!ret.IsOk)
                        {
                            return ret;
                        }
                    }
                    else if (cmd is NormalTimerCmd)
                    {
                        // 如果当前拼版最近的一个BadMark为跳过结果则跳过轨迹
                        if (curBadMarkIsSkip)
                        {
                            continue;
                        }
                        int sleepMills = (cmd as NormalTimerCmd).WaitInMills;
                        if (this.Program.RuntimeSettings.FluidMoveMode == Settings.FluidMoveMode.普通)
                        {
                            this.OnTimerSleeping?.Invoke(DateUtils.CurrTimeInMills, sleepMills);
                            Thread.Sleep(sleepMills);
                        }
                        else
                        {
                            Machine.Instance.Robot.BufDelay(sleepMills);
                        }
                    }
                    else if (cmd is TimerCmd)
                    {
                        // 如果当前拼版最近的一个BadMark为跳过结果则跳过轨迹
                        if (curBadMarkIsSkip)
                        {
                            continue;
                        }
                        recordPassBlockDelayTime(doMultipassCmd, passBlockIndex, DateUtils.CurrTimeInMills + (cmd as TimerCmd).WaitInMills);
                    }
                    else if (cmd is ChangeSpeedCmd)
                    {
                        // 如果当前拼版最近的一个BadMark为跳过结果则跳过轨迹
                        if (curBadMarkIsSkip)
                        {
                            continue;
                        }
                        ChangeSpeedCmd command = cmd as ChangeSpeedCmd;

                        // 写入串口
                        if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀
                            || Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀
                            || Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀
                            || Machine.Instance.Valve2.ValveSeries == ValveSeries.齿轮泵阀)
                        {
                            if (!SensorMgr.Instance.SvValve.Write("LB " + command.Speed.ToString() + "F")) 
                            {
                                return Result.FAILED;
                            }
                        }

                        this.Program.RuntimeSettings.SvOrGearValveCurrSpeed = command.Speed;
                        int sleepMills = command.WaitInMills;
                        this.OnTimerSleeping?.Invoke(DateUtils.CurrTimeInMills, sleepMills);
                        Thread.Sleep(sleepMills);

                    }
                }
            } // end of foreach

            return Result.OK;
        }

        /// <summary>
        /// 执行外部调用接口
        /// </summary>
        /// <param name="directive"></param>
        /// <returns></returns>
        private Result onExecuteDirective(Directive directive)
        {
            //Log.Print("execute directive : " + directive.GetType().Name);
            string msg = "execute directive : " + directive.GetType().Name;
            Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, msg);
            Result result = directive.Execute();
            if (!result.IsOk)
            {
                //Log.Print("execute directive ERROR : " + result.ErrMsg + ", stop running!");
                msg = "execute directive ERROR : " + result.ErrMsg + ", stop running!";
                Logger.DEFAULT.Error(LogCategory.RUNNING, TAG, msg);
                //abortInner();
            }
            return result;
        }

        /// <summary>
        /// 存储pass block延时截止时间戳 及 pass block序号
        /// </summary>
        /// <param name="passBlockIndex"></param>
        /// <param name="timerDelayInMills"></param>
        private void recordPassBlockDelayTime(DoMultipassCmd doMultipassCmd, int passBlockIndex, long timerDelayInMills)
        {
            if (doMultipassCmd == null)
            {
                return;
            }
            // 存储pass block序号
            List<int> indexes = null;
            if (doMultipassPassBlockIndexes.ContainsKey(doMultipassCmd))
            {
                indexes = doMultipassPassBlockIndexes[doMultipassCmd];
                if (!indexes.Contains(passBlockIndex))
                {
                    indexes.Add(passBlockIndex);
                }
            }
            else
            {
                indexes = new List<int>();
                indexes.Add(passBlockIndex);
                doMultipassPassBlockIndexes.Add(doMultipassCmd, indexes);
            }
            // 存储pass block延时截止时间戳
            if (doMultipassPassBlockDelayTimes.ContainsKey(doMultipassCmd))
            {
                Dictionary<int, long> passBlockDelayTimes = doMultipassPassBlockDelayTimes[doMultipassCmd];
                if (passBlockDelayTimes.ContainsKey(passBlockIndex))
                {
                    passBlockDelayTimes[passBlockIndex] = timerDelayInMills;
                }
                else
                {
                    passBlockDelayTimes.Add(passBlockIndex, timerDelayInMills);
                }
            }
            else
            {
                Dictionary<int, long> passBlockDelayTimes = new Dictionary<int, long>();
                passBlockDelayTimes.Add(passBlockIndex, timerDelayInMills);
                doMultipassPassBlockDelayTimes.Add(doMultipassCmd, passBlockDelayTimes);
            }
        }

        /// <summary>
        /// 获取 DoMultipassCmd 里面的某个pass block被执行的最早开始时间
        /// </summary>
        /// <param name="doMultipassCmd"></param>
        /// <param name="passBlockIndex"></param>
        /// <returns></returns>
        private long getPassBlockExecMostEarlyTime(DoMultipassCmd doMultipassCmd, int passBlockIndex)
        {
            if (doMultipassPassBlockDelayTimes.ContainsKey(doMultipassCmd))
            {
                // 获取前一个pass block的序号
                int lastIndex = VALUE_INVALID;
                if (doMultipassPassBlockIndexes.ContainsKey(doMultipassCmd))
                {
                    List<int> passBlockIndexes = doMultipassPassBlockIndexes[doMultipassCmd];
                    bool found = false;
                    for (int i = 0; i < passBlockIndexes.Count; i++)
                    {
                        if (passBlockIndexes[i] == passBlockIndex)
                        {
                            // 第一个分组不存在上一个分组，所以需要从第二个算起
                            if (i > 0)
                            {
                                lastIndex = passBlockIndexes[i - 1];
                            }
                            found = true;
                            break;
                        }
                    }
                    // 如果没有找到，则以列表最末尾的序号为上一个pass block序号
                    if (!found && passBlockIndexes.Count > 0)
                    {
                        lastIndex = passBlockIndexes[passBlockIndexes.Count - 1];
                    }
                }
                // 没有找到上一个分组
                if (lastIndex == VALUE_INVALID)
                {
                    return DateUtils.CurrTimeInMills;
                }
                // 获取上一个分组对应的延时截止时间戳
                return doMultipassPassBlockDelayTimes[doMultipassCmd][lastIndex];
            }
            else
            {
                return DateUtils.CurrTimeInMills;
            }
        }

        /// <summary>
        /// 集中处理测高命令
        /// </summary>
        /// <returns></returns>
        private Result executeMeasureHeightCmds()
        {           
            List<MeasureHeightCmd> mhCmdList = null;
            if (Program.ModuleStructure.MHCmdsSorted == null)
            {
                mhCmdList = Program.ModuleStructure.GetAllMeasureHeightCmds();         
            }
            else
            {
                mhCmdList = Program.ModuleStructure.MHCmdsSorted;
            }

            if (mhCmdList.Count <= 0)
            {
                return Result.OK;
            }
            // 所有测高执行之前关闭光源
            Machine.Instance.Light.None();
            // 执行测高点读数
            Result ret = Result.OK;
            foreach (MeasureHeightCmd mhCmd in mhCmdList)
            {
                // 检测是否应该暂停的操作应该放在下一次外部命令被执行之前，当后续没有外部命令的时候可以直接退出运行
                checkPaused(mhCmd as SupportDirectiveCmd);
                // 暂停状态下中止，直接退出
                if (this.pendingState == ProgramInnerState.ABORTED)
                {
                    return Result.FAILED;
                }
                MeasureHeight measureHeight = (MeasureHeight)mhCmd.ToDirective(coordinateCorrector);
                ret = onExecuteDirective(measureHeight);
                if (!ret.IsOk)
                {
                    break;
                }
                // 保存测高点实际读数
                mhCmd.RealHtValue = (double)ret.Param;
                currFinishedSteps++;
            }
            // 所有测高执行之后重置光源
            Machine.Instance.Light.ResetToLast();
            return ret;
        }

        private Result executeMeasureHeightCmdsInMeasureCmd()
        {

            List<MeasureHeightCmd> mhCmdList = null;
            mhCmdList = Program.ModuleStructure.GetAllMeasureGlueHTCmds();
    
            if (mhCmdList.Count <= 0)
            {
                return Result.OK;
            }
            // 所有测高执行之前关闭光源
            Machine.Instance.Light.None();
            // 执行测高点读数
            Result ret = Result.OK;
            foreach (MeasureHeightCmd mhCmd in mhCmdList)
            {
                // 检测是否应该暂停的操作应该放在下一次外部命令被执行之前，当后续没有外部命令的时候可以直接退出运行
                checkPaused(mhCmd as SupportDirectiveCmd);
                // 暂停状态下中止，直接退出
                if (this.pendingState == ProgramInnerState.ABORTED)
                {
                    return Result.FAILED;
                }
                MeasureHeight measureHeight = (MeasureHeight)mhCmd.ToDirective(coordinateCorrector);
                ret = onExecuteDirective(measureHeight);
                if (!ret.IsOk)
                {
                    break;
                }
                // 保存测高点实际读数
                mhCmd.RealHtValue = (double)ret.Param;
                currFinishedSteps++;
            }
            // 所有测高执行之后重置光源
            Machine.Instance.Light.ResetToLast();
            return ret;
        }
        /// <summary>
        /// 将Workpice的所有RunnableModule子节点遍历并配对适合双阀生产的Runnable
        /// </summary>
        /// <param name="modules"></param>
        private void ModulesAutoPairing(List<RunnableModule> modules)
        {
            //workpiece中没有引用pattern或者引用少于2个不做匹配
            if (modules == null || modules.Count < 2)
            {
                return;
            }
            PointD NeedleOffset = Machine.Instance.Robot.CalibPrm.NeedleCamera2 - Machine.Instance.Robot.CalibPrm.NeedleCamera1.ToVector();
            double simulDistence = Program.RuntimeSettings.SimulDistence;
            //如果同步距离小于双阀原点则不能补偿
            if (simulDistence < Math.Abs(NeedleOffset.X) + 2)
            {
                return;
            }
            for (int i = 0; i < modules.Count; i++)
            {
                if (modules[i].Mode == ModuleMode.MainMode || modules[i].Mode == ModuleMode.SimulMode || modules[i].Mode == ModuleMode.SkipMode)
                {
                    continue;
                }
                for (int j = i + 1; j < modules.Count; j++)
                {
                    if (modules[j].Mode == ModuleMode.MainMode || modules[j].Mode == ModuleMode.SimulMode || modules[j].Mode == ModuleMode.SkipMode)
                    {
                        continue;
                    }
                    //如果不是同一pattern不可以同步生产
                    if (!modules[i].CommandsModule.Name.Equals(modules[j].CommandsModule.Name))
                    {
                        continue;
                    }
                    VectorD distence = coordinateCorrector.GetRunnableDistence(modules[i], modules[j]);
                    VectorD Offset = new VectorD();

                    Offset.X = Math.Abs(distence.X) - Math.Abs(simulDistence);
                    if (distence.X < 0)
                    {
                        Offset.Y = (NeedleOffset.Y) + distence.Y;
                    }
                    else
                    {
                        Offset.Y = (NeedleOffset.Y) - distence.Y;
                    }
                    //双阀间距与拼版间距在指定偏差内才可以设定为同步状态,并且给主阀RunnableModule的同步坐标转换器赋值
                    if (Math.Abs(Offset.X) < 5 && Math.Abs(Offset.Y) < 8)
                    {
                        //将所有主拼版的子拼版递归赋值为mainMode
                        if (distence.X < 0)
                        {
                            if (Offset.Y < 1)
                            {
                                modules[i].Mode = ModuleMode.SimulMode;
                                modules[j].Mode = ModuleMode.MainMode;
                                this.hasSimulPattern = true;
                                modules[j].SimulTransformer = coordinateCorrector.GetSimul(modules[j], modules[i]);
                                //同步模式时主阀pattern保存副阀pattern偏移量，用于执行时使用
                                modules[j].SimulModuleOffsetX = modules[i].ModuleOffsetX;
                                modules[j].SimulModuleOffsetY = modules[i].ModuleOffsetY;
                                Program.ModuleStructure.SetAllChildModuleSimulTransfromer(modules[j], modules[i], this.coordinateCorrector);
                                break;
                            }
                        }
                        else
                        {
                            if (Offset.Y < 1)
                            {
                                modules[i].Mode = ModuleMode.MainMode;
                                modules[j].Mode = ModuleMode.SimulMode;
                                this.hasSimulPattern = true;
                                modules[i].SimulTransformer = coordinateCorrector.GetSimul(modules[i], modules[j]);
                                //同步模式时主阀pattern保存副阀pattern偏移量，用于执行时使用
                                modules[i].SimulModuleOffsetX = modules[j].ModuleOffsetX;
                                modules[i].SimulModuleOffsetY = modules[j].ModuleOffsetY;
                                Program.ModuleStructure.SetAllChildModuleSimulTransfromer(modules[i], modules[j], this.coordinateCorrector);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 检查程序的飞拍参数设置和阀速度写入是否成功
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        private Result CheckFlyMarkAndWriteValveSpeed(FluidProgram program)
        {
            Result rst = Result.OK;
            //启用飞拍，当飞拍校准参数无效且不是校准状态时提示，并禁止运行
            if (program.RuntimeSettings.isFlyMarks && !program.RuntimeSettings.FlyOffsetIsValid && !program.RuntimeSettings.isAdjustFlyOffset)
            {
                //MessageBox.Show("the Fly mark offset is invalid");
                MessageBox.Show(msgText[2]);
                return Result.FAILED;
            }

            //如果有螺杆阀则打开串口写入默认速度数据
            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀 || Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀
                || Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀 || Machine.Instance.Valve2.ValveSeries == ValveSeries.齿轮泵阀)
            {
                //因为RTV客户是自己的机台，没有螺杆阀通讯COM口，所以这里先屏蔽掉
                //if (!SensorMgr.Instance.SvValve.Open())
                //{
                //    if (MessageBox.Show(Machine.Instance.HardwareErrorString, "螺杆阀通讯错误，是否忽略?", MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                //    {
                //        return;
                //    }
                //}

                if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀)
                {
                    SvValve valve1 = Machine.Instance.Valve1 as SvValve;
                    SensorMgr.Instance.SvValve.Write("LB " + valve1.Prm.ForwardSpeed.ToString() + "F");
                    SensorMgr.Instance.SvValve.Write("LA " + valve1.Prm.SuckBackTime.ToString() + "F");
                    program.RuntimeSettings.SvOrGearValveCurrSpeed = valve1.Prm.ForwardSpeed;
                }
                else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
                {
                    GearValve valve1 = Machine.Instance.Valve1 as GearValve;
                    SensorMgr.Instance.SvValve.Write("LB " + valve1.Prm.ForwardSpeed.ToString() + "F");
                    program.RuntimeSettings.SvOrGearValveCurrSpeed = valve1.Prm.ForwardSpeed;
                }

                if (Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀)
                {
                    SvValve valve2 = Machine.Instance.Valve2 as SvValve;
                    SensorMgr.Instance.SvValve.Write("LB " + valve2.Prm.ForwardSpeed.ToString() + "F");
                    SensorMgr.Instance.SvValve.Write("LA " + valve2.Prm.SuckBackTime.ToString() + "F");
                }
                else if (Machine.Instance.Valve2.ValveSeries == ValveSeries.齿轮泵阀)
                {
                    GearValve valve2 = Machine.Instance.Valve2 as GearValve;
                    SensorMgr.Instance.SvValve.Write("LB " + valve2.Prm.ForwardSpeed.ToString() + "F");
                }

                if (SvOrGearValveSpeedWeightValve.VavelSpeedWeightDic.Count != 0)
                {
                    program.RuntimeSettings.VavelSpeedDic = SvOrGearValveSpeedWeightValve.VavelSpeedWeightDic;
                }
            }
            return rst;
        }


        #endregion


        #region 状态维护
        /// <summary>
        /// 设置运行状态
        /// </summary>
        private void setRunningState(ProgramInnerState runningState, ProgramInnerState pendingState)
        {
            ProgramOuterState oldState = calculateOuterState(this.runningState, this.pendingState);
            ProgramOuterState newState = calculateOuterState(runningState, pendingState);
            this.runningState = runningState;
            this.pendingState = pendingState;
            if (oldState != newState)
            {
                OnStateChanged?.Invoke(oldState, newState);
            }
            Log.Print("setRunningState runningState=" + runningState + ", pendingState=" + pendingState
                + ", oldState=" + oldState + ", newState=" + newState);
        }

        /// <summary>
        /// 根据程序内部状态计算外部状态
        /// </summary>
        /// <param name="runningState"></param>
        /// <param name="pendingState"></param>
        /// <returns></returns>
        private ProgramOuterState calculateOuterState(ProgramInnerState runningState, ProgramInnerState pendingState)
        {
            ProgramOuterState state = ProgramOuterState.IDLE;
            switch (runningState)
            {
                case ProgramInnerState.IDLE:
                    state = ProgramOuterState.IDLE;
                    break;
                case ProgramInnerState.PAUSED:
                    switch (pendingState)
                    {
                        case ProgramInnerState.PAUSED:
                            state = ProgramOuterState.PAUSED;
                            break;
                        case ProgramInnerState.ABORTED:
                            state = ProgramOuterState.ABORTED;
                            break;
                    }
                    break;
                case ProgramInnerState.RUNNING:
                    switch (pendingState)
                    {
                        case ProgramInnerState.PAUSED:
                            state = ProgramOuterState.PAUSING;
                            break;
                        case ProgramInnerState.RUNNING:
                            state = ProgramOuterState.RUNNING;
                            break;
                        case ProgramInnerState.ABORTED:
                            state = ProgramOuterState.ABORTING;
                            break;
                    }
                    break;
                case ProgramInnerState.ABORTED:
                    state = ProgramOuterState.ABORTED;
                    break;
            }
            return state;
        }
        #endregion


        #region patter weight

        public int ShotNums = 0;
        public Result executePatternWeight(Valve valve, IReadOnlyList<Command> cmdList, long mostEarlyExecTime, DoMultipassCmd doMultipassCmd = null, int passBlockIndex = VALUE_INVALID)
        {
            if (cmdList == null || cmdList.Count <= 0)
            {
                return Result.OK;
            }
            long waitInMills = mostEarlyExecTime - DateUtils.CurrTimeInMills;
            if (waitInMills > 0)
            {
                this.OnTimerSleeping?.Invoke(DateUtils.CurrTimeInMills, (int)waitInMills);
                // 将命令间的延时交给上层去控制
                Thread.Sleep((int)waitInMills);
                //// 将命令间的延时交给硬件层去控制
                //onExecuteDirective(new Executant.Timer(waitInMills));
            }
            Result ret = Result.OK;
            foreach (Command cmd in cmdList)
            {
                // 检测是否中止运行
                if (this.pendingState == ProgramInnerState.ABORTED)
                {
                    return Result.FAILED;
                }

                // 需要交给外部去执行的命令
                if (cmd is IPatternWeightable)
                {
                    // 检测是否应该暂停的操作应该放在下一次外部命令被执行之前，当后续没有外部命令的时候可以直接退出运行
                    checkPaused(cmd as SupportDirectiveCmd);
                    // 暂停状态下中止，直接退出
                    if (this.pendingState == ProgramInnerState.ABORTED)
                    {
                        return Result.FAILED;
                    }
                    Directive dircCmd = (cmd as IPatternWeightable).ToDirectiveISpray();
                    ret = this.onExecuteISpray(valve, dircCmd);
                    this.ShotNums += dircCmd.shortNum;
                    if (!ret.IsOk)
                    {
                        setRunningState(ProgramInnerState.ABORTED, ProgramInnerState.ABORTED);
                        continue;
                    }
                    currFinishedSteps++;
                }
                // 内部处理的命令：
                // 1. 程序逻辑控制命令： DoCmd DoMultipassCmd LoopBlockCmd PassBlockCmd
                // 2. 其他命令： SetHeightSenseModeCmd TimerCmd
                else
                {
                    if (cmd is DoCmd)
                    {
                        DoCmd doCmd = cmd as DoCmd;
                        RunnableModule rm = doCmd.AssociatedRunnableModule;
                        ret = executePatternWeight(valve, rm.CmdList, DateUtils.CurrTimeInMills);
                        if (!ret.IsOk)
                        {
                            return ret;
                        }
                    }
                    // 由于 DoMultipassCmd 包含在 LoopBlockCmd 指令中，所以不单独处理 DoMultipassCmd
                    else if (cmd is LoopBlockCmd)
                    {
                        LoopBlockCmd loopBlockCmd = cmd as LoopBlockCmd;
                        for (int i = loopBlockCmd.Start; i <= loopBlockCmd.End; i++)
                        {
                            foreach (DoMultipassCmd doMultiCmd in loopBlockCmd.DoMultipassCmdList)
                            {
                                RunnableModule rm = doMultiCmd.AssociatedRunnableModule;
                               
                                ret = executePatternWeight(valve, rm.CmdList, getPassBlockExecMostEarlyTime(doMultiCmd, i), doMultiCmd, i);
                                if (!ret.IsOk)
                                {
                                    return ret;
                                }
                            }//foreach end

                        }
                    }
                    else if (cmd is PassBlockCmd && (cmd as PassBlockCmd).Index == passBlockIndex)
                    {
                        ret = executePatternWeight(valve, (cmd as PassBlockCmd).CmdList, DateUtils.CurrTimeInMills, doMultipassCmd, passBlockIndex);
                        if (!ret.IsOk)
                        {
                            return ret;
                        }
                    }
                    else if (cmd is NormalTimerCmd)
                    {
                        int sleepMills = (cmd as NormalTimerCmd).WaitInMills;
                        this.OnTimerSleeping?.Invoke(DateUtils.CurrTimeInMills, sleepMills);
                        Thread.Sleep(sleepMills);
                    }
                    else if (cmd is TimerCmd)
                    {
                        recordPassBlockDelayTime(doMultipassCmd, passBlockIndex, DateUtils.CurrTimeInMills + (cmd as TimerCmd).WaitInMills);
                    }
                }
            } // end of foreach


            return Result.OK;
        }
        private Result onExecuteISpray(Valve valve, Directive directive)
        {
            Result result = Result.OK;
            Log.Print("execute directive : " + directive.GetType().Name);            
            if (directive is Dot)
            {
                Dot dot = directive as Dot;
                result = dot.Spray(valve);
            }
            else if (directive is Line)
            {
                Line line = directive as Line;
                result = line.Spray(valve);
            }
            else if (directive is Arc)
            {
                Arc arc = directive as Arc;
                result = arc.Spray(valve);
            }
            if (!result.IsOk)
            {
                Log.Print("execute directive ERROR : " + result.ErrMsg + ", stop running!");                
            }
            return result;
        }

        #endregion

        # region RTV
        private bool RtvMeasureHasError(out List<string[]> resultList,out List<int> indexList)
        {
            bool error = false;

            List<string[]> results = new List<string[]>();
            List<int> indexs = new List<int>();
            int initIndex = 0;


            CustomRTV rtv = this.GetCustom() as CustomRTV;
            foreach (var item in rtv.Results)
            {
                results.Add(item);
                initIndex++;
                if (Convert.ToDouble(item[0])> Convert.ToDouble(item[1]) || Convert.ToDouble(item[0]) < Convert.ToDouble(item[2])
                    || Convert.ToDouble(item[3]) > Convert.ToDouble(item[4]) || Convert.ToDouble(item[3]) < Convert.ToDouble(item[5]))
                {
                    int index = initIndex;
                    indexs.Add(index);
                    error = true;
                }
            }

            resultList = results;
            indexList = indexs;
            return error;
        }

        private Result ShowRtvResultForm()
        {
            Result result = Result.OK;
            List<string[]> resultList = new List<string[]>();
            List<int> indexList = new List<int>();
            if (this.RtvMeasureHasError(out resultList, out indexList))
            {
               DialogResult dialogResult = new RtvResultForm().SetUp(resultList, indexList).ShowDialog();

               if (dialogResult == DialogResult.Cancel)
               {
                    result = Result.FAILED;
                    this.stopProgram(true);
               }
               else if (dialogResult == DialogResult.Retry)
               {
                    this.ReCheckWidth();
               }
            }

            return result;
        }      
        private void ReCheckWidth()
        {
            CustomRTV rtv = this.GetCustom() as CustomRTV;
            rtv.Results.Clear();
            foreach (var item in Program.ModuleStructure.WorkpieceModule.CmdList)
            {
                if (item is MeasureCmd)
                {
                    this.onExecuteDirective((item as MeasureCmd).ToDirective(coordinateCorrector));
                }
            }

            this.ShowRtvResultForm();
        }
        #endregion

        #region Interfaces

        public void HnadleAlarmDi()
        {
            if (!this.isOffline && this.CurrWorkState == WorkState.Programing)
            {
                //解除注册
                AlarmServer.Instance.UnRegister(this);
                //终止程序运行
                this.Abort();
                //弹出报警
                AlarmServer.Instance.ShowDiAlarm();
            }
        }

        public void SaveMsgLanguageResource()
        {
            for (int i = 0; i < this.msgText.Length; i++)
            {
                LanguageHelper.Instance.SaveMsgLngResource(this.GetType().Name, i, this.msgText[i]);
            }
        }

        public void ReadMsgLanguageResource()
        {
            for (int i = 0; i < this.msgText.Length; i++)
            {
                string temp = LanguageHelper.Instance.ReadMsgLngResource(this.GetType().Name, i);
                if (!temp.Equals(""))
                {
                    this.msgText[i] = temp;
                }
            }
        }

        #endregion
    }
}