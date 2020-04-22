using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.App.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision;
using Anda.Fluid.App.Settings;
using Anda.Fluid.Drive.ValveSystem;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Domain.Vision;
using System.IO;
using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.App.EditInspection;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.Sensors.Heater;
using DrawingPanel.Msg;
using DrawingPanel;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Domain.AccessControl.User;

using Anda.Fluid.Domain.FluProgram.Executant;
using System.Diagnostics;
using System.Threading;
using Anda.Fluid.Domain;
using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.International;
using static Anda.Fluid.Domain.FluProgram.Grammar.GrammarParser;
using Anda.Fluid.App.EditMark;

namespace Anda.Fluid.App
{
    public partial class ProgramControlMetro : UserControlEx, IMsgReceiver, IMsgSender, IControlUpdating,IMultiDrawCmdEditable
    {
        private static readonly string TAG = "ProgramControl";
        private FluidProgram program;
        private string currProgramPath;
        private const string ORIGIN = "Origin";
        /// <summary>
        /// 当前轨迹的节点，多节点轨迹才使用，用于切换下一步轨迹
        /// </summary>
        private int curCmdLineNode = 0;

        private int curCmdLineNodeCount = 0;

        #region 文本变量（用于语言切换）
        //private string msgBatchUpdateTip1 = "No Glue Command was selected.";
        private string msgBatchUpdateTip1 = "没有点胶轨迹被选中.";
        //private string msgBatchUpdateTip2 = "No Command was selected.";
        private string msgBatchUpdateTip2 = "没有轨迹被选中.";
        #endregion

        public ProgramControlMetro()
        {
            InitializeComponent();

            this.Load += ProgramControl_Load;
            this.treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
            this.treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            this.contextMenuStrip1.Items.Add("Delete");
            //this.contextMenuStrip1.Items[0].Click += ToolStripMenuItem_Delete_Click;
            this.contextMenuStrip1.ItemClicked += ToolStripMenuItem_Click;

            tsbPause.Enabled = false;
            tsbAbort.Enabled = false;
            scriptEditor1.OnEditCmdLineEvent += ScriptEditor1_OnEditCmdLineEvent;
            scriptEditor1.OnCommandsModuleLoadedEvent += ScriptEditor1_OnCommandsModuleLoadedEvent;

            this.cbxSimulation.Checked = false;

            this.InitMenu();

            this.InitExecutor();

            //注册绘图消息接受者
            DrawingMsgCenter.Instance.RegisterReceiver(this.canvasControll1);
            DrawingMsgCenter.Instance.RegisterSingleDrawEditor(this.scriptEditor1);
            DrawingMsgCenter.Instance.RegisterMultiDrawEditor(this);

            ControlUpatingMgr.Add(this);
            this.ReadLanguageResources();
            //this.conveyorControl1.OfflineEntered += ConveyorControl1_OfflineEntered;
        }

        private IWin32Window owner;
        public void SetOwner(IWin32Window owner)
        {
            this.owner = owner;
        }

        public void SaveSettingDefault()
        {
            Properties.Settings.Default.Save();
        }

        private void InitMenu()
        {
            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀 || Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀)
            {
                this.tsbChangeSvSpeed.Enabled = false;
            }
            this.tsbNew.Text = "New Program";
            this.tsbOpen.Text = "Open Program";
            this.tsbSave.Text = "Save Program";
            this.tsbCopy.Text = "Copy";
            this.tsbCut.Text = "Cut";
            this.tsbPaste.Text = "Paste";
            this.tsbRun.Text = "Run";
            this.tsbSingle.Text = "Single Step";
            this.tsbPause.Text = "Pause";
            this.tsbAbort.Text = "Abort";
            this.tsbDoScale.Text = "Do Scale";
            this.tsbDoPurge.Text = "Do Purge";
            this.tsbSetting.Text = "Setting";
            this.tsbSystemLoc.Text = "System Loc";
            this.tsbBoardOut.Text = "Board Out";
            this.tsbBoardIn.Text = "Board In";
            this.tsbInspection.Text = "Inspection";

            this.tsbDisable.Text = "Disable";
            this.tsbComments.Text = "Comments";
            this.tsbMark.Text = "Mark";
            this.tsbHS.Text = "Height";
            this.tsbDot.Text = "Dot";
            this.tsbSingleLine.Text = "Line";
            this.tsbPolyLine.Text = "PolyLine";
            this.tsbLine.Text = "Lines";
            this.tsbCircle.Text = "Circle";
            this.tsbArc.Text = "Arc";
            this.tsbSnake.Text = "Snake";
            this.tsbNewPattern.Text = "New Pattern";
            this.tsbDoPatten.Text = "Do Pattern";
            this.tsbMultiPassPatten.Text = "Do Multipass Pattern";
            this.tsbPassBlock.Text = "Pass Block";
            this.tsbLoopBlock.Text = "Loop Pass";
            this.tsbNormalTimer.Text = "Normal Timer";
            this.tsbFinishShot.Text = "Finish Shot";
            this.tsbMatrixTimer.Text = "Matrix Timer";
            this.tsbArray.Text = "Array";
            this.tsbMultiPatternArray.Text = "Multi Pattern Array";
            this.tsbMatrix.Text = "Matrix";
            this.tsbMove.Text = "Move";
            this.tsbConveyorWidth.Text = "ConveyorWidth";
            this.tsbChangeSvSpeed.Text = "ChangeSpeed";
            this.tsbLastStep.Text = "Last Step";
            this.tsbNextStep.Text = "Next Step";
            this.tsbNozzleCheck.Text = "Nozzle Check";
            this.tsbBatchUpdate.Text = "Batch Update";
            this.tsbASVMark.Text = "ASV Mark";
            this.tsbAdd.Text = "Add";

            this.tsbFluidMode.Items.Add(ValveRunMode.Wet);
            this.tsbFluidMode.Items.Add(ValveRunMode.Dry);
            this.tsbFluidMode.Items.Add(ValveRunMode.Look);
            this.tsbFluidMode.Items.Add(ValveRunMode.AdjustLine);
            this.tsbFluidMode.Items.Add(ValveRunMode.InspectRect);
            this.tsbFluidMode.Items.Add(ValveRunMode.InspectDot);
            this.tsbFluidMode.SelectedIndexChanged += TsbFluidMode_SelectedIndexChanged;
            this.tsbFluidMode.SelectedIndex = (int)Machine.Instance.Valve1.RunMode;

