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
using Anda.Fluid.App.Metro.EditControls;
using MetroSet_UI.Controls;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.App.Script;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.International.Access;
using static Anda.Fluid.Domain.AccessControl.User.PageAccessEnums;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;


namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageJobsFluid : MetroSetUserControl, IMsgReceiver,IAccessControllable
    {
        private PageJobsProgram pageJobsProgram;
        private MetroSetButtonImg cmdBtnSelected;
        //权限执行
        private AccessExecutor accessExecutor;

        public PageJobsFluid()
        {
            InitializeComponent();
            //权限
            this.accessExecutor = new AccessExecutor(this);
            this.LoadAccess();
            AccessControlMgr.Instance.Register(this);
        }

        public void ConnectPage(PageJobsProgram pageJobProgram)
        {
            this.pageJobsProgram = pageJobProgram;
            this.pageJobsProgram.CmdLineDoubleClicked += PageJobProgram_CmdLineDoubleClicked;
            this.pageJobsProgram.CmdButtonClicked += PageJobsProgram_CmdButtonClicked;
        }


        #region Handle Events

        /// <summary>
        /// 当前选择的按钮，边框标识，对应参数界面
        /// </summary>
        /// <param name="btn"></param>
        private void selectCmdButton(MetroSetButtonImg btn)
        {
            if (this.cmdBtnSelected != null)
            {
                this.cmdBtnSelected.ShowBorder = false;
            }
            this.cmdBtnSelected = btn;
            if (this.cmdBtnSelected != null)
            {
                this.cmdBtnSelected.ShowBorder = true;
            }
        }

        /// <summary>
        /// 点击不同的命令，切换对应的参数界面
        /// </summary>
        /// <param name="control"></param>
        private void addPage(Control control)
        {
            this.metroSetPanel1.Controls.Clear();
            control.Dock = DockStyle.Fill;
            this.metroSetPanel1.Controls.Add(control);

            if (control is ICanSelectButton)
            {
                (control as ICanSelectButton).SetSelectButtons();
            }
            else
            {
                HookHotKeyMgr.Instance.GetSelectKey().ClearButtons();
            }
        }

        private void clearPage()
        {
            this.metroSetPanel1.Controls.Clear();
        }

     
        /// <summary>
        /// 事件处理：点击按钮加载对应的参数界面
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="pattern"></param>
        /// <param name="btnType"></param>
        private void PageJobsProgram_CmdButtonClicked(MetroSetButtonImg btn, Pattern pattern, CmdButtonType btnType)
        {
            this.selectCmdButton(btn);
            switch (btnType)
            {
                case CmdButtonType.None:
                    break;
                case CmdButtonType.New:
                    this.addPage(new NewProgramMetro());
                    break;
                case CmdButtonType.Open:
                    break;
                case CmdButtonType.Save:
                    break;
                case CmdButtonType.Copy:
                    break;
                case CmdButtonType.Cut:
                    break;
                case CmdButtonType.Paste:
                    break;
                case CmdButtonType.Config:
                    break;
                case CmdButtonType.EditOrigin:
                    this.addPage(new EditPatternOriginMetro(pattern, this.pageJobsProgram.CurrProgram.Workpiece));
                    break;
                case CmdButtonType.Start:
                    break;
                case CmdButtonType.Step:
                    break;
                case CmdButtonType.Pause:
                    break;
                case CmdButtonType.Abort:
                    break;
                case CmdButtonType.ConveyorSelect:
                    break;
                case CmdButtonType.ConveyorWidth:
                    break;
                case CmdButtonType.Locations:
                    this.addPage(new EditSystemLocMetro(this.pageJobsProgram.CurrProgram));
                    break;
                case CmdButtonType.Inspect:
                    break;
                case CmdButtonType.NewPattern:
                    this.addPage(new NewPatternMetro(this.pageJobsProgram.CurrProgram.Workpiece));
                    break;
                case CmdButtonType.DoPattern:
                    this.addPage(new EditDoPatternMetro(pattern));
                    break;
                case CmdButtonType.DoMultipass:
                    this.addPage(new EditDoMultipassMetro(pattern));
                    break;
                case CmdButtonType.Disable:
                    this.clearPage();
                    break;
                case CmdButtonType.Comments:
                    this.addPage(new EditCommentsMetro());
                    break;
                case CmdButtonType.Mark:
                    this.clearPage();
                    break;
                case CmdButtonType.AsvMark:
                    this.clearPage();
                    break;
                case CmdButtonType.BadMark:
                    this.clearPage();
                    break;
                case CmdButtonType.NozzleCheck:
                    this.clearPage();
                    break;
                case CmdButtonType.Height:
                    this.addPage(new EditHeightMetro(pattern));
                    break;
                case CmdButtonType.Timer:
                    this.addPage(new EditTimerMetro());
                    break;
                case CmdButtonType.LoopPass:
                    this.addPage(new EditLoopPassMetro());
                    break;
                case CmdButtonType.PassBlock:
                    this.addPage(new EditPassBlockMetro(null));
                    break;
                case CmdButtonType.MultipassArray:
                    this.addPage(new EditMultipassArrayMetro(pattern));
                    break;
                case CmdButtonType.MultipassTimer:
                    this.addPage(new EditMultipassTimerMetro());
                    break;
                case CmdButtonType.Purge:
                    this.clearPage();
                    break;
                case CmdButtonType.SvSpeed:
                    this.addPage(new EditSvSpeedMetro());
                    break;
                case CmdButtonType.MoveAbsXy:
                    this.addPage(new EditMoveAbsXyMetro());
                    break;
                case CmdButtonType.MoveAbsZ:
                    this.addPage(new EditMoveAbsZMetro());
                    break;
                case CmdButtonType.MoveXy:
                    this.addPage(new EditMoveXyMetro());
                    break;
                case CmdButtonType.MoveLoc:
                    this.addPage(new EditMoveLocMetro(this.pageJobsProgram.CurrProgram));
                    break;
                case CmdButtonType.Dot:
                    this.addPage(new EditDotMetro(pattern));
                    break;
                case CmdButtonType.Line:
                    this.addPage(new EditLineMetro(pattern));
                    break;
                case CmdButtonType.Polyline:
                    this.addPage(new EditPolylineMetro(pattern));
                    break;
                case CmdButtonType.Arc:
                    this.addPage(new EditArcMetro(pattern));
                    break;
                case CmdButtonType.Circle:
                    this.addPage(new EditCircleMetro(pattern));
                    break;
                case CmdButtonType.Snakeline:
                    this.addPage(new EditSnakelineMetro(pattern));
                    break;
                case CmdButtonType.PatternArray:
                    this.addPage(new EditPatternArrayMetro(pattern));
                    break;
                case CmdButtonType.PatternsArray:
                    this.addPage(new EditPatternsArrayMetro(pattern));
                    break;
                case CmdButtonType.FinishShot:
                    this.addPage(new EditFinishShotMetro(pattern));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 事件处理：双击脚本编辑器命令行
        /// 按钮标识：如果是PageJobsProgram界面的按钮，直接用传递过来的btn
        ///          如果点击的命令行对应按钮在本界面，则直接用对应按钮
        /// </summary>
        private void PageJobProgram_CmdLineDoubleClicked(CommandsModule commandsModule, CmdLine cmdLine, MetroSetButtonImg btn)
        {
            if (cmdLine is MeasureHeightCmdLine)
            {
                this.addPage(new EditHeightMetro(commandsModule as Pattern, cmdLine as MeasureHeightCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is CircleCmdLine)
            {
                this.addPage(new EditCircleMetro(commandsModule as Pattern, cmdLine as CircleCmdLine));
                this.selectCmdButton(btnEditCircle);
            }
            else if (cmdLine is ArcCmdLine)
            {
                this.addPage(new EditArcMetro(commandsModule as Pattern, cmdLine as ArcCmdLine));
                this.selectCmdButton(btnEditArc);
            }
            else if (cmdLine is CommentCmdLine)
            {
                this.addPage(new EditCommentsMetro(cmdLine as CommentCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is StepAndRepeatCmdLine)
            {
                this.addPage(new EditPatternArrayMetro(commandsModule as Pattern, cmdLine as StepAndRepeatCmdLine));
                this.selectCmdButton(btnEditPatternArray);
            }
            else if (cmdLine is DoCmdLine)
            {
                this.addPage(new EditDoPatternMetro(commandsModule as Pattern, cmdLine as DoCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is DoMultiPassCmdLine)
            {
                this.addPage(new EditDoMultipassMetro(commandsModule as Pattern, cmdLine as DoMultiPassCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is DotCmdLine)
            {
                this.addPage(new EditDotMetro(commandsModule as Pattern, cmdLine as DotCmdLine));
                this.selectCmdButton(btnEditDot);
            }
            else if (cmdLine is FinishShotCmdLine)
            {
                this.addPage(new EditFinishShotMetro(commandsModule as Pattern, cmdLine as FinishShotCmdLine));
                this.selectCmdButton(btnEditFinishShot);
            }
            else if (cmdLine is SnakeLineCmdLine)
            {
                this.addPage(new EditSnakelineMetro(commandsModule as Pattern, cmdLine as SnakeLineCmdLine));
                this.selectCmdButton(btnEditSnakeline);
            }
            else if (cmdLine is LineCmdLine)
            {
                this.addPage(new EditLineMetro(commandsModule as Pattern, cmdLine as LineCmdLine));
                this.selectCmdButton(btnEditLine);
            }
            else if (cmdLine is LoopPassCmdLine)
            {
                this.addPage(new EditLoopPassMetro(cmdLine as LoopPassCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is MarkCmdLine)
            {
                this.clearPage();
            }
            else if (cmdLine is BadMarkCmdLine)
            {
                this.clearPage();
            }
            else if (cmdLine is MoveAbsXyCmdLine)
            {
                this.addPage(new EditMoveAbsXyMetro(cmdLine as MoveAbsXyCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is MoveAbsZCmdLine)
            {
                this.addPage(new EditMoveAbsZMetro(cmdLine as MoveAbsZCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is MoveToLocationCmdLine)
            {
                this.addPage(new EditMoveLocMetro(this.pageJobsProgram.CurrProgram, cmdLine as MoveToLocationCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is MoveXyCmdLine)
            {
                this.addPage(new EditMoveXyMetro(cmdLine as MoveXyCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is SetHeightSenseModeCmdLine)
            {
                // TODO 测高模式第二阶段再做
            }
            else if (cmdLine is StartPassCmdLine)
            {
                this.addPage(new EditStartPassMetro(cmdLine as StartPassCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is NormalTimerCmdLine)
            {
                this.addPage(new EditTimerMetro(cmdLine as NormalTimerCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is TimerCmdLine)
            {
                this.addPage(new EditMultipassTimerMetro(cmdLine as TimerCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is PassBlockCmdLine)
            {
                this.addPage(new EditPassBlockMetro(cmdLine as PassBlockCmdLine));
                this.selectCmdButton(btn);
            }
            else if (cmdLine is NozzleCheckCmdLine)
            {
                this.clearPage();
            }
        }


        private void btnEditDot_Click(object sender, EventArgs e)
        {
            if (this.pageJobsProgram == null) return;
            if (this.pageJobsProgram.CurrCommandsModule == null) return;
            this.selectCmdButton(btnEditDot);
            this.addPage(new EditDotMetro(this.pageJobsProgram.CurrCommandsModule as Pattern));
        }

        private void btnEditLine_Click(object sender, EventArgs e)
        {
            if (this.pageJobsProgram == null) return;
            if (this.pageJobsProgram.CurrCommandsModule == null) return;
            this.selectCmdButton(btnEditLine);
            this.addPage(new EditLineMetro(this.pageJobsProgram.CurrCommandsModule as Pattern));
        }

        private void btnEditPolyline_Click(object sender, EventArgs e)
        {
            if (this.pageJobsProgram == null) return;
            if (this.pageJobsProgram.CurrCommandsModule == null) return;
            this.selectCmdButton(btnEditPolyline);
            this.addPage(new EditPolylineMetro(this.pageJobsProgram.CurrCommandsModule as Pattern));
        }

        private void btnEditArc_Click(object sender, EventArgs e)
        {
            if (this.pageJobsProgram == null) return;
            if (this.pageJobsProgram.CurrCommandsModule == null) return;
            this.selectCmdButton(btnEditArc);
            this.addPage(new EditArcMetro(this.pageJobsProgram.CurrCommandsModule as Pattern));
        }

        private void btnEditCircle_Click(object sender, EventArgs e)
        {
            if (this.pageJobsProgram == null) return;
            if (this.pageJobsProgram.CurrCommandsModule == null) return;
            this.selectCmdButton(btnEditCircle);
            this.addPage(new EditCircleMetro(this.pageJobsProgram.CurrCommandsModule as Pattern));
        }

        private void btnEditSnakeline_Click(object sender, EventArgs e)
        {
            if (this.pageJobsProgram == null) return;
            if (this.pageJobsProgram.CurrCommandsModule == null) return;
            this.selectCmdButton(btnEditSnakeline);
            this.addPage(new EditSnakelineMetro(this.pageJobsProgram.CurrCommandsModule as Pattern));
        }

        private void btnEditPatternArray_Click(object sender, EventArgs e)
        {
            if (this.pageJobsProgram == null) return;
            if (this.pageJobsProgram.CurrCommandsModule == null) return;
            this.selectCmdButton(btnEditPatternArray);
            this.addPage(new EditPatternArrayMetro(this.pageJobsProgram.CurrCommandsModule as Pattern));
        }

        private void btnEditPatternsArray_Click(object sender, EventArgs e)
        {
            if (this.pageJobsProgram == null) return;
            if (this.pageJobsProgram.CurrCommandsModule == null) return;
            this.selectCmdButton(btnEditPatternsArray);
            this.addPage(new EditPatternsArrayMetro(this.pageJobsProgram.CurrCommandsModule as Pattern));
        }

        private void btnEditFinishShot_Click(object sender, EventArgs e)
        {
            if (this.pageJobsProgram == null) return;
            if (this.pageJobsProgram.CurrCommandsModule == null) return;
            this.selectCmdButton(btnEditFinishShot);
            this.addPage(new EditFinishShotMetro(this.pageJobsProgram.CurrCommandsModule as Pattern));
        }

        #endregion

        public void HandleMsg(string msgName, IMsgSender sender, params object[] args)
        {
            if(msgName == MsgDef.MSG_PARAMPAGE_CLEAR)
            {
                this.clearPage();
                this.selectCmdButton(null);
            }
        }
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
            
            
            //左边
            string containerName = this.GetType().Name;
            this.DefaultContainerAccess.ContainerName = containerName;
            this.DefaultContainerAccess.ContainerAccessDescription = "命令窗口";

            this.DefaultContainerAccess.ControlAccessList.Clear();
            this.DefaultContainerAccess.AddContainerOperator();
            //点 线 折线， 弧线 圆  蛇形线  
            ControlAccess accessCommandEnable = new ControlAccess(containerName, "轨迹命令使能");
            accessCommandEnable.AddAccessTechnician(new List<string>() { this.btnEditDot.Name, this.btnEditLine.Name, this.btnEditPolyline.Name, this.btnEditArc.Name, this.btnEditCircle.Name, this.btnEditSnakeline.Name, this.btnEditPatternArray.Name, this.btnEditPatternsArray.Name, this.btnEditFinishShot.Name });
            this.DefaultContainerAccess.AddControlAccess(accessCommandEnable);
            //metroSetPanel1
            this.DefaultContainerAccess.AddControlAccess(new ControlAccess(containerName, "命令编辑局域", this.metroSetPanel1.Name, AccessEnums.RoleEnums.Operator));
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
