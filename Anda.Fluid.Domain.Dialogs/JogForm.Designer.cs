using Anda.Fluid.Domain.Motion;

namespace Anda.Fluid.Domain.Dialogs
{
    partial class JogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JogForm));
            this.gbxJog = new System.Windows.Forms.GroupBox();
            this.lightSettingControl1 = new Anda.Fluid.Domain.Vision.LightSettingControl();
            this.nudValve2 = new System.Windows.Forms.NumericUpDown();
            this.lblValve2 = new System.Windows.Forms.Label();
            this.lblValve1 = new System.Windows.Forms.Label();
            this.btnValve2Stop = new System.Windows.Forms.Button();
            this.btnValve2Spray = new System.Windows.Forms.Button();
            this.btnAir2 = new System.Windows.Forms.Button();
            this.nudAir2 = new System.Windows.Forms.NumericUpDown();
            this.btnEditValve2 = new System.Windows.Forms.Button();
            this.btnValve1Stop = new System.Windows.Forms.Button();
            this.btnValve1Spray = new System.Windows.Forms.Button();
            this.btnAir1 = new System.Windows.Forms.Button();
            this.nudAir1 = new System.Windows.Forms.NumericUpDown();
            this.jogControl1 = new Anda.Fluid.Domain.Motion.JogControl();
            this.btnEditValve1 = new System.Windows.Forms.Button();
            this.nudValve1 = new System.Windows.Forms.NumericUpDown();
            this.gbxJog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValve2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAir2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAir1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValve1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxJog
            // 
            this.gbxJog.Controls.Add(this.lightSettingControl1);
            this.gbxJog.Controls.Add(this.nudValve2);
            this.gbxJog.Controls.Add(this.lblValve2);
            this.gbxJog.Controls.Add(this.lblValve1);
            this.gbxJog.Controls.Add(this.btnValve2Stop);
            this.gbxJog.Controls.Add(this.btnValve2Spray);
            this.gbxJog.Controls.Add(this.btnAir2);
            this.gbxJog.Controls.Add(this.nudAir2);
            this.gbxJog.Controls.Add(this.btnEditValve2);
            this.gbxJog.Controls.Add(this.btnValve1Stop);
            this.gbxJog.Controls.Add(this.btnValve1Spray);
            this.gbxJog.Controls.Add(this.btnAir1);
            this.gbxJog.Controls.Add(this.nudAir1);
            this.gbxJog.Controls.Add(this.jogControl1);
            this.gbxJog.Controls.Add(this.btnEditValve1);
            this.gbxJog.Controls.Add(this.nudValve1);
            resources.ApplyResources(this.gbxJog, "gbxJog");
            this.gbxJog.Name = "gbxJog";
            this.gbxJog.TabStop = false;
            // 
            // lightSettingControl1
            // 
            resources.ApplyResources(this.lightSettingControl1, "lightSettingControl1");
            this.lightSettingControl1.Name = "lightSettingControl1";
            // 
            // nudValve2
            // 
            resources.ApplyResources(this.nudValve2, "nudValve2");
            this.nudValve2.Name = "nudValve2";
            // 
            // lblValve2
            // 
            resources.ApplyResources(this.lblValve2, "lblValve2");
            this.lblValve2.Name = "lblValve2";
            // 
            // lblValve1
            // 
            resources.ApplyResources(this.lblValve1, "lblValve1");
            this.lblValve1.Name = "lblValve1";
            // 
            // btnValve2Stop
            // 
            this.btnValve2Stop.Image = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Stop_16px;
            resources.ApplyResources(this.btnValve2Stop, "btnValve2Stop");
            this.btnValve2Stop.Name = "btnValve2Stop";
            this.btnValve2Stop.UseVisualStyleBackColor = true;
            this.btnValve2Stop.Click += new System.EventHandler(this.btnValve2Stop_Click);
            // 
            // btnValve2Spray
            // 
            this.btnValve2Spray.Image = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Play_16px;
            resources.ApplyResources(this.btnValve2Spray, "btnValve2Spray");
            this.btnValve2Spray.Name = "btnValve2Spray";
            this.btnValve2Spray.UseVisualStyleBackColor = true;
            this.btnValve2Spray.Click += new System.EventHandler(this.btnValve2Spray_Click);
            // 
            // btnAir2
            // 
            resources.ApplyResources(this.btnAir2, "btnAir2");
            this.btnAir2.Name = "btnAir2";
            this.btnAir2.UseVisualStyleBackColor = true;
            this.btnAir2.Click += new System.EventHandler(this.btnAir2_Click);
            // 
            // nudAir2
            // 
            resources.ApplyResources(this.nudAir2, "nudAir2");
            this.nudAir2.Name = "nudAir2";
            // 
            // btnEditValve2
            // 
            resources.ApplyResources(this.btnEditValve2, "btnEditValve2");
            this.btnEditValve2.Name = "btnEditValve2";
            this.btnEditValve2.UseVisualStyleBackColor = true;
            this.btnEditValve2.Click += new System.EventHandler(this.btnEditValve2_Click);
            // 
            // btnValve1Stop
            // 
            this.btnValve1Stop.Image = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Stop_16px;
            resources.ApplyResources(this.btnValve1Stop, "btnValve1Stop");
            this.btnValve1Stop.Name = "btnValve1Stop";
            this.btnValve1Stop.UseVisualStyleBackColor = true;
            this.btnValve1Stop.Click += new System.EventHandler(this.btnValve1Stop_Click);
            // 
            // btnValve1Spray
            // 
            this.btnValve1Spray.Image = global::Anda.Fluid.Domain.Dialogs.Properties.Resources.Play_16px;
            resources.ApplyResources(this.btnValve1Spray, "btnValve1Spray");
            this.btnValve1Spray.Name = "btnValve1Spray";
            this.btnValve1Spray.UseVisualStyleBackColor = true;
            this.btnValve1Spray.Click += new System.EventHandler(this.btnValve1Spray_Click);
            // 
            // btnAir1
            // 
            resources.ApplyResources(this.btnAir1, "btnAir1");
            this.btnAir1.Name = "btnAir1";
            this.btnAir1.UseVisualStyleBackColor = true;
            this.btnAir1.Click += new System.EventHandler(this.btnAir1_Click);
            // 
            // nudAir1
            // 
            resources.ApplyResources(this.nudAir1, "nudAir1");
            this.nudAir1.Name = "nudAir1";
            // 
            // jogControl1
            // 
            resources.ApplyResources(this.jogControl1, "jogControl1");
            this.jogControl1.Name = "jogControl1";
            // 
            // btnEditValve1
            // 
            resources.ApplyResources(this.btnEditValve1, "btnEditValve1");
            this.btnEditValve1.Name = "btnEditValve1";
            this.btnEditValve1.UseVisualStyleBackColor = true;
            this.btnEditValve1.Click += new System.EventHandler(this.btnEditValve1_Click);
            // 
            // nudValve1
            // 
            resources.ApplyResources(this.nudValve1, "nudValve1");
            this.nudValve1.Name = "nudValve1";
            // 
            // JogForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxJog);
            this.Name = "JogForm";
            this.gbxJog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudValve2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAir2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAir1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValve1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbxJog;
        private System.Windows.Forms.NumericUpDown nudValve2;
        private System.Windows.Forms.Label lblValve2;
        private System.Windows.Forms.Label lblValve1;
        private System.Windows.Forms.Button btnValve2Stop;
        private System.Windows.Forms.Button btnValve2Spray;
        private System.Windows.Forms.Button btnAir2;
        private System.Windows.Forms.NumericUpDown nudAir2;
        private System.Windows.Forms.Button btnEditValve2;
        private System.Windows.Forms.Button btnValve1Stop;
        private System.Windows.Forms.Button btnValve1Spray;
        private System.Windows.Forms.Button btnAir1;
        private System.Windows.Forms.NumericUpDown nudAir1;
        private JogControl jogControl1;
        private System.Windows.Forms.Button btnEditValve1;
        private System.Windows.Forms.NumericUpDown nudValve1;
        private Vision.LightSettingControl lightSettingControl1;
    }
}