            this.tsbConveyorSelected.Items.Add("Conveyor1");
            this.tsbConveyorSelected.Items.Add("Conveyor2");         
            this.tsbConveyorSelected.SelectedIndex = 0;
            this.tsbConveyorSelected.SelectedIndexChanged += this.tsbConveyorSelected_SelectedIndexChanged;

            
        }
        public override void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.SaveKeyValueToResources(this.tsbNew.Name, this.tsbNew.Text);
            this.SaveKeyValueToResources(this.tsbOpen.Name, this.tsbOpen.Text);
            this.SaveKeyValueToResources(this.tsbSave.Name, this.tsbSave.Text);
            this.SaveKeyValueToResources(this.tsbCopy.Name, this.tsbCopy.Text);
            this.SaveKeyValueToResources(this.tsbCut.Name, this.tsbCut.Text);
            this.SaveKeyValueToResources(this.tsbPaste.Name, this.tsbPaste.Text);
            this.SaveKeyValueToResources(this.tsbRun.Name, this.tsbRun.Text);
            this.SaveKeyValueToResources(this.tsbSingle.Name, this.tsbSingle.Text);
            this.SaveKeyValueToResources(this.tsbPause.Name, this.tsbPause.Text);
            this.SaveKeyValueToResources(this.tsbAbort.Name, this.tsbAbort.Text);
            this.SaveKeyValueToResources(this.tsbDoScale.Name, this.tsbDoScale.Text);
            this.SaveKeyValueToResources(this.tsbDoPurge.Name, this.tsbDoPurge.Text);
            this.SaveKeyValueToResources(this.tsbSetting.Name, this.tsbSetting.Text);

            this.SaveKeyValueToResources(this.tsbSystemLoc.Name, this.tsbSystemLoc.Text);
            this.SaveKeyValueToResources(this.tsbBoardOut.Name, this.tsbBoardOut.Text);
            this.SaveKeyValueToResources(this.tsbBoardIn.Name, this.tsbBoardIn.Text);
            this.SaveKeyValueToResources(this.tsbInspection.Name, this.tsbInspection.Text);

            this.SaveKeyValueToResources(this.tsbDisable.Name, this.tsbDisable.Text);
            this.SaveKeyValueToResources(this.tsbComments.Name, this.tsbComments.Text);
            this.SaveKeyValueToResources(this.tsbMark.Name, this.tsbMark.Text);
            this.SaveKeyValueToResources(this.tsbHS.Name, this.tsbHS.Text);
            this.SaveKeyValueToResources(this.tsbDot.Name, this.tsbDot.Text);
            this.SaveKeyValueToResources(this.tsbSingleLine.Name, this.tsbSingleLine.Text);
            this.SaveKeyValueToResources(this.tsbPolyLine.Name, this.tsbPolyLine.Text);
            this.SaveKeyValueToResources(this.tsbLine.Name, this.tsbLine.Text);
            this.SaveKeyValueToResources(this.tsbCircle.Name, this.tsbCircle.Text);

            this.SaveKeyValueToResources(this.tsbArc.Name, this.tsbArc.Text);
            this.SaveKeyValueToResources(this.tsbSnake.Name, this.tsbSnake.Text);
            this.SaveKeyValueToResources(this.tsbNewPattern.Name, this.tsbNewPattern.Text);
            this.SaveKeyValueToResources(this.tsbDoPatten.Name, this.tsbDoPatten.Text);
            this.SaveKeyValueToResources(this.tsbMultiPassPatten.Name, this.tsbMultiPassPatten.Text);
            this.SaveKeyValueToResources(this.tsbPassBlock.Name, this.tsbPassBlock.Text);
            this.SaveKeyValueToResources(this.tsbLoopBlock.Name, this.tsbLoopBlock.Text);
            this.SaveKeyValueToResources(this.tsbNormalTimer.Name, this.tsbNormalTimer.Text);
            this.SaveKeyValueToResources(this.tsbFinishShot.Name, this.tsbFinishShot.Text);
            this.SaveKeyValueToResources(this.tsbMatrixTimer.Name, this.tsbMatrixTimer.Text);
            this.SaveKeyValueToResources(this.tsbArray.Name, this.tsbArray.Text);
            this.SaveKeyValueToResources(this.tsbMultiPatternArray.Name, this.tsbMultiPatternArray.Text);
            this.SaveKeyValueToResources(this.tsbMatrix.Name, this.tsbMatrix.Text);
            this.SaveKeyValueToResources(this.tsbMove.Name, this.tsbMove.Text);
            this.SaveKeyValueToResources(this.tsbLastStep.Name, this.tsbLastStep.Text);
            this.SaveKeyValueToResources(this.tsbNextStep.Name, this.tsbNextStep.Text);
            this.SaveKeyValueToResources(this.tsbNozzleCheck.Name, this.tsbNozzleCheck.Text);
            this.SaveKeyValueToResources(this.tsbBatchUpdate.Name, this.tsbBatchUpdate.Text);
            this.SaveKeyValueToResources(this.tsbASVMark.Name, this.tsbASVMark.Text);
            this.SaveKeyValueToResources("msgBatchUpdateTip1", msgBatchUpdateTip1);
            this.SaveKeyValueToResources("msgBatchUpdateTip2", msgBatchUpdateTip1);

