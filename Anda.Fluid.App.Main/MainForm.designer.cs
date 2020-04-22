using Anda.Fluid.Infrastructure.GenKey;
using System;
using System.Windows.Forms;

namespace Anda.Fluid.App.Main
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tplProgram = new Anda.Fluid.App.Main.TabPanel();
            this.tplAlarm = new Anda.Fluid.App.Main.TabPanel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tplPos = new Anda.Fluid.App.Main.TabPanel();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tplRunInfo = new Anda.Fluid.App.Main.TabPanel();
            this.tplManual = new Anda.Fluid.App.Main.TabPanel();
            this.pnlNavigate2 = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlNavigate1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMachineState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMotionState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVisionState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLaserState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScaleState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHeaterState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPropor1State = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPropor2State = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1218, 837);
            this.splitContainer1.SplitterDistance = 879;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tplProgram);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tplAlarm);
            this.splitContainer2.Size = new System.Drawing.Size(879, 837);
            this.splitContainer2.SplitterDistance = 646;
            this.splitContainer2.TabIndex = 0;
            // 
            // tplProgram
            // 
            this.tplProgram.Location = new System.Drawing.Point(43, 39);
            this.tplProgram.Name = "tplProgram";
            this.tplProgram.Size = new System.Drawing.Size(479, 322);
            this.tplProgram.TabIndex = 0;
            // 
            // tplAlarm
            // 
            this.tplAlarm.Location = new System.Drawing.Point(308, 55);
            this.tplAlarm.Name = "tplAlarm";
            this.tplAlarm.Size = new System.Drawing.Size(147, 58);
            this.tplAlarm.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tplPos);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(335, 837);
            this.splitContainer3.SplitterDistance = 153;
            this.splitContainer3.TabIndex = 0;
            // 
            // tplPos
            // 
            this.tplPos.Location = new System.Drawing.Point(115, 39);
            this.tplPos.Name = "tplPos";
            this.tplPos.Size = new System.Drawing.Size(65, 92);
            this.tplPos.TabIndex = 2;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.tplRunInfo);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tplManual);
            this.splitContainer4.Size = new System.Drawing.Size(335, 680);
            this.splitContainer4.SplitterDistance = 420;
            this.splitContainer4.TabIndex = 2;
            // 
            // tplRunInfo
            // 
            this.tplRunInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tplRunInfo.Location = new System.Drawing.Point(84, 27);
            this.tplRunInfo.Name = "tplRunInfo";
            this.tplRunInfo.Size = new System.Drawing.Size(142, 361);
            this.tplRunInfo.TabIndex = 1;
            // 
            // tplManual
            // 
            this.tplManual.Location = new System.Drawing.Point(46, 52);
            this.tplManual.Name = "tplManual";
            this.tplManual.Size = new System.Drawing.Size(150, 62);
            this.tplManual.TabIndex = 0;
            // 
            // pnlNavigate2
            // 
            this.pnlNavigate2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNavigate2.Location = new System.Drawing.Point(1314, 0);
            this.pnlNavigate2.Name = "pnlNavigate2";
            this.pnlNavigate2.Size = new System.Drawing.Size(90, 837);
            this.pnlNavigate2.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.splitContainer1);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1218, 837);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlNavigate1
            // 
            this.pnlNavigate1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNavigate1.Location = new System.Drawing.Point(1224, 0);
            this.pnlNavigate1.Name = "pnlNavigate1";
            this.pnlNavigate1.Size = new System.Drawing.Size(90, 837);
            this.pnlNavigate1.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblMachineState,
            this.toolStripStatusLabel2,
            this.lblMotionState,
            this.toolStripStatusLabel3,
            this.lblVisionState,
            this.toolStripStatusLabel4,
            this.lblLaserState,
            this.toolStripStatusLabel5,
            this.lblScaleState,
            this.toolStripStatusLabel6,
            this.lblHeaterState,
            this.toolStripStatusLabel7,
            this.lblPropor1State,
            this.toolStripStatusLabel8,
            this.lblPropor2State});
            this.statusStrip1.Location = new System.Drawing.Point(0, 840);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1404, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusLabel1.Text = "Machine:";
            // 
            // lblMachineState
            // 
            this.lblMachineState.Name = "lblMachineState";
            this.lblMachineState.Size = new System.Drawing.Size(30, 17);
            this.lblMachineState.Text = "Idle";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel2.Text = "Motion:";
            // 
            // lblMotionState
            // 
            this.lblMotionState.Name = "lblMotionState";
            this.lblMotionState.Size = new System.Drawing.Size(26, 17);
            this.lblMotionState.Text = "OK";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(46, 17);
            this.toolStripStatusLabel3.Text = "Vision:";
            // 
            // lblVisionState
            // 
            this.lblVisionState.Name = "lblVisionState";
            this.lblVisionState.Size = new System.Drawing.Size(26, 17);
            this.lblVisionState.Text = "OK";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel4.Text = "Laser:";
            // 
            // lblLaserState
            // 
            this.lblLaserState.Name = "lblLaserState";
            this.lblLaserState.Size = new System.Drawing.Size(26, 17);
            this.lblLaserState.Text = "OK";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(41, 17);
            this.toolStripStatusLabel5.Text = "Scale:";
            // 
            // lblScaleState
            // 
            this.lblScaleState.Name = "lblScaleState";
            this.lblScaleState.Size = new System.Drawing.Size(26, 17);
            this.lblScaleState.Text = "OK";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(50, 17);
            this.toolStripStatusLabel6.Text = "Heater:";
            // 
            // lblHeaterState
            // 
            this.lblHeaterState.Name = "lblHeaterState";
            this.lblHeaterState.Size = new System.Drawing.Size(26, 17);
            this.lblHeaterState.Text = "OK";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel7.Text = "Propor1:";
            // 
            // lblPropor1State
            // 
            this.lblPropor1State.Name = "lblPropor1State";
            this.lblPropor1State.Size = new System.Drawing.Size(26, 17);
            this.lblPropor1State.Text = "OK";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel8.Text = "Propor2:";
            // 
            // lblPropor2State
            // 
            this.lblPropor2State.Name = "lblPropor2State";
            this.lblPropor2State.Size = new System.Drawing.Size(26, 17);
            this.lblPropor2State.Text = "OK";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1404, 862);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlNavigate1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlNavigate2);
            this.Name = "MainForm";
            this.Text = "Main";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private TabPanel tplProgram;
        private TabPanel tplAlarm;
        private TabPanel tplRunInfo;
        private System.Windows.Forms.Panel pnlNavigate2;
        private TabPanel tplPos;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlNavigate1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private TabPanel tplManual;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblMachineState;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblMotionState;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblVisionState;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblLaserState;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel lblScaleState;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel lblHeaterState;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel lblPropor1State;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel lblPropor2State;
    }
}

