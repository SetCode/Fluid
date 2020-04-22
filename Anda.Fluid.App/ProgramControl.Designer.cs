using Anda.Fluid.App.Script;

namespace Anda.Fluid.App
{
    partial class ProgramControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelRunningState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelModuleName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelGrammarCheckInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslOfflineCycle = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCt = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblProgramPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.canvasControll1 = new DrawingPanel.Display.CanvasControll();
            this.positionVControl1 = new Anda.Fluid.Domain.Motion.PositionVControl();
            this.cbxSimulation = new System.Windows.Forms.CheckBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.scriptEditor1 = new Anda.Fluid.App.Script.ScriptEditor();
            this.toolStripLeft1 = new System.Windows.Forms.ToolStrip();
            this.tsbDisable = new System.Windows.Forms.ToolStripButton();
            this.tsbComments = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbConveyorBarcode = new System.Windows.Forms.ToolStripButton();
            this.tsbBarcode = new System.Windows.Forms.ToolStripButton();
            this.tsbHS = new System.Windows.Forms.ToolStripButton();
            this.tsbNozzleCheck = new System.Windows.Forms.ToolStripButton();
            this.tsbMeasurement = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNewPattern = new System.Windows.Forms.ToolStripButton();
            this.tsbDoPatten = new System.Windows.Forms.ToolStripButton();
            this.tsbMultiPassPatten = new System.Windows.Forms.ToolStripButton();
            this.tsbLoopBlock = new System.Windows.Forms.ToolStripButton();
            this.tsbPassBlock = new System.Windows.Forms.ToolStripButton();
            this.tsbMatrix = new System.Windows.Forms.ToolStripButton();
            this.tsbMatrixTimer = new System.Windows.Forms.ToolStripButton();
            this.tsbMove = new System.Windows.Forms.ToolStripDropDownButton();
            this.moveXYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveAbsXYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveAbsZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToLocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPurg = new System.Windows.Forms.ToolStripButton();
            this.tsbChangeSvSpeed = new System.Windows.Forms.ToolStripButton();
            this.toolStripLeft2 = new System.Windows.Forms.ToolStrip();
            this.tsbMark = new System.Windows.Forms.ToolStripButton();
            this.tsbASVMark = new System.Windows.Forms.ToolStripButton();
            this.tsbBadMark = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbDot = new System.Windows.Forms.ToolStripButton();
            this.tsbSingleLine = new System.Windows.Forms.ToolStripButton();
            this.tsbPolyLine = new System.Windows.Forms.ToolStripButton();
            this.tsbLine = new System.Windows.Forms.ToolStripButton();
            this.tsbArc = new System.Windows.Forms.ToolStripButton();
            this.tsbCircle = new System.Windows.Forms.ToolStripButton();
            this.tsbSnake = new System.Windows.Forms.ToolStripButton();
            this.tsbMultiTraces = new System.Windows.Forms.ToolStripButton();
            this.tsbComplexLine = new System.Windows.Forms.ToolStripButton();
            this.tsbArray = new System.Windows.Forms.ToolStripButton();
            this.tsbMultiPatternArray = new System.Windows.Forms.ToolStripButton();
            this.tsbFinishShot = new System.Windows.Forms.ToolStripButton();
            this.tsbNormalTimer = new System.Windows.Forms.ToolStripButton();
            this.toolStripTop1 = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
            this.tsbMovePrev = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveNext = new System.Windows.Forms.ToolStripButton();
            this.tsbBatchUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLastStep = new System.Windows.Forms.ToolStripButton();
            this.tsbNextStep = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSetting = new System.Windows.Forms.ToolStripButton();
            this.tsbMotionSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSystemLoc = new System.Windows.Forms.ToolStripButton();
            this.tsbInspection = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDoScale = new System.Windows.Forms.ToolStripButton();
            this.tsbDoPurge = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCorrectMark = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadTraj = new System.Windows.Forms.ToolStripButton();
            this.tsbFineTune = new System.Windows.Forms.ToolStripButton();
            this.btnBlobs = new System.Windows.Forms.ToolStripButton();
            this.toolStripTop2 = new System.Windows.Forms.ToolStrip();
            this.lblFind = new System.Windows.Forms.ToolStripLabel();
            this.tstFindTrack = new System.Windows.Forms.ToolStripTextBox();
            this.tsbModifyComp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblRunMode = new System.Windows.Forms.ToolStripLabel();
            this.tsbFluidMode = new System.Windows.Forms.ToolStripComboBox();
            this.lblRunCycles = new System.Windows.Forms.ToolStripLabel();
            this.tscCycle = new System.Windows.Forms.ToolStripComboBox();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.tsbSingle = new System.Windows.Forms.ToolStripButton();
            this.tsbPause = new System.Windows.Forms.ToolStripButton();
            this.tsbAbort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblConveyor = new System.Windows.Forms.ToolStripLabel();
            this.tsbConveyorSelected = new System.Windows.Forms.ToolStripComboBox();
            this.tsbConveyorWidth = new System.Windows.Forms.ToolStripButton();
            this.tsbBoardIn = new System.Windows.Forms.ToolStripButton();
            this.tsbBoardOut = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.toolStripLeft1.SuspendLayout();
            this.toolStripLeft2.SuspendLayout();
            this.toolStripTop1.SuspendLayout();
            this.toolStripTop2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip2);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.canvasControll1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.positionVControl1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.cbxSimulation);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.treeView1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.scriptEditor1);
            this.toolStripContainer1.ContentPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1047, 681);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.toolStripLeft1);
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.toolStripLeft2);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(1126, 799);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripTop1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripTop2);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelRunningState,
            this.toolStripStatusLabel2,
            this.labelModuleName,
            this.toolStripStatusLabel3,
            this.labelGrammarCheckInfo,
            this.toolStripStatusLabel7,
            this.tslOfflineCycle,
            this.toolStripStatusLabel4,
            this.lblCt});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1126, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(40, 17);
            this.toolStripStatusLabel1.Text = "State:";
            // 
            // labelRunningState
            // 
            this.labelRunningState.Name = "labelRunningState";
            this.labelRunningState.Size = new System.Drawing.Size(34, 17);
            this.labelRunningState.Text = "IDLE";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel2.Text = "Module:";
            // 
            // labelModuleName
            // 
            this.labelModuleName.Name = "labelModuleName";
            this.labelModuleName.Size = new System.Drawing.Size(23, 17);
            this.labelModuleName.Text = "---";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabel3.Text = "Grammar:";
            // 
            // labelGrammarCheckInfo
            // 
            this.labelGrammarCheckInfo.Name = "labelGrammarCheckInfo";
            this.labelGrammarCheckInfo.Size = new System.Drawing.Size(60, 17);
            this.labelGrammarCheckInfo.Text = "no error.";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(83, 17);
            this.toolStripStatusLabel7.Text = "Offline Cycle:";
            // 
            // tslOfflineCycle
            // 
            this.tslOfflineCycle.Name = "tslOfflineCycle";
            this.tslOfflineCycle.Size = new System.Drawing.Size(15, 17);
            this.tslOfflineCycle.Text = "1";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabel4.Text = "Cycle Time:";
            // 
            // lblCt
            // 
            this.lblCt.Name = "lblCt";
            this.lblCt.Size = new System.Drawing.Size(38, 17);
            this.lblCt.Text = "0.00s";
            // 
            // statusStrip2
            // 
            this.statusStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel5,
            this.lblMsg,
            this.toolStripStatusLabel6,
            this.lblProgramPath});
            this.statusStrip2.Location = new System.Drawing.Point(0, 22);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1126, 22);
            this.statusStrip2.TabIndex = 21;
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(64, 17);
            this.toolStripStatusLabel5.Text = "Message:";
            // 
            // lblMsg
            // 
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(44, 17);
            this.lblMsg.Text = "Empty";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(96, 17);
            this.toolStripStatusLabel6.Text = "ProgramPath：";
            // 
            // lblProgramPath
            // 
            this.lblProgramPath.Name = "lblProgramPath";
            this.lblProgramPath.Size = new System.Drawing.Size(44, 17);
            this.lblProgramPath.Text = "Empty";
            // 
            // canvasControll1
            // 
            this.canvasControll1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvasControll1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.canvasControll1.Location = new System.Drawing.Point(369, 3);
            this.canvasControll1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.canvasControll1.Name = "canvasControll1";
            this.canvasControll1.Size = new System.Drawing.Size(489, 675);
            this.canvasControll1.TabIndex = 22;
            // 
            // positionVControl1
            // 
            this.positionVControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.positionVControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionVControl1.Location = new System.Drawing.Point(867, 594);
            this.positionVControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.positionVControl1.Name = "positionVControl1";
            this.positionVControl1.Size = new System.Drawing.Size(181, 84);
            this.positionVControl1.TabIndex = 21;
            // 
            // cbxSimulation
            // 
            this.cbxSimulation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSimulation.AutoSize = true;
            this.cbxSimulation.Location = new System.Drawing.Point(954, 570);
            this.cbxSimulation.Name = "cbxSimulation";
            this.cbxSimulation.Size = new System.Drawing.Size(96, 18);
            this.cbxSimulation.TabIndex = 20;
            this.cbxSimulation.Text = "Simulation";
            this.cbxSimulation.UseVisualStyleBackColor = true;
            this.cbxSimulation.CheckedChanged += new System.EventHandler(this.cbxSimulation_CheckedChanged);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Location = new System.Drawing.Point(867, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(179, 561);
            this.treeView1.TabIndex = 19;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // scriptEditor1
            // 
            this.scriptEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.scriptEditor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scriptEditor1.Location = new System.Drawing.Point(3, 3);
            this.scriptEditor1.Name = "scriptEditor1";
            this.scriptEditor1.Size = new System.Drawing.Size(359, 675);
            this.scriptEditor1.TabIndex = 0;
            // 
            // toolStripLeft1
            // 
            this.toolStripLeft1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripLeft1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStripLeft1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDisable,
            this.tsbComments,
            this.toolStripSeparator3,
            this.tsbConveyorBarcode,
            this.tsbBarcode,
            this.tsbHS,
            this.tsbNozzleCheck,
            this.tsbMeasurement,
            this.toolStripSeparator9,
            this.tsbNewPattern,
            this.tsbDoPatten,
            this.tsbMultiPassPatten,
            this.tsbLoopBlock,
            this.tsbPassBlock,
            this.tsbMatrix,
            this.tsbMatrixTimer,
            this.tsbMove,
            this.tsbPurg,
            this.tsbChangeSvSpeed});
            this.toolStripLeft1.Location = new System.Drawing.Point(0, 3);
            this.toolStripLeft1.Name = "toolStripLeft1";
            this.toolStripLeft1.Size = new System.Drawing.Size(44, 652);
            this.toolStripLeft1.TabIndex = 0;
            // 
            // tsbDisable
            // 
            this.tsbDisable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDisable.Image = global::Anda.Fluid.App.Properties.Resources.Unavailable_30px;
            this.tsbDisable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDisable.Name = "tsbDisable";
            this.tsbDisable.Size = new System.Drawing.Size(42, 34);
            this.tsbDisable.Text = "Disable";
            this.tsbDisable.Click += new System.EventHandler(this.tsbDisable_Click);
            // 
            // tsbComments
            // 
            this.tsbComments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbComments.Image = global::Anda.Fluid.App.Properties.Resources.Comments_30px;
            this.tsbComments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbComments.Name = "tsbComments";
            this.tsbComments.Size = new System.Drawing.Size(42, 34);
            this.tsbComments.Text = "Comment";
            this.tsbComments.Click += new System.EventHandler(this.tsbComments_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(42, 6);
            // 
            // tsbConveyorBarcode
            // 
            this.tsbConveyorBarcode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbConveyorBarcode.Image = global::Anda.Fluid.App.Properties.Resources.Barcode_Scanner_30px;
            this.tsbConveyorBarcode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConveyorBarcode.Name = "tsbConveyorBarcode";
            this.tsbConveyorBarcode.Size = new System.Drawing.Size(42, 34);
            this.tsbConveyorBarcode.Text = "toolStripButton1";
            this.tsbConveyorBarcode.Click += new System.EventHandler(this.tsbConveyorBarcode_Click);
            // 
            // tsbBarcode
            // 
            this.tsbBarcode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBarcode.Image = global::Anda.Fluid.App.Properties.Resources.QR_Code_30px;
            this.tsbBarcode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBarcode.Name = "tsbBarcode";
            this.tsbBarcode.Size = new System.Drawing.Size(42, 34);
            this.tsbBarcode.Text = "toolStripButton1";
            this.tsbBarcode.Click += new System.EventHandler(this.tsbBarcode_Click);
            // 
            // tsbHS
            // 
            this.tsbHS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHS.Image = global::Anda.Fluid.App.Properties.Resources.Height_30px;
            this.tsbHS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHS.Name = "tsbHS";
            this.tsbHS.Size = new System.Drawing.Size(42, 34);
            this.tsbHS.Text = "toolStripButton1";
            this.tsbHS.Click += new System.EventHandler(this.tsbHS_Click);
            // 
            // tsbNozzleCheck
            // 
            this.tsbNozzleCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNozzleCheck.Image = global::Anda.Fluid.App.Properties.Resources.Ok_30px;
            this.tsbNozzleCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNozzleCheck.Name = "tsbNozzleCheck";
            this.tsbNozzleCheck.Size = new System.Drawing.Size(42, 34);
            this.tsbNozzleCheck.Text = "Nozzle Check";
            this.tsbNozzleCheck.Click += new System.EventHandler(this.tsbNozzleCheck_Click);
            // 
            // tsbMeasurement
            // 
            this.tsbMeasurement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMeasurement.Image = global::Anda.Fluid.App.Properties.Resources.Ruler_30px;
            this.tsbMeasurement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMeasurement.Name = "tsbMeasurement";
            this.tsbMeasurement.Size = new System.Drawing.Size(42, 34);
            this.tsbMeasurement.Text = "toolStripButton1";
            this.tsbMeasurement.Click += new System.EventHandler(this.tsbMeasurement_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(42, 6);
            // 
            // tsbNewPattern
            // 
            this.tsbNewPattern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewPattern.Image = global::Anda.Fluid.App.Properties.Resources.New_View_30px;
            this.tsbNewPattern.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewPattern.Name = "tsbNewPattern";
            this.tsbNewPattern.Size = new System.Drawing.Size(42, 34);
            this.tsbNewPattern.Text = "toolStripButton1";
            this.tsbNewPattern.Click += new System.EventHandler(this.tsbNewPattern_Click);
            // 
            // tsbDoPatten
            // 
            this.tsbDoPatten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDoPatten.Image = global::Anda.Fluid.App.Properties.Resources.Open_View_30px;
            this.tsbDoPatten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDoPatten.Name = "tsbDoPatten";
            this.tsbDoPatten.Size = new System.Drawing.Size(42, 34);
            this.tsbDoPatten.Text = "toolStripButton19";
            this.tsbDoPatten.Click += new System.EventHandler(this.tsbPatten_Click);
            // 
            // tsbMultiPassPatten
            // 
            this.tsbMultiPassPatten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMultiPassPatten.Image = global::Anda.Fluid.App.Properties.Resources.Static_Views_30px;
            this.tsbMultiPassPatten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMultiPassPatten.Name = "tsbMultiPassPatten";
            this.tsbMultiPassPatten.Size = new System.Drawing.Size(42, 34);
            this.tsbMultiPassPatten.Text = "toolStripButton20";
            this.tsbMultiPassPatten.Click += new System.EventHandler(this.tsbMultiPassPatten_Click);
            // 
            // tsbLoopBlock
            // 
            this.tsbLoopBlock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLoopBlock.Image = global::Anda.Fluid.App.Properties.Resources.Repeat_30px;
            this.tsbLoopBlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoopBlock.Name = "tsbLoopBlock";
            this.tsbLoopBlock.Size = new System.Drawing.Size(42, 34);
            this.tsbLoopBlock.Text = "toolStripButton21";
            this.tsbLoopBlock.Click += new System.EventHandler(this.tsbLoopBlock_Click);
            // 
            // tsbPassBlock
            // 
            this.tsbPassBlock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPassBlock.Image = global::Anda.Fluid.App.Properties.Resources.Advance_30px;
            this.tsbPassBlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPassBlock.Name = "tsbPassBlock";
            this.tsbPassBlock.Size = new System.Drawing.Size(42, 34);
            this.tsbPassBlock.Text = "toolStripButton22";
            this.tsbPassBlock.Click += new System.EventHandler(this.tsbPassBlock_Click);
            // 
            // tsbMatrix
            // 
            this.tsbMatrix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMatrix.Image = global::Anda.Fluid.App.Properties.Resources.Data_Grid_30px;
            this.tsbMatrix.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMatrix.Name = "tsbMatrix";
            this.tsbMatrix.Size = new System.Drawing.Size(42, 34);
            this.tsbMatrix.Text = "toolStripButton1";
            this.tsbMatrix.Click += new System.EventHandler(this.tsbMatrix_Click);
            // 
            // tsbMatrixTimer
            // 
            this.tsbMatrixTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMatrixTimer.Image = global::Anda.Fluid.App.Properties.Resources.Timer_30px;
            this.tsbMatrixTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMatrixTimer.Name = "tsbMatrixTimer";
            this.tsbMatrixTimer.Size = new System.Drawing.Size(42, 34);
            this.tsbMatrixTimer.Text = "toolStripButton23";
            this.tsbMatrixTimer.Click += new System.EventHandler(this.tsbTimer_Click);
            // 
            // tsbMove
            // 
            this.tsbMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveXYToolStripMenuItem,
            this.moveAbsXYToolStripMenuItem,
            this.moveAbsZToolStripMenuItem,
            this.moveToLocToolStripMenuItem});
            this.tsbMove.Image = global::Anda.Fluid.App.Properties.Resources.Car_30px;
            this.tsbMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMove.Name = "tsbMove";
            this.tsbMove.Size = new System.Drawing.Size(42, 34);
            this.tsbMove.Text = "toolStripDropDownButton1";
            // 
            // moveXYToolStripMenuItem
            // 
            this.moveXYToolStripMenuItem.Name = "moveXYToolStripMenuItem";
            this.moveXYToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.moveXYToolStripMenuItem.Text = "MoveXY";
            this.moveXYToolStripMenuItem.Click += new System.EventHandler(this.moveXYToolStripMenuItem_Click);
            // 
            // moveAbsXYToolStripMenuItem
            // 
            this.moveAbsXYToolStripMenuItem.Name = "moveAbsXYToolStripMenuItem";
            this.moveAbsXYToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.moveAbsXYToolStripMenuItem.Text = "MoveAbsXY";
            this.moveAbsXYToolStripMenuItem.Click += new System.EventHandler(this.moveAbsXYToolStripMenuItem_Click);
            // 
            // moveAbsZToolStripMenuItem
            // 
            this.moveAbsZToolStripMenuItem.Name = "moveAbsZToolStripMenuItem";
            this.moveAbsZToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.moveAbsZToolStripMenuItem.Text = "MoveAbsZ";
            this.moveAbsZToolStripMenuItem.Click += new System.EventHandler(this.moveAbsZToolStripMenuItem_Click);
            // 
            // moveToLocToolStripMenuItem
            // 
            this.moveToLocToolStripMenuItem.Name = "moveToLocToolStripMenuItem";
            this.moveToLocToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.moveToLocToolStripMenuItem.Text = "Move To Loc";
            this.moveToLocToolStripMenuItem.Click += new System.EventHandler(this.moveToLocToolStripMenuItem_Click);
            // 
            // tsbPurg
            // 
            this.tsbPurg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPurg.Image = global::Anda.Fluid.App.Properties.Resources.Broom_30px;
            this.tsbPurg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPurg.Name = "tsbPurg";
            this.tsbPurg.Size = new System.Drawing.Size(42, 34);
            this.tsbPurg.Text = "Purge";
            this.tsbPurg.Click += new System.EventHandler(this.tsbPurg_Click);
            // 
            // tsbChangeSvSpeed
            // 
            this.tsbChangeSvSpeed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChangeSvSpeed.Image = global::Anda.Fluid.App.Properties.Resources.Gears_30px;
            this.tsbChangeSvSpeed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChangeSvSpeed.Name = "tsbChangeSvSpeed";
            this.tsbChangeSvSpeed.Size = new System.Drawing.Size(42, 34);
            this.tsbChangeSvSpeed.Text = "toolStripButton1";
            this.tsbChangeSvSpeed.Click += new System.EventHandler(this.tsbChangeSvSpeed_Click);
            // 
            // toolStripLeft2
            // 
            this.toolStripLeft2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripLeft2.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStripLeft2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMark,
            this.tsbASVMark,
            this.tsbBadMark,
            this.toolStripSeparator17,
            this.tsbAdd,
            this.tsbDot,
            this.tsbSingleLine,
            this.tsbPolyLine,
            this.tsbLine,
            this.tsbArc,
            this.tsbCircle,
            this.tsbSnake,
            this.tsbMultiTraces,
            this.tsbComplexLine,
            this.tsbArray,
            this.tsbMultiPatternArray,
            this.tsbFinishShot,
            this.tsbNormalTimer});
            this.toolStripLeft2.Location = new System.Drawing.Point(44, 3);
            this.toolStripLeft2.Name = "toolStripLeft2";
            this.toolStripLeft2.Size = new System.Drawing.Size(35, 646);
            this.toolStripLeft2.TabIndex = 1;
            // 
            // tsbMark
            // 
            this.tsbMark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMark.Image = global::Anda.Fluid.App.Properties.Resources.Hunt_30px;
            this.tsbMark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMark.Name = "tsbMark";
            this.tsbMark.Size = new System.Drawing.Size(33, 34);
            this.tsbMark.Text = "toolStripButton14";
            this.tsbMark.Click += new System.EventHandler(this.tsbMark_Click);
            // 
            // tsbASVMark
            // 
            this.tsbASVMark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbASVMark.Image = global::Anda.Fluid.App.Properties.Resources.Xbox_A_30px;
            this.tsbASVMark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbASVMark.Name = "tsbASVMark";
            this.tsbASVMark.Size = new System.Drawing.Size(33, 34);
            this.tsbASVMark.Text = "ASV Mark";
            this.tsbASVMark.Click += new System.EventHandler(this.tsbASVMark_Click);
            // 
            // tsbBadMark
            // 
            this.tsbBadMark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBadMark.Image = global::Anda.Fluid.App.Properties.Resources.Location_Off_30px;
            this.tsbBadMark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBadMark.Name = "tsbBadMark";
            this.tsbBadMark.Size = new System.Drawing.Size(33, 34);
            this.tsbBadMark.Text = "Bad Mark";
            this.tsbBadMark.Click += new System.EventHandler(this.tsbBadMark_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(33, 6);
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = global::Anda.Fluid.App.Properties.Resources.Variation_30px;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(33, 34);
            this.tsbAdd.Text = "Disable";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbDot
            // 
            this.tsbDot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDot.Image = global::Anda.Fluid.App.Properties.Resources.Menu_2_30px;
            this.tsbDot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDot.Name = "tsbDot";
            this.tsbDot.Size = new System.Drawing.Size(33, 34);
            this.tsbDot.Text = "toolStripButton15";
            this.tsbDot.Click += new System.EventHandler(this.tsbDot_Click);
            // 
            // tsbSingleLine
            // 
            this.tsbSingleLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSingleLine.Image = global::Anda.Fluid.App.Properties.Resources.Line_30px;
            this.tsbSingleLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSingleLine.Name = "tsbSingleLine";
            this.tsbSingleLine.Size = new System.Drawing.Size(33, 34);
            this.tsbSingleLine.Text = "SingleLine";
            this.tsbSingleLine.Click += new System.EventHandler(this.tsbSingleLine_Click);
            // 
            // tsbPolyLine
            // 
            this.tsbPolyLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPolyLine.Image = global::Anda.Fluid.App.Properties.Resources.Polyline_30px;
            this.tsbPolyLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPolyLine.Name = "tsbPolyLine";
            this.tsbPolyLine.Size = new System.Drawing.Size(33, 34);
            this.tsbPolyLine.Text = "toolStripButton1";
            this.tsbPolyLine.Click += new System.EventHandler(this.tsbPolyLine_Click);
            // 
            // tsbLine
            // 
            this.tsbLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLine.Image = global::Anda.Fluid.App.Properties.Resources.Line_Chart_30px;
            this.tsbLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLine.Name = "tsbLine";
            this.tsbLine.Size = new System.Drawing.Size(33, 34);
            this.tsbLine.Text = "toolStripButton16";
            this.tsbLine.Click += new System.EventHandler(this.tsbLine_Click);
            // 
            // tsbArc
            // 
            this.tsbArc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArc.Image = global::Anda.Fluid.App.Properties.Resources.Rotate_Right_30px;
            this.tsbArc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArc.Name = "tsbArc";
            this.tsbArc.Size = new System.Drawing.Size(33, 34);
            this.tsbArc.Text = "toolStripButton17";
            this.tsbArc.Click += new System.EventHandler(this.tsbArc_Click);
            // 
            // tsbCircle
            // 
            this.tsbCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCircle.Image = global::Anda.Fluid.App.Properties.Resources.circle_16px_29712_easyicon_net;
            this.tsbCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCircle.Name = "tsbCircle";
            this.tsbCircle.Size = new System.Drawing.Size(33, 34);
            this.tsbCircle.Text = "toolStripButton18";
            this.tsbCircle.Click += new System.EventHandler(this.tsbCircle_Click);
            // 
            // tsbSnake
            // 
            this.tsbSnake.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSnake.Image = global::Anda.Fluid.App.Properties.Resources.Align_Justify_30px;
            this.tsbSnake.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSnake.Name = "tsbSnake";
            this.tsbSnake.Size = new System.Drawing.Size(33, 34);
            this.tsbSnake.Text = "toolStripButton1";
            this.tsbSnake.Click += new System.EventHandler(this.tsbSnake_Click);
            // 
            // tsbMultiTraces
            // 
            this.tsbMultiTraces.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMultiTraces.Image = global::Anda.Fluid.App.Properties.Resources.Journey_30px;
            this.tsbMultiTraces.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMultiTraces.Name = "tsbMultiTraces";
            this.tsbMultiTraces.Size = new System.Drawing.Size(33, 34);
            this.tsbMultiTraces.Text = "toolStripButton1";
            this.tsbMultiTraces.Click += new System.EventHandler(this.tsbMultiTraces_Click);
            // 
            // tsbComplexLine
            // 
            this.tsbComplexLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbComplexLine.Image = global::Anda.Fluid.App.Properties.Resources.Swap_30px;
            this.tsbComplexLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbComplexLine.Name = "tsbComplexLine";
            this.tsbComplexLine.Size = new System.Drawing.Size(33, 34);
            this.tsbComplexLine.Text = "toolStripButton18";
            this.tsbComplexLine.Click += new System.EventHandler(this.tsbComplexLine_Click);
            // 
            // tsbArray
            // 
            this.tsbArray.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArray.Image = global::Anda.Fluid.App.Properties.Resources.Grid_30px;
            this.tsbArray.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArray.Name = "tsbArray";
            this.tsbArray.Size = new System.Drawing.Size(33, 34);
            this.tsbArray.Text = "toolStripButton1";
            this.tsbArray.Click += new System.EventHandler(this.tsbArray_Click);
            // 
            // tsbMultiPatternArray
            // 
            this.tsbMultiPatternArray.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMultiPatternArray.Image = global::Anda.Fluid.App.Properties.Resources.Data_Sheet_30px;
            this.tsbMultiPatternArray.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMultiPatternArray.Name = "tsbMultiPatternArray";
            this.tsbMultiPatternArray.Size = new System.Drawing.Size(33, 34);
            this.tsbMultiPatternArray.Text = "toolStripButton1";
            this.tsbMultiPatternArray.Click += new System.EventHandler(this.tsbMultiPatternArray_Click);
            // 
            // tsbFinishShot
            // 
            this.tsbFinishShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFinishShot.Image = global::Anda.Fluid.App.Properties.Resources.Wind_Speed_Less_1_30px;
            this.tsbFinishShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFinishShot.Name = "tsbFinishShot";
            this.tsbFinishShot.Size = new System.Drawing.Size(33, 34);
            this.tsbFinishShot.Text = "FinishShot";
            this.tsbFinishShot.Click += new System.EventHandler(this.tsbFinishShot_Click);
            // 
            // tsbNormalTimer
            // 
            this.tsbNormalTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNormalTimer.Image = global::Anda.Fluid.App.Properties.Resources.Time_30px;
            this.tsbNormalTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNormalTimer.Name = "tsbNormalTimer";
            this.tsbNormalTimer.Size = new System.Drawing.Size(33, 34);
            this.tsbNormalTimer.Text = "toolStripButton1";
            this.tsbNormalTimer.Click += new System.EventHandler(this.tsbNormalTimer_Click);
            // 
            // toolStripTop1
            // 
            this.toolStripTop1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripTop1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStripTop1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.toolStripSeparator11,
            this.tsbCopy,
            this.tsbCut,
            this.tsbPaste,
            this.tsbMovePrev,
            this.tsbMoveNext,
            this.tsbBatchUpdate,
            this.toolStripSeparator12,
            this.tsbLastStep,
            this.tsbNextStep,
            this.toolStripSeparator14,
            this.tsbSetting,
            this.tsbMotionSetting,
            this.toolStripSeparator5,
            this.tsbSystemLoc,
            this.tsbInspection,
            this.toolStripSeparator6,
            this.tsbDoScale,
            this.tsbDoPurge,
            this.toolStripSeparator7,
            this.tsbCorrectMark,
            this.toolStripSeparator4,
            this.tsbLoadTraj,
            this.tsbFineTune,
            this.btnBlobs});
            this.toolStripTop1.Location = new System.Drawing.Point(3, 0);
            this.toolStripTop1.Name = "toolStripTop1";
            this.toolStripTop1.Size = new System.Drawing.Size(768, 37);
            this.toolStripTop1.TabIndex = 0;
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = global::Anda.Fluid.App.Properties.Resources.New_File_30px;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(34, 34);
            this.tsbNew.Text = "toolStripButton1";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::Anda.Fluid.App.Properties.Resources.Open_30px;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(34, 34);
            this.tsbOpen.Text = "toolStripButton2";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::Anda.Fluid.App.Properties.Resources.Save_30px;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(34, 34);
            this.tsbSave.Text = "toolStripButton3";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 37);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Image = global::Anda.Fluid.App.Properties.Resources.Copy_30px;
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(34, 34);
            this.tsbCopy.Text = "toolStripButton5";
            this.tsbCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Image = global::Anda.Fluid.App.Properties.Resources.Cut_30px;
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(34, 34);
            this.tsbCut.Text = "toolStripButton6";
            this.tsbCut.Click += new System.EventHandler(this.tsbCut_Click);
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Image = global::Anda.Fluid.App.Properties.Resources.Paste_30px;
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(34, 34);
            this.tsbPaste.Text = "toolStripButton7";
            this.tsbPaste.Click += new System.EventHandler(this.tsbPaste_Click);
            // 
            // tsbMovePrev
            // 
            this.tsbMovePrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMovePrev.Image = global::Anda.Fluid.App.Properties.Resources.Long_Arrow_Up_30px;
            this.tsbMovePrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMovePrev.Name = "tsbMovePrev";
            this.tsbMovePrev.Size = new System.Drawing.Size(34, 34);
            this.tsbMovePrev.Text = "toolStripButton2";
            this.tsbMovePrev.Click += new System.EventHandler(this.tsbMovePrev_Click);
            // 
            // tsbMoveNext
            // 
            this.tsbMoveNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveNext.Image = global::Anda.Fluid.App.Properties.Resources.Long_Arrow_Down_30px;
            this.tsbMoveNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveNext.Name = "tsbMoveNext";
            this.tsbMoveNext.Size = new System.Drawing.Size(34, 34);
            this.tsbMoveNext.Text = "toolStripButton1";
            this.tsbMoveNext.Click += new System.EventHandler(this.tsbMoveNext_Click);
            // 
            // tsbBatchUpdate
            // 
            this.tsbBatchUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBatchUpdate.Image = global::Anda.Fluid.App.Properties.Resources.Bulleted_List_30px;
            this.tsbBatchUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBatchUpdate.Name = "tsbBatchUpdate";
            this.tsbBatchUpdate.Size = new System.Drawing.Size(34, 34);
            this.tsbBatchUpdate.Text = "batch update";
            this.tsbBatchUpdate.Click += new System.EventHandler(this.tsbBatchUpdate_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 37);
            // 
            // tsbLastStep
            // 
            this.tsbLastStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLastStep.Image = global::Anda.Fluid.App.Properties.Resources.Skip_to_Start_30px;
            this.tsbLastStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLastStep.Name = "tsbLastStep";
            this.tsbLastStep.Size = new System.Drawing.Size(34, 34);
            this.tsbLastStep.Text = "toolStripButton2";
            this.tsbLastStep.Click += new System.EventHandler(this.tsbLastStep_Click);
            // 
            // tsbNextStep
            // 
            this.tsbNextStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNextStep.Image = global::Anda.Fluid.App.Properties.Resources.End_30px;
            this.tsbNextStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNextStep.Name = "tsbNextStep";
            this.tsbNextStep.Size = new System.Drawing.Size(34, 34);
            this.tsbNextStep.Text = "toolStripButton2";
            this.tsbNextStep.Click += new System.EventHandler(this.tsbNextStep_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 37);
            // 
            // tsbSetting
            // 
            this.tsbSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSetting.Image = global::Anda.Fluid.App.Properties.Resources.Settings_30px;
            this.tsbSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetting.Name = "tsbSetting";
            this.tsbSetting.Size = new System.Drawing.Size(34, 34);
            this.tsbSetting.Text = "toolStripButton1";
            this.tsbSetting.Click += new System.EventHandler(this.tsbSetting_Click);
            // 
            // tsbMotionSetting
            // 
            this.tsbMotionSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMotionSetting.Image = global::Anda.Fluid.App.Properties.Resources.Car_30px;
            this.tsbMotionSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMotionSetting.Name = "tsbMotionSetting";
            this.tsbMotionSetting.Size = new System.Drawing.Size(34, 34);
            this.tsbMotionSetting.Text = "toolStripButton1";
            this.tsbMotionSetting.Click += new System.EventHandler(this.tsbMotionSetting_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 37);
            // 
            // tsbSystemLoc
            // 
            this.tsbSystemLoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSystemLoc.Image = global::Anda.Fluid.App.Properties.Resources.Location_30px;
            this.tsbSystemLoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSystemLoc.Name = "tsbSystemLoc";
            this.tsbSystemLoc.Size = new System.Drawing.Size(34, 34);
            this.tsbSystemLoc.Text = "toolStripButton1";
            this.tsbSystemLoc.Click += new System.EventHandler(this.tsbSystemLoc_Click);
            // 
            // tsbInspection
            // 
            this.tsbInspection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInspection.Image = global::Anda.Fluid.App.Properties.Resources.Search_30px;
            this.tsbInspection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInspection.Name = "tsbInspection";
            this.tsbInspection.Size = new System.Drawing.Size(34, 34);
            this.tsbInspection.Text = "toolStripButton1";
            this.tsbInspection.Click += new System.EventHandler(this.tsbInspection_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 37);
            // 
            // tsbDoScale
            // 
            this.tsbDoScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDoScale.Image = global::Anda.Fluid.App.Properties.Resources.Scales_30px;
            this.tsbDoScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDoScale.Name = "tsbDoScale";
            this.tsbDoScale.Size = new System.Drawing.Size(34, 34);
            this.tsbDoScale.Text = "toolStripButton1";
            this.tsbDoScale.Click += new System.EventHandler(this.tsbDoScale_Click);
            // 
            // tsbDoPurge
            // 
            this.tsbDoPurge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDoPurge.Image = global::Anda.Fluid.App.Properties.Resources.Broom_30px;
            this.tsbDoPurge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDoPurge.Name = "tsbDoPurge";
            this.tsbDoPurge.Size = new System.Drawing.Size(34, 34);
            this.tsbDoPurge.Text = "toolStripButton2";
            this.tsbDoPurge.Click += new System.EventHandler(this.tsbDoPurge_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 37);
            // 
            // tsbCorrectMark
            // 
            this.tsbCorrectMark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCorrectMark.Image = global::Anda.Fluid.App.Properties.Resources.Point_Objects_30px;
            this.tsbCorrectMark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCorrectMark.Name = "tsbCorrectMark";
            this.tsbCorrectMark.Size = new System.Drawing.Size(34, 34);
            this.tsbCorrectMark.Text = "toolStripButton2";
            this.tsbCorrectMark.Click += new System.EventHandler(this.tsbCorrectMark_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 37);
            // 
            // tsbLoadTraj
            // 
            this.tsbLoadTraj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLoadTraj.Image = global::Anda.Fluid.App.Properties.Resources.New_Document_30px;
            this.tsbLoadTraj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadTraj.Name = "tsbLoadTraj";
            this.tsbLoadTraj.Size = new System.Drawing.Size(34, 34);
            this.tsbLoadTraj.Text = "离线编程";
            this.tsbLoadTraj.Click += new System.EventHandler(this.tsbLoadTraj_Click);
            // 
            // tsbFineTune
            // 
            this.tsbFineTune.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFineTune.Image = global::Anda.Fluid.App.Properties.Resources.Collect_48px;
            this.tsbFineTune.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFineTune.Name = "tsbFineTune";
            this.tsbFineTune.Size = new System.Drawing.Size(34, 34);
            this.tsbFineTune.Text = "轨迹快捷微调";
            this.tsbFineTune.Click += new System.EventHandler(this.tsbFineTune_Click);
            // 
            // btnBlobs
            // 
            this.btnBlobs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBlobs.Image = global::Anda.Fluid.App.Properties.Resources.Polygon_30px;
            this.btnBlobs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBlobs.Name = "btnBlobs";
            this.btnBlobs.Size = new System.Drawing.Size(34, 34);
            this.btnBlobs.Text = "Blobs";
            this.btnBlobs.Click += new System.EventHandler(this.btnBlobs_Click);
            // 
            // toolStripTop2
            // 
            this.toolStripTop2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripTop2.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStripTop2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFind,
            this.tstFindTrack,
            this.tsbModifyComp,
            this.toolStripSeparator2,
            this.lblRunMode,
            this.tsbFluidMode,
            this.lblRunCycles,
            this.tscCycle,
            this.tsbRun,
            this.tsbSingle,
            this.tsbPause,
            this.tsbAbort,
            this.toolStripSeparator1,
            this.lblConveyor,
            this.tsbConveyorSelected,
            this.tsbConveyorWidth,
            this.tsbBoardIn,
            this.tsbBoardOut});
            this.toolStripTop2.Location = new System.Drawing.Point(3, 37);
            this.toolStripTop2.Name = "toolStripTop2";
            this.toolStripTop2.Size = new System.Drawing.Size(974, 37);
            this.toolStripTop2.TabIndex = 1;
            // 
            // lblFind
            // 
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(44, 34);
            this.lblFind.Text = "查找：";
            // 
            // tstFindTrack
            // 
            this.tstFindTrack.Name = "tstFindTrack";
            this.tstFindTrack.Size = new System.Drawing.Size(120, 37);
            this.tstFindTrack.TextChanged += new System.EventHandler(this.tstFindTrack_TextChanged);
            // 
            // tsbModifyComp
            // 
            this.tsbModifyComp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModifyComp.Image = global::Anda.Fluid.App.Properties.Resources.wrench_25px;
            this.tsbModifyComp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModifyComp.Name = "tsbModifyComp";
            this.tsbModifyComp.Size = new System.Drawing.Size(34, 34);
            this.tsbModifyComp.Text = "toolStripButton1";
            this.tsbModifyComp.Click += new System.EventHandler(this.tsbModifyComp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 37);
            // 
            // lblRunMode
            // 
            this.lblRunMode.Name = "lblRunMode";
            this.lblRunMode.Size = new System.Drawing.Size(68, 34);
            this.lblRunMode.Text = "运行模式：";
            // 
            // tsbFluidMode
            // 
            this.tsbFluidMode.Name = "tsbFluidMode";
            this.tsbFluidMode.Size = new System.Drawing.Size(120, 37);
            this.tsbFluidMode.SelectedIndexChanged += new System.EventHandler(this.TsbFluidMode_SelectedIndexChanged);
            // 
            // lblRunCycles
            // 
            this.lblRunCycles.Name = "lblRunCycles";
            this.lblRunCycles.Size = new System.Drawing.Size(68, 34);
            this.lblRunCycles.Text = "运行次数：";
            // 
            // tscCycle
            // 
            this.tscCycle.Name = "tscCycle";
            this.tscCycle.Size = new System.Drawing.Size(75, 37);
            this.tscCycle.SelectedIndexChanged += new System.EventHandler(this.TscCycle_SelectedIndexChanged);
            // 
            // tsbRun
            // 
            this.tsbRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRun.Image = global::Anda.Fluid.App.Properties.Resources.Play_30px;
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(34, 34);
            this.tsbRun.Text = "toolStripButton8";
            this.tsbRun.Click += new System.EventHandler(this.tsbRun_Click);
            // 
            // tsbSingle
            // 
            this.tsbSingle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSingle.Image = global::Anda.Fluid.App.Properties.Resources.Stairs_30px;
            this.tsbSingle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSingle.Name = "tsbSingle";
            this.tsbSingle.Size = new System.Drawing.Size(34, 34);
            this.tsbSingle.Text = "toolStripButton9";
            this.tsbSingle.Click += new System.EventHandler(this.tsbSingle_Click);
            // 
            // tsbPause
            // 
            this.tsbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPause.Image = global::Anda.Fluid.App.Properties.Resources.Pause_30px;
            this.tsbPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPause.Name = "tsbPause";
            this.tsbPause.Size = new System.Drawing.Size(34, 34);
            this.tsbPause.Text = "toolStripButton10";
            this.tsbPause.Click += new System.EventHandler(this.tsbPause_Click);
            // 
            // tsbAbort
            // 
            this.tsbAbort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbort.Image = global::Anda.Fluid.App.Properties.Resources.Cancel_30px;
            this.tsbAbort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbort.Name = "tsbAbort";
            this.tsbAbort.Size = new System.Drawing.Size(34, 34);
            this.tsbAbort.Text = "toolStripButton11";
            this.tsbAbort.Click += new System.EventHandler(this.tsbAbort_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // lblConveyor
            // 
            this.lblConveyor.Name = "lblConveyor";
            this.lblConveyor.Size = new System.Drawing.Size(44, 34);
            this.lblConveyor.Text = "轨道：";
            // 
            // tsbConveyorSelected
            // 
            this.tsbConveyorSelected.Name = "tsbConveyorSelected";
            this.tsbConveyorSelected.Size = new System.Drawing.Size(100, 37);
            this.tsbConveyorSelected.SelectedIndexChanged += new System.EventHandler(this.tsbConveyorSelected_SelectedIndexChanged);
            // 
            // tsbConveyorWidth
            // 
            this.tsbConveyorWidth.BackgroundImage = global::Anda.Fluid.App.Properties.Resources.Width;
            this.tsbConveyorWidth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsbConveyorWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbConveyorWidth.Image = global::Anda.Fluid.App.Properties.Resources.Torrent_30px;
            this.tsbConveyorWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConveyorWidth.Name = "tsbConveyorWidth";
            this.tsbConveyorWidth.Size = new System.Drawing.Size(34, 34);
            this.tsbConveyorWidth.Text = "toolStripButton1";
            this.tsbConveyorWidth.Click += new System.EventHandler(this.tsbConveyorWidth_Click);
            // 
            // tsbBoardIn
            // 
            this.tsbBoardIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBoardIn.Image = global::Anda.Fluid.App.Properties.Resources.BTN_HOLD_ON;
            this.tsbBoardIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBoardIn.Name = "tsbBoardIn";
            this.tsbBoardIn.Size = new System.Drawing.Size(34, 34);
            this.tsbBoardIn.Text = "toolStripButton1";
            this.tsbBoardIn.Click += new System.EventHandler(this.tsbBoardIn_Click);
            // 
            // tsbBoardOut
            // 
            this.tsbBoardOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBoardOut.Image = global::Anda.Fluid.App.Properties.Resources.BTN_HOLD_OFF;
            this.tsbBoardOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBoardOut.Name = "tsbBoardOut";
            this.tsbBoardOut.Size = new System.Drawing.Size(34, 34);
            this.tsbBoardOut.Text = "toolStripButton1";
            this.tsbBoardOut.Click += new System.EventHandler(this.tsbBoardOut_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ProgramControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "ProgramControl";
            this.Size = new System.Drawing.Size(1126, 799);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.toolStripLeft1.ResumeLayout(false);
            this.toolStripLeft1.PerformLayout();
            this.toolStripLeft2.ResumeLayout(false);
            this.toolStripLeft2.PerformLayout();
            this.toolStripTop1.ResumeLayout(false);
            this.toolStripTop1.PerformLayout();
            this.toolStripTop2.ResumeLayout(false);
            this.toolStripTop2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStripLeft1;
        private System.Windows.Forms.ToolStrip toolStripTop1;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripButton tsbPaste;
        private System.Windows.Forms.ToolStripButton tsbDisable;
        private System.Windows.Forms.ToolStripButton tsbComments;
        private System.Windows.Forms.ToolStripButton tsbDoPatten;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbMultiPassPatten;
        private System.Windows.Forms.ToolStripButton tsbLoopBlock;
        private System.Windows.Forms.ToolStripButton tsbPassBlock;
        private System.Windows.Forms.ToolStripButton tsbMatrixTimer;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelRunningState;
        private System.Windows.Forms.ToolStripStatusLabel labelModuleName;
        private System.Windows.Forms.ToolStripStatusLabel labelGrammarCheckInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.CheckBox cbxSimulation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbMatrix;
        private System.Windows.Forms.ToolStripDropDownButton tsbMove;
        private System.Windows.Forms.ToolStripMenuItem moveXYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveAbsXYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveAbsZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToLocToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblCt;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private System.Windows.Forms.ToolStripButton tsbNewPattern;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private Domain.Motion.PositionVControl positionVControl1;
        private System.Windows.Forms.ToolStripButton tsbPurg;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel lblProgramPath;
        private System.Windows.Forms.ToolStripButton tsbChangeSvSpeed;
        private System.Windows.Forms.ToolStripButton tsbNextStep;
        private System.Windows.Forms.ToolStripButton tsbLastStep;
        private System.Windows.Forms.ToolStripButton tsbNozzleCheck;
        private System.Windows.Forms.ToolStripButton tsbBatchUpdate;
        private DrawingPanel.Display.CanvasControll canvasControll1;
        private ScriptEditor scriptEditor1;
        private System.Windows.Forms.ToolStripButton tsbMovePrev;
        private System.Windows.Forms.ToolStripButton tsbMoveNext;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel tslOfflineCycle;
        private System.Windows.Forms.ToolStripButton tsbMeasurement;
        private System.Windows.Forms.ToolStrip toolStripLeft2;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbDot;
        private System.Windows.Forms.ToolStripButton tsbSingleLine;
        private System.Windows.Forms.ToolStripButton tsbPolyLine;
        private System.Windows.Forms.ToolStripButton tsbLine;
        private System.Windows.Forms.ToolStripButton tsbArc;
        private System.Windows.Forms.ToolStripButton tsbCircle;
        private System.Windows.Forms.ToolStripButton tsbSnake;
        private System.Windows.Forms.ToolStripButton tsbArray;
        private System.Windows.Forms.ToolStripButton tsbMultiPatternArray;
        private System.Windows.Forms.ToolStripButton tsbNormalTimer;
        private System.Windows.Forms.ToolStripButton tsbFinishShot;
        private System.Windows.Forms.ToolStripButton tsbBarcode;
        private System.Windows.Forms.ToolStripButton tsbConveyorBarcode;
        private System.Windows.Forms.ToolStripButton tsbMultiTraces;
        private System.Windows.Forms.ToolStripButton tsbMark;
        private System.Windows.Forms.ToolStripButton tsbASVMark;
        private System.Windows.Forms.ToolStripButton tsbBadMark;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripButton tsbComplexLine;
        private System.Windows.Forms.ToolStrip toolStripTop2;
        private System.Windows.Forms.ToolStripButton tsbSetting;
        private System.Windows.Forms.ToolStripButton tsbMotionSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbSystemLoc;
        private System.Windows.Forms.ToolStripButton tsbInspection;
        private System.Windows.Forms.ToolStripTextBox tstFindTrack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox tsbFluidMode;
        private System.Windows.Forms.ToolStripComboBox tscCycle;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.ToolStripButton tsbSingle;
        private System.Windows.Forms.ToolStripButton tsbPause;
        private System.Windows.Forms.ToolStripButton tsbAbort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbHS;
        private System.Windows.Forms.ToolStripButton tsbDoScale;
        private System.Windows.Forms.ToolStripButton tsbDoPurge;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripLabel lblFind;
        private System.Windows.Forms.ToolStripLabel lblRunMode;
        private System.Windows.Forms.ToolStripLabel lblRunCycles;
        private System.Windows.Forms.ToolStripButton tsbCorrectMark;
        private System.Windows.Forms.ToolStripButton tsbFineTune;
        private System.Windows.Forms.ToolStripLabel lblConveyor;
        private System.Windows.Forms.ToolStripComboBox tsbConveyorSelected;
        private System.Windows.Forms.ToolStripButton tsbConveyorWidth;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbLoadTraj;
        public System.Windows.Forms.ToolStripButton tsbBoardIn;
        public System.Windows.Forms.ToolStripButton tsbBoardOut;
        private System.Windows.Forms.ToolStripButton tsbModifyComp;
        private System.Windows.Forms.ToolStripButton btnBlobs;
    }
}
