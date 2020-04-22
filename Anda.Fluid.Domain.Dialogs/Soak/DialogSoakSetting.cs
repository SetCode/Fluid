using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Motion.Locations;
using Anda.Fluid.Drive.ValveSystem;
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

namespace Anda.Fluid.Domain.Dialogs.Soak
{
    public partial class DialogSoakSetting : Form
    {  
        private PointD soakPos;
        private double soakHeight;

        public DialogSoakSetting()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            this.txtLocationX.Text = 0.ToString("0.000");
            this.txtLocationY.Text = 0.ToString("0.000");
            this.txtLocationZ.Text = 0.ToString("0.000");

            this.txtLocationX.Text = Machine.Instance.Robot.SystemLocations.SoakLoc.X.ToString("0.000");
            this.txtLocationY.Text = Machine.Instance.Robot.SystemLocations.SoakLoc.Y.ToString("0.000");
            this.txtLocationZ.Text = Machine.Instance.Robot.SystemLocations.SoakLoc.Z.ToString("0.000");
          
            this.soakPos = new PointD(Machine.Instance.Robot.SystemLocations.SoakLoc.X, Machine.Instance.Robot.SystemLocations.SoakLoc.Y);
            this.soakHeight = Machine.Instance.Robot.SystemLocations.SoakLoc.Z;
        }
      
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.SystemLocations.SoakLoc.X = this.soakPos.X;
            Machine.Instance.Robot.SystemLocations.SoakLoc.Y = this.soakPos.Y;
            Machine.Instance.Robot.SystemLocations.SoakLoc.Z = this.soakHeight;
          
            this.Close();
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 示教XY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeachXY_Click(object sender, EventArgs e)
        {
            this.txtLocationX.Text = Machine.Instance.Robot.PosX.ToString("0.000");
            this.txtLocationY.Text = Machine.Instance.Robot.PosY.ToString("0.000");
            this.soakPos = new PointD(Machine.Instance.Robot.PosX, Machine.Instance.Robot.PosY);
        }
        /// <summary>
        /// 到位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGtoXY_Click(object sender, EventArgs e)
        {
            if (this.soakPos==null)
            {
                return;
            }
            PointD target = new PointD(this.soakPos);
            if (this.rdoCamera.Checked)
            {
                target = this.soakPos;
            }
            else if (this.rdoValve1.Checked)
            {
                target = this.soakPos.ToNeedle(ValveType.Valve1);
            }
            Task.Factory.StartNew(new Action(()=>
            {
                Machine.Instance.Robot.ManualMovePosXYAndReply(target);
            }));
            
        }
        /// <summary>
        /// 示教Z
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeachZ_Click(object sender, EventArgs e)
        {
            this.txtLocationZ.Text = Machine.Instance.Robot.PosZ.ToString("0.000");
            this.soakHeight = Machine.Instance.Robot.PosZ;
        }
        /// <summary>
        /// Z到位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGtoZ_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.MovePosZAndReply(this.soakHeight);
        }
    }
}
