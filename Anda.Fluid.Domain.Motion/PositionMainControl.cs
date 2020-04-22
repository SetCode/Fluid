using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.UI;

namespace Anda.Fluid.Domain.Motion
{
    public partial class PositionMainControl : UserControl, IControlUpdating
    {
        public PositionMainControl()
        {
            InitializeComponent();

            this.btnMm.Click += BtnMm_Click;
            this.btnInch.Click += BtnInch_Click;

            ControlUpatingMgr.Add(this);
        }

        public void Updating()
        {
            if(!this.IsHandleCreated)
            {
                return;
            }

            this.lblPosX.Text = Machine.Instance.Robot?.PosX.ToString();
            this.lblPosY.Text = Machine.Instance.Robot?.PosY.ToString();
            this.lblPosZ.Text = Machine.Instance.Robot?.PosZ.ToString();
            if (Machine.Instance.Setting.AxesStyle == Drive.Motion.ActiveItems.RobotAxesStyle.XYZAB)
            {
                this.lblPosA.Text = Machine.Instance.Robot?.PosA.ToString();
                this.lblPosB.Text = Machine.Instance.Robot?.PosB.ToString();
            }
            else if (Machine.Instance.Robot.RobotIsXYZU || Machine.Instance.Robot.RobotIsXYZUV)
            {
                this.lblA.Visible = true;
                this.lblPosA.Visible = true;
                this.lblB.Visible = false;
                this.lblPosB.Visible = false;
                this.lblA.Text = "U:";
                this.lblPosA.Text = Machine.Instance.Robot?.PosU.ToString();
            }
            else if (Machine.Instance.Setting.AxesStyle == Drive.Motion.ActiveItems.RobotAxesStyle.XYZR)
            {
                this.lblA.Visible = true;
                this.lblPosA.Visible = true;
                this.lblB.Visible = false;
                this.lblPosB.Visible = false;
                this.lblA.Text = "R:";
                this.lblPosA.Text = Machine.Instance.Robot?.PosR.ToString();
            }
            else
            {
                this.lblPosA.Text = "N/A";
                this.lblPosB.Text = "N/A";
                this.lblA.Visible = false;
                this.lblPosA.Visible = false;
                this.lblB.Visible = false;
                this.lblPosB.Visible = false;
            }
            
        }

        private void BtnInch_Click(object sender, EventArgs e)
        {
         
        }

        private void BtnMm_Click(object sender, EventArgs e)
        {
           
        }
    }
}
