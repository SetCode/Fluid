using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Domain.Vision;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class DialogBase : JogFormBase
    {
        private IOptional optional;
        private bool isCamCtrlFill = false;

        public DialogBase()
        {
            InitializeComponent();

            this.optional = this as IOptional;

            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = this.Size;
           
        }

        public DialogBase(PointD origin) : this()
        {
            this.positionVControl1.SetRelativeOrigin(origin);
          
        }
        public void SetupLight(ExecutePrm prm)
        {
            this.lightSettingControl1.SetupLight(prm);
        }

        protected GroupBox GbxOption => this.gbxOption;
        protected Label LblTitle => this.lblTitle;
        protected CameraControl CamCtrl => this.cameraControl1;
        protected Button BtnPrev => this.btnPrev;
        protected Button BtnNext => this.btnNext;
        protected Button BtnTeach => this.btnTeach;
        protected Button BtnHelp => this.btnHelp;
        protected Button BtnDone => this.btnDone;
        protected Button BtnCancel => this.btnCancel;

        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.optional?.DoPrev();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.optional?.DoNext();
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            this.optional?.DoTeach();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.optional?.DoDone();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.optional?.DoCancel();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W && e.Alt)
            {
                this.change();
                return;
            }
            base.OnKeyDown(e);
        }

        private void change()
        {
            if (!isCamCtrlFill)
            {
                this.gbxCam.Controls.Remove(this.cameraControl1);
                this.gbxCam.Visible = false;
                this.gbxOption.Visible = false;
                this.gbxJog.Visible = false;
                this.gbxContent.Visible = false;
                this.groupBox1.Visible = false;
                this.lblTitle.Visible = false;
                this.positionVControl1.Visible = false;

                this.Controls.Add(this.cameraControl1);
                this.cameraControl1.Dock = DockStyle.Fill;
                this.isCamCtrlFill = true;
            }
            else
            {
                this.Controls.Remove(this.cameraControl1);
                this.gbxCam.Visible = true;
                this.gbxOption.Visible = true;
                this.gbxJog.Visible = true;
                this.gbxContent.Visible = true;
                this.groupBox1.Visible = true;
                this.lblTitle.Visible = true;
                this.positionVControl1.Visible = true;

                this.gbxCam.Controls.Add(this.cameraControl1);
                this.cameraControl1.Dock = DockStyle.Fill;
                this.isCamCtrlFill = false;
            }
        }

      
    }
}
