using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Calib;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
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
    public partial class JogMapForm : Form
    {
        public JogMapForm()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.numericUpDown1.Increment = 0.01M;
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Value = 10;
            this.rbnBilinear.Checked = true;
            this.rbnRBF.Checked = false;
        }

        private async void moveDeltaXY(PointD delta)
        {
            PointD target = null;
            if(this.rbnBilinear.Checked)
            {
                Logger.DEFAULT.Info(LogCategory.MANUAL, "test bilinear, current: " + Machine.Instance.Robot.PosXY.ToString());
                target = (Machine.Instance.Robot.PosXY.ToSystem() + delta).ToMachine();
                Logger.DEFAULT.Info(LogCategory.MANUAL, "test bilinear, target: " + target.ToString());
            }
            else
            {
                Logger.DEFAULT.Info(LogCategory.MANUAL, "test RBF2D, current: " + Machine.Instance.Robot.PosXY.ToString());
                PointD currentNet = Machine.Instance.Robot.MachineToNet(Machine.Instance.Robot.PosXY);
                target = Machine.Instance.Robot.NetToMachine(currentNet + delta);
                Logger.DEFAULT.Info(LogCategory.MANUAL, "test RBF2D, target: " + target.ToString());
            }
            this.Enabled = false;
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.Robot.MovePosXYAndReply(target);
            });
            this.Enabled = true;
        }

        private void btnXp_Click(object sender, EventArgs e)
        {
            this.moveDeltaXY(new PointD((double)this.numericUpDown1.Value, 0));
        }

        private void btnXn_Click(object sender, EventArgs e)
        {
            this.moveDeltaXY(new PointD(-(double)this.numericUpDown1.Value, 0));
        }

        private void btnYp_Click(object sender, EventArgs e)
        {
            this.moveDeltaXY(new PointD(0, (double)this.numericUpDown1.Value));
        }

        private void btnYn_Click(object sender, EventArgs e)
        {
            this.moveDeltaXY(new PointD(0, -(double)this.numericUpDown1.Value));
        }

        private void rbnRBF_CheckedChanged(object sender, EventArgs e)
        {
            this.btnLearning.Enabled = this.rbnRBF.Checked;
        }

        private void rbnBilinear_CheckedChanged(object sender, EventArgs e)
        {
            this.btnLearning.Enabled = this.rbnRBF.Checked;
        }

        private async void btnLearning_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            await Task.Factory.StartNew(() =>
            {
                Machine.Instance.Robot.InitRBF2D();
            });
            this.Enabled = true;
        }
    }
}
