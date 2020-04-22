using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Sensors;
using Anda.Fluid.Domain.Sensors.HeaterController;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class FormThermostat : FormEx
    {
        private OmronControlTempCtrl omronController;
        private AikaControlTempCtrl aikaController;
        public FormThermostat()
        {
            HeaterControllerMgr.Instance.FindBy(0).Opeate(OperateHeaterController.打开温控器设定界面时);
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                HeaterControllerMgr.Instance.FindBy(1).Opeate(OperateHeaterController.打开温控器设定界面时);
            }

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            this.label1.Visible = false;
            if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika)
            {
                this.aikaController = new AikaControlTempCtrl();
                this.Size = new Size(this.aikaController.Size.Width + 20, this.aikaController.Size.Height + 40);
                this.aikaController.Parent = this;
                this.aikaController.Location = new Point(0, 0);
                this.aikaController.Show();
            }
            else if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Omron)
            {
                this.omronController = new OmronControlTempCtrl();
                this.Size = new Size(this.omronController.Size.Width + 20, this.omronController.Size.Height + 40);
                this.omronController.Parent = this;
                this.omronController.Location = new Point(0, 0);
                this.omronController.Show();
            }
            else
            {
                this.label1.Visible = true;
            }
            this.ReadLanguageResources();
        }

        private void FormThermostat_FormClosed(object sender, FormClosedEventArgs e)
        {
            HeaterControllerMgr.Instance.FindBy(0).Opeate(OperateHeaterController.关闭温控器设定界面时);
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                HeaterControllerMgr.Instance.FindBy(1).Opeate(OperateHeaterController.关闭温控器设定界面时);
            }
        }
    }
}
