using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Sensors;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Barcode
{
    public class BarcodeScanner : EntityBase<int>, IAlarmSenderable
    {
        public enum Vendor
        {
            SR700 = 0,
            Disable = 1
        }
        public BarcodeScanner(int key):base(key)
        {

        }

        public IBarcodeSannable BarcodeScannable { get; set; }

        public string Name => this.GetType().Name;

        public object Obj => this;

        public BarcodeScanner LoadSetting(BarcodeScanSetting scanSetting)
        {
            if (scanSetting == null)
            {
                this.BarcodeScannable = new BarcodeScannableKencey(scanSetting.EasySerialPort);
                return this;
            }
            switch (scanSetting.Vendor)
            {
                case BarcodeScanner.Vendor.SR700:
                    this.BarcodeScannable = new BarcodeScannableKencey(scanSetting.EasySerialPort);
                    break;
                case BarcodeScanner.Vendor.Disable:
                    this.BarcodeScannable = new BarcodeScannableDisable(scanSetting.EasySerialPort);
                    break;
            }
            return this;
        }

        public BarcodeScanner SetBarcodeScannable(Vendor vendor)
        {
            if (this.Key == 0)
            {
                SensorMgr.Instance.barcodeScanner1.Vendor = vendor;
                switch (vendor)
                {
                    case BarcodeScanner.Vendor.SR700:
                        this.BarcodeScannable = new BarcodeScannableKencey(SensorMgr.Instance.barcodeScanner1.EasySerialPort);
                        break;
                    case BarcodeScanner.Vendor.Disable:
                        this.BarcodeScannable = new BarcodeScannableDisable(SensorMgr.Instance.barcodeScanner1.EasySerialPort);
                        break;
                }
            }
            else
            {
                SensorMgr.Instance.barcodeScanner2.Vendor = vendor;
                switch (vendor)
                {
                    case BarcodeScanner.Vendor.SR700:
                        this.BarcodeScannable = new BarcodeScannableKencey(SensorMgr.Instance.barcodeScanner2.EasySerialPort);
                        break;
                    case BarcodeScanner.Vendor.Disable:
                        this.BarcodeScannable = new BarcodeScannableDisable(SensorMgr.Instance.barcodeScanner2.EasySerialPort);
                        break;
                }
            }
            
            return this;
        }

        public static string GetCmdReadValue(Vendor vendor)
        {
            string rtn = string.Empty;
            switch (vendor)
            {
                case BarcodeScanner.Vendor.SR700:
                    rtn = BarcodeScannableKencey.CMDREADSTRING;
                    break;
                case BarcodeScanner.Vendor.Disable:
                    break;
            }
            return rtn;
        }

        public void SetDelimiter(char Delimiter)
        {
            this.BarcodeScannable.Delimiter = Delimiter;
        }

        public void Init()
        {
            // Ming 20200317
            //open scanner
            this.BarcodeScannable.Disconnect();
            if (!this.BarcodeScannable.Connect(TimeSpan.FromSeconds(1)))
            {
                if (this.BarcodeScannable is BarcodeScannableDisable == false)
                {
                    Logger.DEFAULT.Error(LogCategory.MANUAL | LogCategory.RUNNING, BarcodeScannable.GetType().Name, "serial port fatal");
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.SerialPortOpenAlarm);
                    return;
                }
            }

            //test scanner
            bool flagTest = false;
            string res = "";
            if (this.BarcodeScannable.ReadValue(TimeSpan.FromSeconds(1), out res) >= 0 )
            {
                flagTest = true;
            }
            if (!flagTest)
            {
                Logger.DEFAULT.Error(LogCategory.MANUAL | LogCategory.RUNNING, BarcodeScannable.GetType().Name, "serial port fatal");
                if (this.BarcodeScannable is BarcodeScannableDisable == false)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.SerialPortOpenAlarm);
                }
            }
            else
            {
                Logger.DEFAULT.Info(LogCategory.MANUAL | LogCategory.RUNNING, BarcodeScannable.GetType().Name, "serial port connected successfully");
            }
        }
    }
}
