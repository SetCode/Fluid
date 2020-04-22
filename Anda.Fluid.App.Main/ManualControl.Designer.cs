namespace Anda.Fluid.App.Main
{
    partial class ManualControl
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
            this.btnDownConveyorStart = new System.Windows.Forms.Button();
            this.btnDownConveyorEnd = new System.Windows.Forms.Button();
            this.btnGlueManage = new System.Windows.Forms.Button();
            this.btnBoardExit = new System.Windows.Forms.Button();
            this.btnBoardEnter = new System.Windows.Forms.Button();
            this.btnConveyor = new System.Windows.Forms.Button();
            this.btnLaser = new System.Windows.Forms.Button();
            this.btnScanner = new System.Windows.Forms.Button();
            this.btnHeatIO = new System.Windows.Forms.Button();
            this.btnScale = new System.Windows.Forms.Button();
            this.btnPrime = new System.Windows.Forms.Button();
            this.btnPurge = new System.Windows.Forms.Button();
            this.btnSMEMAOut = new System.Windows.Forms.Button();
            this.btnSMEMAEnter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDownConveyorStart
            // 
            this.btnDownConveyorStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDownConveyorStart.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownConveyorStart.Location = new System.Drawing.Point(140, 134);
            this.btnDownConveyorStart.Name = "btnDownConveyorStart";
            this.btnDownConveyorStart.Size = new System.Drawing.Size(40, 40);
            this.btnDownConveyorStart.TabIndex = 10;
            this.btnDownConveyorStart.Text = "下轨启动";
            this.btnDownConveyorStart.UseVisualStyleBackColor = true;
            this.btnDownConveyorStart.Visible = false;
            this.btnDownConveyorStart.Click += new System.EventHandler(this.btnDownConveyorStart_Click);
            // 
            // btnDownConveyorEnd
            // 
            this.btnDownConveyorEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDownConveyorEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownConveyorEnd.Location = new System.Drawing.Point(200, 134);
            this.btnDownConveyorEnd.Name = "btnDownConveyorEnd";
            this.btnDownConveyorEnd.Size = new System.Drawing.Size(40, 40);
            this.btnDownConveyorEnd.TabIndex = 11;
            this.btnDownConveyorEnd.Text = "下轨停止";
            this.btnDownConveyorEnd.UseVisualStyleBackColor = true;
            this.btnDownConveyorEnd.Visible = false;
            this.btnDownConveyorEnd.Click += new System.EventHandler(this.btnDownConveyorEnd_Click);
            // 
            // btnGlueManage
            // 
            this.btnGlueManage.Image = global::Anda.Fluid.App.Main.Properties.Resources.Water_30px;
            this.btnGlueManage.Location = new System.Drawing.Point(200, 78);
            this.btnGlueManage.Name = "btnGlueManage";
            this.btnGlueManage.Size = new System.Drawing.Size(40, 40);
            this.btnGlueManage.TabIndex = 8;
            this.btnGlueManage.UseVisualStyleBackColor = true;
            this.btnGlueManage.Click += new System.EventHandler(this.btnGlueManage_Click);
            // 
            // btnBoardExit
            // 
            this.btnBoardExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBoardExit.Image = global::Anda.Fluid.App.Main.Properties.Resources.BTN_HOLD_OFF;
            this.btnBoardExit.Location = new System.Drawing.Point(80, 134);
            this.btnBoardExit.Name = "btnBoardExit";
            this.btnBoardExit.Size = new System.Drawing.Size(40, 40);
            this.btnBoardExit.TabIndex = 7;
            this.btnBoardExit.UseVisualStyleBackColor = true;
            this.btnBoardExit.Click += new System.EventHandler(this.btnBoardExit_Click);
            // 
            // btnBoardEnter
            // 
            this.btnBoardEnter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBoardEnter.Image = global::Anda.Fluid.App.Main.Properties.Resources.BTN_HOLD_ON;
            this.btnBoardEnter.Location = new System.Drawing.Point(20, 134);
            this.btnBoardEnter.Name = "btnBoardEnter";
            this.btnBoardEnter.Size = new System.Drawing.Size(40, 40);
            this.btnBoardEnter.TabIndex = 6;
            this.btnBoardEnter.UseVisualStyleBackColor = true;
            this.btnBoardEnter.Click += new System.EventHandler(this.btnBoardEnter_Click);
            // 
            // btnConveyor
            // 
            this.btnConveyor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConveyor.Image = global::Anda.Fluid.App.Main.Properties.Resources.Data_Transfer_30px;
            this.btnConveyor.Location = new System.Drawing.Point(140, 78);
            this.btnConveyor.Name = "btnConveyor";
            this.btnConveyor.Size = new System.Drawing.Size(40, 40);
            this.btnConveyor.TabIndex = 4;
            this.btnConveyor.UseVisualStyleBackColor = true;
            this.btnConveyor.Click += new System.EventHandler(this.btnConveyor_Click);
            // 
            // btnLaser
            // 
            this.btnLaser.Image = global::Anda.Fluid.App.Main.Properties.Resources.laser_16x30;
            this.btnLaser.Location = new System.Drawing.Point(80, 78);
            this.btnLaser.Name = "btnLaser";
            this.btnLaser.Size = new System.Drawing.Size(40, 40);
            this.btnLaser.TabIndex = 5;
            this.btnLaser.UseVisualStyleBackColor = true;
            this.btnLaser.Click += new System.EventHandler(this.btnLaser_Click);
            // 
            // btnScanner
            // 
            this.btnScanner.Image = global::Anda.Fluid.App.Main.Properties.Resources.barcode_scanner_32x32;
            this.btnScanner.Location = new System.Drawing.Point(20, 78);
            this.btnScanner.Name = "btnScanner";
            this.btnScanner.Size = new System.Drawing.Size(40, 40);
            this.btnScanner.TabIndex = 4;
            this.btnScanner.UseVisualStyleBackColor = true;
            this.btnScanner.Click += new System.EventHandler(this.btnScanner_Click);
            // 
            // btnHeatIO
            // 
            this.btnHeatIO.Image = global::Anda.Fluid.App.Main.Properties.Resources.Campfire_30px;
            this.btnHeatIO.Location = new System.Drawing.Point(200, 20);
            this.btnHeatIO.Name = "btnHeatIO";
            this.btnHeatIO.Size = new System.Drawing.Size(40, 40);
            this.btnHeatIO.TabIndex = 3;
            this.btnHeatIO.UseVisualStyleBackColor = true;
            this.btnHeatIO.Click += new System.EventHandler(this.btnHeatIO_Click);
            // 
            // btnScale
            // 
            this.btnScale.Image = global::Anda.Fluid.App.Main.Properties.Resources.Scales_30px;
            this.btnScale.Location = new System.Drawing.Point(140, 20);
            this.btnScale.Name = "btnScale";
            this.btnScale.Size = new System.Drawing.Size(40, 40);
            this.btnScale.TabIndex = 2;
            this.btnScale.UseVisualStyleBackColor = true;
            this.btnScale.Click += new System.EventHandler(this.btnScale_Click);
            // 
            // btnPrime
            // 
            this.btnPrime.Image = global::Anda.Fluid.App.Main.Properties.Resources.Vaccine_Drop_30px;
            this.btnPrime.Location = new System.Drawing.Point(80, 20);
            this.btnPrime.Name = "btnPrime";
            this.btnPrime.Size = new System.Drawing.Size(40, 40);
            this.btnPrime.TabIndex = 1;
            this.btnPrime.UseVisualStyleBackColor = true;
            this.btnPrime.Click += new System.EventHandler(this.btnPrime_Click);
            // 
            // btnPurge
            // 
            this.btnPurge.Image = global::Anda.Fluid.App.Main.Properties.Resources.Broom_30px;
            this.btnPurge.Location = new System.Drawing.Point(20, 20);
            this.btnPurge.Name = "btnPurge";
            this.btnPurge.Size = new System.Drawing.Size(40, 40);
            this.btnPurge.TabIndex = 0;
            this.btnPurge.UseVisualStyleBackColor = true;
            this.btnPurge.Click += new System.EventHandler(this.btnPurge_Click);
            // 
            // btnSMEMAOut
            // 
            this.btnSMEMAOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSMEMAOut.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSMEMAOut.Location = new System.Drawing.Point(200, 133);
            this.btnSMEMAOut.Name = "btnSMEMAOut";
            this.btnSMEMAOut.Size = new System.Drawing.Size(40, 40);
            this.btnSMEMAOut.TabIndex = 13;
            this.btnSMEMAOut.Text = "后端出板";
            this.btnSMEMAOut.UseVisualStyleBackColor = true;
            this.btnSMEMAOut.Click += new System.EventHandler(this.btnSMEMAOut_Click);
            // 
            // btnSMEMAEnter
            // 
            this.btnSMEMAEnter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSMEMAEnter.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnSMEMAEnter.Location = new System.Drawing.Point(140, 133);
            this.btnSMEMAEnter.Name = "btnSMEMAEnter";
            this.btnSMEMAEnter.Size = new System.Drawing.Size(40, 40);
            this.btnSMEMAEnter.TabIndex = 12;
            this.btnSMEMAEnter.Text = "前端进板";
            this.btnSMEMAEnter.UseVisualStyleBackColor = true;
            this.btnSMEMAEnter.Click += new System.EventHandler(this.btnSMEMAEnter_Click);
            // 
            // ManualControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSMEMAOut);
            this.Controls.Add(this.btnSMEMAEnter);
            this.Controls.Add(this.btnDownConveyorEnd);
            this.Controls.Add(this.btnDownConveyorStart);
            this.Controls.Add(this.btnGlueManage);
            this.Controls.Add(this.btnBoardExit);
            this.Controls.Add(this.btnBoardEnter);
            this.Controls.Add(this.btnConveyor);
            this.Controls.Add(this.btnLaser);
            this.Controls.Add(this.btnScanner);
            this.Controls.Add(this.btnHeatIO);
            this.Controls.Add(this.btnScale);
            this.Controls.Add(this.btnPrime);
            this.Controls.Add(this.btnPurge);
            this.Name = "ManualControl";
            this.Size = new System.Drawing.Size(264, 247);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPurge;
        private System.Windows.Forms.Button btnPrime;
        private System.Windows.Forms.Button btnScale;
        private System.Windows.Forms.Button btnHeatIO;

        private System.Windows.Forms.Button btnConveyor;

        private System.Windows.Forms.Button btnScanner;
        private System.Windows.Forms.Button btnLaser;
        private System.Windows.Forms.Button btnBoardEnter;
        private System.Windows.Forms.Button btnBoardExit;
        private System.Windows.Forms.Button btnGlueManage;
        private System.Windows.Forms.Button btnDownConveyorStart;
        private System.Windows.Forms.Button btnDownConveyorEnd;
        private System.Windows.Forms.Button btnSMEMAOut;
        private System.Windows.Forms.Button btnSMEMAEnter;
    }
}
