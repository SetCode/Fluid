using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using MetroSet_UI.Controls;
using MetroSet_UI.Child;
using Anda.Fluid.Drive;
using System.IO;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Msg;
using DrawingPanel.Msg;
using DrawingPanel;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Drive.HotKeys;
using System.Reflection;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.App.Metro.Pages;
using Anda.Fluid.App.Common;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.App.EditMark;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Domain.Conveyor.Forms;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.AccessControl;
using Anda.Fluid.Drive.GlueManage;
using Anda.Fluid.Domain.Dialogs.GlueManage;

using Anda.Fluid.Infrastructure.International.Access;
using static Anda.Fluid.Domain.AccessControl.User.PageAccessEnums;
using Anda.Fluid.Drive.HotKeys.HotKeySort;
using Anda.Fluid.App.EditHalcon;

namespace Anda.Fluid.App.Metro.Forms
{
    public partial class MainMetro : MetroSetForm, IAlarmObservable, IMsgSender, IMsgReceiver, IControlUpdating,IAccessControllable
    {
        private Timer timer;
        private SensorReadMetro sensorsForm;
        //权限执行
        private AccessExecutor accessExecutor;
        public  string StrRoot = "AFM";
        public  string StrManual = "MainMetro";
       
        public MainMetro()
        {
            //加载配置文件
            this.loadConfig();
            //加载报警
            this.setupAlarm();
            //加载数据库
            this.setupDataBase();
            //加载硬件
            if(!Machine.Instance.SetupAll())
            {
                return;
            }
            //初始化窗口组件及样式
            InitializeComponent();

            Instance = this;
            this.setupStyle();
            this.setupPages();
            //注册消息监听
            this.setupReceivers();
            this.setupDrawing();
            //注册时间
            this.setupEvents();
            //初始化路径
            this.createPath();
            //开启UI定时器
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = 100;
            this.timer.Tick += Timer_Tick; ;
            this.timer.Start();
            //权限
            this.accessExecutor = new AccessExecutor(this);
            this.LoadAccess();
            AccessControlMgr.Instance.Register(this);
        }


        #region Style

        private void setupStyle()
        {
            this.WindowState = FormWindowState.Maximized;
            this.btnJobs.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnSystem.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnSetup.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnRecipes.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnAlarms.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnDatalog.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnHelp.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
        }

        #endregion


        #region Navigation & Pages

        private MetroSetButton selectedNavigateBtn;
        private PageJobsProduction pageJobsProduction;
        private PageJobsProgram pageJobsProgram;
        private PageJobsFluid pageJobsFluid;
        private PageState pageState;
        private VisionBar visionBar;
        private JogMetro jogMetro;
        private PageSetupMachine pageSetupMachine;
        private PageSetupCamera pageSetupCamera;
        private PageSetupAxes pageSetupAxes;
        private PageSetupRobot pageSetupRobot;
        private PageSetupValves pageSetupValves;
        private PageSetupSensors pageSetupSensors;
        private PageSetupConveyors pageSetupConveyors;
        private PageSetupWeight pageSetupWeight;
        private PageSetupAccess pageSetupAccess;
        private PageSetupMap pageSetupMap;
        private PageSystemManual pageSystemManual;
        private PageSystemHeater pageSystemHeater;
        private PageSystemLaser pageSystemLaser;
        private PageSystemCalib10 pageSystemCalib10;
        private PageSystemCPK pageSystemCPK;
        private PageAlarmsLog pageAlarmsLog;
        private PageHelpHotKeys pageHelpHotKeys;

        private void setupPages()
        {
            this.metroSetTabControl1.SelectedIndexChanged += MetroSetTabControl1_SelectedIndexChanged;
            this.pageJobsProduction = new PageJobsProduction();
            this.pageJobsProgram = new PageJobsProgram();
            this.pageJobsFluid = new PageJobsFluid();
            this.pageJobsFluid.Dock = DockStyle.Fill;
            this.pageJobsFluid.ConnectPage(this.pageJobsProgram);
            this.pageState = new PageState();
            this.pageState.Dock = DockStyle.Fill;
            this.visionBar = new VisionBar();
            this.visionBar.ConnectCameraView(this.cameraView1);
            this.panelVisonBar.Visible = false;
            this.jogMetro = new JogMetro();
            this.pageSetupMachine = new PageSetupMachine();
            this.pageSetupCamera = new PageSetupCamera();
            this.pageSetupAxes = new PageSetupAxes();
            this.pageSetupRobot = new PageSetupRobot();
            this.pageSetupValves = new PageSetupValves();
            this.pageSetupSensors = new PageSetupSensors();
            this.pageSetupConveyors = new PageSetupConveyors();
            this.pageSetupWeight = new PageSetupWeight();
            this.pageSetupAccess = new PageSetupAccess();
            this.pageSetupMap = new PageSetupMap();
            this.pageSystemManual = new PageSystemManual();
            this.pageSystemHeater = new PageSystemHeater();
            this.pageSystemLaser = new PageSystemLaser();
            this.pageSystemCalib10 = new PageSystemCalib10();
            this.pageSystemCPK = new PageSystemCPK();
            this.pageAlarmsLog = new PageAlarmsLog();
            this.pageHelpHotKeys = new PageHelpHotKeys();
            this.btnJobs_Click(null, null);
            MsgCenter.SendMsg(MsgDef.ENTER_MAIN, this, this);
      
        }

