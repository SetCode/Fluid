using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using MetroSet_UI.Controls;
using Anda.Fluid.Domain.FluProgram;
using DrawingPanel.Msg;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.App.Common;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.App.EditMark;
using Anda.Fluid.Domain;
using Anda.Fluid.App.Settings;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.App.EditInspection;
using Anda.Fluid.App.CADImport;
using System.IO;
using Anda.Fluid.Infrastructure.International.Access;
using static Anda.Fluid.Domain.AccessControl.User.PageAccessEnums;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageJobsProgram : MetroSetUserControl, IMsgReceiver, IMsgSender, IControlUpdating, IMultiDrawCmdEditable,IAccessControllable
    {
        #region Internal Vars

        private const string TAG = "ProgramControl";
        private const string ORIGIN = "Origin";
        // 点胶程序
        private FluidProgram program;
        // 当前点胶程序路径
        private string currProgramPath;
        // 当前轨迹的节点，多节点轨迹才使用，用于切换下一步轨迹
        private int curCmdLineNode = 0;
        //
        private int curCmdLineNodeCount = 0;
        //
        private string msgBatchUpdateTip1 = "没有点胶轨迹被选中.";
        //
        private string msgBatchUpdateTip2 = "没有轨迹被选中.";
        // 轨道1图标
        private Image imgConveyor1 = Properties.Resources.misc_numbers_1_32px;
        // 轨道2图标
        private Image imgConveyor2 = Properties.Resources.misc_numbers_2_32px;
        // 继续图标
        private Image imgResume = Properties.Resources.media_seek_forward_3_32px;
        // 暂停图标
        private Image imgPause = Properties.Resources.media_playback_pause_3_32px;

        //权限执行
        private AccessExecutor accessExecutor;
        #endregion


        #region Constructor

        public PageJobsProgram()
        {
            InitializeComponent();
            this.ShowBorder = false;
            // 注册Pattern树相关事件
            this.treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
            this.treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            this.contextMenuStrip1.Items.Add("Delete");
            this.contextMenuStrip1.ItemClicked += ToolStripMenuItem_Click;
            // 注册脚本编辑器事件
            scriptEditor1.OnEditCmdLineEvent += ScriptEditor1_OnEditCmdLineEvent;
            scriptEditor1.OnCommandsModuleLoadedEvent += ScriptEditor1_OnCommandsModuleLoadedEvent;
            // 初始化菜单
            this.initMenu();
            // 初始化程序执行器
            this.initExecutor();
            // 注册绘图消息接受者
            //TODO
            //DrawingMsgCenter.Instance.RegisterReceiver(this.canvasControll1);
            DrawingMsgCenter.Instance.RegisterSingleDrawEditor(this.scriptEditor1);
            DrawingMsgCenter.Instance.RegisterMultiDrawEditor(this);
            // 注册界面刷新
            ControlUpatingMgr.Add(this);
            //this.ReadLanguageResources();
            //权限加载
            this.accessExecutor = new AccessExecutor(this);
            this.LoadAccess();
            AccessControlMgr.Instance.Register(this);
        }

        #endregion


        #region Properties

        public CommandsModule CurrCommandsModule => this.scriptEditor1.CurrCommandsModule;

        public FluidProgram CurrProgram => this.program;

        
        #endregion


        #region Menu

        private void initMenu()
        {
            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀 || Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀
                || Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀 || Machine.Instance.Valve2.ValveSeries == ValveSeries.齿轮泵阀)
            {
                this.btnSvSpeed.Enabled = true;
            }

            this.cmbRunMode.Items.Add(ValveRunMode.Wet);
            this.cmbRunMode.Items.Add(ValveRunMode.Dry);
            this.cmbRunMode.Items.Add(ValveRunMode.Look);
            this.cmbRunMode.Items.Add(ValveRunMode.AdjustLine);
            this.cmbRunMode.Items.Add(ValveRunMode.InspectRect);
            this.cmbRunMode.Items.Add(ValveRunMode.InspectDot);
            this.cmbRunMode.SelectedIndexChanged += TsbFluidMode_SelectedIndexChanged;
            this.cmbRunMode.SelectedIndex = (int)Machine.Instance.Valve1.RunMode;

            this.cmbConveyor.Items.Add("轨道1");
            this.cmbConveyor.Items.Add("轨道2");
            this.cmbConveyor.SelectedIndexChanged += cmbConveyor_SelectedIndexChanged;
            this.cmbConveyor.SelectedIndex = 0;

        }

        private void TsbFluidMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbRunMode.SelectedIndex < 0)
            {
                return;
            }
            ValveRunMode mode = Machine.Instance.Valve1.RunMode;
            Machine.Instance.Valve1.RunMode = (ValveRunMode)this.cmbRunMode.SelectedIndex;
            if (mode != Machine.Instance.Valve1.RunMode)
            {
                string msg = string.Format("valveRunMode oldValue:{0}->newMode:{1}", mode.ToString(), Machine.Instance.Valve1.RunMode.ToString());
                Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.SETTING, this.GetType().Name, msg);
            }
        }

        private void initExecutor()
        {
            Executor.Instance.OnProgramRunning += () =>
            {
                enableEditMenu(false);
            };
            Executor.Instance.OnProgramAborted += () =>
            {
                enableEditMenu(true);
            };
            Executor.Instance.OnProgramDone += () =>
            {
                enableEditMenu(true);
            };
            Executor.Instance.OnWorkStateChanged += workState =>
            {
                switch (workState)
                {
                    case Executor.WorkState.Idle:
                        enableEditMenu(true);
                        break;
                    case Executor.WorkState.Preparing:
                        enableEditMenu(false);
                        break;
                    case Executor.WorkState.WaitingForBoard:
                        enableEditMenu(false);
                        break;
                    case Executor.WorkState.Programing:
                        enableEditMenu(false);
                        break;
                    case Executor.WorkState.Stopping:
                        enableEditMenu(false);
                        break;
                }
            };
            Executor.Instance.OnWorkRunning += () =>
            {
                enableEditMenu(false);
            };
            Executor.Instance.OnWorkDone += () =>
            {
                enableEditMenu(true);
            };
            Executor.Instance.OnWorkStop += () =>
            {
                enableEditMenu(true);
            };

            Executor.Instance.OnStateChanged += (oldState, newState) =>
            {
                if (!this.IsHandleCreated)
                {
                    return;
                }
                Log.Print("program running state changed, oldState=" + oldState + ", newState=" + newState);
                try
                {
                    // 根据程序执行状态的变化更新按钮状态
                    this.Invoke(new MethodInvoker(() =>
                    {
                        labelRunningState.Text = newState.ToString();
                        switch (newState)
                        {
                            case Executor.ProgramOuterState.IDLE:
                                btnStart.Enabled = true;
                                btnStep.Enabled = true;
                                btnPause.Enabled = false;
                                btnAbort.Enabled = false;
                                btnPause.Image = this.imgPause;
                                scriptEditor1.cancelMark();
                                break;
                            case Executor.ProgramOuterState.RUNNING:
                                btnStart.Enabled = false;
                                btnStep.Enabled = false;
                                btnPause.Enabled = true;
                                btnAbort.Enabled = true;
                                btnPause.Image = this.imgPause;
                                scriptEditor1.cancelMark();
                                break;
                            case Executor.ProgramOuterState.PAUSING:
                                btnStart.Enabled = false;
                                btnStep.Enabled = false;
                                btnPause.Enabled = false;
                                btnAbort.Enabled = false;
                                scriptEditor1.cancelMark();
                                break;
                            case Executor.ProgramOuterState.PAUSED:
                                btnStart.Enabled = false;
                                btnStep.Enabled = true;
                                btnPause.Enabled = true;
                                btnAbort.Enabled = true;
                                btnPause.Image = this.imgResume;
                                if (Executor.Instance.CurrPausedCmd != null)
                                {
                                    CommandsModule module = Executor.Instance.CurrPausedCmd.RunnableModule.CommandsModule;
                                    int index = module.FindCmdLineIndex(Executor.Instance.CurrPausedCmd.CmdLine);
                                    scriptEditor1.MarkRunningPausedPosition(module, index);
                                }
                                break;
                            case Executor.ProgramOuterState.ABORTING:
                                btnStart.Enabled = false;
                                btnStep.Enabled = false;
                                btnPause.Enabled = false;
                                btnAbort.Enabled = false;
                                scriptEditor1.cancelMark();
                                break;
                            case Executor.ProgramOuterState.ABORTED:
                                btnStart.Enabled = true;
                                btnStep.Enabled = true;
                                btnPause.Enabled = false;
                                btnAbort.Enabled = false;
                                btnPause.Image = this.imgPause;
                                scriptEditor1.cancelMark();
                                break;
                        }
                    }));
                }
                catch (Exception)
                {
                }
            };
        }

        private void enableEditMenu(bool b)
        {
            return;
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                //if (b)
                //{
                //    Role role = RoleMgr.Instance.CurrentRole;
                //    treeView1.Enabled = role.ProgramFormAccess.CanUseProgramList;
                //    scriptEditor1.SetEnable(role.ProgramFormAccess.CanUseGluePathCmd);

                //    btnNew.Enabled = role.ProgramFormAccess.CanUseProgramFileOperate;
                //    btnCopy.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnCut.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnPaste.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnConfig.Enabled = role.ProgramFormAccess.CanEnterProgramSettingForm;
                //    cmbRunMode.Enabled = role.MainFormAccess.CanUseCbxRunMode;
                //    btnLoc.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnBatch.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;

                //    btnDisable.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnComments.Enabled = role.ProgramFormAccess.CanUseOtherCmd;
                //    btnMark.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnAsvMark.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnBadMark.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnNozzleCheck.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnHeight.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnTimer.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnNewPattern.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnDoPattern.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnDoMultipass.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnPassBlock.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnLoopPass.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnMultipassTimer.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnMultipassArray.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnPurge.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnSvSpeed.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //    btnMove.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                //}
                //else
                //{
                    treeView1.Enabled = b;
                    scriptEditor1.SetEnable(b);

                    btnNew.Enabled = b;
                    btnCopy.Enabled = b;
                    btnCut.Enabled = b;
                    btnPaste.Enabled = b;
                    btnConfig.Enabled = b;
                    cmbRunMode.Enabled = b;
                    btnLoc.Enabled = b;
                    btnBatch.Enabled = b;

                    btnDisable.Enabled = b;
                    btnComments.Enabled = b;
                    btnMark.Enabled = b;
                    btnAsvMark.Enabled = b;
                    btnBadMark.Enabled = b;
                    btnNozzleCheck.Enabled = b;
                    btnHeight.Enabled = b;
                    btnTimer.Enabled = b;
                    btnNewPattern.Enabled = b;
                    btnDoPattern.Enabled = b;
                    btnDoMultipass.Enabled = b;
                    btnPassBlock.Enabled = b;
                    btnLoopPass.Enabled = b;
                    btnMultipassTimer.Enabled = b;
                    btnMultipassArray.Enabled = b;
                    btnPurge.Enabled = b;
                    btnSvSpeed.Enabled = b;
                    btnMove.Enabled = b;

                //}
                btnOpen.Enabled = b;
                btnSave.Enabled = b;
                btnInspect.Enabled = b;
                btnPrevCmdPt.Enabled = b;
                btnNextCmdPt.Enabled = b;
                //btnConveyorSelect.Enabled = b;
                btnConveyorWidth.Enabled = b;
                txtFindComp.Enabled = b;
            }));
        }

        private void enableRunMenu(bool b)
        {
            btnStart.Enabled = b;
            btnStep.Enabled = b;
            btnPause.Enabled = b;
            btnAbort.Enabled = b;
        }

        private void enableAllMenu(bool b)
        {
            this.enableEditMenu(b);
            this.enableRunMenu(b);
        }

        private void cbxSimulation_CheckedChanged(object sender)
        {
            Machine.Instance.Robot.IsSimulation = this.cbxSimulation.Checked;
        }

        private void showMessage(Color color, string text)
        {
            this.BeginInvoke(new MethodInvoker(
                () =>
                {
                    this.lblMessage.ForeColor = color;
                    this.lblMessage.Text = text;
                }));
        }

        #endregion


        #region Pattern Tree

        /// <summary>
        /// 刷新程序结构显示
        /// </summary>
        private void refreshProgramStructure()
        {
            treeView1.Nodes.Clear();
            if (program == null)
            {
                return;
            }
            // 根节点Program
            var programNode = treeView1.Nodes.Add(program.Name);
            programNode.Tag = program;
            // Workpiece
            var workpieceNode = programNode.Nodes.Add(program.Workpiece.Name);
            workpieceNode.Tag = program.Workpiece;
            workpieceNode.Nodes.Add(ORIGIN);

            // 用户创建的Pattern
            foreach (Pattern p in program.Patterns)
            {
                var patternNode = programNode.Nodes.Add(p.Name);
                patternNode.Tag = p;
                patternNode.Nodes.Add(ORIGIN);
            }
            treeView1.ExpandAll();
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text == ORIGIN)
            {
                return;
            }

            // 左键单击节点加载对应命令列表
            if (e.Button == MouseButtons.Left)
            {
                loadCommandsModule(e.Node.Tag as CommandsModule);

                //切换轴坐标显示控件的原点
                CommandsModule commandsModule = (CommandsModule)e.Node.Tag;
                if (commandsModule is Workpiece)
                {
                    Workpiece workpiece = commandsModule as Workpiece;
                    //TODO this.positionVControl1.SetRelativeOrigin(workpiece.GetOriginPos());
                }
                else if (commandsModule is Pattern)
                {
                    Pattern pattern = commandsModule as Pattern;
                    //TODO this.positionVControl1.SetRelativeOrigin(pattern.GetOriginPos());
                }
            }
            // 右键点击Pattern节点弹出删除菜单
            else if (e.Button == MouseButtons.Right)
            {
                if (!(e.Node.Tag is FluidProgram) && !(e.Node.Tag is Workpiece))
                {
                    treeView1.SelectedNode = e.Node;
                    e.Node.ContextMenuStrip = contextMenuStrip1;
                    contextMenuStrip1.Show(e.X, e.Y);
                }
            }
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Node.Text == ORIGIN)
                {
                    Pattern p = e.Node.Parent.Tag as Pattern;
                    this.CmdButtonClicked?.Invoke(null, p, CmdButtonType.EditOrigin);
                    //if (new EditPattern(p, program.Workpiece).ShowDialog() == DialogResult.OK)
                    //{
                    //    Log.Print("change pattern origin to : " + p.Origin);
                    //    // 移动到新原点位置
                    //    Machine.Instance.Robot.MoveSafeZ();
                    //    Machine.Instance.Robot.ManualMovePosXY(p.GetOriginPos());
                    //}
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            for (int i = 0; i < this.treeView1.Nodes.Count; i++)
            {
                this.treeView1.Nodes[i].BackColor = Color.FromArgb(30, 30, 30);
                for (int j = 0; j < this.treeView1.Nodes[i].Nodes.Count; j++)
                {
                    this.treeView1.Nodes[i].Nodes[j].BackColor = Color.FromArgb(30, 30, 30);
                    for (int k = 0; k < this.treeView1.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        this.treeView1.Nodes[i].Nodes[j].Nodes[k].BackColor = Color.FromArgb(30, 30, 30);
                    }
                }
            }
            this.treeView1.SelectedNode.BackColor = Color.SkyBlue;
        }

        private void ToolStripMenuItem_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemText = e.ClickedItem.Text;
            if (itemText == "Delete")
            {
                Pattern pattern = treeView1.SelectedNode.Tag as Pattern;
                program.RemovePattern(pattern.Name);
                treeView1.SelectedNode.Remove();
                if (scriptEditor1.CurrCommandsModule.Name == pattern.Name)
                {
                    loadCommandsModule(program.Workpiece);
                }
                checkGrammar();
            }

        }

        #endregion


        #region Grammer & Commonds

        /// <summary>
        /// 加载命令模块
        /// </summary>
        /// <param name="module"></param>
        private void loadCommandsModule(CommandsModule module)
        {
            scriptEditor1.LoadData(module);
            labelModuleName.Text = scriptEditor1.CurrCommandsModule.Name;
        }

        /// <summary>
        /// 检查程序语法
        /// </summary>
        private void checkGrammar()
        {
            Log.Print("begin to check grammar...");
            if (program == null)
            {
                Log.Print("Refused checking grammar : program is null.");
                return;
            }
            Result result = program.Parse();
            this.onCheckGrammerFinished(result);
        }

        private void onCheckGrammerFinished(Result result)
        {
            if (result.IsOk)
            {
                Log.Print("check grammar result : OK");
                labelGrammarCheckInfo.ForeColor = Color.Green;
                labelGrammarCheckInfo.Text = "no errors.";
                scriptEditor1.cancelMark();
            }
            else
            {
                labelGrammarCheckInfo.ForeColor = Color.Red;
                if (result.Param == null)
                {
                    labelGrammarCheckInfo.Text = result.ErrMsg;
                }
                else
                {
                    CmdLine cmdLine = result.Param as CmdLine;
                    int index = cmdLine.CommandsModule.FindCmdLineIndex(cmdLine);
                    labelGrammarCheckInfo.Text = cmdLine.CommandsModule.Name + " : " + result.ErrMsg + " line " + (index + 1);
                    scriptEditor1.MarkCompileError(cmdLine.CommandsModule, index);
                }
                Log.Print("check grammar result : " + labelGrammarCheckInfo.Text);
            }
        }

        #endregion


        #region Fluid Program

        private void PageJobsProgram_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 加载最新程序
        /// </summary>
        public void LoadFluProgram()
        {
            if (FluidProgram.CurrentFilePath != this.currProgramPath)
            {
                this.onLoadProgram(FluidProgram.CurrentFilePath);
            }
        }

        /// <summary>
        /// 提示保存程序：新建、加载、退出
        /// </summary>
        public void SaveProgramIfChanged()
        {
            if (FluidProgram.Current == null)
            {
                return;
            }
            if (!FluidProgram.Current.HasChanged)
            {
                return;
            }
            if (MetroSetMessageBox.Show(this, string.Format("程序:[{0}] 被修改, 是否保存?", FluidProgram.Current.Name), "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LoadFlu.Instance.SaveFile(FluidProgram.Current);
            }
        }

        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="msgName"></param>
        /// <param name="sender"></param>
        /// <param name="args"></param> 
        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            // 添加Pattern
            if (msgName == Constants.MSG_ADD_PATTERN)
            {
                onAddPattern(args[0] as Pattern);
                FluidProgram.Current.HasChanged = true;
                FluidProgram.Current.RuntimeSettings.FlyOffsetIsValid = false;
            }
            // 完成添加单个点胶命令
            else if (msgName == Constants.MSG_FINISH_ADDING_CMD_LINE)
            {
                scriptEditor1.CurrCommandsModule.IsModified = true;
                onFinishAddingCmdLines(args);
                FluidProgram.Current.HasChanged = true;
                scriptEditor1.CurrCommandsModule.IsModified = false;
            }
            // 完成添加多个点胶命令
            else if (msgName == Constants.MSG_FINISH_ADDING_CMD_LINES)
            {
                scriptEditor1.CurrCommandsModule.IsModified = true;

                //如果是来自批量平移旋转窗体的指令就要设置为Copy的指令
                if (sender is BatchUpdateCmdLineForm)
                {
                    onFinishAddingCmdLineList(args, true);
                }
                else
                {
                    onFinishAddingCmdLineList(args, false);
                }
                FluidProgram.Current.HasChanged = true;
                scriptEditor1.CurrCommandsModule.IsModified = false;
            }
            // 完成插入点胶命令
            else if (msgName == Constants.MSG_FINISH_INSERTING_CMD_LINE)
            {
                scriptEditor1.CurrCommandsModule.IsModified = true;
                onFinishInsertingCmdLines(args);
                FluidProgram.Current.HasChanged = true;
                scriptEditor1.CurrCommandsModule.IsModified = false;
            }
            // 完成删除命令
            else if (msgName == Constants.MSG_FINISH_DELETING_CMD_LINE)
            {
                onFinishDeletingCmdLines(args);
                FluidProgram.Current.HasChanged = true;
            }
            // 完成编辑点胶命令
            else if (msgName == Constants.MSG_FINISH_EDITING_CMD_LINE)
            {
                scriptEditor1.CurrCommandsModule.IsModified = true;
                scriptEditor1.OnFinishEditingCmdLine(args[0] as CmdLine);
                checkGrammar();
                FluidProgram.Current.HasChanged = true;
                scriptEditor1.CurrCommandsModule.IsModified = false;
            }
            // 完成Pattern原点编辑
            else if (msgName == Constants.MSG_FINISH_EDITING_PATTREN_ORIGIN)
            {
                scriptEditor1.CurrCommandsModule.IsModified = true;
                checkGrammar();
                FluidProgram.Current.HasChanged = true;
                scriptEditor1.CurrCommandsModule.IsModified = false;
            }
            // 系统预定义坐标变量有变化
            else if (msgName == Constants.MSG_SYS_POSITIONS_DEFS_CHANGED)
            {
                checkGrammar();
                FluidProgram.Current.HasChanged = true;
            }
            // 新建程序
            else if (msgName == Constants.MSG_NEW_PROGRAM)
            {
                onCreateProgram(args[0] as string, (double)args[1], (double)args[2]);
                FluidProgram.Current.RuntimeSettings.FlyOffsetIsValid = false;
                FluidProgram.Current.HasChanged = true;
            }
            // 加载程序
            else if (msgName == Constants.MSG_LOAD_PROGRAM)
            {
                onLoadProgram(args[0] as string);
            }
            // 保存程序
            else if (msgName == Constants.MSG_SAVE_PROGRAM)
            {
                onSaveProgram();
                FluidProgram.Current.HasChanged = false;
            }
            else if (msgName == MsgType.MSG_LINEEDITLOOK_SHOW)
            {
                lineEditFormshow(sender, args);
            }
            else if (msgName == MsgConstants.SWITCH_USER || msgName == MsgConstants.MODIFY_ACCESS)
            {
                string curState = labelRunningState.Text;
                if (curState == Executor.ProgramOuterState.IDLE.ToString()
                || curState == Executor.ProgramOuterState.ABORTED.ToString())
                {
                    enableEditMenu(true);
                }
                else
                {
                    enableEditMenu(false);
                }
            }
            //示教轨道2作业原点
            else if (msgName == Constants.MSG_TEACH_CONVEYOR2_ORIGIN)
            {
                FluidProgram.Current.Conveyor2OriginOffset = new PointD();
                FluidProgram.Current.Conveyor2OriginOffset.X = (double)args[0] - FluidProgram.Current.Workpiece.OriginPos.X;
                FluidProgram.Current.Conveyor2OriginOffset.Y = (double)args[1] - FluidProgram.Current.Workpiece.OriginPos.Y;
            }
            //阀组变换，切换螺杆阀变速指令按钮的显示enable
            else if (msgName == MachineMsg.SETUP_VALVE)
            {
                if ((ValveSeries)args[0] == ValveSeries.螺杆阀 || (ValveSeries)args[1] == ValveSeries.螺杆阀
                    || (ValveSeries)args[0] == ValveSeries.齿轮泵阀 || (ValveSeries)args[1] == ValveSeries.齿轮泵阀)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.btnSvSpeed.Enabled = true;
                    }));
                }
                else
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.btnSvSpeed.Enabled = false;
                    }));
                }
            }
            else if (msgName == LngMsg.SWITCH_LNG)
            {
                //TODO this.ReadLanguageResources();
                this.scriptEditor1.ReadLanguageResources();
            }
        }

        /// <summary>
        /// 添加pattern
        /// </summary>
        /// <param name="pattern"></param>
        private void onAddPattern(Pattern pattern)
        {
            Result result = program.AddPattern(pattern);
            if (result.IsOk)
            {
                loadCommandsModule(pattern);
                refreshProgramStructure();
            }
            else
            {
                this.showMessage(Color.Red, result.ErrMsg);
            }
        }

        /// <summary>
        /// 添加单个命令完成
        /// </summary>
        private void onFinishAddingCmdLines(object[] args)
        {
            List<CmdLine> cmdLineList = new List<CmdLine>();
            foreach (object obj in args)
            {
                cmdLineList.Add(obj as CmdLine);
            }
            scriptEditor1.OnFinishAddingCmdLines(cmdLineList, false);
            checkGrammar();
        }

        /// <summary>
        /// 添加多个命令完成
        /// </summary>
        private void onFinishAddingCmdLineList(object[] args, bool isCopy)
        {
            List<CmdLine> cmdLineList = new List<CmdLine>();
            cmdLineList = args[0] as List<CmdLine>;
            scriptEditor1.OnFinishAddingCmdLines(cmdLineList, isCopy);
            checkGrammar();
        }

        /// <summary>
        /// 插入命令完成
        /// </summary>
        /// <param name="args"></param>
        private void onFinishInsertingCmdLines(object[] args)
        {
            int index = (int)args[0];
            List<CmdLine> cmdLineList = args[1] as List<CmdLine>;
            scriptEditor1.OnFinishInsertingCmdLines(index, cmdLineList);
            checkGrammar();
        }

        /// <summary>
        /// 删除命令行完成
        /// </summary>
        /// <param name="args"></param>
        private void onFinishDeletingCmdLines(object[] args)
        {
            int index = (int)args[0];
            int count = (int)args[1];
            scriptEditor1.OnFinishDeletingCmdLines(index, count);
            checkGrammar();
        }

        /// <summary>
        /// 创建程序
        /// </summary>
        /// <param name="programName">程序名称</param>
        /// <param name="workpiecePosX">workpiece原点坐标X</param>
        /// <param name="workpiecePosY">workpiece原点坐标Y</param>
        private void onCreateProgram(string programName, double workpiecePosX, double workpiecePosY)
        {
            program = FluidProgram.Create(programName, workpiecePosX, workpiecePosY);
            program.RuntimeSettings.StandardBoardHeight = Machine.Instance.Robot.CalibPrm.StandardHeight;
            loadCommandsModule(program.Workpiece);
            checkGrammar();
            refreshProgramStructure();
            FluidProgram.CurrentFilePath = string.Empty;
            this.currProgramPath = string.Empty;
            program.InitHardware();

            this.showMessage(Color.Orange, "新建的程序等待保存...");
        }

        /// <summary>
        /// 加载程序
        /// </summary>
        /// <param name="programPath">程序名称</param>
        private void onLoadProgram(string programPath)
        {
            // 程序已加载
            if (program != null && this.currProgramPath == programPath)
            {
                this.showMessage(Color.Orange, "程序已加载.");
                return;
            }
            // 程序为空
            if (FluidProgram.Current == null)
            {
                this.showMessage(Color.Red, "程序为空.");
                return;
            }

            this.lblProgramPath.Text = FluidProgram.CurrentFilePath;
            this.program = FluidProgram.Current;
            Log.Dprint("Loading program: loading commands module...");
            loadCommandsModule(program.Workpiece);
            Log.Dprint("Loading program: loading commands module done!");

            if (FluidProgram.Current != null)
            {
                this.onCheckGrammerFinished(FluidProgram.Current.ParseResult);
            }
            refreshProgramStructure();
            this.showMessage(Color.Green, "程序加载成功.");
            this.currProgramPath = programPath;
        }

        /// <summary>
        /// 保存程序
        /// </summary>
        private void onSaveProgram()
        {
            this.lblProgramPath.Text = FluidProgram.CurrentFilePath;
            program.Save(
              FluidProgram.CurrentFilePath,
              () =>
              {
                  this.BeginInvoke(new MethodInvoker(() =>
                  {
                      this.enableAllMenu(false);
                  }));
                  this.showMessage(Color.Orange, "程序保存中...");
              },
              () =>
              {
                  Properties.Settings.Default.ProgramName = FluidProgram.CurrentFilePath;
                  this.currProgramPath = FluidProgram.CurrentFilePath;
                  this.showMessage(Color.Green, "程序保存成功.");
              },
              (errCode, errMsg) =>
              {
                  this.showMessage(Color.Red, errMsg);
              },
              () =>
              {
                  this.BeginInvoke(new MethodInvoker(() =>
                  {
                      this.enableAllMenu(true);
                  }));
              });
        }

        private void lineEditFormshow(IMsgSender sender, params object[] arg)
        {
            //TODO
            LineCmdLine lineCmdLine = arg[0] as LineCmdLine;
            Line line = sender as Line;
            if (lineCmdLine == null || line == null)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                switch (lineCmdLine.LineMethod)
                {
                    case LineMethod.Single:
                        new EditSingleLineForm(line.RunnableModule.CommandsModule as Pattern, lineCmdLine, line).ShowDialog();
                        break;
                    case LineMethod.Multi:
                        new EditMultiLinesForm(line.RunnableModule.CommandsModule as Pattern, lineCmdLine, line, Convert.ToInt32(arg[1])).ShowDialog();
                        break;
                    case LineMethod.Poly:
                        new EditPolyLineForm(line.RunnableModule.CommandsModule as Pattern, lineCmdLine, line, Convert.ToInt32(arg[1])).ShowDialog();
                        break;
                    default:
                        break;

                }
            }));
        }

        #endregion


        #region Events

        /// <summary>
        /// 脚本命令行点击事件
        /// </summary>
        public event Action<CommandsModule, CmdLine, MetroSetButtonImg> CmdLineDoubleClicked;

        public event Action<MetroSetButtonImg, Pattern, CmdButtonType> CmdButtonClicked;

        #endregion


        #region Script Editor

        /// <summary>
        /// 处理双击脚本编辑框中的命令行，如果当前命令行可以被编辑，弹出命令编辑窗口
        /// </summary>
        /// <param name="commandsModule"></param>
        /// <param name="cmdLine"></param>
        private void ScriptEditor1_OnEditCmdLineEvent(CommandsModule commandsModule, CmdLine cmdLine)
        {
            Machine.Instance.Robot.MoveSafeZ();
            Pattern pattern = commandsModule as Pattern;
            Log.Print("OnEditCmdLineEvent : cmdLine type = " + cmdLine.GetType().Name);
            SwitchSymbolForm.Instance.SetCurCommandsModule(commandsModule);
            if (cmdLine is MeasureHeightCmdLine)
            {
                MeasureHeightCmdLine heightCmdLine = cmdLine as MeasureHeightCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((heightCmdLine.Position + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnHeight);
            }
            else if (cmdLine is CircleCmdLine)
            {
                CircleCmdLine circleCmdLine = cmdLine as CircleCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((circleCmdLine.Start + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, null);
            }
            else if (cmdLine is ArcCmdLine)
            {
                ArcCmdLine arcCmdLine = cmdLine as ArcCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((arcCmdLine.Start + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, null);
            }
            else if (cmdLine is CommentCmdLine)
            {
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnComments);
            }
            else if (cmdLine is StepAndRepeatCmdLine)
            {
                StepAndRepeatCmdLine stepAndRepeatCmdLine = cmdLine as StepAndRepeatCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((stepAndRepeatCmdLine.Origin + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, null);
            }
            else if (cmdLine is DoCmdLine)
            {
                if (commandsModule is FluidProgram)
                {
                    return;
                }
                DoCmdLine doCmdLine = cmdLine as DoCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((doCmdLine.Origin + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnDoPattern);
            }
            else if (cmdLine is DoMultiPassCmdLine)
            {
                DoMultiPassCmdLine doMultiPassCmdLine = cmdLine as DoMultiPassCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((doMultiPassCmdLine.Origin + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnDoMultipass);
            }
            else if (cmdLine is DotCmdLine)
            {
                DotCmdLine dotCmdLine = cmdLine as DotCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((dotCmdLine.Position + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, null);
            }
            else if (cmdLine is FinishShotCmdLine)
            {
                FinishShotCmdLine finishShotCmdLine = cmdLine as FinishShotCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((finishShotCmdLine.Position + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, null);
            }
            else if (cmdLine is SnakeLineCmdLine)
            {
                SnakeLineCmdLine snakeLineCmdLine = cmdLine as SnakeLineCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((snakeLineCmdLine.Point1 + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, null);
            }
            else if (cmdLine is LineCmdLine)
            {
                LineCmdLine lineCmdLine = cmdLine as LineCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((lineCmdLine.LineCoordinateList[0].Start + pattern.GetOriginSys()).ToMachine());
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, null);
            }
            else if (cmdLine is LoopPassCmdLine)
            {
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnLoopPass);
            }
            else if (cmdLine is MarkCmdLine)
            {
                PointD originSys = pattern.GetOriginSys();
                MarkCmdLine markCmdLine = cmdLine as MarkCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((markCmdLine.PosInPattern + originSys).ToMachine());
                if (markCmdLine.ModelFindPrm.IsUnStandard)
                {
                    new EditAsvMarkForm(pattern, markCmdLine).ShowDialog();
                }
                else
                {
                    if (new EditModelFindForm(markCmdLine.ModelFindPrm, pattern).ShowDialog() == DialogResult.OK)
                    {
                        markCmdLine.PosInPattern.X = markCmdLine.ModelFindPrm.PosInPattern.X;
                        markCmdLine.PosInPattern.Y = markCmdLine.ModelFindPrm.PosInPattern.Y;
                        MsgCenter.Broadcast(Constants.MSG_FINISH_EDITING_CMD_LINE, this, markCmdLine);
                    }
                }
            }
            else if (cmdLine is BadMarkCmdLine)
            {
                PointD origin = (commandsModule as Pattern).GetOriginSys();
                BadMarkCmdLine badMarkCmdLine = cmdLine as BadMarkCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((badMarkCmdLine.Position + pattern.GetOriginSys()).ToMachine());
                new EditBadMarkForm(commandsModule as Pattern, badMarkCmdLine).ShowDialog();
            }
            else if (cmdLine is MoveAbsXyCmdLine)
            {
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnMove);
            }
            else if (cmdLine is MoveAbsZCmdLine)
            {
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnMove);
            }
            else if (cmdLine is MoveToLocationCmdLine)
            {
                if (this.program == null)
                {
                    return;
                }
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnMove);
            }
            else if (cmdLine is MoveXyCmdLine)
            {
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnMove);
            }
            else if (cmdLine is SetHeightSenseModeCmdLine)
            {
                // TODO 测高模式第二阶段再做
            }
            else if (cmdLine is StartPassCmdLine)
            {
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnPassBlock);
            }
            else if (cmdLine is NormalTimerCmdLine)
            {
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnTimer);
            }
            else if (cmdLine is TimerCmdLine)
            {
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnMultipassTimer);
            }
            else if (cmdLine is PassBlockCmdLine)
            {
                this.CmdLineDoubleClicked?.Invoke(commandsModule, cmdLine, btnPassBlock);
            }
            else if (cmdLine is NozzleCheckCmdLine)
            {
                NozzleCheckCmdLine nozzleCheckCmdLine = cmdLine as NozzleCheckCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((nozzleCheckCmdLine.Position + pattern.GetOriginSys()).ToMachine());
                new EditNozzleCheckForm(commandsModule as Pattern, cmdLine as NozzleCheckCmdLine).ShowDialog();
            }
        }

        private void ScriptEditor1_OnCommandsModuleLoadedEvent(CommandsModule commandsModule)
        {
            if (this.program == null)
            {
                return;
            }
            if (!(commandsModule is Pattern))
            {
                return;
            }
            Pattern pattern = commandsModule as Pattern;
            if (!Machine.Instance.IsProducting)
            {
                this.correctMark(pattern);
            }
        }

        private async void correctMark(Pattern pattern)
        {
            this.enableAllMenu(false);
            int ret = 0;
            await Task.Factory.StartNew(() =>
            {
                ret = PatternCorrector.Instance.Correct(pattern);
                if (ret >= 0)
                {
                    // 移动到原点位置
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    Machine.Instance.Robot.ManualMovePosXYAndReply(pattern.GetOriginPos());
                }
            });

            if (ret == 0)
            {
                // 只有当进行了坐标校正后才需要刷新命令行显示，重新解析语法
                scriptEditor1.UpdateCmdLines();
                checkGrammar();
            }
            else if (ret == -1)
            {
                MetroSetMessageBox.Show(this, string.Format("Pattern[{0}] Mark校正失败", pattern.Name));
            }
            enableAllMenu(true);
        }

        #endregion


        #region Interfaces

        public void EditMultiDrawCmd(List<int> cmdLineNo)
        {
            if (this.Parent == null)
            {
                return;
            }
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                List<CmdLine> selectCmdLines = new List<CmdLine>();
                for (int i = 0; i < cmdLineNo.Count; i++)
                {
                    selectCmdLines.Add(this.scriptEditor1.listView1.Items[cmdLineNo[i]].Tag as CmdLine);
                }
                this.batchUpdate(selectCmdLines);
            }
        }

        public void Updating()
        {
            double sec = Math.Round(Executor.Instance.WatchTime, 2);
            this.lblCt.Text = string.Format("{0}s", sec);
        }

        #endregion


        #region Right Command Buttons

        private void btnNewPattern_Click(object sender, EventArgs e)
        {
            if (program == null)
            {
                return;
            }
            this.CmdButtonClicked?.Invoke(btnNewPattern, null, CmdButtonType.NewPattern);
        }

        private void btnDoPattern_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnDoPattern, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.DoPattern);
            }
        }

        private void btnMultipassPattern_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnDoMultipass, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.DoMultipass);
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                scriptEditor1.Disable();
                checkGrammar();
                this.CmdButtonClicked?.Invoke(btnDisable, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.Disable);
            }
        }

        private void btnComments_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnComments, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.Comments);
            }
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnMark, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.Mark);
                Pattern pattern = scriptEditor1.CurrCommandsModule as Pattern;
                MarkCmdLine markCmdLine = new MarkCmdLine();
                EditModelFindForm emf = new EditModelFindForm(markCmdLine.ModelFindPrm, pattern);
                if (emf.ShowDialog() == DialogResult.OK)
                {
                    markCmdLine.PosInPattern.X = markCmdLine.ModelFindPrm.PosInPattern.X;
                    markCmdLine.PosInPattern.Y = markCmdLine.ModelFindPrm.PosInPattern.Y;
                    MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, markCmdLine);
                }
            }
        }

        private void btnAsvMark_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnAsvMark, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.AsvMark);
                Pattern pattern = scriptEditor1.CurrCommandsModule as Pattern;
                EditAsvMarkForm eamf = new EditAsvMarkForm(pattern);
                eamf.ShowDialog();
            }
        }

        private void btnBadMark_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnBadMark, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.BadMark);
                new EditBadMarkForm(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void btnNozzleCheck_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnNozzleCheck, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.NozzleCheck);
                new EditNozzleCheckForm(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void btnHeight_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnHeight, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.Height);
            }
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnTimer, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.Timer);
            }
        }

        private void btnLoopPass_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnLoopPass, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.LoopPass);
            }
        }

        private void btnPassBlock_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnPassBlock, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.PassBlock);
            }
        }

        private void btnMultipassArray_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnMultipassArray, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.MultipassArray);
            }
        }

        private void btnMultipassTimer_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnMultipassTimer, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.MultipassTimer);
            }
        }

        private void btnPurge_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnPurge, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.Purge);
                PurgeCmdLine purgeCmdLine = new PurgeCmdLine();
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, purgeCmdLine);
            }
        }

        private void btnSvSpeed_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null)
            {
                this.CmdButtonClicked?.Invoke(btnSvSpeed, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.SvSpeed);
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            this.cmsMove.Show(btnMove, 0, 0);
        }

        private void moveXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnMove, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.MoveXy);
            }
        }

        private void moveAbsXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnMove, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.MoveAbsXy);
            }
        }

        private void moveAbsZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnMove, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.MoveAbsZ);
            }
        }

        private void moveToLocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                this.CmdButtonClicked?.Invoke(btnMove, scriptEditor1.CurrCommandsModule as Pattern, CmdButtonType.MoveLoc);
            }
        }

        #endregion


        #region Top Command Buttons

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.SaveProgramIfChanged();
            this.CmdButtonClicked?.Invoke(btnNew, null, CmdButtonType.New);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.SaveProgramIfChanged();
            LoadFlu.Instance.OpenFile(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (program == null)
            {
                this.showMessage(Color.Orange, "程序为空.");
                return;
            }
            LoadFlu.Instance.SaveFile(this.program);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                scriptEditor1.Copy();
            }
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                scriptEditor1.CurrCommandsModule.IsModified = true;
                scriptEditor1.Cut();
                checkGrammar();
                scriptEditor1.CurrCommandsModule.IsModified = false;
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                scriptEditor1.Paste();
                checkGrammar();
            }
        }

        private void btnMovePrev_Click(object sender, EventArgs e)
        {
            this.scriptEditor1.MovePrev();
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            this.scriptEditor1.MoveNext();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (this.program == null)
            {
                return;
            }
            new ProgramSettingForm(this.program).ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Executor.Instance.Run();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            Executor.Instance.SingleStep();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Executor.Instance.PauseResume();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Executor.Instance.Abort();
        }

        private void btnConveyorWidth_Click(object sender, EventArgs e)
        {
            //TODO
            new ConveyorWidthForm().ShowDialog();
        }

        private void btnConveyorSelect_Click(object sender, EventArgs e)
        {
            //if (this.program == null) return;
            //if (this.btnConveyorSelect.Tag == null)
            //{
            //    this.btnConveyorSelect.Tag = 1;
            //}
            //int tag = (int)this.btnConveyorSelect.Tag;
            //if (tag == 1)
            //{
            //    this.btnConveyorSelect.Image = imgConveyor2;
            //    tag = 2;
            //}
            //else if (tag == 2)
            //{
            //    this.btnConveyorSelect.Image = imgConveyor1;
            //    tag = 1;
            //}
            //this.selectConveyor(tag);
        }

        private void cmbConveyor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.program == null) return;
            switch (this.cmbConveyor.SelectedIndex)
            {
                case 0:
                    this.program.ExecutantOriginOffset = new PointD(0, 0);
                    break;
                case 1:
                    this.program.ExecutantOriginOffset = this.program.Conveyor2OriginOffset;
                    break;
            }
        }

        private void selectConveyor(int i)
        {
            //if (this.program == null) return;
            //switch (i)
            //{
            //    case 1:
            //        this.program.ExecutantOriginOffset = new PointD(0, 0);
            //        break;
            //    case 2:
            //        this.program.ExecutantOriginOffset = this.program.Conveyor2OriginOffset;
            //        break;
            //}
            //this.btnConveyorSelect.Tag = i;
        }

        private void btnInspect_Click(object sender, EventArgs e)
        {
            Inspection inspection = InspectionMgr.Instance.FindBy((int)InspectionKey.Dot1);
            if (inspection == null)
            {
                return;
            }
            //TODO
            new EditInspectionForm(inspection).ShowDialog();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (this.program == null)
            {
                return;
            }
            //new SystemPositionDefinations1(this.program).ShowDialog();
            this.CmdButtonClicked?.Invoke(btnLoc, null, CmdButtonType.Locations);
        }

        private void txtFindComp_TextChanged(object sender)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                string track = this.txtFindComp.Text.Trim();
                this.scriptEditor1.CompareTrackNumber(track);
            }
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                List<CmdLine> selectCmdLines = this.scriptEditor1.GetSelectCmdLines();
                this.batchUpdate(selectCmdLines);
            }
        }

        private void batchUpdate(List<CmdLine> selectCmdLines)
        {
            //如果没有点胶指令则不进入批量修改界面
            bool hasGlueCmdLine = false;
            foreach (CmdLine cmdLine in selectCmdLines)
            {
                if (cmdLine is CircleCmdLine || cmdLine is ArcCmdLine || cmdLine is DotCmdLine || cmdLine is SnakeLineCmdLine || cmdLine is LineCmdLine)
                {
                    hasGlueCmdLine = true;
                    break;
                }
            }
            if (selectCmdLines.Count <= 0)
            {
                MetroSetMessageBox.Show(this, msgBatchUpdateTip2);
                return;
            }
            if (!hasGlueCmdLine)
            {
                MetroSetMessageBox.Show(this, msgBatchUpdateTip1);
                return;
            }
            else
            {
                new BatchUpdateCmdLineForm(scriptEditor1.CurrCommandsModule as Pattern, selectCmdLines).ShowDialog();
                this.scriptEditor1.UpdateCmdLines();
                checkGrammar();
                FluidProgram.Current.HasChanged = true;
            }
        }

        private void btnPrevCmdPt_Click(object sender, EventArgs e)
        {
            bool changeCmdLine = false;
            if (this.curCmdLineNode >= this.curCmdLineNodeCount)
            {
                this.curCmdLineNode = this.curCmdLineNodeCount - 1;
            }
            if (this.curCmdLineNode < 0)
            {
                changeCmdLine = true;
            }
            CmdLine cmdLine = this.scriptEditor1.GetLastCmdline(changeCmdLine);

            if (cmdLine == null)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Pattern pattern = this.scriptEditor1.CurrCommandsModule as Pattern;
            PointD originSys = pattern.GetOriginSys();
            if (cmdLine is MeasureHeightCmdLine)
            {
                MeasureHeightCmdLine measureHeightCmdLine = cmdLine as MeasureHeightCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((measureHeightCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(btnHeight, pattern, CmdButtonType.Height);
            }
            else if (cmdLine is CircleCmdLine)
            {
                CircleCmdLine circleCmdLine = cmdLine as CircleCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((circleCmdLine.Start + originSys).ToMachine());
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.Circle);
            }
            else if (cmdLine is ArcCmdLine)
            {
                ArcCmdLine arcCmdLine = cmdLine as ArcCmdLine;
                this.curCmdLineNodeCount = 2;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                if (this.curCmdLineNode == 1)
                {
                    Machine.Instance.Robot.ManualMovePosXY((arcCmdLine.End + originSys).ToMachine());
                }
                else if (curCmdLineNode == 0)
                {
                    Machine.Instance.Robot.ManualMovePosXY((arcCmdLine.Start + originSys).ToMachine());
                }
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.Arc);
            }
            else if (cmdLine is CommentCmdLine)
            {
                return;
            }
            else if (cmdLine is StepAndRepeatCmdLine)
            {
                StepAndRepeatCmdLine stepAndRepeatCmdLine = cmdLine as StepAndRepeatCmdLine;
                this.curCmdLineNodeCount = stepAndRepeatCmdLine.DoCmdLineList.Count;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((stepAndRepeatCmdLine.DoCmdLineList[this.curCmdLineNode].Origin + originSys).ToMachine());
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.PatternArray);
            }
            else if (cmdLine is DoCmdLine)
            {
                DoCmdLine doCmdLine = cmdLine as DoCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((doCmdLine.Origin + originSys).ToMachine());
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(btnDoPattern, pattern, CmdButtonType.DoPattern);
            }
            else if (cmdLine is DoMultiPassCmdLine)
            {
                DoMultiPassCmdLine doMultiPassCmdLine = cmdLine as DoMultiPassCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((doMultiPassCmdLine.Origin + originSys).ToMachine());
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(btnDoMultipass, pattern, CmdButtonType.DoMultipass);
            }
            else if (cmdLine is DotCmdLine)
            {
                DotCmdLine dotCmdLine = cmdLine as DotCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((dotCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.Dot);
            }
            else if (cmdLine is FinishShotCmdLine)
            {
                FinishShotCmdLine finishShotCmdLine = cmdLine as FinishShotCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((finishShotCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.FinishShot);
            }
            else if (cmdLine is SnakeLineCmdLine)
            {
                //todo
                return;
            }
            else if (cmdLine is LineCmdLine)
            {
                LineCmdLine lineCmdLine = cmdLine as LineCmdLine;
                this.curCmdLineNodeCount = 2 * lineCmdLine.LineCoordinateList.Count;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                if (this.curCmdLineNode % 2 == 0)
                {
                    Machine.Instance.Robot.ManualMovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].Start + originSys).ToMachine());

                }
                if (this.curCmdLineNode % 2 == 1)
                {
                    Machine.Instance.Robot.ManualMovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].End + originSys).ToMachine());
                }
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.Line);
            }
            else if (cmdLine is LoopPassCmdLine)
            {
                return;
            }
            else if (cmdLine is MarkCmdLine)
            {
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                MarkCmdLine markCmdLine = cmdLine as MarkCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((markCmdLine.PosInPattern + originSys).ToMachine());
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(btnMark, pattern, CmdButtonType.Mark);
            }
            else if (cmdLine is BadMarkCmdLine)
            {
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                BadMarkCmdLine badMarkCmdLine = cmdLine as BadMarkCmdLine;
                Machine.Instance.Robot.ManualMovePosXY((badMarkCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode--;
                this.CmdButtonClicked?.Invoke(btnBadMark, pattern, CmdButtonType.BadMark);
            }
            else if (cmdLine is MoveAbsXyCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is MoveAbsZCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is MoveToLocationCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is MoveXyCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is SetHeightSenseModeCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is StartPassCmdLine)
            {
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is NormalTimerCmdLine)
            {
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is TimerCmdLine)
            {
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is PassBlockCmdLine)
            {
                this.curCmdLineNodeCount = 0;
                return;
            }
            else
            {
                this.curCmdLineNodeCount = 0;
                this.curCmdLineNode = 0;
                return;
            }
        }

        private void btnNextCmdPt_Click(object sender, EventArgs e)
        {
            bool changeCmdLine = false;
            if (this.curCmdLineNode < 0)
            {
                this.curCmdLineNode = 0;
            }
            if (this.curCmdLineNode > this.curCmdLineNodeCount - 1)
            {
                changeCmdLine = true;
            }
            CmdLine cmdLine = this.scriptEditor1.GetNextCmdLine(changeCmdLine);


            if (cmdLine == null)
            {
                return;
            }
            Machine.Instance.Robot.MoveSafeZ();
            Pattern pattern = this.scriptEditor1.CurrCommandsModule as Pattern;
            PointD originSys = pattern.GetOriginSys();
            if (cmdLine is MeasureHeightCmdLine)
            {
                MeasureHeightCmdLine measureHeightCmdLine = cmdLine as MeasureHeightCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((measureHeightCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(btnHeight, pattern, CmdButtonType.Height);
            }
            else if (cmdLine is CircleCmdLine)
            {
                CircleCmdLine circleCmdLine = cmdLine as CircleCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((circleCmdLine.Start + originSys).ToMachine());
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.Circle);
            }
            else if (cmdLine is ArcCmdLine)
            {
                ArcCmdLine arcCmdLine = cmdLine as ArcCmdLine;
                this.curCmdLineNodeCount = 2;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                if (this.curCmdLineNode == 1)
                {
                    Machine.Instance.Robot.ManualMovePosXY((arcCmdLine.End + originSys).ToMachine());

                }
                else if (curCmdLineNode == 0)
                {
                    Machine.Instance.Robot.ManualMovePosXY((arcCmdLine.Start + originSys).ToMachine());
                }
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.Arc);
            }
            else if (cmdLine is CommentCmdLine)
            {
                return;
            }
            else if (cmdLine is StepAndRepeatCmdLine)
            {
                StepAndRepeatCmdLine stepAndRepeatCmdLine = cmdLine as StepAndRepeatCmdLine;
                this.curCmdLineNodeCount = stepAndRepeatCmdLine.DoCmdLineList.Count;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((stepAndRepeatCmdLine.DoCmdLineList[this.curCmdLineNode].Origin + originSys).ToMachine());
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.PatternArray);
            }
            else if (cmdLine is DoCmdLine)
            {
                DoCmdLine doCmdLine = cmdLine as DoCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((doCmdLine.Origin + originSys).ToMachine());
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(btnDoPattern, pattern, CmdButtonType.DoPattern);
            }
            else if (cmdLine is DoMultiPassCmdLine)
            {
                DoMultiPassCmdLine doMultiPassCmdLine = cmdLine as DoMultiPassCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((doMultiPassCmdLine.Origin + originSys).ToMachine());
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(btnDoMultipass, pattern, CmdButtonType.DoMultipass);
            }
            else if (cmdLine is DotCmdLine)
            {
                DotCmdLine dotCmdLine = cmdLine as DotCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((dotCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.Dot);
            }
            else if (cmdLine is FinishShotCmdLine)
            {
                FinishShotCmdLine finishShotCmdLine = cmdLine as FinishShotCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((finishShotCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.FinishShot);
            }
            else if (cmdLine is SnakeLineCmdLine)
            {
                //todo
                return;
            }
            else if (cmdLine is LineCmdLine)
            {
                LineCmdLine lineCmdLine = cmdLine as LineCmdLine;
                this.curCmdLineNodeCount = 2 * lineCmdLine.LineCoordinateList.Count;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                if (this.curCmdLineNode % 2 == 0)
                {
                    Machine.Instance.Robot.ManualMovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].Start + originSys).ToMachine());
                }
                if (this.curCmdLineNode % 2 == 1)
                {
                    Machine.Instance.Robot.ManualMovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].End + originSys).ToMachine());
                }
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(null, pattern, CmdButtonType.Line);
            }
            else if (cmdLine is LoopPassCmdLine)
            {
                return;
            }
            else if (cmdLine is MarkCmdLine)
            {
                MarkCmdLine markCmdLine = cmdLine as MarkCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((markCmdLine.PosInPattern + originSys).ToMachine());
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(btnMark, pattern, CmdButtonType.Mark);
            }
            else if (cmdLine is BadMarkCmdLine)
            {
                BadMarkCmdLine badMarkCmdLine = cmdLine as BadMarkCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                Machine.Instance.Robot.ManualMovePosXY((badMarkCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode++;
                this.CmdButtonClicked?.Invoke(btnBadMark, pattern, CmdButtonType.BadMark);
            }
            else if (cmdLine is MoveAbsXyCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is MoveAbsZCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is MoveToLocationCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is MoveXyCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is SetHeightSenseModeCmdLine)
            {
                //todo
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is StartPassCmdLine)
            {
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is NormalTimerCmdLine)
            {
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is TimerCmdLine)
            {
                this.curCmdLineNodeCount = 0;
                return;
            }
            else if (cmdLine is PassBlockCmdLine)
            {
                this.curCmdLineNodeCount = 0;
                return;
            }
            else
            {
                this.curCmdLineNodeCount = 0;
                this.curCmdLineNode = this.curCmdLineNodeCount;
                return;
            }
        }

        private void btnMotionSetting_Click(object sender, EventArgs e)
        {
            if (this.program == null) return;
            new ProgramSettingMotionForm(this.program.MotionSettings).ShowDialog();
        }



        #endregion

        #region 权限
        public int Key { get; set; } = (int)ContainerKeys.PageJobsProgram;
        public Control Control => this;


        public ContainerAccess CurrContainerAccess { get; set; } = new ContainerAccess();


        public ContainerAccess DefaultContainerAccess { get; set; } = new ContainerAccess();


        public List<AccessObj> UserAccessControls { get; set; } = new List<AccessObj>();

        public void SetupUserAccessControl()
        {
            
        }
        public void SetDefaultAccess()
        {
            
             this.DefaultContainerAccess = new ContainerAccess();
          
            //上面
            string containerName = this.GetType().Name;
            this.DefaultContainerAccess.ContainerName = containerName;
            this.DefaultContainerAccess.ContainerAccessDescription = "编程界面";
            this.DefaultContainerAccess.ControlAccessList.Clear();
            this.DefaultContainerAccess.AddContainerOperator();

            ControlAccess accessProgram = new ControlAccess(containerName,"程序文件编辑");
            accessProgram.AddAccessTechnician(new List<string> { this.btnNew.Name, this.btnOpen.Name, this.btnSave.Name, this.btnCopy.Name, this.btnCut.Name, this.btnPaste.Name});
            this.DefaultContainerAccess.AddControlAccess(accessProgram);

            ControlAccess accessDebug = new ControlAccess(containerName, "程序调试");
            accessDebug.AddAccessTechnician(new List<string>() { this.btnMovePrev.Name, this.btnMoveNext.Name, this.btnPrevCmdPt.Name, this.btnNextCmdPt.Name, this.btnBatch.Name });
            this.DefaultContainerAccess.AddControlAccess(accessDebug);

            ControlAccess accessConfigAndSetting = new ControlAccess(containerName, "生产配置和设置");
            accessConfigAndSetting.AddAccessTechnician(new List<string>() { this.btnConfig.Name, this.btnMotionSetting.Name, this.cmbRunMode.Name, this.cmbConveyor.Name, this.btnConveyorWidth.Name, this.btnInspect.Name, this.btnLoc.Name, this.cbxSimulation.Name });
            this.DefaultContainerAccess.AddControlAccess(accessConfigAndSetting);

            ControlAccess accessEditProgram = new ControlAccess(containerName, "程序编辑");
            accessEditProgram.AddAccessTechnician(new List<string>() { this.treeView1.Name, this.btnNewPattern.Name, this.btnDoPattern.Name, this.btnDoMultipass.Name, this.btnDisable.Name, this.btnComments.Name, this.btnMark.Name, this.btnAsvMark.Name, this.btnBadMark.Name, this.btnNozzleCheck.Name, this.btnHeight.Name, this.btnTimer.Name, this.btnLoopPass.Name, this.btnPassBlock.Name, this.btnMultipassArray.Name, this.btnMultipassTimer.Name, this.btnPurge.Name, this.btnSvSpeed.Name, this.btnMove.Name, this.scriptEditor1.Name, this.txtFindComp.Name });
            this.DefaultContainerAccess.AddControlAccess(accessEditProgram);

            AccessControlMgr.Instance.AddContainerAccess(this.DefaultContainerAccess);
        }

        public void LoadAccess()
        {
            this.accessExecutor.LoadAccess();
        }

     

        public void UpdateUIByAccess()
        {
            this.accessExecutor.UpdateUIByAccess();
        }

        public void UpdateAccess()
        {
            
        }
        #endregion 
    }
}
