using Anda.Fluid.App.Common;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.App.EditHalcon;
using Anda.Fluid.App.EditMark;
using Anda.Fluid.App.Main.EventBroker;
using Anda.Fluid.Domain;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Domain.Conveyor.Forms;
using Anda.Fluid.Domain.Data;
using Anda.Fluid.Domain.Dialogs;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Domain.SVO;
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.GlueManage;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;
using Anda.Fluid.Drive.MachineStates;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.GenKey;
using Anda.Fluid.Infrastructure.Hook;
using Anda.Fluid.Infrastructure.HotKeying;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Infrastructure.Utils;
using DrawingPanel;
using DrawingPanel.Display;
using DrawingPanel.Msg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Anda.Fluid.Domain.FluProgram.Grammar.GrammarParser;

namespace Anda.Fluid.App.Main
{
    public partial class MainForm : FormEx, IAlarmObservable, IMsgSender, IMsgReceiver, IControlUpdating
    {
        private const string OK = "OK";
        private const string ERROR = "ERROR";

        private System.Windows.Forms.Timer timer;
        private PositionMainControl ctlCurrPos;
        private AlarmControl ctlAlarm1;
        private AlarmTransparencyForm transAlarmForm;
        private CanvasControll canvasControll;

        //RTV
        private RTV.RTVInfoCtl ctlRtvInfo;

        /// <summary>
        /// 主窗口
        /// </summary>
        public static MainForm Ins { get; private set; }
        /// <summary>
        /// 全局配置
        /// </summary>
        public GlobalConfig Config { get; set; }
        /// <summary>
        /// 主窗口菜单栏
        /// </summary>
        public NavigateMain MainNav { get; private set; }
        /// <summary>
        /// 编程窗口菜单栏
        /// </summary>
        public NavigateProgram ProgramNav { get; private set; }
        /// <summary>
        /// 运行菜单栏
        /// </summary>
        public NavigateRun RunNav { get; private set; }
        /// <summary>
        /// 编程轨迹控件
        /// </summary>
        public ProgramControl ProgramCtl { get; private set; }
        /// <summary>
        /// 相机控件
        /// </summary>
        //public CameraControl CameraCtl { get; private set; }
        /// <summary>
        /// 运行信息控件
        /// </summary>
        public RunInfoControl RunInfoCtl { get; private set; }

        /// <summary>
        /// 运行信息控件2
        /// </summary>
        public RunInfoControl2 RunInfoCtl2 { get; private set; }
        /// <summary>
        /// 手动操作控件
        /// </summary>
        public ManualControl ManualCtl { get; private set; }

        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private String strMachine = "Machine:";
        private String strMotion = "Motion:";
        private String strVision = "Vision:";
        private String strLaser = "Laser:";
        private String strScale = "Scale:";
        private String strHeater = "Heater:";
        private String strPropor1 = "Propor1:";
        private String strPropor2 = "Propor2:";


