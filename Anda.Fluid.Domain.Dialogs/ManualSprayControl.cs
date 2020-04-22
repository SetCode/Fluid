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
using Anda.Fluid.Domain.AccessControl.User;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Drive.ValveSystem.Series;
using Anda.Fluid.Drive.ValveSystem;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class ManualSprayControl : UserControl
    {
        private bool Valve1isSpraying = false;
        private bool Valve2isSpraying = false;
        private System.Timers.Timer timer1;
        private System.Timers.Timer timer2;
        public ManualSprayControl()
        {
            InitializeComponent();

            this.nudValve1.Minimum = 0;
            this.nudValve1.Maximum = 32760;
            this.nudAir1.Minimum = 0;
            this.nudAir1.Maximum = 500;
            this.nudValve2.Minimum = 0;
            this.nudValve2.Maximum = 32760;
            this.nudAir2.Minimum = 0;
            this.nudAir2.Maximum = 500;

            if (Machine.Instance.Valve1 != null)
            {
                this.nudAir1.Value = Machine.Instance.Valve1.Proportioner.Proportional.CurrentValue;
            }


            bool hasValve2 = false;
            if (Machine.Instance.Setting != null)
            {
                hasValve2 = Machine.Instance.Setting.ValveSelect == ValveSelection.双阀;
            }
            this.lblValve2.Enabled = hasValve2;
            this.nudValve2.Enabled = hasValve2;
            //if (RoleMgr.Instance.CurrentRole != null)
            //{
            //    this.btnEditValve1.Enabled = RoleMgr.Instance.CurrentRole.OtherFormAccess.CanJogFormValveEdit;
            //    this.btnEditValve2.Enabled = hasValve2 && RoleMgr.Instance.CurrentRole.OtherFormAccess.CanJogFormValveEdit;
            //}
            this.btnValve2Spray.Enabled = hasValve2;
            this.btnValve2Stop.Enabled = hasValve2;
            this.nudAir2.Enabled = hasValve2;
            this.btnAir2.Enabled = hasValve2;
            if (Machine.Instance.Valve2 != null && hasValve2)
            {
                this.nudAir2.Value = Machine.Instance.Valve2.Proportioner.Proportional.CurrentValue;
            }
            timer1 = new System.Timers.Timer();
            timer2 = new System.Timers.Timer();
            timer1.Elapsed += Timer1_Elapsed;
            timer2.Elapsed += Timer2_Elapsed;
            timer1.AutoReset = false;
            timer2.AutoReset = false;

            //HookHotKeyMgr.Instance.EnableValveKey = true;

            //this.ReadLanguageResources();
        }

        private void Timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Valve2isSpraying = false;
            timer2.Stop();
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Valve1isSpraying = false;
            this.timer1.Stop();
        }

        private void btnEditValve1_Click(object sender, EventArgs e)
        {
            new ValveSettingForm().Setup(Machine.Instance.Valve1).ShowDialog(this);
        }

        private void btnEditValve2_Click(object sender, EventArgs e)
        {
            new ValveSettingForm().Setup(Machine.Instance.Valve2).ShowDialog(this);
        }

        private void btnAir1_Click(object sender, EventArgs e)
        {
            Machine.Instance.Valve1.Proportioner.ChangeProgramValue((ushort)nudAir1.Value);
            Machine.Instance.Valve1.Proportioner.Proportional.SetValue((ushort)nudAir1.Value);
        }

        private void btnAir2_Click(object sender, EventArgs e)
        {
            Machine.Instance.Valve2.Proportioner.Proportional.SetValue((ushort)nudAir2.Value);
        }

        private void btnValve1Spray_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Valve1.ValveSeries == ValveSeries.喷射阀)
            {
                JtValve valve1 = (JtValve)Machine.Instance.Valve1;
                if (this.nudValve1.Value == 0)
                {
                    Machine.Instance.Valve1.Spraying();
                    Valve1isSpraying = true;
                }
                else
                {
                    Machine.Instance.Valve1.SprayCycle((short)this.nudValve1.Value);
                    Valve1isSpraying = true;
                    timer1.Interval = (short)this.nudValve1.Value * (Machine.Instance.Valve1.Prm.JtValvePrm.OnTime + Machine.Instance.Valve1.Prm.JtValvePrm.OffTime) / 1000;
                    timer1.Start();
                }
            }
            else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀
                || Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
            {
                Machine.Instance.Valve1.Spraying();
            }
        }

        private void btnValve2Spray_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Valve1.ValveSeries == Drive.ValveSystem.ValveSeries.喷射阀)
            {
                JtValve valve2 = (JtValve)Machine.Instance.Valve2;
                if (this.nudValve2.Value == 0)
                {
                    Machine.Instance.Valve2.Spraying();
                    Valve2isSpraying = true;
                }
                else
                {
                    Machine.Instance.Valve2.SprayCycle((short)this.nudValve2.Value);
                    Valve2isSpraying = true;
                    timer2.Interval = (short)this.nudValve2.Value * (Machine.Instance.Valve2.Prm.JtValvePrm.OnTime + Machine.Instance.Valve2.Prm.JtValvePrm.OffTime) / 1000;
                }
            }
            else if (Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀
                || Machine.Instance.Valve2.ValveSeries == ValveSeries.齿轮泵阀)
            {
                Machine.Instance.Valve2.Spraying();
            }
        }

        private void btnValve1Stop_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Valve1.ValveSeries == Drive.ValveSystem.ValveSeries.喷射阀)
            {
                if (this.Valve1isSpraying)
                {
                    Machine.Instance.Valve1.SprayOff();
                    this.timer1.Stop();
                    this.Valve1isSpraying = false;
                }
            }
            else if (Machine.Instance.Valve1.ValveSeries == ValveSeries.螺杆阀
                || Machine.Instance.Valve1.ValveSeries == ValveSeries.齿轮泵阀)
            {
                Machine.Instance.Valve1.SprayOff();
            }
        }

        private void btnValve2Stop_Click(object sender, EventArgs e)
        {
            if (Machine.Instance.Valve2.ValveSeries == Drive.ValveSystem.ValveSeries.喷射阀)
            {
                if (this.Valve2isSpraying)
                {
                    Machine.Instance.Valve2.SprayOff();
                    this.timer2.Stop();
                    this.Valve2isSpraying = false;
                }
            }
            else if (Machine.Instance.Valve2.ValveSeries == ValveSeries.螺杆阀
                || Machine.Instance.Valve2.ValveSeries == ValveSeries.齿轮泵阀)
            {
                Machine.Instance.Valve2.SprayOff();
            }
        }
    }
}
