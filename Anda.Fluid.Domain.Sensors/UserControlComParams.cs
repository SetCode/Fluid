using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.Communication;
using System.IO.Ports;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class UserControlComParams : UserControl
    {
        public UserControlComParams()
        {
            InitializeComponent();
            ComParams = new ComParams();
        }
        public ComParams ComParams { get; private set; } = null;

        private void UserControlComParams_Load(object sender, EventArgs e)
        {
            try
            {
                string[] portNames = SerialPortMgr.GetComPortList();
                foreach (var item in portNames)
                {
                    comboBoxPortName.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SwitchParams()
        {
            try
            {
                this.ComParams.PortName = (string)comboBoxPortName.SelectedItem;
                this.ComParams.DataBits = int.Parse((string)comboBoxDatabits.SelectedItem);
                this.ComParams.BaudRate = int.Parse((string)comboBoxBuadRate.SelectedItem);
                switch ((string)comboBoxStopBit.SelectedItem)
                {
                    case "One":
                        this.ComParams.StopBits = StopBits.One;
                        break;
                    case "OnePointFive":
                        this.ComParams.StopBits = StopBits.OnePointFive;
                        break;
                    case "Two":
                        this.ComParams.StopBits = StopBits.Two;
                        break;
                    default:
                        this.ComParams.StopBits = StopBits.One;
                        break;

                }
                switch ((string)comboBoxParity.SelectedItem)
                {
                    case "None":
                        this.ComParams.Parity = Parity.None;
                        break;
                    case "Odd":
                        this.ComParams.Parity = Parity.Odd;
                        break;
                    case "Space":
                        this.ComParams.Parity = Parity.Space;
                        break;
                    case "Mark":
                        this.ComParams.Parity = Parity.Mark;
                        break;
                    case "Even":
                        this.ComParams.Parity = Parity.Even;
                        break;
                    default:
                        this.ComParams.Parity = Parity.None;
                        break;
                }
            }
            catch (Exception )
            {
               
            }
        }
        public void EnableCombobox(bool trueOrFalse)
        {
            if (trueOrFalse)
            {
                foreach (ComboBox item in flpComParams.Controls)
                {
                    item.Enabled = true;
                }
            }
            else
            {
                foreach (ComboBox item in flpComParams.Controls)
                {
                    item.Enabled = false;
                }
            }
        }
        private void comboBoxPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SwitchParams();
        }
    }
}
