using Anda.Fluid.Domain.Motion;
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

namespace Anda.Fluid.App
{
    public partial class EditFormBase : JogFormBase
    {
        private bool isCamCtrlFill = false;

        public EditFormBase()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = this.Size;

        }

        public EditFormBase(PointD Origin) : this()
        {
            this.positionVControl1.SetRelativeOrigin(Origin);
           
        }

        public void SetupLight(ExecutePrm prm)
        {
            this.lightSettingControl1.SetupLight(prm);
        }
        public ExecutePrm GetLightExecutePrm()
        {

            return (ExecutePrm)this.lightSettingControl1.ExecutePrm.Clone();
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
            if (!this.isCamCtrlFill)
            {
                this.gbx1.Visible = false;
                this.gbx2.Visible = false;
                this.jogControl1.Visible = false;
                this.positionVControl1.Visible = false;

                this.panel1.Controls.Remove(this.cameraControl1);
                this.panel1.Visible = false;
                this.Controls.Add(this.cameraControl1);
                this.cameraControl1.Dock = DockStyle.Fill;
                this.isCamCtrlFill = true;
            }
            else 
            {
                this.gbx1.Visible = true;
                this.gbx2.Visible = true;
                this.jogControl1.Visible = true;
                this.positionVControl1.Visible = true;

                this.Controls.Remove(this.cameraControl1);
                this.panel1.Visible = true;
                this.panel1.Controls.Add(this.cameraControl1);
                this.cameraControl1.Dock = DockStyle.Fill;
                this.isCamCtrlFill = false;
            }
        }

       
    }
}