        private void MetroSetTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.selectedNavigateBtn != btnJobs)
            {
                return;
            }
            switch(this.metroSetTabControl1.SelectedIndex)
            {
                case 0:
                    this.addStatePage();
                    MsgCenter.SendMsg(MsgDef.ENTER_MAIN, this, this);
                    break;
                case 1:
                    this.addFluidPage();
                    MsgCenter.SendMsg(MsgDef.ENTER_EDIT, this, this);
                    break;
            }
        }

        private void btnJobs_Click(object sender, EventArgs e)
        {
            selectNavigation(btnJobs);
            this.metroSetTabControl1.Controls.Clear();
            this.metroSetTabControl1.Controls.Add(newTabPage("生产界面", this.pageJobsProduction));
            this.metroSetTabControl1.Controls.Add(newTabPage("编程界面", this.pageJobsProgram));
            this.visionBar.Dock = DockStyle.Fill;
            this.panelVisonBar.Controls.Add(this.visionBar);
            this.addStatePage();
        }

        private void btnSystem_Click(object sender, EventArgs e)
        {
            selectNavigation(btnSystem);
            this.metroSetTabControl1.Controls.Clear();
            this.metroSetTabControl1.Controls.Add(newTabPage("手动调试", this.pageSystemManual));
            this.metroSetTabControl1.Controls.Add(newTabPage("十步校正", this.pageSystemCalib10));
            this.metroSetTabControl1.Controls.Add(newTabPage("加热状态", this.pageSystemHeater));
            this.metroSetTabControl1.Controls.Add(newTabPage("测高诊断", this.pageSystemLaser));
            //this.metroSetTabControl1.Controls.Add(newTabPage("维修诊断", null));       
            //this.metroSetTabControl1.Controls.Add(newTabPage("相机标定", null));
            this.metroSetTabControl1.Controls.Add(newTabPage("CPK", this.pageSystemCPK));
            this.addStatePage();
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            selectNavigation(btnSetup);
            this.metroSetTabControl1.Controls.Clear();
            this.metroSetTabControl1.Controls.Add(newTabPage("机型", this.pageSetupMachine));
            this.metroSetTabControl1.Controls.Add(newTabPage("相机", this.pageSetupCamera));
            this.metroSetTabControl1.Controls.Add(newTabPage("单轴", this.pageSetupAxes));
            this.metroSetTabControl1.Controls.Add(newTabPage("阀组", this.pageSetupValves));
            this.metroSetTabControl1.Controls.Add(newTabPage("传感器", this.pageSetupSensors));
            this.metroSetTabControl1.Controls.Add(newTabPage("轨道", this.pageSetupConveyors));
            this.metroSetTabControl1.Controls.Add(newTabPage("权限管理", this.pageSetupAccess));
            this.metroSetTabControl1.Controls.Add(newTabPage("棋盘校正", this.pageSetupMap));
            this.addStatePage();
        }

        private void btnRecipes_Click(object sender, EventArgs e)
        {
            selectNavigation(btnRecipes);
            this.metroSetTabControl1.Controls.Clear();
            this.metroSetTabControl1.Controls.Add(newTabPage("运动参数", this.pageSetupRobot));
            this.metroSetTabControl1.Controls.Add(newTabPage("称重参数", this.pageSetupWeight));
            this.addStatePage();
        }

        private void btnAlarms_Click(object sender, EventArgs e)
        {
            selectNavigation(btnAlarms);
            this.metroSetTabControl1.Controls.Clear();
            this.metroSetTabControl1.Controls.Add(newTabPage("报警记录", this.pageAlarmsLog));
            this.addStatePage();
        }

        private void btnDatalog_Click(object sender, EventArgs e)
        {
            selectNavigation(btnDatalog);
            this.metroSetTabControl1.Controls.Clear();
            this.metroSetTabControl1.Controls.Add(newTabPage("历史数据", null));
            this.metroSetTabControl1.Controls.Add(newTabPage("日志事件", null));
            this.metroSetTabControl1.Controls.Add(newTabPage("OEE", null));
            this.metroSetTabControl1.Controls.Add(newTabPage("SPC", null));
            this.addStatePage();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            selectNavigation(btnHelp);
            this.metroSetTabControl1.Controls.Clear();
            this.metroSetTabControl1.Controls.Add(newTabPage("快捷键", this.pageHelpHotKeys));
            this.addStatePage();
        }

        private void selectNavigation(MetroSetButton btn)
        {
            if (this.selectedNavigateBtn != null)
            {
                this.selectedNavigateBtn.Selected = false;
            }
            btn.Selected = true;
            this.selectedNavigateBtn = btn;
        }

        private MetroSetTabPage newTabPage(string text, Control control)
        {
            MetroSetTabPage page = new MetroSetTabPage()
            {
                Text = text + "   ",
                Style = this.styleManager1.Style
            };
            if (control != null)
            {
                page.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }
            return page;
        }

        private void addFluidPage()
        {
            this.panelState.Controls.Clear();
            this.panelState.Controls.Add(this.pageJobsFluid);
        }

        private void addStatePage()
        {
            this.panelState.Controls.Clear();
            this.panelState.Controls.Add(this.pageState);
        }

        #endregion


        #region Load & Exit

        private void MetroForm_Load(object sender, EventArgs e)
        {
            //加载机台状态机
            Machine.Instance.SetupFSM();
            //MsgCenter.Broadcast(LngMsg.SWITCH_LNG, this, this.Config.Lang);
            //显示系统配置信息
            MsgCenter.SendMsg(MachineMsg.SETUP_INFO, this, this.pageJobsProduction);
            //实例化进度条窗口
            LoadingMetro loadingForm = new LoadingMetro();
            //广播忙碌状态
            MsgCenter.Broadcast(MsgDef.BUSY, this, null);
            Task.Factory.StartNew(() =>
            {
                //初始化所有硬件
                Machine.Instance.InitAll();
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    //初始化完成，广播空闲状态
                    MsgCenter.Broadcast(MsgDef.IDLE, this);
                    MsgCenter.Broadcast(MachineMsg.INIT_VISION, this);
                }));
                //启动轨道
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道状态机启动);
            });
            //显示进度条窗口
            loadingForm.ShowDialog();
            //切换用户
            AccountMgr.Instance.SwitchUser(AccountMgr.Instance.FindBy(RoleType.Developer.ToString()));
            //初始化点胶program
            this.initFluProgram();
            //胶水管控参数初始化
            GlueManagerMgr.Instance.Setup();
            GlueManagerMgr.Instance.Start();
            //安装全局热键钩子
            HookHotKeyMgr.Instance.Setup();
            //初始化热键
            this.setupHotKeys();
        }

        private void MetroForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //若新建程序没保存，提示保存
            this.pageJobsProgram.SaveProgramIfChanged();
            //提示是否退出应用程序
            if (MetroSetMessageBox.Show(this, "确定退出程序?", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
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
            Properties.Settings.Default.Save();
            //保存全局配置
            this.saveConfig();
            GlueManagerMgr.Instance.Unload();
            //卸载全局热键
            HookHotKeyMgr.Instance.Unload();

            //权限保存            
            //AccessControlMgr.Instance.Save();

            //强制结束所有线程
            Environment.Exit(0);

           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region Properties

        /// <summary>
        /// 主窗口
        /// </summary>
        public static MainMetro Instance { get; private set; } 
        /// <summary>
        /// 全局配置
        /// </summary>
        public GlobalConfig Config { get; set; }
              

        #endregion


        #region Config

        /// <summary>
        /// 加载用户配置文件/权限配置文件/加载语言配置文件
        /// </summary>
        private void loadConfig()
        {
            string path = SettingsPath.PathBusiness + "\\" + typeof(GlobalConfig).Name;
            this.Config = JsonUtil.Deserialize<GlobalConfig>(path);
            if (this.Config == null)
            {
                this.Config = new GlobalConfig();
            }
            ///控件初始化之前加载权限管理类
            RoleMgr.Instance.Load();
            AccountMgr.Instance.Load();

            AccessControlMgr.Instance.Load();
            this.switchConfig();
        }

        private void switchConfig()
        {
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

        private void saveConfig()
        {
            string path = SettingsPath.PathBusiness + "\\" + typeof(GlobalConfig).Name;
            JsonUtil.Serialize<GlobalConfig>(path, this.Config);
        }

        private void createPath()
        {
            DirUtils.CreateDir(SettingsPath.PathSettings);
            DirUtils.CreateDir(SettingsPath.PathMachine);
            DirUtils.CreateDir(SettingsPath.PathBusiness);
        }

        #endregion


        #region Alarms

        private void setupAlarm()
        {
            AlarmServer.Instance.Register(this);
            //this.transAlarmForm = new AlarmTransparencyForm();
            //this.transAlarmForm.SetOwner(this);
            //AlarmServer.Instance.Register(this.transAlarmForm);
            AlarmServer.Instance.Register(Machine.Instance.FSM);
        }

        #endregion


        #region DataBase

        private void setupDataBase()
        {
            if (File.Exists("MysqlEnable.txt"))
            {
                DbService.Instance.Enable = true;
            }
            DbService.Instance.Start();
        }

        #endregion


        #region Message Recievers

        /// <summary>
        /// 注册消息监听
        /// </summary>
        private void setupReceivers()
        {
            MsgCenter.RegisterReceiver(this, Constants.MSG_LOAD_PROGRAM, 
                MachineMsg.SETUP_INFO, MachineMsg.INIT_VISION, MsgConstants.SWITCH_USER, 
                MsgConstants.MODIFY_ACCESS, Domain.MsgType.MSG_FIND_MARK_FIALED, Domain.MsgType.MSG_BLOBS_FIALED, LngMsg.SWITCH_LNG);
            MsgCenter.RegisterReceiver(this.pageJobsProgram, Constants.MSG_ADD_PATTERN, 
                Constants.MSG_FINISH_ADDING_CMD_LINE, Constants.MSG_FINISH_ADDING_CMD_LINES, 
                Constants.MSG_FINISH_INSERTING_CMD_LINE, Constants.MSG_FINISH_DELETING_CMD_LINE, 
                Constants.MSG_FINISH_EDITING_CMD_LINE, Constants.MSG_SYS_POSITIONS_DEFS_CHANGED, 
                Constants.MSG_NEW_PROGRAM, Constants.MSG_LOAD_PROGRAM, 
                Constants.MSG_FINISH_EDITING_PATTREN_ORIGIN, Constants.MSG_SAVE_PROGRAM, 
                Domain.MsgType.MSG_LINEEDITLOOK_SHOW, MsgConstants.SWITCH_USER, 
                MsgConstants.MODIFY_ACCESS, LngMsg.SWITCH_LNG, Constants.MSG_TEACH_CONVEYOR2_ORIGIN, 
                MachineMsg.SETUP_VALVE);
            MsgCenter.RegisterReceiver(this.pageJobsFluid, MsgDef.MSG_PARAMPAGE_CLEAR);
            MsgCenter.RegisterReceiver(this.pageJobsProduction, MsgDef.IDLE, MsgDef.BUSY, MsgDef.RUNNING, MsgDef.PAUSED, 
                Constants.MSG_NEW_PROGRAM, MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS, 
                LngMsg.SWITCH_LNG, MachineMsg.SETUP_INFO);
            //MsgCenter.RegisterReceiver(this.MainNav, MsgType.IDLE, MsgType.BUSY, MsgType.RUNNING, MsgType.PAUSED, MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS, LngMsg.SWITCH_LNG);
            //MsgCenter.RegisterReceiver(this.ProgramNav, MsgType.IDLE, MsgType.BUSY, MsgType.RUNNING, MsgType.PAUSED, MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS, LngMsg.SWITCH_LNG);
            //MsgCenter.RegisterReceiver(this.RunNav, MsgType.IDLE, MsgType.BUSY);
            //MsgCenter.RegisterReceiver(this.RunInfoCtl, MsgType.IDLE, MsgType.BUSY, MsgType.RUNNING, MsgType.PAUSED, Constants.MSG_NEW_PROGRAM, MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS, LngMsg.SWITCH_LNG);
            //MsgCenter.RegisterReceiver(this.ManualCtl, MsgType.IDLE, MsgType.BUSY, MsgType.RUNNING, MsgType.PAUSED, MsgConstants.SWITCH_USER, MsgConstants.MODIFY_ACCESS);
            //MsgCenter.Broadcast(MsgType.IDLE, this, null);
        }

        private void setupDrawing()
        {
            //DrawingMsgCenter.Instance.RegisterReceiver(DrawProgram.Instance);
        }

        #endregion


        #region Handle Events

        private void setupEvents()
        {
            //增加到界面刷新列表
            ControlUpatingMgr.Add(this);
            //为了同步气压而增加的事件
            Machine.Instance.Valve1.Proportioner.ChangeProgramAirEvent += FluidProgram.CurrentOrDefault().ChangeAirValue;
        }

        public void Updating()
        {
            this.lblPosX.Text = Machine.Instance.Robot?.PosX.ToString();
            this.lblPosY.Text = Machine.Instance.Robot?.PosY.ToString();
            this.lblPosZ.Text = Machine.Instance.Robot?.PosZ.ToString();
            if (Machine.Instance.Setting.AxesStyle == Drive.Motion.ActiveItems.RobotAxesStyle.XYZAB)
            {
                this.lblPosA.Text = Machine.Instance.Robot?.PosA.ToString();
                this.lblPosB.Text = Machine.Instance.Robot?.PosB.ToString();
            }
            else
            {
                this.lblPosA.Text = "N/A";
                this.lblPosB.Text = "N/A";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ControlUpatingMgr.UpdateControls();
        }

        public void HandleAlarmEvent(AlarmEvent e)
        {

        }

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            // 加载程序
            if (msgName == Constants.MSG_LOAD_PROGRAM)
            {
                string programPath = args[0] as string;
                onLoadProgram(programPath);
            }
            // 进入主界面
            else if (msgName == MsgDef.ENTER_MAIN)
            {
                onEnterProductionPage();
            }
            // 进入编程界面
            else if (msgName == MsgDef.ENTER_EDIT)
            {
                onEnterProgramPage();
            }
            // 初始化相机
            else if (msgName == MachineMsg.INIT_VISION)
            {
                this.cameraView1.SetupCamera(Machine.Instance.Camera);
                //this.cameraView1.UpdateUI();
            }
            // 切换语言
            else if (msgName == LngMsg.SWITCH_LNG)
            {
                //this.Config.Lang = (LanguageType)args[0];
                //this.SwitchConfig();
                //this.SaveConfig();
                //this.CameraCtl.UpdateUI();
                //CameraForm cameraForm = FormMgr.GetForm<CameraForm>();
                //JogForm jogForm = FormMgr.GetForm<JogForm>();
                //cameraForm.UpdateUI();
                //jogForm.UpdateUI();
                //if (!cameraForm.Visible)
                //{
                //    cameraForm.Close();
                //}
                //if (!jogForm.Visible)
                //{
                //    jogForm.Close();
                //}
                //this.ReadLanguageResources();
            }
            //切换了用户或修改了权限表需要更新界面
            else if (msgName == MsgConstants.SWITCH_USER || msgName == MsgConstants.MODIFY_ACCESS)
            {
                this.UpdateAllUIAccess();
                ////只有Jog和Camera2个非模态窗口，相机界面无运行时参数，所以只需要更新Jog界面。
                //JogForm jogForm = FormMgr.GetForm<JogForm>();
                //jogForm.UpdateUI();
                //if (!jogForm.Visible)
                //{
                //    jogForm.Close();
                //}
            }
            else if (msgName == Domain.MsgType.MSG_FIND_MARK_FIALED)
            {
                this.onFindMarkFailed(args[0] as Pattern, args[1] as Mark);
            }
            else if (msgName == Domain.MsgType.MSG_BLOBS_FIALED)
            {
                this.onFindBlobsFailed(args[0] as Blobs);
            }
            else if (msgName == MsgDef.IDLE)
            {
                HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, true);
                this.btnPurge.Enabled = true;
                this.btnPrime.Enabled = true;
                this.btnScale.Enabled = true;
                this.btnHeaterIO.Enabled = true;
                this.btnScanner.Enabled = true;
                this.btnLaser.Enabled = true;
                this.btnBoardIn.Enabled = true;
                this.btnBoardOut.Enabled = true;
                this.btnConveyorState.Enabled = true;
                this.btnVisionBar.Enabled = true;
            }
            else if (msgName == MsgDef.RUNNING || msgName == MsgDef.PAUSED || msgName == MsgDef.BUSY)
            {
                HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.JogKey, false);
                this.btnPurge.Enabled = false;
                this.btnPrime.Enabled = false;
                this.btnScale.Enabled = false;
                this.btnHeaterIO.Enabled = false;
                this.btnScanner.Enabled = false;
                this.btnLaser.Enabled = false;
                this.btnBoardIn.Enabled = false;
                this.btnBoardOut.Enabled = false;
                this.btnVisionBar.Enabled = false;
                if (msgName == MsgDef.RUNNING || msgName == MsgDef.PAUSED)
                {
                    this.btnConveyorState.Enabled = true;
                }
                else
                {
                    this.btnConveyorState.Enabled = false;
                }

            }
        }

        private void onLoadProgram(string programPath)
        {
            if (FluidProgram.Current != null && FluidProgram.CurrentFilePath == programPath)
            {
                AlarmServer.Instance.Fire(FluidProgram.Current, AlarmInfoFlu.WarnProgramAlreadyLoaded);
                return;
            }
            this.pageJobsProgram.Enabled = false;
            this.pageJobsFluid.Enabled = false;
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
                    MsgCenter.SendMsg(MsgDef.RUNINFO_PROGRAM, this, this.pageJobsProduction, program.Name);
                },
                (errCode, errMsg) =>
                {
                    AlarmServer.Instance.Fire(FluidProgram.Current, AlarmInfoFlu.ErrorLoadingProgram.AppendMsg(errMsg));
                });
            this.pageJobsProgram.Enabled = true;
            this.pageJobsFluid.Enabled = true;

            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.需要更新绘图程序, FluidProgram.CurrentOrDefault());
            DrawingMsgCenter.Instance.SendMsg(DrawingMessage.进入了Workpiece界面);
        }

        private void onEnterProductionPage()
        {
            HookHotKeyMgr.Instance.GetRunKey().OnRun = RunBroker.Instance.StartWork;
            ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.退出编程界面);

        }

        private void onEnterProgramPage()
        {
            this.pageJobsProgram.LoadFluProgram();
            HookHotKeyMgr.Instance.GetRunKey().OnRun = Executor.Instance.Run;
            ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.进入编程界面);
        }

        private void onFindMarkFailed(Pattern pattern, Mark mark)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Executor.Instance.FindMarkDialogResult = new EditModelFindForm(mark.ModelFindPrm, null, true).ShowDialog();
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

        #endregion


        #region FluProgram

        private void initFluProgram()
        {
            // 加载上一次点胶程序
            string lastProgramPath = Properties.Settings.Default.ProgramName;
            if (!File.Exists(lastProgramPath))
            {
                return;
            }
            MsgCenter.Broadcast(Constants.MSG_LOAD_PROGRAM, this, lastProgramPath);
            // 注册点胶执行器相关事件
            Executor.Instance.OnWorkStateChanged += workState =>
            {
                switch (workState)
                {
                    case Executor.WorkState.Idle:
                        beginBroadcastIdle();
                        break;
                    case Executor.WorkState.Preparing:
                        beginBroadcastRunning();
                        break;
                    case Executor.WorkState.WaitingForBoard:
                        beginBroadcastRunning();
                        break;
                    case Executor.WorkState.Programing:
                        beginBroadcastRunning();
                        break;
                    case Executor.WorkState.Stopping:
                        beginBroadcastRunning();
                        break;
                }
            };
            Executor.Instance.OnWorkRunning += () =>
            {
                beginBroadcastRunning();
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    MsgCenter.SendMsg(MsgDef.RUNINFO_START_DATETIME, this, this.pageJobsProduction, Executor.Instance.StartDateTime);
                }));
            };
            Executor.Instance.OnWorkDone += () =>
            {
                beginBroadcastIdle();
            };
            Executor.Instance.OnWorkStop += () =>
            {
                beginBroadcastIdle();
            };
            Executor.Instance.OnProgramRunning += () =>
            {
                beginBroadcastRunning();
            };
            Executor.Instance.OnProgramDone += () =>
            {
                if (!this.IsHandleCreated)
                {
                    return;
                }
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    MsgCenter.SendMsg(MsgDef.RUNINFO_RESULT, this, this.pageJobsProduction,
                        Executor.Instance.FinishedBoardCount,
                        Executor.Instance.FailedCount,
                        Executor.Instance.CycleTime);
                }));
            };
            Executor.Instance.OnProgramPausing += () =>
            {
                beginBroadcastRunning();
            };
            Executor.Instance.OnProgramPaused += () =>
            {
                beginBroadcastPaused();
            };
            Executor.Instance.OnProgramAborting += () =>
            {
                beginBroadcastRunning();
            };
            Executor.Instance.OnProgramAborted += () =>
            {
                beginBroadcastIdle();
            };
            Executor.Instance.OnTimerSleeping += (currMills, waitMills) =>
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    new ShowTimerMetro(currMills, waitMills).Show(this);
                }));
            };
        }

        private void beginBroadcastIdle()
        {
            this.BeginInvoke(new Action(() =>
            {
                MsgCenter.Broadcast(MsgDef.IDLE, this, null);
            }));
        }

        private void beginBroadcastRunning()
        {
            this.BeginInvoke(new Action(() =>
            {
                MsgCenter.Broadcast(MsgDef.RUNNING, this, null);
            }));
        }

        private void beginBroadcastPaused()
        {
            this.BeginInvoke(new Action(() =>
            {
                MsgCenter.Broadcast(MsgDef.PAUSED, this, null);
            }));
        }

        #endregion


        #region Hot Keys

        private void setupHotKeys()
        {
            HookHotKeyMgr.Instance.GetRunKey().OnRun = RunBroker.Instance.StartWork;
            HookHotKeyMgr.Instance.GetRunKey().OnPause = Executor.Instance.PauseResume;
            HookHotKeyMgr.Instance.GetRunKey().OnStep = Executor.Instance.SingleStep;
            HookHotKeyMgr.Instance.GetRunKey().OnAbort = Executor.Instance.Abort;
            HookHotKeyMgr.Instance.GetRunKey().OnStop = Executor.Instance.Stop;
        }

        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.Up || keyData == Keys.Down
                    || keyData == Keys.Left || keyData == Keys.Right
                    || keyData == Keys.Home || keyData == Keys.End
                    || keyData == (Keys.Up | Keys.Control) || keyData == (Keys.Down | Keys.Control)
                    || keyData == (Keys.Left | Keys.Control) || keyData == (Keys.Right | Keys.Control)
                    || keyData == (Keys.Home | Keys.Control) || keyData == (Keys.End | Keys.Control))
                {
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        #endregion


        #region Manual Buttons

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new LoginForm().ShowDialog(this);
        }

        private void btnReset_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                LoadingMetro loadingForm = new LoadingMetro();
                MsgCenter.Broadcast(MsgDef.BUSY, this, null);
                Task.Factory.StartNew(() =>
                {
                    Machine.Instance.InitAll();
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        MsgCenter.Broadcast(MsgDef.IDLE, this, null);
                        MsgCenter.Broadcast(MachineMsg.INIT_VISION, this);
                    }));
                });
                loadingForm.ShowDialog();
            }
            else if(e.Button == MouseButtons.Right)
            {
                this.cmsInit.Show(btnReset, 0, 0);
            }
        }

        private async void cmsInitItem_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.BUSY, this, null);
            await Task.Factory.StartNew(() =>
            {
                if (sender.Equals(cmsiHome))
                {
                    Machine.Instance.MoveHome();
                }
                else if (sender.Equals(cmsiInitMotion))
                {
                    AlarmServer.Instance.MachineInitDone = false;
                    Machine.Instance.InitMotion();
                    AlarmServer.Instance.MachineInitDone = true;
                }
                else if (sender.Equals(cmsiInitVistion))
                {
                    AlarmServer.Instance.MachineInitDone = false;
                    Machine.Instance.InitVision();
                    AlarmServer.Instance.MachineInitDone = true;
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        MsgCenter.Broadcast(MachineMsg.INIT_VISION, this);
                    }));
                }
                else if (sender.Equals(cmsiInitSensors))
                {
                    AlarmServer.Instance.MachineInitDone = false;
                    Machine.Instance.InitSensors();
                    AlarmServer.Instance.MachineInitDone = true;
                }
            });
            MsgCenter.Broadcast(MsgDef.IDLE, this, null);
        }

        private void btnJog_Click(object sender, EventArgs e)
        {
            FormMgr.GetForm<JogMetro>().Show(this);
        }

        private async void btnPurge_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.BUSY, this, null);
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.Valve1.DoPurgeAndPrime();
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    if (Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
                    {
                        Machine.Instance.Robot.MovePosABAndReply(new PointD());
                    }
                    Machine.Instance.Valve2.DoPurgeAndPrime();
                }
            });
            MsgCenter.Broadcast(MsgDef.IDLE, this, null);
        }

        private async void btnPrime_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.BUSY, this, null);
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.Valve1.DoPrime();
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    if (Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
                    {
                        Machine.Instance.Robot.MovePosABAndReply(new PointD());
                    }
                    Machine.Instance.Valve2.DoPrime();
                }
            });
            MsgCenter.Broadcast(MsgDef.IDLE, this, null);
        }

        private async void btnScale_Click(object sender, EventArgs e)
        {
            MsgCenter.Broadcast(MsgDef.BUSY, this, null);
            await Task.Factory.StartNew(() =>
            {
                bool scaleSts = true;
                if (Machine.Instance.Scale.Scalable.CommunicationOK == ComCommunicationSts.ERROR)
                {
                    scaleSts = false;
                }
                else
                {
                    scaleSts = Machine.Instance.Scale.Scalable.CommunicationTest();
                }
                if (!scaleSts)
                {
                    //string msg = "Scale is disconnect,Please check scale!";
                    string msg = "天平连接失败，请检查!";
                    MetroSetMessageBox.Show(this, msg);
                    return;
                }
                Result ret = Result.OK;
                ret = Machine.Instance.Valve1.AutoRunWeighingWithPurge();
                if (!ret.IsOk)
                {
                    return;
                }

                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    ret = Machine.Instance.Valve2.AutoRunWeighingWithPurge();
                    if (!ret.IsOk)
                    {
                        return;
                    }
                }
            });
            MsgCenter.Broadcast(MsgDef.IDLE, this, null);
        }

        private void btnHeater_Click(object sender, EventArgs e)
        {
            bool b = !DoType.胶枪加热1.Sts().Value;
            DoType.胶枪加热1.Set(b);
            if (b)
            {
                btnHeaterIO.ImageAlpha = 1;
            }
            else
            {
                btnHeaterIO.ImageAlpha = 0.5F;
            }
        }

        private void btnScanner_Click(object sender, EventArgs e)
        {
            this.showSensorReadForm(0);
        }

        private void btnLaser_Click(object sender, EventArgs e)
        {
            this.showSensorReadForm(1);
        }

        private void showSensorReadForm(int type)
        {
            if (this.sensorsForm == null)
            {
                this.sensorsForm = new SensorReadMetro(type);
            }
            else
            {
                if (this.sensorsForm.Visible)
                {
                    return;
                }
                this.sensorsForm = new SensorReadMetro(type);
            }
            this.sensorsForm.TopMost = true;
            this.sensorsForm.Show(this);
        }

        private void btnGlueMgr_Click(object sender, EventArgs e)
        {
            FormMgr.Show<GlueManageForm>(this);
        }

        private void btnConveyorState_Click(object sender, EventArgs e)
        {
            FormMgr.Show<ConveyorControlForm>(this);
        }

        private void btnBoardIn_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                this.manualBoardEnter(1);
            }
            else
            {
                this.manualBoardEnter(0);
            }
        }

        private void btnBoardOut_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                this.maunalBoardExit(1);
            }
            else
            {
                this.maunalBoardExit(0);
            }
        }

        private void manualBoardEnter(int ConveyorId)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //气缸松开
                ConveyorController.Instance.SetWorkingSiteStopper(ConveyorId, false);
                ConveyorController.Instance.SetWorkingSiteLift(ConveyorId, false);

                //如果工作站电眼或出板电眼感应有板，则反转一段距离再进入
                if (ConveyorController.Instance.SingleSiteArriveSensor(ConveyorId).Is(StsType.High)
                        || ConveyorController.Instance.SingleSiteExitSensor(ConveyorId).Is(StsType.High))
                {
                    ConveyorController.Instance.ConveyorBack(ConveyorId);

                    //回转计时开始
                    DateTime startBackTime = DateTime.Now;
                    while (ConveyorController.Instance.SingleSiteEnterSensor(ConveyorId).Is(StsType.Low)
                            || ConveyorController.Instance.SingleSiteEnterSensor(ConveyorId).Is(StsType.IsFalling))
                    {
                        TimeSpan timeSpan = DateTime.Now - startBackTime;
                        if (timeSpan >= TimeSpan.FromSeconds(10))
                        {
                            MetroSetMessageBox.Show(this, "可能发生卡板");
                            goto end;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                    ConveyorController.Instance.ConveyorAbortStop(ConveyorId);

                    System.Threading.Thread.Sleep(2);
                }

                //如果没有感应到有板，则直接正转进入
                ConveyorController.Instance.ConveyorForward(ConveyorId);
                ConveyorController.Instance.SetWorkingSiteStopper(ConveyorId, true);

                DateTime startForwardTime = DateTime.Now;
                while (!ConveyorController.Instance.SingleSiteArriveSensor(ConveyorId).Is(StsType.High))
                {
                    TimeSpan timeSpan = DateTime.Now - startForwardTime;
                    if (timeSpan >= TimeSpan.FromSeconds(10))
                    {
                        MetroSetMessageBox.Show(this, "可能发生卡板");
                        goto end;
                    }
                    System.Threading.Thread.Sleep(1);
                }

                //电眼感应到位延时
                System.Threading.Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(0).WorkingSitePrm.BoardArrivedDelay);

                ConveyorController.Instance.SetWorkingSiteLift(ConveyorId, true);
                end:
                ConveyorController.Instance.ConveyorAbortStop(ConveyorId);
            }));
        }

        private void maunalBoardExit(int ConveyorId)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //气缸松开
                ConveyorController.Instance.SetWorkingSiteStopper(ConveyorId, false);
                ConveyorController.Instance.SetWorkingSiteLift(ConveyorId, false);
                System.Threading.Thread.Sleep(1000);

                ConveyorController.Instance.ConveyorForward(ConveyorId);

                DateTime startTime = DateTime.Now;
                while (!ConveyorController.Instance.SingleSiteExitSensor(ConveyorId).Is(StsType.High))
                {
                    TimeSpan timeSpan = DateTime.Now - startTime;
                    if (timeSpan >= TimeSpan.FromSeconds(20))
                    {
                        goto end;
                    }
                    System.Threading.Thread.Sleep(1);
                }
                end:
                ConveyorController.Instance.ConveyorAbortStop(ConveyorId);
            }));
        }

        private void btnVisionBar_Click(object sender, EventArgs e)
        {
            if (this.btnVisionBar.Tag == null)
            {
                this.btnVisionBar.Tag = false;
            }
            bool b = (bool)this.btnVisionBar.Tag;
            this.panelVisonBar.Visible = !b;
            this.btnVisionBar.Tag = !b;
        }

        private void btnUpdateCameraView_Click(object sender, EventArgs e)
        {
            this.panelCamera.Controls.Clear();
            this.panelCamera.Controls.Add(this.cameraView1);
        }

        #endregion


        #region Run Buttons

        private void btnStart_Click(object sender, EventArgs e)
        {
            RunBroker.Instance.StartWork();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Executor.Instance.PauseResume();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Executor.Instance.Stop();
        }



        #endregion

        #region  ACCESS 权限
        public int Key { get; set; } = (int)ContainerKeys.MainMetro;
        public Control Control => this;
       

        public List<AccessObj> UserAccessControls { get; set; } = new List<AccessObj>();

        public ContainerAccess CurrContainerAccess { get; set; } = new ContainerAccess();
        

        public ContainerAccess DefaultContainerAccess { get; set; } = new ContainerAccess();


        public void SetDefaultAccess()
        {           
             this.DefaultContainerAccess = new ContainerAccess();
            
            //左边
            string containerName = this.GetType().Name;
            this.DefaultContainerAccess.ContainerName = containerName;
            this.DefaultContainerAccess.ContainerAccessDescription = "主界面";

            this.DefaultContainerAccess.ControlAccessList.Clear();
            this.DefaultContainerAccess.AddContainerOperator();

            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "初始化", this.btnReset.Name, AccessEnums.RoleEnums.Operator));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "进入手动界面", this.btnJog.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "清洗阀", this.btnPurge.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "排胶", this.btnPrime.Name, AccessEnums.RoleEnums.Operator));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "称重", this.btnScale.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "加热器设置", this.btnHeaterIO.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "条码枪", this.btnScanner.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "测高", this.btnLaser.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "胶水管控", this.btnGlueMgr.Name, AccessEnums.RoleEnums.Technician));

            //上面
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "进入主页", this.btnJobs.Name, AccessEnums.RoleEnums.Operator));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "进入系统页面", this.btnSystem.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "进入设置页面", this.btnSetup.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "进入配方页面", this.btnRecipes.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "进入报警页面", this.btnAlarms.Name, AccessEnums.RoleEnums.Operator));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "进入数据统计页面", this.btnDatalog.Name, AccessEnums.RoleEnums.Technician));
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "进入帮助页面", this.btnHelp.Name, AccessEnums.RoleEnums.Operator));

            //右边
            //btnVisionBar
            this.DefaultContainerAccess.ControlAccessList.Add(new ControlAccess(containerName, "光源设置", this.btnVisionBar.Name, AccessEnums.RoleEnums.Technician)); 
            ControlAccess conveyorAccess = new ControlAccess(containerName, "轨道操作");
            conveyorAccess.Add(new List<string> { this.btnConveyorState.Name,this.btnBoardIn.Name , this.btnBoardOut.Name}, AccessEnums.RoleEnums.Technician);
            this.DefaultContainerAccess.ControlAccessList.Add(conveyorAccess);

            AccessControlMgr.Instance.AddContainerAccess(this.DefaultContainerAccess);
            
           
        }
        public void SetupUserAccessControl()
        {
            
        }
        public void LoadAccess()
        {
            this.accessExecutor.LoadAccess();           
        }

        public void UpdateUIByAccess()
        {
            this.accessExecutor.UpdateUIByAccess();
        }

        public void UpdateAllUIAccess()
        {
            foreach (var item in AccessControlMgr.Instance.accessControls)
            {
                item.UpdateUIByAccess();
            }
        }
                      
        #endregion


    }
}