            this.SaveKeyListToResources(tsbFluidMode.Name, new List<string> {
                ValveRunMode.Wet.ToString(),
                ValveRunMode.Dry.ToString(),
                ValveRunMode.Look.ToString(),
                ValveRunMode.AdjustLine.ToString(),
                ValveRunMode.InspectRect.ToString(),
                ValveRunMode.InspectDot.ToString()});
            this.SaveKeyListToResources(tsbConveyorSelected.Name, new List<string> { "Conveyor1" , "Conveyor2" });

        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            if (this.HasLngResources())
            {
                this.tsbNew.Text = this.ReadKeyValueFromResources(this.tsbNew.Name);
                this.tsbOpen.Text = this.ReadKeyValueFromResources(this.tsbOpen.Name);
                this.tsbSave.Text = this.ReadKeyValueFromResources(this.tsbSave.Name);
                this.tsbCopy.Text = this.ReadKeyValueFromResources(this.tsbCopy.Name);
                this.tsbCut.Text = this.ReadKeyValueFromResources(this.tsbCut.Name);
                this.tsbPaste.Text = this.ReadKeyValueFromResources(this.tsbPaste.Name);
                this.tsbRun.Text = this.ReadKeyValueFromResources(this.tsbRun.Name);
                this.tsbSingle.Text = this.ReadKeyValueFromResources(this.tsbSingle.Name);
                this.tsbPause.Text = this.ReadKeyValueFromResources(this.tsbPause.Name);
                this.tsbAbort.Text = this.ReadKeyValueFromResources(this.tsbAbort.Name);
                this.tsbDoScale.Text = this.ReadKeyValueFromResources(this.tsbDoScale.Name);
                this.tsbDoPurge.Text = this.ReadKeyValueFromResources(this.tsbDoPurge.Name);
                this.tsbSetting.Text = this.ReadKeyValueFromResources(this.tsbSetting.Name);

                this.tsbSystemLoc.Text = this.ReadKeyValueFromResources(this.tsbSystemLoc.Name);
                this.tsbBoardOut.Text = this.ReadKeyValueFromResources(this.tsbBoardOut.Name);
                this.tsbBoardIn.Text = this.ReadKeyValueFromResources(this.tsbBoardIn.Name);
                this.tsbInspection.Text = this.ReadKeyValueFromResources(this.tsbInspection.Name);

                this.tsbDisable.Text = this.ReadKeyValueFromResources(this.tsbDisable.Name);
                this.tsbComments.Text = this.ReadKeyValueFromResources(this.tsbComments.Name);
                this.tsbMark.Text = this.ReadKeyValueFromResources(this.tsbMark.Name);
                this.tsbHS.Text = this.ReadKeyValueFromResources(this.tsbHS.Name);
                this.tsbDot.Text = this.ReadKeyValueFromResources(this.tsbDot.Name);
                this.tsbSingleLine.Text = this.ReadKeyValueFromResources(this.tsbSingleLine.Name);
                this.tsbPolyLine.Text = this.ReadKeyValueFromResources(this.tsbPolyLine.Name);
                this.tsbLine.Text = this.ReadKeyValueFromResources(this.tsbLine.Name);
                this.tsbCircle.Text = this.ReadKeyValueFromResources(this.tsbCircle.Name);

                this.tsbArc.Text = this.ReadKeyValueFromResources(this.tsbArc.Name);
                this.tsbSnake.Text = this.ReadKeyValueFromResources(this.tsbSnake.Name);
                this.tsbNewPattern.Text = this.ReadKeyValueFromResources(this.tsbNewPattern.Name);
                this.tsbDoPatten.Text = this.ReadKeyValueFromResources(this.tsbDoPatten.Name);
                this.tsbMultiPassPatten.Text = this.ReadKeyValueFromResources(this.tsbMultiPassPatten.Name);
                this.tsbPassBlock.Text = this.ReadKeyValueFromResources(this.tsbPassBlock.Name);
                this.tsbLoopBlock.Text = this.ReadKeyValueFromResources(this.tsbLoopBlock.Name);
                this.tsbNormalTimer.Text = this.ReadKeyValueFromResources(this.tsbNormalTimer.Name);
                this.tsbFinishShot.Text = this.ReadKeyValueFromResources(this.tsbFinishShot.Name);
                this.tsbMatrixTimer.Text = this.ReadKeyValueFromResources(this.tsbMatrixTimer.Name);
                this.tsbArray.Text = this.ReadKeyValueFromResources(this.tsbArray.Name);
                this.tsbMultiPatternArray.Text = this.ReadKeyValueFromResources(this.tsbMultiPatternArray.Name);
                this.tsbMatrix.Text = this.ReadKeyValueFromResources(this.tsbMatrix.Name);
                this.tsbMove.Text = this.ReadKeyValueFromResources(this.tsbMove.Name);
                this.tsbLastStep.Text = this.ReadKeyValueFromResources(this.tsbLastStep.Name);
                this.tsbNextStep.Text = this.ReadKeyValueFromResources(this.tsbNextStep.Name);
                this.tsbNozzleCheck.Text = this.ReadKeyValueFromResources(this.tsbNozzleCheck.Name);
                this.tsbBatchUpdate.Text = this.ReadKeyValueFromResources(this.tsbBatchUpdate.Name);
                this.tsbASVMark.Text = this.ReadKeyValueFromResources(this.tsbASVMark.Name);
                this.msgBatchUpdateTip1 = this.ReadKeyValueFromResources("msgBatchUpdateTip1");
                this.msgBatchUpdateTip2 = this.ReadKeyValueFromResources("msgBatchUpdateTip2");

                List<string> lngTextList = this.ReadKeyListFromResources(tsbFluidMode.Name);
                for (int i = 0;i< tsbFluidMode.Items.Count;i++)
                {
                    //索引超限防呆
                    if (i >= lngTextList.Count)
                    {
                        continue;
                    }
                    tsbFluidMode.Items[i] = lngTextList[i];
                }
                lngTextList = this.ReadKeyListFromResources(tsbConveyorSelected.Name);
                for (int i = 0; i < tsbConveyorSelected.Items.Count; i++)
                {
                    //索引超限防呆
                    if (i >= lngTextList.Count)
                    {
                        continue;
                    }
                    tsbConveyorSelected.Items[i] = lngTextList[i];
                }
            }
        }

        private void InitExecutor()
        {
            Executor.Instance.OnProgramRunning += () =>
            {
                EnableEditMenu(false);
            };
            Executor.Instance.OnProgramAborted += () =>
            {
                EnableEditMenu(true);
            };
            Executor.Instance.OnProgramDone += () =>
            {
                EnableEditMenu(true);
            };
            Executor.Instance.OnWorkStateChanged += workState =>
            {
                switch (workState)
                {
                    case Executor.WorkState.Idle:
                        EnableEditMenu(true);
                        break;
                    case Executor.WorkState.Preparing:
                        EnableEditMenu(false);
                        break;
                    case Executor.WorkState.WaitingForBoard:
                        EnableEditMenu(false);
                        break;
                    case Executor.WorkState.Programing:
                        EnableEditMenu(false);
                        break;
                    case Executor.WorkState.Stopping:
                        EnableEditMenu(false);
                        break;
                }
            };
            Executor.Instance.OnWorkRunning += () =>
            {
                EnableEditMenu(false);
            };
            Executor.Instance.OnWorkDone += () =>
            {
                EnableEditMenu(true);
            };
            Executor.Instance.OnWorkStop += () =>
            {
                EnableEditMenu(true);
            };

            Executor.Instance.OnStateChanged += (oldState, newState) =>
            {
                if(!this.IsHandleCreated)
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
                                tsbRun.Enabled = true;
                                tsbSingle.Enabled = true;
                                tsbPause.Enabled = false;
                                tsbAbort.Enabled = false;
                                tsbPause.Image = Properties.Resources.Pause_16px;
                                scriptEditor1.cancelMark();
                                break;
                            case Executor.ProgramOuterState.RUNNING:
                                tsbRun.Enabled = false;
                                tsbSingle.Enabled = false;
                                tsbPause.Enabled = true;
                                tsbAbort.Enabled = true;
                                tsbPause.Image = Properties.Resources.Pause_16px;
                                scriptEditor1.cancelMark();
                                break;
                            case Executor.ProgramOuterState.PAUSING:
                                tsbRun.Enabled = false;
                                tsbSingle.Enabled = false;
                                tsbPause.Enabled = false;
                                tsbAbort.Enabled = false;
                                scriptEditor1.cancelMark();
                                break;
                            case Executor.ProgramOuterState.PAUSED:
                                tsbRun.Enabled = false;
                                tsbSingle.Enabled = true;
                                tsbPause.Enabled = true;
                                tsbAbort.Enabled = true;
                                tsbPause.Image = Properties.Resources.Resume_Button_16px;
                                if (Executor.Instance.CurrPausedCmd != null)
                                {
                                    CommandsModule module = Executor.Instance.CurrPausedCmd.RunnableModule.CommandsModule;
                                    int index = module.FindCmdLineIndex(Executor.Instance.CurrPausedCmd.CmdLine);
                                    scriptEditor1.MarkRunningPausedPosition(module, index);
                                }
                                break;
                            case Executor.ProgramOuterState.ABORTING:
                                tsbRun.Enabled = false;
                                tsbSingle.Enabled = false;
                                tsbPause.Enabled = false;
                                tsbAbort.Enabled = false;
                                scriptEditor1.cancelMark();
                                break;
                            case Executor.ProgramOuterState.ABORTED:
                                tsbRun.Enabled = true;
                                tsbSingle.Enabled = true;
                                tsbPause.Enabled = false;
                                tsbAbort.Enabled = false;
                                tsbPause.Image = Properties.Resources.Pause_16px;
                                scriptEditor1.cancelMark();
                                break;
                        }
                    }));
                }
                catch (Exception)
                {
                    // exception ignore.
                }
            };
        }

        private void EnableEditMenu(bool b)
        {
            if(!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (b == true)
                {
                    Role role = RoleMgr.Instance.CurrentRole;
                    scriptEditor1.SetEnable(role.ProgramFormAccess.CanUseGluePathCmd);

                    treeView1.Enabled = role.ProgramFormAccess.CanUseProgramList;
                    tsbFluidMode.Enabled = role.MainFormAccess.CanUseCbxRunMode;
                    tsbCopy.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbCut.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbBatchUpdate.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbPaste.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbNew.Enabled = role.ProgramFormAccess.CanUseProgramFileOperate;
                    toolStrip2.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    #region 轨迹指令
                    tsbComments.Enabled = role.ProgramFormAccess.CanUseOtherCmd;
                    tsbAdd.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbDisable.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbMark.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbASVMark.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbHS.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbDot.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbSingleLine.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbPolyLine.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbLine.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbCircle.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbArc.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbSnake.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbNewPattern.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbDoPatten.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbMultiPassPatten.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbPassBlock.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbLoopBlock.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbNormalTimer.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbFinishShot.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbMatrixTimer.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbArray.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbMultiPatternArray.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbMatrix.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbMove.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbNozzleCheck.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    #endregion
                    tsbDoScale.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbDoPurge.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbSystemLoc.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                    tsbSetting.Enabled = role.ProgramFormAccess.CanEnterProgramSettingForm;
                    tsbCorrectMark.Enabled = role.ProgramFormAccess.CanUseGluePathCmd;
                }
                else
                {
                    scriptEditor1.SetEnable(b);
                    treeView1.Enabled = b;
                    tsbFluidMode.Enabled = b;
                    tsbCopy.Enabled = b;
                    tsbCut.Enabled = b;
                    tsbPaste.Enabled = b;
                    tsbBatchUpdate.Enabled = b;
                    tsbNew.Enabled = b;
                    toolStrip2.Enabled = b;
                    tsbDoScale.Enabled = b;
                    tsbDoPurge.Enabled = b;
                    tsbSystemLoc.Enabled = b;
                    tsbSetting.Enabled = b;
                    tsbCorrectMark.Enabled = b;
                }
                tsbOpen.Enabled = b;
                tsbSave.Enabled = b;
                //scriptEditor1.Enabled = b;
                tsbBoardIn.Enabled = b;
                tsbBoardOut.Enabled = b;
                tsbInspection.Enabled = b;
                tsbLastStep.Enabled = b;
                tsbNextStep.Enabled = b;
                tsbConveyorSelected.Enabled = b;
                tsbConveyorWidth.Enabled = b;
                tstFindTrack.Enabled = b;
            }));
        }

        private void EnableAllMenu(bool b)
        {
            this.EnableEditMenu(b);
            tsbRun.Enabled = b;
            tsbSingle.Enabled = b;
            tsbPause.Enabled = b;
            tsbAbort.Enabled = b;
        }

        private void TsbFluidMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tsbFluidMode.SelectedIndex < 0)
            {
                return;
            }
            ValveRunMode mode = Machine.Instance.Valve1.RunMode;
            Machine.Instance.Valve1.RunMode = (ValveRunMode)this.tsbFluidMode.SelectedIndex;
            if (mode != Machine.Instance.Valve1.RunMode)
            {
                string msg = string.Format("valveRunMode oldValue:{0}->newMode:{1}", mode.ToString(), Machine.Instance.Valve1.RunMode.ToString());
                Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.SETTING, this.GetType().Name, msg);
            }
        }

        private void ProgramControl_Load(object sender, EventArgs e)
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
            //if (MessageBox.Show(string.Format("Program:[{0}] has changed, save or not?", FluidProgram.Current.Name), "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            if (MessageBox.Show(string.Format("程序:[{0}] 被修改, 是否板保存?", FluidProgram.Current.Name), "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoadFlu.Instance.SaveFile(FluidProgram.Current);
            }
        }

        /// <summary>
        /// 加载上一次程序
        /// </summary>
        public void LoadLastProgram()
        {
            string filePath = Properties.Settings.Default.ProgramName;
            if (!File.Exists(filePath))
            {
                return;
            }
            MsgCenter.Broadcast(Constants.MSG_LOAD_PROGRAM, this, Properties.Settings.Default.ProgramName);
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
                if (sender is ProgramControl)
                {
                    onLoadProgram(args[0] as string);
                }
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
                    EnableEditMenu(true);
                }
                else
                {
                    EnableEditMenu(false);
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
                if ((ValveSeries)args[0] == ValveSeries.螺杆阀 || (ValveSeries)args[1] == ValveSeries.螺杆阀)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.tsbChangeSvSpeed.Enabled = true;
                    }));
                }
                else
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.tsbChangeSvSpeed.Enabled = false;
                    }));
                }
            }
            else if(msgName == LngMsg.SWITCH_LNG)
            {
                this.ReadLanguageResources();
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
                this.ShowMessage(Color.Red, result.ErrMsg);
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
            scriptEditor1.OnFinishAddingCmdLines(cmdLineList,false);
            checkGrammar();
        }
        /// <summary>
        /// 添加多个命令完成
        /// </summary>
        private void onFinishAddingCmdLineList(object[] args,bool isCopy)
        {
            List<CmdLine> cmdLineList = new List<CmdLine>();
            cmdLineList = args[0] as List<CmdLine>;
            scriptEditor1.OnFinishAddingCmdLines(cmdLineList,isCopy);
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

            this.ShowMessage(Color.Orange, "new program wait to be saved.");
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
                this.ShowMessage(Color.Orange, "program has already loaded.");
                return;
            }
            // 程序为空
            if (FluidProgram.Current == null)
            {
                this.ShowMessage(Color.Red, "program is null.");
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
            this.ShowMessage(Color.Green, "program loaded ok.");

            this.currProgramPath = programPath;
        }

        private void onSaveProgram()
        {
            this.lblProgramPath.Text = FluidProgram.CurrentFilePath;
            program.Save(
              FluidProgram.CurrentFilePath,
              () =>
              {
                  this.BeginInvoke(new MethodInvoker(() =>
                  {
                      this.EnableAllMenu(false);
                  }));
                  this.ShowMessage(Color.Orange, "program is saving.");
              },
              () =>
              {
                  Properties.Settings.Default.ProgramName = FluidProgram.CurrentFilePath;
                  this.currProgramPath = FluidProgram.CurrentFilePath;
                  this.ShowMessage(Color.Green, "program saved success.");
              },
              (errCode, errMsg) =>
              {
                  this.ShowMessage(Color.Red, errMsg);
              },
              () =>
              {
                  this.BeginInvoke(new MethodInvoker(() =>
                  {
                      this.EnableAllMenu(true);
                  }));
              });
        }

        private void lineEditFormshow(IMsgSender sender, params object[] arg)
        {
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
     
        /// <summary>
        /// 处理双击脚本编辑框中的命令行，如果当前命令行可以被编辑，弹出命令编辑窗口
        /// </summary>
        /// <param name="commandsModule"></param>
        /// <param name="cmdLine"></param>
        private void ScriptEditor1_OnEditCmdLineEvent(CommandsModule commandsModule, CmdLine cmdLine)
        {
            Log.Print("OnEditCmdLineEvent : cmdLine type = " + cmdLine.GetType().Name);
            SwitchSymbolForm.Instance.SetCurCommandsModule(commandsModule);
            if (cmdLine is MeasureHeightCmdLine)
            {
                new EditMeasureHeightForm1(commandsModule as Pattern, cmdLine as MeasureHeightCmdLine).ShowDialog();
            }
            else if (cmdLine is CircleCmdLine)
            {
                new EditCircleForm1(commandsModule as Pattern, cmdLine as CircleCmdLine).ShowDialog();
            }
            else if (cmdLine is ArcCmdLine)
            {
                new EditArcForm1(commandsModule as Pattern, cmdLine as ArcCmdLine).ShowDialog();
            }
            else if (cmdLine is CommentCmdLine)
            {
                new EditCommentForm(cmdLine as CommentCmdLine).ShowDialog();
            }
            else if (cmdLine is StepAndRepeatCmdLine)
            {
                //new EditStepAndRepeatForm2(commandsModule as Pattern, cmdLine as StepAndRepeatCmdLine).ShowDialog();
                new EditStepAndRepeatForm3(commandsModule as Pattern, cmdLine as StepAndRepeatCmdLine).ShowDialog();

            }
            else if (cmdLine is DoCmdLine)
            {
                if(commandsModule is FluidProgram)
                {
                    return;
                }
                new EditDoForm1(commandsModule as Pattern, cmdLine as DoCmdLine).ShowDialog();
            }
            else if (cmdLine is DoMultiPassCmdLine)
            {
                new EditDoMultipassForm1(commandsModule as Pattern, cmdLine as DoMultiPassCmdLine).ShowDialog();
            }
            else if (cmdLine is DotCmdLine)
            {
                new EditDotForm1(commandsModule as Pattern, cmdLine as DotCmdLine).ShowDialog();
            }
            else if (cmdLine is FinishShotCmdLine)
            {
                new EditFinishShotForm1(commandsModule as Pattern, cmdLine as FinishShotCmdLine).ShowDialog();
            }
            else if (cmdLine is SnakeLineCmdLine)
            {
                new EditSnakeLineForm1(commandsModule as Pattern, cmdLine as SnakeLineCmdLine).ShowDialog();
            }
            else if (cmdLine is LineCmdLine)
            {
                LineCmdLine lineCmdLine = cmdLine as LineCmdLine;
                switch (lineCmdLine.LineMethod)
                {
                    case Domain.FluProgram.Common.LineMethod.Multi:
                        new EditMultiLinesForm(commandsModule as Pattern, lineCmdLine).ShowDialog();
                        break;
                    case Domain.FluProgram.Common.LineMethod.Single:
                        new EditSingleLineForm(commandsModule as Pattern, lineCmdLine).ShowDialog();
                        break;
                    case Domain.FluProgram.Common.LineMethod.Poly:
                        new EditPolyLineForm(commandsModule as Pattern, lineCmdLine).ShowDialog();
                        break;
                }

            }
            else if (cmdLine is LoopPassCmdLine)
            {
                new EditLoopPassForm(cmdLine as LoopPassCmdLine).ShowDialog();
            }
            else if (cmdLine is MarkCmdLine)
            {
                Pattern pattern = commandsModule as Pattern;
                PointD originSys = pattern.GetOriginSys();
                MarkCmdLine markCmdLine = cmdLine as MarkCmdLine;
                Machine.Instance.Robot.MoveSafeZ();
                //Machine.Instance.Robot.MovePosXY((markCmdLine.PosInPattern + originSys).ToMachine());
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
                new EditBadMarkForm(commandsModule as Pattern, badMarkCmdLine).ShowDialog();
            }
            else if (cmdLine is MoveAbsXyCmdLine)
            {
                new EditMoveAbsXyForm1(cmdLine as MoveAbsXyCmdLine).ShowDialog();
            }
            else if (cmdLine is MoveAbsZCmdLine)
            {
                new EditMoveAbsZForm1(cmdLine as MoveAbsZCmdLine).ShowDialog();
            }
            else if (cmdLine is MoveToLocationCmdLine)
            {
                if(this.program == null)
                {
                    return;
                }
                new EditMoveToLocationForm(this.program, cmdLine as MoveToLocationCmdLine).ShowDialog();
            }
            else if (cmdLine is MoveXyCmdLine)
            {
                new EditMoveXyForm(cmdLine as MoveXyCmdLine).ShowDialog();
            }
            else if (cmdLine is SetHeightSenseModeCmdLine)
            {
                // TODO 测高模式第二阶段再做
            }
            else if (cmdLine is StartPassCmdLine)
            {
                new EditStartPassForm(cmdLine as StartPassCmdLine).ShowDialog();
            }
            else if (cmdLine is NormalTimerCmdLine)
            {
                new EditNormalTimerForm(cmdLine as NormalTimerCmdLine).ShowDialog();
            }
            else if (cmdLine is TimerCmdLine)
            {
                new EditTimerForm(cmdLine as TimerCmdLine).ShowDialog();
            }
            else if (cmdLine is PassBlockCmdLine)
            {
                new AddPassBlockForm(cmdLine as PassBlockCmdLine).ShowDialog();
            }
            else if (cmdLine is NozzleCheckCmdLine)
            {
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

        private void tsbCorrectMark_Click(object sender, EventArgs e)
        {
            if (this.program == null)
            {
                return;
            }
            if (!(this.scriptEditor1.CurrCommandsModule is Pattern))
            {
                return;
            }
            Pattern pattern = this.scriptEditor1.CurrCommandsModule as Pattern;
            this.correctMark(pattern);
        }

        private async void correctMark(Pattern pattern)
        {
            this.EnableAllMenu(false);
            int ret = 0;
            await Task.Factory.StartNew(() =>
            {
                ret = PatternCorrector.Instance.Correct(pattern);
                if(ret >= 0)
                {
                    // 移动到原点位置
                    Machine.Instance.Robot.MoveSafeZAndReply();
                    //Machine.Instance.Robot.MovePosXYAndReply(pattern.GetOriginPos());
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
                //MessageBox.Show("Correct mark failed!");
                MessageBox.Show("mark校正失败!");
            }
            EnableAllMenu(true);
        }

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
            //if (Executor.Instance.CurrProgramState != Executor.ProgramOuterState.IDLE
            //    && Executor.Instance.CurrProgramState != Executor.ProgramOuterState.ABORTED)
            //{
            //    Log.Print("Refused checking grammar : executor.CurrState=" + Executor.Instance.CurrProgramState);
            //    return;
            //}

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
            if(e.Node.Text == ORIGIN)
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
                    this.positionVControl1.SetRelativeOrigin(workpiece.GetOriginPos());
                }
                else if (commandsModule is Pattern)
                {
                    Pattern pattern = commandsModule as Pattern;
                    this.positionVControl1.SetRelativeOrigin(pattern.GetOriginPos());
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
            if(e.Button == MouseButtons.Left)
            {
                if(e.Node.Text == ORIGIN)
                {
                    Pattern p = e.Node.Parent.Tag as Pattern;
                    if (new EditPattern(p, program.Workpiece).ShowDialog() == DialogResult.OK)
                    {
                        Log.Print("change pattern origin to : " + p.Origin);
                        // 移动到新原点位置
                        Machine.Instance.Robot.MoveSafeZ();                        
                        //Machine.Instance.Robot.MovePosXY(p.GetOriginPos());
                        Machine.Instance.Robot.ManualMovePosXY(p.GetOriginPos());
                    }
                }
            }
        }

        // 右键删除pattern
        private void ToolStripMenuItem_Delete_Click(object sender, EventArgs e)
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

        private void tsbNew_Click(object sender, EventArgs e)
        {
            this.SaveProgramIfChanged();
            new NewProgram1().ShowDialog();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            this.SaveProgramIfChanged();
            LoadFlu.Instance.OpenFile(this);
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (program == null)
            {
                this.ShowMessage(Color.Orange, "program is null.");
                return;
            }
            LoadFlu.Instance.SaveFile(this.program);
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                scriptEditor1.Copy();
            }
        }

        private void tsbCut_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                scriptEditor1.CurrCommandsModule.IsModified = true;
                scriptEditor1.Cut();
                checkGrammar();
                scriptEditor1.CurrCommandsModule.IsModified = false;
            }
        }

        private void tsbPaste_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                scriptEditor1.Paste();
                checkGrammar();
            }
        }

        private void tsbRun_Click(object sender, EventArgs e)
        {
            Executor.Instance.Run();
        }

        private void tsbSingle_Click(object sender, EventArgs e)
        {
            Executor.Instance.SingleStep();
        }

        private void tsbPause_Click(object sender, EventArgs e)
        {
            Executor.Instance.PauseResume();
        }

        private void tsbAbort_Click(object sender, EventArgs e)
        {
            Executor.Instance.Abort();
        }

        private void tsbDisable_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                scriptEditor1.Disable();
                checkGrammar();
            }
        }

        private void tsbComments_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditCommentForm().ShowDialog();
            }
        }

        private void tsbMark_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
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

        private void tsbBadMark_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditBadMarkForm(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbDot_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditDotForm1(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbFinishShot_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditFinishShotForm1(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbSingleLine_Click(object sender, EventArgs e)
        {
            Log.Print("tsbSingleLine_Click:   " + Thread.CurrentThread.ManagedThreadId.ToString());
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditSingleLineForm(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbPolyLine_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditPolyLineForm(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbLine_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditMultiLinesForm(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbArc_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditArcForm1(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbCircle_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditCircleForm1(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbNewPattern_Click(object sender, EventArgs e)
        {
            if (program == null)
            {
                return;
            }
            new AddPattern1(this.program.Workpiece).ShowDialog();
        }

        private void tsbPatten_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {                
                new EditDoForm1(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbMultiPassPatten_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditDoMultipassForm1(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbLoopBlock_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditLoopPassForm().ShowDialog();
            }
        }

        private void tsbPassBlock_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new AddPassBlockForm(null).ShowDialog();
            }
        }

        private void tsbArray_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                //new EditStepAndRepeatForm2(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
                new EditStepAndRepeatForm3(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbMatrix_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new AddMatrixForm(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbTimer_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditTimerForm().ShowDialog();
            }
        }

        private void tsbNormalTimer_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditNormalTimerForm().ShowDialog();
            }
        }

        private void tsbHS_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditMeasureHeightForm1(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void tsbSnake_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditSnakeLineForm1(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        private void moveXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditMoveXyForm().ShowDialog();
            }
        }

        private void moveAbsXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditMoveAbsXyForm1().ShowDialog();
            }
        }

        private void moveAbsZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditMoveAbsZForm1().ShowDialog();
            }
        }

        private void moveToLocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.program == null)
            {
                return;
            }
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditMoveToLocationForm(this.program).ShowDialog();
            }
        }

        private void tsbSystemLoc_Click(object sender, EventArgs e)
        {
            if(this.program == null)
            {
                return;
            }
            new SystemPositionDefinations1(this.program).ShowDialog();
        }

        private async void tsbDoPurge_Click(object sender, EventArgs e)
        {
            this.EnableAllMenu(false);
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.Valve1.DoPurgeAndPrime();
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    Machine.Instance.Valve2.DoPurgeAndPrime();
                }
            });
            this.EnableAllMenu(true);
        }

        private async void tsbDoScale_Click(object sender, EventArgs e)
        {
            this.EnableAllMenu(false);
            await Task.Factory.StartNew(() =>
            {
                if (this.program == null)
                {
                    return;
                }
                bool scaleSts = true;
                if (Machine.Instance.Scale.Scalable.CommunicationOK == false)
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
                    string msg = "天平断开，请确认!";
                    MessageBox.Show(msg);
                    return;
                }

                Result ret = Result.OK;
                ret = Machine.Instance.Valve1.AutoRunWeighingWithPurge();
                if (!ret.IsOk)
                {
                    this.program.RuntimeSettings.SingleDropWeight = 0;
                    return;
                }
                this.program.RuntimeSettings.SingleDropWeight = Machine.Instance.Valve1.weightPrm.SingleDotWeight;
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
                {
                    ret = Machine.Instance.Valve2.AutoRunWeighingWithPurge();
                    if (!ret.IsOk)
                    {
                        return;
                    }
                }
            });
            this.EnableAllMenu(true);
        }

        private void cbxSimulation_CheckedChanged(object sender, EventArgs e)
        {
            Machine.Instance.Robot.IsSimulation = this.cbxSimulation.Checked;
        }

        private void tsbSetting_Click(object sender, EventArgs e)
        {
            if (this.program == null)
            {
                return;
            }
            new ProgramSettingForm(this.program).ShowDialog();
        }

        private void ShowMessage(Color color, string text)
        {
            this.BeginInvoke(new MethodInvoker(
                () =>
                {
                    this.lblMsg.BackColor = color;
                    this.lblMsg.Text = text;
                }));
        }

        private void tsbInspection_Click(object sender, EventArgs e)
        {
            //Inspection inspection = InspectionMgr.Instance.InspectionDotMgr.FindBy((int)InspectionKey.Dot);
            Inspection inspection = InspectionMgr.Instance.FindBy((int)InspectionKey.Dot);
            if (inspection == null)
            {
                return;
            }
            new EditInspectionForm(inspection).ShowDialog();
        }

        private void tsbBoardIn_Click(object sender, EventArgs e)
        {
            if (this.tsbConveyorSelected.SelectedIndex == 0)
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道1手动进板);
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Conveyor1 bord in");
            }
            else
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道2手动进板);
                Logger.DEFAULT.Info(LogCategory.RUNNING, TAG, "Conveyor1 bord in");
            }
        }

        private void tsbBoardOut_Click(object sender, EventArgs e)
        {
            if (this.tsbConveyorSelected.SelectedIndex == 0)
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道1手动出板);
            }
            else
            {
                ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道2手动出板);
            }
        }

        private void tsbPurg_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                PurgeCmdLine purgeCmdLine = new PurgeCmdLine();
                MsgCenter.Broadcast(Constants.MSG_FINISH_ADDING_CMD_LINE, this, purgeCmdLine);
            }
        }

        public void Updating()
        {
            double sec = Math.Round(Executor.Instance.WatchTime, 2);
            this.lblCt.Text = string.Format("{0}s", sec);
        }

        private void tsbConveyorSelected_SelectedIndexChanged(object sender,EventArgs e)
        {
            if (this.tsbConveyorSelected.SelectedIndex == 0)
            {
                this.program.ExecutantOriginOffset = new PointD(0, 0);
            }
            else
            {
                this.program.ExecutantOriginOffset = this.program.Conveyor2OriginOffset;
            }
        }

        private void tsbConveyorWidth_Click(object sender, EventArgs e)
        {
            new ConveyorWidthForm().ShowDialog();
        }

        private void tsbMultiPatternArray_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditStepAndRepeatForm1(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }
        private void tsbChangeSvSpeed_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null)
            {
                new EditChangeSvSpeedForm().ShowDialog();
            }
        }
        private void tsbNozzleCheck_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                new EditNozzleCheckForm(scriptEditor1.CurrCommandsModule as Pattern).ShowDialog();
            }
        }

        ///<summary>
        /// Description	:切换到当前指令的当前点位的前一个点位、或是前一条指令的最后一个点位
        /// Author  	:liyi
        /// Date		:2019/07/06
        ///</summary>   
        private void tsbLastStep_Click(object sender, EventArgs e)
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
                //Machine.Instance.Robot.MovePosXY((measureHeightCmdLine.Position + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((measureHeightCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode--;
            }
            else if (cmdLine is CircleCmdLine)
            {
                CircleCmdLine circleCmdLine = cmdLine as CircleCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((circleCmdLine.Start + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((circleCmdLine.Start + originSys).ToMachine());
                this.curCmdLineNode--;
            }
            else if (cmdLine is ArcCmdLine)
            {
                ArcCmdLine arcCmdLine = cmdLine as ArcCmdLine;
                this.curCmdLineNodeCount = 2;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                if (this.curCmdLineNode == 1)
                {
                    //Machine.Instance.Robot.MovePosXY((arcCmdLine.End + originSys).ToMachine());
                    Machine.Instance.Robot.ManualMovePosXY((arcCmdLine.End + originSys).ToMachine());
                }
                else if(curCmdLineNode == 0)
                {
                    //Machine.Instance.Robot.MovePosXY((arcCmdLine.Start + originSys).ToMachine());
                    Machine.Instance.Robot.ManualMovePosXY((arcCmdLine.Start + originSys).ToMachine());
                }
                this.curCmdLineNode--;
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
                //Machine.Instance.Robot.MovePosXY((stepAndRepeatCmdLine.DoCmdLineList[this.curCmdLineNode].Origin + originSys).ToMachine());                
                Machine.Instance.Robot.ManualMovePosXY((stepAndRepeatCmdLine.DoCmdLineList[this.curCmdLineNode].Origin + originSys).ToMachine());
                this.curCmdLineNode--;
            }
            else if (cmdLine is DoCmdLine)
            {
                DoCmdLine doCmdLine = cmdLine as DoCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((doCmdLine.Origin + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((doCmdLine.Origin + originSys).ToMachine());
                this.curCmdLineNode--;
            }
            else if (cmdLine is DoMultiPassCmdLine)
            {
                DoMultiPassCmdLine doMultiPassCmdLine = cmdLine as DoMultiPassCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((doMultiPassCmdLine.Origin + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((doMultiPassCmdLine.Origin + originSys).ToMachine());
                this.curCmdLineNode--;
            }
            else if (cmdLine is DotCmdLine)
            {
                DotCmdLine dotCmdLine = cmdLine as DotCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((dotCmdLine.Position + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((dotCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode--;
            }
            else if (cmdLine is FinishShotCmdLine)
            {
                FinishShotCmdLine finishShotCmdLine = cmdLine as FinishShotCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((finishShotCmdLine.Position + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((finishShotCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode--;
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
                if (this.curCmdLineNode%2 == 0)
                {
                    //Machine.Instance.Robot.MovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].Start + originSys).ToMachine());
                    Machine.Instance.Robot.ManualMovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].Start + originSys).ToMachine());

                }
                if (this.curCmdLineNode%2 == 1)
                {
                    //Machine.Instance.Robot.MovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].End + originSys).ToMachine());
                    Machine.Instance.Robot.ManualMovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].End + originSys).ToMachine());
                }
                this.curCmdLineNode--;
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
                //Machine.Instance.Robot.MovePosXY((markCmdLine.PosInPattern + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((markCmdLine.PosInPattern + originSys).ToMachine());
                this.curCmdLineNode--;
            }
            else if (cmdLine is BadMarkCmdLine)
            {
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? this.curCmdLineNodeCount - 1 : this.curCmdLineNode;
                BadMarkCmdLine badMarkCmdLine = cmdLine as BadMarkCmdLine;
                //Machine.Instance.Robot.MovePosXY((badMarkCmdLine.Position + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((badMarkCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode--;
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
        ///<summary>
        /// Description	:切换到当前指令的当前点位的后一个点位、或是后一条指令的第一个点位
        /// Author  	:liyi
        /// Date		:2019/07/06
        ///</summary>   
        private void tsbNextStep_Click(object sender, EventArgs e)
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
                //Machine.Instance.Robot.MovePosXY((measureHeightCmdLine.Position + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((measureHeightCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode++;
            }
            else if (cmdLine is CircleCmdLine)
            {
                CircleCmdLine circleCmdLine = cmdLine as CircleCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((circleCmdLine.Start + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((circleCmdLine.Start + originSys).ToMachine());
                this.curCmdLineNode++;
            }
            else if (cmdLine is ArcCmdLine)
            {
                ArcCmdLine arcCmdLine = cmdLine as ArcCmdLine;
                this.curCmdLineNodeCount = 2;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                if (this.curCmdLineNode == 1)
                {
                    //Machine.Instance.Robot.MovePosXY((arcCmdLine.End + originSys).ToMachine());
                    Machine.Instance.Robot.ManualMovePosXY((arcCmdLine.End + originSys).ToMachine());

                }
                else if (curCmdLineNode == 0)
                {
                    //Machine.Instance.Robot.MovePosXY((arcCmdLine.Start + originSys).ToMachine());
                    Machine.Instance.Robot.ManualMovePosXY((arcCmdLine.Start + originSys).ToMachine());
                }
                this.curCmdLineNode++;
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
                //Machine.Instance.Robot.MovePosXY((stepAndRepeatCmdLine.DoCmdLineList[this.curCmdLineNode].Origin + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((stepAndRepeatCmdLine.DoCmdLineList[this.curCmdLineNode].Origin + originSys).ToMachine());
                this.curCmdLineNode++;
            }
            else if (cmdLine is DoCmdLine)
            {
                DoCmdLine doCmdLine = cmdLine as DoCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((doCmdLine.Origin + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((doCmdLine.Origin + originSys).ToMachine());
                this.curCmdLineNode++;
            }
            else if (cmdLine is DoMultiPassCmdLine)
            {
                DoMultiPassCmdLine doMultiPassCmdLine = cmdLine as DoMultiPassCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((doMultiPassCmdLine.Origin + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((doMultiPassCmdLine.Origin + originSys).ToMachine());
                this.curCmdLineNode++;
            }
            else if (cmdLine is DotCmdLine)
            {
                DotCmdLine dotCmdLine = cmdLine as DotCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((dotCmdLine.Position + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((dotCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode++;
            }
            else if (cmdLine is FinishShotCmdLine)
            {
                FinishShotCmdLine finishShotCmdLine = cmdLine as FinishShotCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((finishShotCmdLine.Position + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((finishShotCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode++;
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
                    //Machine.Instance.Robot.MovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].Start + originSys).ToMachine());
                    Machine.Instance.Robot.ManualMovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].Start + originSys).ToMachine());
                }
                if (this.curCmdLineNode % 2 == 1)
                {
                    //Machine.Instance.Robot.MovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].End + originSys).ToMachine());
                    Machine.Instance.Robot.ManualMovePosXY((lineCmdLine.LineCoordinateList[this.curCmdLineNode / 2].End + originSys).ToMachine());
                }
                this.curCmdLineNode++;
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
                //Machine.Instance.Robot.MovePosXY((markCmdLine.PosInPattern + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((markCmdLine.PosInPattern + originSys).ToMachine());
                this.curCmdLineNode++;
            }
            else if (cmdLine is BadMarkCmdLine)
            {
                BadMarkCmdLine badMarkCmdLine = cmdLine as BadMarkCmdLine;
                this.curCmdLineNodeCount = 1;
                this.curCmdLineNode = changeCmdLine ? 0 : this.curCmdLineNode;
                //Machine.Instance.Robot.MovePosXY((badMarkCmdLine.Position + originSys).ToMachine());
                Machine.Instance.Robot.ManualMovePosXY((badMarkCmdLine.Position + originSys).ToMachine());
                this.curCmdLineNode++;
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            for (int i = 0; i < this.treeView1.Nodes.Count; i++)
            {
                this.treeView1.Nodes[i].BackColor = Color.White;
                for (int j = 0; j < this.treeView1.Nodes[i].Nodes.Count; j++)
                {
                    this.treeView1.Nodes[i].Nodes[j].BackColor = Color.White;
                    for (int k = 0; k < this.treeView1.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        this.treeView1.Nodes[i].Nodes[j].Nodes[k].BackColor = Color.White;
                    }
                }
            }
            this.treeView1.SelectedNode.BackColor = Color.SkyBlue;
            
        }
        /// <summary>
        /// 批量修改轨迹属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbBatchUpdate_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                List<CmdLine> selectCmdLines = this.scriptEditor1.GetSelectCmdLines();
                this.BatchUpdate(selectCmdLines);
            }
        }

        private void tsbASVMark_Click(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                Pattern pattern = scriptEditor1.CurrCommandsModule as Pattern;
                //MarkCmdLine markCmdLine = new MarkCmdLine();
                //markCmdLine.IsNoStandard = true;
                EditAsvMarkForm eamf = new EditAsvMarkForm(pattern);
                eamf.ShowDialog();
            }
        }

        private void tstFindTrack_TextChanged(object sender, EventArgs e)
        {
            if (scriptEditor1.CurrCommandsModule != null && scriptEditor1.CurrCommandsModule is Pattern)
            {
                string track = this.tstFindTrack.Text.Trim();
                this.scriptEditor1.CompareTrackNumber(track);
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if (EditFormParent.IsShown)
            {
                return;
            }
            if (scriptEditor1.CurrCommandsModule == null)
            {
                return;
            }
            new EditFormParent(scriptEditor1.CurrCommandsModule as Pattern).Show(this.owner);
        }

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
                this.BatchUpdate(selectCmdLines);
            }
        }

        private void BatchUpdate(List<CmdLine> selectCmdLines)
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
                MessageBox.Show(msgBatchUpdateTip2);

                return;
            }
            if (!hasGlueCmdLine)
            {
                MessageBox.Show(msgBatchUpdateTip1);
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
    }
}
