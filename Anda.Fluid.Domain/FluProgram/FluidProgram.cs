using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static Anda.Fluid.Infrastructure.Common.CommonDelegates;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Alarming;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Drive.Sensors.Proportionor;
using static Anda.Fluid.Domain.FluProgram.Grammar.GrammarParser;
using Anda.Fluid.Drive.ValveSystem.Prm;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.ValveSystem;
using System.Drawing;
using Anda.Fluid.Infrastructure.Data;
using System.Drawing.Imaging;

namespace Anda.Fluid.Domain.FluProgram
{
    [Serializable]
    public class FluidProgram : CommandsModule, IAlarmSenderable
    {
        [NonSerialized]
        public static string CurrentFilePath = string.Empty;

        [NonSerialized]
        public bool HasChanged = false;

        #region 由于要实现双轨双程序，因此将这四项由Executor中移至此处
        [NonSerialized]
        /// <summary>
        /// 上次清洗时间
        /// </summary>
        public DateTime lastPurgeTime;

        [NonSerialized]
        /// <summary>
        /// 上次清洗板数
        /// </summary>
        public int lastPurgeCount;

        [NonSerialized]
        /// <summary>
        /// 上次称重时间
        /// </summary>
        public DateTime lastScaleTime;

        [NonSerialized]
        /// <summary>
        /// 上次称重板数
        /// </summary>
        public int lastScaleCount;
        #endregion

        private ProgramSettings programSettings = new ProgramSettings();
        /// <summary>
        /// 需要存储在program文件中的参数
        /// </summary>
        public ProgramSettings ProgramSettings
        {
            get { return programSettings; }
        }

        private bool lotControlEnable = false;

        public bool LotControlEnable
        {
            get { return lotControlEnable; }
            set { lotControlEnable = value; }
        }


        private RuntimeSettings runtimeSettings = new RuntimeSettings();
        /// <summary>
        /// 程序运行时参数
        /// </summary>
        public RuntimeSettings RuntimeSettings
        {
            get { return runtimeSettings; }
        }

        private MotionSettings motionSettings = new MotionSettings().Default();
        /// <summary>
        /// 程序运动参数，每个程序一套运动参数
        /// </summary>
        public MotionSettings MotionSettings
        {
            get
            {
                if(motionSettings == null)
                {
                    motionSettings = new MotionSettings().Default();
                }
                return motionSettings;
            }
        }
        
        /// <summary>
        /// 轨道2与轨道1的作业原点机械坐标偏差
        /// </summary>
        public PointD Conveyor2OriginOffset { get; set; } = new PointD(0, 0);

        /// <summary>
        /// 执行层的原点坐标偏差，轨道1是0，轨道2是Conveyor2OriginOffset，由轨道发出消息来动态赋值。
        /// </summary>
        [NonSerialized]
        public PointD ExecutantOriginOffset = new PointD(0, 0);

        /// <summary>
        /// 轨道的宽度
        /// </summary>
        public double ConveyorWidth { get; set; } = 10;

        /// <summary>
        /// 用户自定义位置
        /// </summary>
        public List<UserPosition> UserPositions { get; private set; } = new List<UserPosition>();

        [NonSerialized]
        private static volatile FluidProgram current;
        /// <summary>
        /// 当前加载的程序
        /// </summary>
        public static FluidProgram Current => current;

        /// <summary>
        /// 轨迹pattern
        /// </summary>        
        //public Pattern TrajPattern;

        private Workpiece workpiece;
        /// <summary>
        /// 主Pattern，相当于main()
        /// </summary>
        public Workpiece Workpiece
        {
            get { return workpiece; }
        }

        private List<Pattern> patterns = new List<Pattern>();
        /// <summary>
        /// Pattern列表（不包含Workpiece）
        /// </summary>
        public List<Pattern> Patterns
        {
            get { return new List<Pattern>(patterns); }
        }

        [NonSerialized]
        private Result parseResult = Result.FAILED;
        /// <summary>
        /// 当前程序语法解析结果
        /// </summary>
        public Result ParseResult
        {
            get { return parseResult; }
        }

        // 语法解析器
        [NonSerialized]
        private GrammarParser grammarParser;

        [NonSerialized]
        private RunnableModuleStructure moduleStructure = new RunnableModuleStructure();
        /// <summary>
        /// 程序的RunnableModule结构信息
        /// </summary>
        public RunnableModuleStructure ModuleStructure
        {
            get
            {
                if(this.moduleStructure == null)
                {
                    Log.Dprint("FluidProgram RunnableModuleStructure");
                    this.moduleStructure = new RunnableModuleStructure();
                }
                return this.moduleStructure;
            }
        }

        object IAlarmSenderable.Obj => this;