        public MainForm()
        {
            lngResources.Add(strMachine, "Machine:");
            lngResources.Add(strMotion, "Motion:");
            lngResources.Add(strVision, "Vision:");
            lngResources.Add(strLaser, "Laser:");
            lngResources.Add(strScale, "Scale:");
            lngResources.Add(strHeater, "Heater:");
            lngResources.Add(strPropor1, "Propor1:");
            lngResources.Add(strPropor2, "Propor2:");

            //数据库存在
            this.LoadConfig();

            this.MainNav = new NavigateMain();
            this.ProgramNav = new NavigateProgram();
            this.RunNav = new NavigateRun();

            this.SetupAlarm();
            //数据库线程启动
            this.SetupDataBase();
            //setup machine
            if (!Machine.Instance.SetupAll())
            {
                return;
            }
            //为了同步气压而增加的事件
            Machine.Instance.Valve1.Proportioner.ChangeProgramAirEvent += FluidProgram.CurrentOrDefault().ChangeAirValue;

            InitializeComponent();
            Ins = this;

            this.WindowState = FormWindowState.Maximized;
            this.Load += FormMain_Load;
            this.FormClosing += FormMain_FormClosing;

            this.ProgramCtl = new ProgramControl();
            this.ProgramCtl.Dock = DockStyle.Fill;
            this.ProgramCtl.SetOwner(this);

            //this.CameraCtl = new CameraControl();
            //this.CameraCtl.Dock = DockStyle.Fill;

            this.ctlCurrPos = new PositionMainControl();
            this.ctlCurrPos.Dock = DockStyle.Fill;

            this.RunInfoCtl = new RunInfoControl();
            this.RunInfoCtl.Dock = DockStyle.Fill;
            this.RunInfoCtl2 = new RunInfoControl2();
            this.RunInfoCtl2.Dock = DockStyle.Fill;

            this.ManualCtl = new ManualControl();
            this.ManualCtl.Dock = DockStyle.Fill;

            this.canvasControll = new CanvasControll();
            this.canvasControll.SetControlMode(true);
            this.canvasControll.Dock = DockStyle.Fill;

            this.SetupPages();
            this.SetupReceivers();
            this.SetupDrawing();

            ControlUpatingMgr.Add(this);
            Machine.Instance.FSM.StateChanged += FSM_StateChanged;
            Machine.Instance.LightTower.Opened += LightTower_Opened;

            this.transAlarmForm.SetOwner(this);
            this.createPath();
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = 100;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
            this.ReadLanguageResources();

        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            if (this.HasLngResources())
            {
                int pagesCount = this.tplProgram.GetPagesCount();
                List<string> lngTextList = this.ReadKeyListFromResources(this.tplProgram.Name);
                for (int i = 0;i < pagesCount; i++)
                {
                    if (i > lngTextList.Count - 1)
                    {
                        break;
                    }
                    this.tplProgram.GetPage(i).TabButton.Text = lngTextList[i];
                }
                pagesCount = this.tplRunInfo.GetPagesCount();
                lngTextList = this.ReadKeyListFromResources(this.tplRunInfo.Name);
                for (int i = 0; i < pagesCount; i++)
                {
                    if (i > lngTextList.Count - 1)
                    {
                        break;
                    }
                    this.tplRunInfo.GetPage(i).TabButton.Text = lngTextList[i];
                }
                pagesCount = this.tplManual.GetPagesCount();
                lngTextList = this.ReadKeyListFromResources(this.tplManual.Name);
                for (int i = 0; i < pagesCount; i++)
                {
                    if (i > lngTextList.Count - 1)
                    {
                        break;
                    }
                    this.tplManual.GetPage(i).TabButton.Text = lngTextList[i];
                }
                pagesCount = this.tplAlarm.GetPagesCount();
                lngTextList = this.ReadKeyListFromResources(this.tplAlarm.Name);
                for (int i = 0; i < pagesCount; i++)
                {
                    if (i > lngTextList.Count - 1)
                    {
                        break;
                    }
                    this.tplAlarm.GetPage(i).TabButton.Text = lngTextList[i];
                }
                pagesCount = this.tplPos.GetPagesCount();
                lngTextList = this.ReadKeyListFromResources(this.tplPos.Name);
                for (int i = 0; i < pagesCount; i++)
                {
                    if (i > lngTextList.Count - 1)
                    {
                        break;
                    }
                    this.tplPos.GetPage(i).TabButton.Text = lngTextList[i];
                }
                lngResources[strMotion] = this.ReadKeyValueFromResources(strMotion);
                lngResources[strVision] = this.ReadKeyValueFromResources(strVision);
                lngResources[strLaser] = this.ReadKeyValueFromResources(strLaser);
                lngResources[strScale] = this.ReadKeyValueFromResources(strScale);
                lngResources[strHeater] = this.ReadKeyValueFromResources(strHeater);
                lngResources[strMachine] = this.ReadKeyValueFromResources(strMachine);
                lngResources[strPropor1] = this.ReadKeyValueFromResources(strPropor1);
                lngResources[strPropor2] = this.ReadKeyValueFromResources(strPropor2);
            }
            this.toolStripStatusLabel1.Text = lngResources[strMachine];
            this.toolStripStatusLabel2.Text = lngResources[strMotion];
            this.toolStripStatusLabel3.Text = lngResources[strVision];
            this.toolStripStatusLabel4.Text = lngResources[strLaser];
            this.toolStripStatusLabel5.Text = lngResources[strScale];
            this.toolStripStatusLabel6.Text = lngResources[strHeater];
            this.toolStripStatusLabel7.Text = lngResources[strPropor1];
            this.toolStripStatusLabel8.Text = lngResources[strPropor2];
        }
       
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyListToResources(this.tplProgram.Name, new List<string> { "program", "camera" });
            this.SaveKeyListToResources(this.tplRunInfo.Name, new List<string> { "run information" });
            this.SaveKeyListToResources(this.tplManual.Name, new List<string> { "manual", "conveyor" });
            this.SaveKeyListToResources(this.tplAlarm.Name, new List<string> { "alarm information" });
            this.SaveKeyListToResources(this.tplPos.Name, new List<string> { "current position" });
        }

