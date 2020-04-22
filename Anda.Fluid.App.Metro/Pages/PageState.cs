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
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Infrastructure.Alarming;

namespace Anda.Fluid.App.Metro.Pages
{
    public partial class PageState : MetroSetUserControl, IControlUpdating
    {
        private const string OK = "OK";
        private const string ERROR = "ERROR";
        private Color defaultForeColor;
        private AlarmTransparencyForm transAlarmForm;
        public PageState()
        {
            InitializeComponent();
            this.defaultForeColor = this.lblMachineState.ForeColor;
            Machine.Instance.FSM.StateChanged += FSM_StateChanged;
            Machine.Instance.LightTower.Opened += LightTower_Opened;
            this.setupAlarms();
            ControlUpatingMgr.Add(this);
        }

        private void setupAlarms()
        {
            this.transAlarmForm = new AlarmTransparencyForm();
            transAlarmForm.TopLevel = false;
            this.metroSetPanel1.Padding = new Padding(5, 5, 5, 5);
            transAlarmForm.Parent = this.metroSetPanel1;
            transAlarmForm.FormBorderStyle = FormBorderStyle.None;
            transAlarmForm.ForeColor = Color.Black;
            transAlarmForm.StartPosition = FormStartPosition.CenterParent;
            transAlarmForm.Dock = DockStyle.Fill;
            AlarmServer.Instance.Register(this.transAlarmForm);
        }

        private void LightTower_Opened(Drive.MachineStates.LightTowerType arg1, bool arg2)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (arg2)
                {
                    switch (arg1)
                    {
                        case Drive.MachineStates.LightTowerType.Red:
                            this.lblMachineState.ForeColor = Color.Red;
                            break;
                        case Drive.MachineStates.LightTowerType.Yellow:
                            this.lblMachineState.ForeColor = Color.Orange;
                            break;
                        case Drive.MachineStates.LightTowerType.Green:
                            this.lblMachineState.ForeColor = Color.Green;
                            break;
                    }
                }
                else
                {
                    this.lblMachineState.ForeColor = this.defaultForeColor;
                }
            }));
        }

        private void FSM_StateChanged(Drive.MachineStates.IMachineStatable obj)
        {
            if (!this.IsHandleCreated)
            {
                return;
            }
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.lblMachineState.Text = obj.StateName;
            }));
        }

        public void Updating()
        {
            this.lblMotionState.Text = Machine.Instance.IsMotionInitDone ? OK : ERROR;
            this.lblMotionState.ForeColor = Machine.Instance.IsMotionInitDone ? Color.Green : Color.Red;
            this.lblVisionState.Text = Machine.Instance.IsVisionInitDone ? OK : ERROR;
            this.lblVisionState.ForeColor = Machine.Instance.IsVisionInitDone ? Color.Green : Color.Red;
            this.lblLaserState.Text = Machine.Instance.Laser.Laserable.CommunicationOK.ToString();
            this.lblLaserState.ForeColor = Machine.Instance.Laser.Laserable.CommunicationOK == ComCommunicationSts.OK ? Color.Green : Color.Red;
            this.lblScaleState.Text = Machine.Instance.Scale.Scalable.CommunicationOK.ToString();
            this.lblScaleState.ForeColor = Machine.Instance.Scale.Scalable.CommunicationOK ==ComCommunicationSts.OK? Color.Green : Color.Red;
            this.lblPropor1State.Text = Machine.Instance.Proportioner1.Proportional.CommunicationOK.ToString();
            this.lblPropor1State.ForeColor = Machine.Instance.Proportioner1.Proportional.CommunicationOK == ComCommunicationSts.OK ? Color.Green : Color.Red;
            if (Machine.Instance.HeaterController1.HeaterControllable is InvalidThermostat)
            {
                this.lblHeaterState.Text = "N/A";
                this.lblHeaterState.ForeColor = Color.Gray;
            }
            else
            {
                this.lblHeaterState.Text = Machine.Instance.HeaterController1.HeaterControllable.CommunicationOK.ToString();
                this.lblHeaterState.ForeColor = Machine.Instance.HeaterController1.HeaterControllable.CommunicationOK == ComCommunicationSts.OK ? Color.Green : Color.Red;
            }
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.单阀)
            {
                this.lblPropor2State.Text = "N/A";
                this.lblPropor2State.ForeColor = Color.Gray;
            }
            else
            {
                this.lblPropor2State.Text = Machine.Instance.Proportioner2.Proportional.CommunicationOK.ToString();
                this.lblPropor2State.ForeColor = Machine.Instance.Proportioner2.Proportional.CommunicationOK == ComCommunicationSts.OK ? Color.Green : Color.Red;
            }

        }
    }
}
