using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.HotKeys;
using Anda.Fluid.Drive.HotKeys.HotKeySort;

namespace Anda.Fluid.App.Metro.Forms
{
    public partial class JogMetro : MetroSetForm
    {
        private bool useValve1 = true;
        private Timer timer;
        public JogMetro()
        {
            InitializeComponent();
            this.AllowResize = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            LoadControl();
            HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.ValveKey, true);
            this.timer = new Timer();
            this.timer.Interval = 100;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
        }

        private void LoadControl()
        {
            this.btnHighOrLow.Click += BtnHighOrLow_Click;

            this.btnXp.MouseDown += BtnXp_MouseDown;
            this.btnXp.MouseUp += BtnXp_MouseUp;
            this.btnXn.MouseDown += BtnXn_MouseDown;
            this.btnXn.MouseUp += BtnXn_MouseUp;

            this.btnYp.MouseDown += BtnYp_MouseDown;
            this.btnYp.MouseUp += BtnYp_MouseUp;
            this.btnYn.MouseDown += BtnYn_MouseDown;
            this.btnYn.MouseUp += BtnYn_MouseUp;

            this.btnZp.MouseDown += BtnZp_MouseDown;
            this.btnZp.MouseUp += BtnZp_MouseUp;
            this.btnZn.MouseDown += BtnZn_MouseDown;
            this.btnZn.MouseUp += BtnZn_MouseUp;

            this.jogComboBox1.Items.Add(0.001);
            this.jogComboBox1.Items.Add(0.002);
            this.jogComboBox1.Items.Add(0.005);
            this.jogComboBox1.Items.Add(0.01);
            this.jogComboBox1.Items.Add(0.02);
            this.jogComboBox1.Items.Add(0.03);
            this.jogComboBox1.Items.Add(0.04);
            this.jogComboBox1.Items.Add(0.05);
            this.jogComboBox1.Items.Add(0.1);
            this.jogComboBox1.Items.Add(0.2);
            this.jogComboBox1.Items.Add(0.3);
            this.jogComboBox1.Items.Add(0.4);
            this.jogComboBox1.Items.Add(0.5);
            this.jogComboBox1.Items.Add(1);
            this.jogComboBox1.Items.Add(2);

            this.checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            this.jogComboBox1.TextChanged += JogComboBox1_TextChanged;

            if (Machine.Instance.Robot != null)
            {
                if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
                {
                    this.btnValveSelect.Enabled = false;
                }
                this.btnValveSelect.Text = this.useValve1 ? "1" : "2";
                this.btnHighOrLow.Text = Machine.Instance.Robot.ManualHighOrLow ? "H" : "L";
                if (!this.jogComboBox1.Items.Contains(Machine.Instance.Robot.ManualIncValue))
                {
                    this.jogComboBox1.Items.Add(Machine.Instance.Robot.ManualIncValue);
                }
                this.jogComboBox1.SelectedItem = Machine.Instance.Robot.ManualIncValue;
                this.checkBox1.Checked = Machine.Instance.Robot.ManualIncOrJog;
            }
        }

        private void CheckBox1_CheckedChanged(object sender)
        {
            if (Machine.Instance.Robot != null)
            {
                Machine.Instance.Robot.ManualIncOrJog = this.checkBox1.Checked;
            }
        }

        private void JogComboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Machine.Instance.Robot.ManualIncValue = double.Parse(this.jogComboBox1.Text);
            }
            catch
            {
                Machine.Instance.Robot.ManualIncValue = 0;
                this.jogComboBox1.Text = "0";
            }
        }

        private void BtnHighOrLow_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.ManualHighOrLow = !Machine.Instance.Robot.ManualHighOrLow;
            this.btnHighOrLow.Text = Machine.Instance.Robot.ManualHighOrLow ? "H" : "L";
        }

        private void btnValveSelect_Click(object sender, EventArgs e)
        {
            this.useValve1 = !this.useValve1;
            this.btnValveSelect.Text = this.useValve1 ? "1" : "2";
        }

        private void BtnZn_MouseUp(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisZ);
        }

        private void BtnZn_MouseDown(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, false, Machine.Instance.Robot.ManualHighOrLow);
        }

        private void BtnZp_MouseUp(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisZ);
        }

        private void BtnZp_MouseDown(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisZ, true, Machine.Instance.Robot.ManualHighOrLow);
        }

        private void BtnXn_MouseUp(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && !this.useValve1 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisA);
            }
            else
            {
                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisX);
            }
        }

        private void BtnXn_MouseDown(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && !this.useValve1 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisA, false, false);
            }
            else
            {
                Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, false, Machine.Instance.Robot.ManualHighOrLow);
            }
        }

        private void BtnXp_MouseUp(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && !this.useValve1 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisA);
            }
            else
            {
                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisX);
            }
        }

        private void BtnXp_MouseDown(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && !this.useValve1 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisA, true, false);
            }
            else
            {
                Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisX, true, Machine.Instance.Robot.ManualHighOrLow);
            }
        }

        private void BtnYn_MouseUp(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && !this.useValve1 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisB);
            }
            else
            {
                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisY);
            }
        }

        private void BtnYn_MouseDown(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && !this.useValve1 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisB, false, false);
            }
            else
            {
                Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, false, Machine.Instance.Robot.ManualHighOrLow);
            }
        }

        private void BtnYp_MouseUp(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && !this.useValve1 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisB);
            }
            else
            {
                Machine.Instance.Robot.ManualMoveStop(Machine.Instance.Robot.AxisY);
            }
        }

        private void BtnYp_MouseDown(object sender, MouseEventArgs e)
        {
            if (Machine.Instance.IsBusy)
            {
                return;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀 && !this.useValve1 && Machine.Instance.Setting.DualValveMode != Drive.ValveSystem.DualValveMode.跟随)
            {
                Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisB, true, false);
            }
            else
            {
                Machine.Instance.Robot.ManualMove(Machine.Instance.Robot.AxisY, true, Machine.Instance.Robot.ManualHighOrLow);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Machine.Instance.Robot == null)
            {
                return;
            }

            if (Machine.Instance.Robot.AxisX.IsPLmt.Value)
            {
                this.btnXp.BackColor = Color.Red;
            }
            else
            {
                this.btnXp.BackColor = SystemColors.Control;
            }
            if (Machine.Instance.Robot.AxisX.IsNLmt.Value)
            {
                this.btnXn.BackColor = Color.Red;
            }
            else
            {
                this.btnXn.BackColor = SystemColors.Control;
            }

            if (Machine.Instance.Robot.AxisY.IsPLmt.Value)
            {
                this.btnYp.BackColor = Color.Red;
            }
            else
            {
                this.btnYp.BackColor = SystemColors.Control;
            }
            if (Machine.Instance.Robot.AxisY.IsNLmt.Value)
            {
                this.btnYn.BackColor = Color.Red;
            }
            else
            {
                this.btnYn.BackColor = SystemColors.Control;
            }

            if (Machine.Instance.Robot.AxisZ.IsPLmt.Value)
            {
                this.btnZp.BackColor = Color.Red;
            }
            else
            {
                this.btnZp.BackColor = SystemColors.Control;
            }
            if (Machine.Instance.Robot.AxisZ.IsNLmt.Value)
            {
                this.btnZn.BackColor = Color.Red;
            }
            else
            {
                this.btnZn.BackColor = SystemColors.Control;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            HookHotKeyMgr.Instance.SetEnable(HotKeySortEnum.ValveKey, false);
        }
    }
}
