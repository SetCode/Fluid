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
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Infrastructure.Msg;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Infrastructure.Reflection;

namespace Anda.Fluid.Domain.Sensors
{
    
    public partial class SettingScaleControl : UserControlEx, IMsgSender
    {
        private ScaleSetting settingBackUp;
        public SettingScaleControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.cbxVendor.Items.Add(Drive.Sensors.Scalage.Scale.Vendor.Sartorius);
            this.cbxVendor.Items.Add(Drive.Sensors.Scalage.Scale.Vendor.Mettler);
            this.cbxVendor.Items.Add(Drive.Sensors.Scalage.Scale.Vendor.Disable);
            this.cbxVendor.SelectedIndex = 0;
            this.cbxVendor.SelectedIndexChanged += CbxVendor_SelectedIndexChanged;
            //this.ckbDTR.Checked = true;
        }

        private void CbxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drive.Sensors.Scalage.Scale.Vendor vendor = (Drive.Sensors.Scalage.Scale.Vendor)this.cbxVendor.SelectedItem;
            Machine.Instance.Scale.SetScalable(vendor);
            CompareObj.CompareProperty(SensorMgr.Instance.Scale, this.settingBackUp, null, this.GetType().Name, true);
            this.txtPrint.Text = Machine.Instance.Scale.Scalable.PrintCmd;
            this.txtTare.Text = Machine.Instance.Scale.Scalable.TareCmd;
            this.txtZero.Text = Machine.Instance.Scale.Scalable.ZeroCmd;

            MsgCenter.Broadcast(MachineMsg.SETUP_INFO, this, null);
            SensorMgr.Instance.Save();
        }

        public SettingScaleControl Setup()
        {
            this.ReadLanguageResources();
            this.cbxVendor.SelectedItem = SensorMgr.Instance.Scale.Vendor;
            this.txtPrint.Text = Machine.Instance.Scale.Scalable.PrintCmd;
            this.txtTare.Text = Machine.Instance.Scale.Scalable.TareCmd;
            this.txtZero.Text = Machine.Instance.Scale.Scalable.ZeroCmd;
            this.ckbDTR.Checked = SensorMgr.Instance.Scale.EasySerialPort.DTR;
            this.ckbRTS.Checked = SensorMgr.Instance.Scale.EasySerialPort.RTS;
            if (SensorMgr.Instance.Scale != null)
            {
                this.settingBackUp = (ScaleSetting)SensorMgr.Instance.Scale.Clone();
            }
            return this;
        }
        


        private void ckbDTR_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.ckbDTR.Checked)
            {
                SensorMgr.Instance.Scale.EasySerialPort.DTR = true;
            }
            else
            {
                SensorMgr.Instance.Scale.EasySerialPort.DTR = false;
            }
            
        }

        private void ckbRTS_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.ckbRTS.Checked)
            {
                SensorMgr.Instance.Scale.EasySerialPort.RTS = true;
            }
            else
            {
                SensorMgr.Instance.Scale.EasySerialPort.RTS = false;
            }
            
        }
    }
}