        private FluidProgram(string name, double workpieceOriginPosX, double workpieceOriginPosY) 
            : base(null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Program name can not be null or empty.");
            }
            this.name = name;
            program = this;
            workpiece = new Workpiece(this, 0, 0);
            workpiece.OriginPos.X = workpieceOriginPosX;
            workpiece.OriginPos.Y = workpieceOriginPosY;
            addDefaultCmdLines();
            this.Conveyor2OriginOffset = new PointD(0, 0);
            this.ExecutantOriginOffset = new PointD(0, 0);

            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀)
            {
                SvValvePrm prm = Machine.Instance.Valve1.Prm.SvValvePrm;
                //TODO 将10替换为默认值
                this.runtimeSettings.VavelSpeedDic.Add(prm.ForwardSpeed, 10);
                //传递程序中的速度重量键值对
                SvOrGearValveSpeedWeightValve.VavelSpeedWeightDic = this.runtimeSettings.VavelSpeedDic;
            }
            else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
            {
                GearValvePrm prm = Machine.Instance.Valve1.Prm.GearValvePrm;
                //TODO 将10替换为默认值
                this.runtimeSettings.VavelSpeedDic.Add(prm.ForwardSpeed, 10);
                //传递程序中的速度重量键值对
                SvOrGearValveSpeedWeightValve.VavelSpeedWeightDic = this.runtimeSettings.VavelSpeedDic;
            }
        }

        /// <summary>
        /// 添加默认的命令
        /// </summary>
        public void addDefaultCmdLines()
        {
            AddCmdLine(new SetHeightSenseModeCmdLine());
            AddCmdLine(new DoCmdLine(workpiece.Name, workpiece.GetOriginPos().X, workpiece.GetOriginPos().Y));
            AddCmdLine(new EndCmdLine());
        }

        /// <summary>
        /// 添加新的Pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public Result AddPattern(Pattern pattern)
        {
            if (pattern == null)
            {
                return new Result(false, null, -1, "Pattern can not be null.");
            }
            if (string.IsNullOrWhiteSpace(pattern.Name))
            {
                return new Result(false, null, -1, "Pattern name can not be empty.");
            }
            if (workpiece.Name == pattern.Name)
            {
                return new Result(false, null, -1, "Pattern name can not be " + workpiece.Name);
            }
            bool found = false;
            foreach (Pattern p in patterns)
            {
                if (p.Name == pattern.Name)
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {
                return new Result(false, null, -1, "Pattern " + pattern.Name + " is already existed.");
            }
            patterns.Add(pattern);
            return Result.OK;
        }

        /// <summary>
        /// 移除指定名称的Pattern
        /// </summary>
        /// <param name="patternName">Pattern名称</param>
        public Result RemovePattern(string patternName)
        {
            if (string.IsNullOrWhiteSpace(patternName))
            {
                return new Result(false, null, -1, "Pattern can not be empty.");
            }
            if (patternName == workpiece.Name)
            {
                return new Result(false, null, -1, "Workpiece can not be removed.");
            }
            foreach (Pattern p in patterns)
            {
                if (patternName == p.Name)
                {
                    patterns.Remove(p);
                    return Result.OK;
                }
            }
            return new Result(false, null, -1, "Pattern " + patternName + " can not be found.");
        }

        /// <summary>
        /// 获取指定名称的 pattern
        /// </summary>
        /// <param name="patternName"></param>
        /// <returns></returns>
        public Pattern GetPatternByName(string patternName)
        {
            if (patternName == workpiece.Name)
            {
                return workpiece;
            }
            foreach (Pattern p in patterns)
            {
                if (p.Name == patternName)
                {
                    return p;
                }
            }
            return null;
        }

        /// <summary>
        /// 解析指令
        /// </summary>
        /// <returns></returns>
        public Result Parse()
        {
            parseResult = Result.FAILED;
            if (grammarParser == null)
            {
                grammarParser = new GrammarParser(this);
            }                      
            parseResult = grammarParser.Parse();
            return parseResult;
        }

        #region 新建、保存、加载程序

        /// <summary>
        /// 返回当前程序，若当前程序为空则返回默认程序
        /// </summary>
        /// <returns></returns>
        public static FluidProgram CurrentOrDefault()
        {
            if(current != null)
            {
                return current;
            }
            else
            {
                return Default();
            }
        }

        [NonSerialized]
        private static FluidProgram defaultProgram;
        /// <summary>
        /// 默认程序，初始化一次
        /// </summary>
        /// <returns>默认空程序</returns>
        public static FluidProgram Default()
        {
            if (defaultProgram == null)
            {
                defaultProgram = new FluidProgram("default", 0, 0);
            }
            return defaultProgram;
        }

        /// <summary>
        /// 创建新程序
        /// </summary>
        /// <param name="name">程序名称</param>
        /// <param name="originPosX">产品坐标原点</param>
        /// <param name="originPosY">产品坐标原点</param>
        /// <returns></returns>
        public static FluidProgram Create(string name, double originPosX, double originPosY)
        {
            current = new FluidProgram(name, originPosX, originPosY);
            current.runtimeSettings.SingleDropWeight = 0.1;
            return current;
        }

        /// <summary>
        /// 保存程序（新建程序需要调用SaveTo, 指定要保存的路径）
        /// </summary>
        /// <param name="onSaving"></param>
        /// <param name="onFinished"></param>
        /// <param name="onError"></param>
        /// <param name="onFinally"></param>
        public void Save(string filePath, Action onSaving, OnFinished onFinished, OnError onError, Action onFinally)
        {
            ThreadUtils.Run(() =>
            {
                Stream fstream = null;
                try
                {
                    onSaving?.Invoke();
                    fstream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    BinaryFormatter binFormat = new BinaryFormatter();
                    binFormat.Serialize(fstream, this);
                    onFinished?.Invoke();
                }
                catch (Exception e)
                {
                    //throw e;
                    onError?.Invoke(-1, e.ToString());
                }
                finally
                {
                    if (fstream != null)
                    {
                        fstream.Close();
                    }
                    onFinally?.Invoke();
                }
            });
        }

        /// <summary>
        /// 加载程序
        /// </summary>
        /// <param name="programName">程序名称</param>
        /// <param name="onFinished"></param>
        /// <param name="onError"></param>
        public static void Load(string programPath, OnFinished<FluidProgram> onFinished, OnError onError)
        {
            Stream fstream = null;
            try
            {
                FluidProgram program = GetProgram(fstream,programPath);  
                
                current = program;

                //传递程序中的速度重量键值对
                SvOrGearValveSpeedWeightValve.VavelSpeedWeightDic = FluidProgram.CurrentOrDefault().runtimeSettings.VavelSpeedDic;

                onFinished?.Invoke(program);
            }
            catch (Exception e)
            {
                onError?.Invoke(-1, e.ToString());
            }
            finally
            {
                if (fstream != null)
                {
                    fstream.Close();
                }
            }
        }

        /// <summary>
        /// 设置需要重新校正mark点
        /// </summary>
        /// <param name="program"></param>
        public void NeedMarkCorrect()
        {
            this.program.workpiece.NeedMarkCorrect = true;
            foreach (Pattern p in this.program.patterns)
            {
                p.NeedMarkCorrect = true;
            }
        }

        /// <summary>
        /// 根据路径获取脚本程序
        /// </summary>
        /// <param name="fstream"></param>
        /// <param name="programPath"></param>
        /// <returns></returns>
        public static FluidProgram GetProgram(Stream fstream,string programPath)
        {
            fstream = new FileStream(programPath, FileMode.Open, FileAccess.Read);
            BinaryFormatter binFormat = new BinaryFormatter();
            FluidProgram program = (FluidProgram)binFormat.Deserialize(fstream);
            // 新增用户位置定义，防止加载之前的程序用户位置为空
            if (program.UserPositions == null)
            {
                program.UserPositions = new List<UserPosition>();
            }
            if (program.Conveyor2OriginOffset == null)
            {
                program.Conveyor2OriginOffset = new PointD();
            }
            if (program.runtimeSettings.FlyOffsetList == null)
            {
                program.runtimeSettings.FlyOffsetList = new List<VectorD>();
            }
            // Load程序后，第一次打开Pattern需要拍摄Mark点进行编程模式下的坐标校正
            program.NeedMarkCorrect();
            // 初始化Mark点
            program.initMarks();

            return program;
        }

        /// <summary>
        /// 程序加载后需要对Mark点初始化
        /// </summary>
        private void initMarks()
        {
            workpiece.InitMarks();
            foreach (Pattern p in patterns)
            {
                p.InitMarks();
            }
        }

        #endregion

        public void InitHardware()
        {
            Logger.DEFAULT.Info(LogCategory.CODE, this.GetType().Name, "InitHardware start");
            Task.Factory.StartNew(() =>
            {
                Machine.Instance.Valve1.Proportioner.Proportional.SetValue((ushort)this.RuntimeSettings.AirPressure);
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    Proportioner.Sleep();
                    Machine.Instance.Valve2.Proportioner.Proportional.SetValue((ushort)this.RuntimeSettings.AirPressure2);
                }

                //写入标准温度
                if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika) 
                {
                    if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                    {
                        Machine.Instance.HeaterController1.Fire(HeaterMsg.设置标准温度值, this.RuntimeSettings.Valve2Temperature, 1);
                    }
                    Machine.Instance.HeaterController1.Fire(HeaterMsg.设置标准温度值, this.RuntimeSettings.Valve1Temperature, 0);
                }
                else
                {
                    if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                    {
                        Machine.Instance.HeaterController2.Fire(HeaterMsg.设置标准温度值, this.RuntimeSettings.Valve2Temperature, 0);
                    }
                    Machine.Instance.HeaterController1.Fire(HeaterMsg.设置标准温度值, this.RuntimeSettings.Valve1Temperature, 0);
                }
            });
            Logger.DEFAULT.Info(LogCategory.CODE, this.GetType().Name, "InitHardware end");
        }

        public void CloseHardware()
        {
            Machine.Instance.Valve1.Proportioner.Proportional.SetValue(0);
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                Proportioner.Sleep();
                Machine.Instance.Valve2.Proportioner.Proportional.SetValue(0);
            }
        }

        public void ChangeAirValue(ushort value)
        {
            FluidProgram.CurrentOrDefault().RuntimeSettings.AirPressure = value;
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                FluidProgram.CurrentOrDefault().RuntimeSettings.AirPressure2 = value;
            }
        }

    }
}