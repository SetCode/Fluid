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
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.DeviceType;
using MetroSet_UI.Controls;

namespace Anda.Fluid.App.Metro
{
    public partial class PageSetupAxes : MetroSetUserControl, IMsgSender
    {
        private Timer timer;
        private MetroSetButton btnSelected;
        public PageSetupAxes()
        {
            InitializeComponent();
            this.ShowBorder = false;
            this.btnX.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnY.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnZ.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnA.Mode = MetroSet_UI.Enums.ButtonMode.Selected;
            this.btnB.Mode = MetroSet_UI.Enums.ButtonMode.Selected;

            this.axisControl1.HomeClicked += AxisControl_HomeClicked;

            this.btnX_Click(null, null);

            this.timer = new Timer();
            this.timer.Interval = 50;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            this.axisControl1.Setup(Machine.Instance.Robot.AxisX);
            this.selectButton(btnX);
        }

        private void btnY_Click(object sender, EventArgs e)
        {
            this.axisControl1.Setup(Machine.Instance.Robot.AxisY);
            this.selectButton(btnY);
        }

        private void btnZ_Click(object sender, EventArgs e)
        {
            this.axisControl1.Setup(Machine.Instance.Robot.AxisZ);
            this.selectButton(btnZ);
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            if(Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                return;
            }
            this.axisControl1.Setup(Machine.Instance.Robot.AxisA);
            this.selectButton(btnA);
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                return;
            }
            this.axisControl1.Setup(Machine.Instance.Robot.AxisB);
            this.selectButton(btnB);
        }

        private void selectButton(MetroSetButton btn)
        {
            if(this.btnSelected != null)
            {
                this.btnSelected.Selected = false;
            }
            this.btnSelected = btn;
            this.btnSelected.Selected = true;
        }

        private void AxisControl_HomeClicked(Axis obj)
        {
            AxisType type = (AxisType)obj.Key;
            switch (type)
            {
                case AxisType.X轴:
                    Machine.Instance.Robot.MoveHomeX();
                    break;
                case AxisType.Y轴:
                    Machine.Instance.Robot.MoveHomeY();
                    break;
                case AxisType.Z轴:
                    Machine.Instance.Robot.MoveHomeZ();
                    break;
                case AxisType.A轴:
                    Machine.Instance.Robot.MoveHomeA();
                    break;
                case AxisType.B轴:
                    Machine.Instance.Robot.MoveHomeB();
                    break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.axisControl1?.UpdateAxis();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.axisControl1.Save();
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            this.axisControl1.Reset();
        }

        private void BtnAllDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("将所有的参数设置为默认值?"), "重置",
        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                foreach (var item in AxisMgr.Instance.FindAll())
                {
                    MotionUtil.ResetAxisPrm(item);
                }
            }
        }
    }
}