        #region Config
        /// <summary>
        /// 加载用户配置文件/权限配置文件/加载语言配置文件
        /// </summary>
        private void LoadConfig()
        {
            string path = SettingsPath.PathBusiness + "\\" + typeof(GlobalConfig).Name;
            this.Config = JsonUtil.Deserialize<GlobalConfig>(path);
            if (this.Config == null)
            {
                this.Config = new GlobalConfig();
            }
            ///控件初始化之前加载权限管理类
            //setup AccessRole
            RoleMgr.Instance.Load();
            //setup UserAccount
            AccountMgr.Instance.Load();
            
            this.SwitchConfig();
        }

        private void createPath()
        {
            DirUtils.CreateDir(SettingsPath.PathSettings);
            DirUtils.CreateDir(SettingsPath.PathMachine);
            DirUtils.CreateDir(SettingsPath.PathBusiness);
        }

        /// <summary>
        /// 注册所有
        /// </summary>
        public void RegisterAllMsgLngClass()
        {
            LanguageHelper.Instance.Register(Executor.Instance);
        }

        private void SwitchConfig()
        {
            this.RegisterAllMsgLngClass();
            switch (this.Config.Lang)
            {
                case LanguageType.en_US:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    LanguageHelper.Instance.SWitchLanguage(LanguageType.en_US);
                    break;
                case LanguageType.zh_CN:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh");
                    LanguageHelper.Instance.SWitchLanguage(LanguageType.zh_CN);
                    break;
            }
        }

        private void SaveConfig()
        {
            string path = SettingsPath.PathBusiness + "\\" + typeof(GlobalConfig).Name;
            JsonUtil.Serialize<GlobalConfig>(path, this.Config);
        }

        #endregion

        private void FormMain_Load(object sender, EventArgs e)
        {
            Machine.Instance.SetupFSM();
            //MsgCenter.Broadcast(LngMsg.SWITCH_LNG, this, this.Config.Lang);
            //显示系统配置信息
            this.OnSetupInfo();
            //弹出加载进度条
            LoadingForm loadingForm = new LoadingForm();
            MsgCenter.Broadcast(MsgType.BUSY, this, null);
            Task.Factory.StartNew(() =>
            {
                Machine.Instance.InitAll();
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    MsgCenter.Broadcast(MsgType.IDLE, this);
                    MsgCenter.Broadcast(MachineMsg.INIT_VISION, this);
                }));
                //setup conveyors
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道状态机启动);
            });

            loadingForm.ShowDialog();
            //AccountMgr.Instance.SwitchUser(AccountMgr.Instance.FindBy(RoleType.Operator.ToString()));
            AccountMgr.Instance.SwitchUser(AccountMgr.Instance.FindBy(RoleType.Operator.ToString()));
            //初始化点胶program
            this.InitFluProgram();
            //胶水管控参数初始化
            GlueManagerMgr.Instance.Setup();
            GlueManagerMgr.Instance.Start();
            //安装全局热键
            HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, true);
            HookHotKeyMgr.Instance.Setup();
            this.setupHotKeys();

            //阀参数数据库初始化
            this.externalButtonAction();

        }
        private void externalButtonAction()
        {
            // 软件不在代码层级直接运行开始
            // 现有逻辑为：UI界面点击开始后在指定逻辑位置循环等待启用IO触发，触发后才开始机台动作
            //MachineServer.Instance.OnRun = new Action(() => { this.RunNav.btnStart_Click(null, null); });
            MachineServer.Instance.OnStop = new Action(() => { this.RunNav.btnAbort_Click(null, null); });
            MachineServer.Instance.OnPausedOrResume = new Action(() => this.RunNav.btnPause_Click(null, null));
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //若新建程序没保存，提示保存
            this.ProgramCtl.SaveProgramIfChanged();
            //提示是否退出应用程序
            //if (MessageBox.Show("Exit the App?", "Exiting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            if (MessageBox.Show("是否退出程序?", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
            //软件退出关闭光源
            Machine.Instance.Light.None();
            //中止点胶程序
            Executor.Instance.Abort();
            //轨道停止
            ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道状态机停止);
            //卸载硬件
            Machine.Instance.UnloadAll();
            //保存硬件配置
            Machine.Instance.SaveAllSettings();
            //各级别权限保存
            RoleMgr.Instance.Save();
            //各用户信息保存
            AccountMgr.Instance.Save();
            //编辑界面保存本次配置
            this.ProgramCtl.SaveSettingDefault();
            //保存全局配置
            this.SaveConfig();
            GlueManagerMgr.Instance.Unload();
            //卸载全局热键
            HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, false);
            HookHotKeyMgr.Instance.Unload();
            //强制结束所有线程
            Environment.Exit(0);
        }

        private void SetupAlarm()
        {
            this.ctlAlarm1 = new AlarmControl();
            this.ctlAlarm1.Dock = DockStyle.Fill;
            AlarmServer.Instance.Register(this.ctlAlarm1);
            AlarmServer.Instance.Register(this);

            this.transAlarmForm = new AlarmTransparencyForm();
            AlarmServer.Instance.Register(this.transAlarmForm);

            AlarmServer.Instance.Register(Machine.Instance.FSM);

            AlarmServer.Instance.OnAlarmFormShown += (dic, list) =>
            {
                DialogResult dr = DialogResult.None;
                this.Invoke(new MethodInvoker(() =>
                {
                    dr = new Infrastructure.Alarming.AlarmForm(dic, list).ShowDialog(this);
                }));
                return dr;
            };

            //RTV
            this.ctlRtvInfo = new RTV.RTVInfoCtl();
            this.ctlRtvInfo.Dock = DockStyle.Fill;
        }

        private void SetupDataBase()
        {
            if (File.Exists("MysqlEnable.txt"))
            {
                DbService.Instance.Enable = true;
            }
            //MySQLDBHelp help = new MySQLDBHelp();
            //if (help.MySqlOpen())
            //{
            //    DbService.Instance.Enable = true;
            //}
            //else
            //{
            //    DbService.Instance.Enable = false;
            //}
            DbService.Instance.Start();
            //string con = ConfigurationManager.ConnectionStrings["afmdbEntities"].ConnectionString;
            //if (System.Data.Entity.Database.Exists(con))
            //{
            //    DbService.Instance.Enable = true;
            //}
            //DbService.Instance.Start();
        }

        private void SetupPages()
        {
            this.pnlNavigate1.Controls.Add(this.MainNav);
            this.pnlNavigate2.Controls.Add(this.RunNav);

            this.tplProgram.AddPages("program");
            this.tplProgram.Dock = DockStyle.Fill;
            this.tplProgram.GetPage(0).Controls.Add(this.canvasControll);
            //this.tplProgram.GetPage(1).Controls.Add(this.CameraCtl);

            this.tplRunInfo.AddPages("run information","运行信息2");
            this.tplRunInfo.Dock = DockStyle.Fill;
            this.tplRunInfo.GetPage(0).Controls.Add(this.RunInfoCtl);
            this.tplRunInfo.GetPage(1).Controls.Add(this.RunInfoCtl2);

            this.tplManual.AddPages("manual");
            this.tplManual.Dock = DockStyle.Fill;
            this.tplManual.GetPage(0).Controls.Add(this.ManualCtl);

            this.tplAlarm.AddPages("alarm information");
            this.tplAlarm.Dock = DockStyle.Fill;

            this.tplAlarm.GetPage(0).Controls.Add(this.ctlAlarm1);

            //RTV
            this.tplAlarm.AddPages("RTV扫码信息");
            this.tplAlarm.GetPage(1).Controls.Add(this.ctlRtvInfo);

            this.tplPos.AddPages("current position");
            this.tplPos.Dock = DockStyle.Fill;
            this.tplPos.GetPage(0).Controls.Add(this.ctlCurrPos);
        }

        /// <summary>
        /// 注册消息监听
        /// </summary>
        private void SetupReceivers()
        {
            MsgCenter.RegisterReceiver(this, Constants.MSG_LOAD_PROGRAM, MachineMsg.SETUP_INFO, MachineMsg.INIT_VISION, MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS, Domain.MsgType.MSG_FIND_MARK_FIALED, Domain.MsgType.MSG_BLOBS_FIALED, LngMsg.SWITCH_LNG, Domain.MsgType.MSG_FIND_BARCODE_FIALED);
            MsgCenter.RegisterReceiver(this.ProgramCtl, Constants.MSG_ADD_PATTERN, Constants.MSG_FINISH_ADDING_CMD_LINE, Constants.MSG_FINISH_ADDING_CMD_LINES, Constants.MSG_FINISH_INSERTING_CMD_LINE, Constants.MSG_FINISH_DELETING_CMD_LINE, Constants.MSG_FINISH_EDITING_CMD_LINE, Constants.MSG_SYS_POSITIONS_DEFS_CHANGED, Constants.MSG_NEW_PROGRAM, Constants.MSG_LOAD_PROGRAM, Constants.MSG_FINISH_EDITING_PATTREN_ORIGIN, Constants.MSG_SAVE_PROGRAM, Domain.MsgType.MSG_LINEEDITLOOK_SHOW, MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS, LngMsg.SWITCH_LNG, Constants.MSG_TEACH_CONVEYOR2_ORIGIN, MachineMsg.SETUP_VALVE,
                Domain.MsgType.MSG_SINGLE_DOT_SETTING,
                Constants.MSG_PATTERN_EDITED);
            MsgCenter.RegisterReceiver(this.MainNav, MsgType.IDLE, MsgType.BUSY, MsgType.RUNNING, MsgType.PAUSED, MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS, LngMsg.SWITCH_LNG);
            MsgCenter.RegisterReceiver(this.ProgramNav, MsgType.IDLE, MsgType.BUSY, MsgType.RUNNING, MsgType.PAUSED, MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS, LngMsg.SWITCH_LNG);
            MsgCenter.RegisterReceiver(this.RunNav, MsgType.IDLE, MsgType.BUSY);
            MsgCenter.RegisterReceiver(this.RunInfoCtl, MsgType.IDLE, MsgType.BUSY, MsgType.RUNNING, MsgType.PAUSED,Constants.MSG_NEW_PROGRAM,MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS, LngMsg.SWITCH_LNG);
            MsgCenter.RegisterReceiver(this.RunInfoCtl2, MachineMsg.SINGLEDROPWEIGHT_UPDATE, Domain.MsgType.MSG_CURRENT_BARCODE, Domain.MsgType.MSG_CURRENT_HEIGHT, LngMsg.SWITCH_LNG);
            MsgCenter.RegisterReceiver(this.ManualCtl, MsgType.IDLE, MsgType.BUSY, MsgType.RUNNING, MsgType.PAUSED,MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS);
            //MsgCenter.RegisterReceiver(this.ConveyorCtl, MsgType.IDLE, MsgType.RUNNING);

            //Rtv
            MsgCenter.RegisterReceiver(this.ctlRtvInfo, LngMsg.MSG_Barcode_Info, LngMsg.MSG_WidthAndHeight_Info, LngMsg.MSG_Clear_RtvInfo);

            MsgCenter.Broadcast(MsgType.IDLE, this, null);
        }

        private void SetupDrawing()
        {
            DrawingMsgCenter.Instance.RegisterReceiver(DrawProgram.Instance);
            DrawingMsgCenter.Instance.RegisterReceiver(this.canvasControll);
        }

        private void InitFluProgram()
        {
            this.ProgramCtl.LoadLastProgram();

            Executor.Instance.OnWorkStateChanged += workState =>
            {
                switch (workState)
                {
                    case Executor.WorkState.Idle:
                        BeginBroadcastIdle();
                        break;
                    case Executor.WorkState.Preparing:
                        BeginBroadcastRunning();
                        break;
                    case Executor.WorkState.WaitingForBoard:
                        BeginBroadcastRunning();
                        break;
                    case Executor.WorkState.Programing:
                        BeginBroadcastRunning();
                        break;
                    case Executor.WorkState.Stopping:
                        BeginBroadcastRunning();
                        break;
                }
            };
            Executor.Instance.OnWorkRunning += () =>
            {
                BeginBroadcastRunning();
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    MsgCenter.SendMsg(MsgType.RUNINFO_START_DATETIME, this, this.RunInfoCtl, Executor.Instance.StartDateTime);
                }));
            };
            Executor.Instance.OnWorkDone += () =>
            {
                BeginBroadcastIdle();
            };
            Executor.Instance.OnWorkStop += () =>
            {
                BeginBroadcastIdle();
            };
            Executor.Instance.OnProgramRunning += () =>
            {
                BeginBroadcastRunning();
            };
            Executor.Instance.OnProgramDone += () =>
            {
                if (!this.IsHandleCreated)
                {
                    return;
                }
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    MsgCenter.SendMsg(MsgType.RUNINFO_RESULT, this, this.RunInfoCtl,
                        Executor.Instance.FinishedBoardCount,
                        Executor.Instance.FailedCount,
                        Executor.Instance.CycleTime);
                }));
            };
            Executor.Instance.OnProgramPausing += () =>
            {
                BeginBroadcastRunning();
            };
            Executor.Instance.OnProgramPaused += () =>
            {
                BeginBroadcastPaused();
            };
            Executor.Instance.OnProgramAborting += () =>
            {
                BeginBroadcastRunning();
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    MsgCenter.SendMsg(MsgType.RUNINFO_RESULT, this, this.RunInfoCtl,
                        Executor.Instance.FinishedBoardCount,
                        Executor.Instance.FailedCount,
                        Executor.Instance.CycleTime);
                }));
            };
            Executor.Instance.OnProgramAborted += () =>
            {
                BeginBroadcastIdle();
            };
            Executor.Instance.OnTimerSleeping += (currMills, waitMills) =>
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    new ShowTimerForm(currMills, waitMills).ShowDialog();
                }));
            };
        }

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            // 更新硬件信息
            if (msgName == MachineMsg.SETUP_INFO)
            {
                OnSetupInfo();
            }
            // 加载程序
            else if (msgName == Constants.MSG_LOAD_PROGRAM)
            {
                string programPath = args[0] as string;
                OnLoadProgram(programPath);
            }
            // 退出AFM
            else if (msgName == MsgType.EXIT)
            {
                this.Close();
            }
            // 进入主界面
            else if (msgName == MsgType.ENTER_MAIN)
            {
                OnEnterMainPage();
            }
            // 进入编程界面
            else if (msgName == MsgType.ENTER_EDIT)
            {
                OnEnterEditPage();
            }
            // 初始化相机
            else if (msgName == MachineMsg.INIT_VISION)
            {
                //this.CameraCtl.SetupCamera(Machine.Instance.Camera);
                //this.CameraCtl.UpdateUI();
            }
            // 切换语言
            else if (msgName == LngMsg.SWITCH_LNG)
            {
                this.Config.Lang = (LanguageType)args[0];
                this.SwitchConfig();
                this.SaveConfig();
                //this.CameraCtl.UpdateUI();
                CameraForm cameraForm = FormMgr.GetForm<CameraForm>();
                JogForm jogForm = FormMgr.GetForm<JogForm>();
                cameraForm.UpdateUI();
                jogForm.UpdateUI();
                if (!cameraForm.Visible)
                {
                    cameraForm.Close();
                }
                if (!jogForm.Visible)
                {
                    jogForm.Close();
                }
                this.ReadLanguageResources();
            }
            //切换了用户或修改了权限表需要更新界面
            else if (msgName == MsgConstants.SWITCH_USER || msgName == MsgConstants.MODIFY_ACCESS)
            {
                //只有Jog和Camera2个非模态窗口，相机界面无运行时参数，所以只需要更新Jog界面。
                JogForm jogForm = FormMgr.GetForm<JogForm>();
                jogForm.UpdateUI();
                if (!jogForm.Visible)
                {
                    jogForm.Close();
                }
            }
            else if (msgName == Domain.MsgType.MSG_FIND_MARK_FIALED)
            {
                this.onFindMarkFailed(args[0] as Pattern, args[1] as Mark);
            }
            else if (msgName == Domain.MsgType.MSG_BLOBS_FIALED)
            {
                this.onFindBlobsFailed(args[0] as Blobs);
            }
            else if (msgName == Domain.MsgType.MSG_FIND_BARCODE_FIALED)
            {
                if (args[1] is Barcode)
                {
                    this.onFindBarcodeFailed(args[0] as Pattern, args[1] as Barcode);
                }
                else if (args[1] is ConveyorBarcode)
                {
                    this.onFindBarcodeFailed(args[0] as Pattern, args[1] as ConveyorBarcode);
                }
            }

        }

        public void HandleAlarmEvent(AlarmEvent e)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }

            if (e.Info.Level < AlarmLevel.Warn)
            {
                return;
            }

            //this.BeginInvoke(new Action(() =>
            //{
            //    this.ProgramNav.BtnAlarm.ShowAlarms();
            //}));
        }

        private void OnSetupInfo()
        {
            String strPropProportioners1 = Machine.Instance.Valve1.ValveSeries.ToString();
            String strPropProportioners2 = Machine.Instance.Valve2.ValveSeries.ToString();
            if ((int)Machine.Instance.Setting.ValveSelect == 0) {
                strPropProportioners2 = "N/A";
            }
            string info = string.Format("{0} [机型:{1}] [板卡:{2}] [阀:{3}] [轨道:{4}] [相机:{5}] [测高:{6}] [天平:{7}] [阀1:{8}, 阀2:{9}],[加热:{10}]",
                Assembly.GetExecutingAssembly().GetName().Version,
                Machine.Instance.Setting.MachineSelect,
                Machine.Instance.Setting.CardSelect,

                Machine.Instance.Setting.ValveSelect,
                Machine.Instance.Setting.ConveyorSelect,
                Machine.Instance.Camera.Prm.Vendor,
                SensorMgr.Instance.Laser.Vendor,
                SensorMgr.Instance.Scale.Vendor,
                strPropProportioners1,
                strPropProportioners2,
                SensorMgr.Instance.Heater.Vendor);
            this.Text = info;

            this.RunInfoCtl.UpdateConveyorProgramSetting();
        }

        private void OnLoadProgram(string programPath)
        {
            if (FluidProgram.Current != null && FluidProgram.CurrentFilePath == programPath)
            {
                AlarmServer.Instance.Fire(FluidProgram.Current, AlarmInfoFlu.WarnProgramAlreadyLoaded);
                return;
            }
            this.ProgramCtl.Enabled = false;
            FluidProgram.Load(programPath,
                program =>
                {
                    if (program == null)
                    {
                        AlarmServer.Instance.Fire(program, AlarmInfoFlu.WarnProgramNull);
                        return;
                    }
                    FluidProgram.CurrentFilePath = programPath;
                    program.HasChanged = false;
                    //加载程序，切换程序相关硬件参数
                    Log.Dprint("Loading program: writing hardware...");
                    program.InitHardware();
                    Log.Dprint("Loading program: writing hardware done!");
                    //语法检查
                    if (Executor.Instance.CurrProgramState != Executor.ProgramOuterState.IDLE
                        && Executor.Instance.CurrProgramState != Executor.ProgramOuterState.ABORTED)
                    {
                        return;
                    }
                    Log.Dprint("Loading program: grammar parsing...");
                    FluidProgram.Current.Parse();
                    Log.Dprint("Loading program: grammar parse done!");
                    //加载飞拍Mark校准值,只能在程序解析后赋值
                    if (FluidProgram.Current.RuntimeSettings.FlyOffsetIsValid)
                    {
                        bool result = FluidProgram.Current.ModuleStructure.SetAllMarkFlyOffset(FluidProgram.Current.RuntimeSettings.FlyOffsetList);
                        if (!result)
                        {
                            FluidProgram.Current.RuntimeSettings.FlyOffsetIsValid = false;
                        }
                    }

                    MsgCenter.SendMsg(MsgType.RUNINFO_PROGRAM, this, this.RunInfoCtl, program.Name);
                },
                (errCode, errMsg) =>
                {
                    AlarmServer.Instance.Fire(FluidProgram.Current, AlarmInfoFlu.ErrorLoadingProgram.AppendMsg(errMsg));
                });
            this.ProgramCtl.Enabled = true;

            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.需要更新绘图程序, FluidProgram.CurrentOrDefault());
            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.进入了Workpiece界面);
        }

        private void OnEnterMainPage()
        {
            this.pnlNavigate1.Controls.Clear();
            this.pnlNavigate1.Controls.Add(this.MainNav);
            this.pnlNavigate2.Controls.Clear();
            this.pnlNavigate2.Controls.Add(this.RunNav);
            this.pnlMain.Controls.Clear();
            this.pnlMain.Controls.Add(this.splitContainer1);
            HookHotKeyMgr.Instance.GetRunKey().OnRun = RunBroker.Instance.StartWork;
            ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.退出编程界面);

        }

        private void OnEnterEditPage()
        {
            this.pnlNavigate1.Controls.Clear();
            this.pnlNavigate1.Controls.Add(this.ProgramNav);
            this.pnlNavigate2.Controls.Clear();
            this.pnlMain.Controls.Clear();
            this.pnlMain.Controls.Add(this.ProgramCtl);
            this.ProgramCtl.LoadFluProgram();
            HookHotKeyMgr.Instance.GetRunKey().OnRun = Executor.Instance.Run;
            ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.进入编程界面);
        }

        private void BeginBroadcastIdle()
        {
            this.BeginInvoke(new Action(() =>
            {
                MsgCenter.Broadcast(MsgType.IDLE, this, null);
            }));
        }

        private void BeginBroadcastRunning()
        {
            this.BeginInvoke(new Action(() =>
            {
                if (Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.Look
                || Machine.Instance.Valve1.RunMode == Drive.ValveSystem.ValveRunMode.InspectDot)
                {
                    FormMgr.Show<CameraForm>(this);
                }
                MsgCenter.Broadcast(MsgType.RUNNING, this, null);
            }));
        }

        private void BeginBroadcastPaused()
        {
            this.BeginInvoke(new Action(() =>
            {
                MsgCenter.Broadcast(MsgType.PAUSED, this, null);
            }));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ControlUpatingMgr.UpdateControls();
            int threadId = Thread.CurrentThread.ManagedThreadId;
        }

        public void Updating()
        {
            this.lblMotionState.Text = Machine.Instance.IsMotionInitDone ? OK : ERROR;
            this.lblMotionState.BackColor = Machine.Instance.IsMotionInitDone ? Color.Green : Color.Red;
            this.lblVisionState.Text = Machine.Instance.IsVisionInitDone ? OK : ERROR;
            this.lblVisionState.BackColor = Machine.Instance.IsVisionInitDone ? Color.Green : Color.Red;
            if (Machine.Instance.Laser.Laserable is LaserableDisable)
            {
                this.lblLaserState.Text = "N/A";
                this.lblLaserState.BackColor = Color.Gray;
            }
            else
            {
                this.lblLaserState.Text = Machine.Instance.Laser.Laserable.CommunicationOK.ToString();
                this.lblLaserState.BackColor = Machine.Instance.Laser.Laserable.CommunicationOK == ComCommunicationSts.OK ? Color.Green : Color.Red;
            }
            if (Machine.Instance.Scale.Scalable is ScalebleDisable)
            {
                this.lblScaleState.Text = "N/A";
                this.lblScaleState.BackColor = Color.Gray;
            }
            else
            {
                this.lblScaleState.Text = Machine.Instance.Scale.Scalable.CommunicationOK.ToString();
                this.lblScaleState.BackColor = Machine.Instance.Scale.Scalable.CommunicationOK == ComCommunicationSts.OK ? Color.Green : Color.Red;
            }
            this.lblPropor1State.Text = Machine.Instance.Proportioner1.Proportional.CommunicationOK.ToString();
            this.lblPropor1State.BackColor = Machine.Instance.Proportioner1.Proportional.CommunicationOK == ComCommunicationSts.OK ? Color.Green : Color.Red;
            if (Machine.Instance.HeaterController1.HeaterControllable is InvalidThermostat)
            {
                this.lblHeaterState.Text = "N/A";
                this.lblHeaterState.BackColor = Color.Gray;
            }
            else
            {
                this.lblHeaterState.Text = Machine.Instance.HeaterController1.HeaterControllable.CommunicationOK.ToString();
                this.lblHeaterState.BackColor = Machine.Instance.HeaterController1.HeaterControllable.CommunicationOK == ComCommunicationSts.OK ? Color.Green : Color.Red;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                this.lblPropor2State.Text = "N/A";
                this.lblPropor2State.BackColor = Color.Gray;
            }
            else
            {
                this.lblPropor2State.Text = Machine.Instance.Proportioner2.Proportional.CommunicationOK.ToString();
                this.lblPropor2State.BackColor = Machine.Instance.Proportioner2.Proportional.CommunicationOK == ComCommunicationSts.OK ? Color.Green : Color.Red;
            }

        }

        private void LightTower_Opened(Drive.MachineStates.LightTowerType arg1, bool arg2)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (arg2)
                {
                    switch (arg1)
                    {
                        case Drive.MachineStates.LightTowerType.Red:
                            this.lblMachineState.BackColor = Color.Red;
                            break;
                        case Drive.MachineStates.LightTowerType.Yellow:
                            this.lblMachineState.BackColor = Color.Orange;
                            break;
                        case Drive.MachineStates.LightTowerType.Green:
                            this.lblMachineState.BackColor = Color.Green;
                            break;
                    }
                }
                else
                {
                    this.lblMachineState.BackColor = SystemColors.Control;
                }
            }));
        }

        private void FSM_StateChanged(Drive.MachineStates.IMachineStatable obj)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.lblMachineState.Text = obj.StateName;
            }));
        }

        private void onFindMarkFailed(Pattern pattern, Mark mark)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Executor.Instance.FindMarkDialogResult = new EditModelFindForm(mark.ModelFindPrm, null, true).ShowDialog(this);
                Executor.Instance.WaitMarkManualDone.Set();
            }));
        }

        private void onFindBlobsFailed(Blobs blobs)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Executor.Instance.FindBlobsDialogResult = new EditBlobsForm(blobs.BlobsTool, true).ShowDialog();
                Executor.Instance.WaitBlobsManualDone.Set();
            }));
        }

        private void onFindBarcodeFailed(Pattern pattern, Barcode barcode)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Executor.Instance.FindMarkDialogResult = new InputBarcodeForm(barcode.BarcodePrm).ShowDialog(this);
                Executor.Instance.WaitMarkManualDone.Set();
            }));
        }

        private void onFindBarcodeFailed(Pattern pattern, ConveyorBarcode conveyorBarcode)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Executor.Instance.FindMarkDialogResult = new InputBarcodeForm(conveyorBarcode).ShowDialog(this);
                Executor.Instance.WaitMarkManualDone.Set();
            }));
        }

        private void setupHotKeys()
        {
            HookHotKeyMgr.Instance.GetRunKey().OnRun = RunBroker.Instance.StartWork;
            HookHotKeyMgr.Instance.GetRunKey().OnPause = Executor.Instance.PauseResume;
            HookHotKeyMgr.Instance.GetRunKey().OnStep = Executor.Instance.SingleStep;
            HookHotKeyMgr.Instance.GetRunKey().OnAbort = Executor.Instance.Abort;
            HookHotKeyMgr.Instance.GetRunKey().OnStop = Executor.Instance.Stop;
        }
    }
}
