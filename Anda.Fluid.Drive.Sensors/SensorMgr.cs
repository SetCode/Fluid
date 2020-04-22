using Anda.Fluid.Drive.Sensors.Barcode;
using Anda.Fluid.Drive.Sensors.Heater;
using Anda.Fluid.Drive.Sensors.HeightMeasure;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using Anda.Fluid.Drive.Sensors.Proportionor;
using Anda.Fluid.Drive.Sensors.Scalage;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors
{
    
    public class SensorMgr
    {
        private static SensorMgr instance = new SensorMgr();
        private SensorMgr() { }
        public static SensorMgr Instance => instance;
        private string path = SettingsPath.PathMachine + "\\" + typeof(SensorMgr).Name;
        public LaserSetting Laser { get; set; }

        public ScaleSetting Scale { get; set; }

        public BarcodeScanSetting barcodeScanner1 { get; set; }

        public BarcodeScanSetting barcodeScanner2 { get; set; }

        public HeaterSetting Heater { get; set; }

        public EasySerialPort Conveyor1 { get; set; }

        public ProportionerSetting Proportioners { get; set; }

        public EasySerialPort DigitalGage { get; set; }

        public EasySerialPort SvValve { get; set; }

        public LightSetting Light { get; set; }

        public string str { get; }

        public bool Save()
        {
            return JsonUtil.Serialize<SensorMgr>(path, this);
        }

        public bool Load()
        {
            instance = JsonUtil.Deserialize<SensorMgr>(path);
            if(instance == null)
            {
                instance = new SensorMgr();
                return false;
            }
            return true;
        }
    }

    public class BarcodeScanSetting : ICloneable
    {
        [CompareAtt("CMP")]
        public EasySerialPort EasySerialPort { get; set; }
        [CompareAtt("CMP")]
        public BarcodeScanner.Vendor Vendor { get; set; }
        public object Clone()
        {
            BarcodeScanSetting setting = new BarcodeScanSetting();
            setting.Vendor = this.Vendor;
            setting.EasySerialPort = (EasySerialPort)this.EasySerialPort.Clone();
            return setting;
        }
    }

    public class LaserSetting : ICloneable
    {
        [CompareAtt("CMP")]
        public EasySerialPort EasySerialPort { get; set; }
        [CompareAtt("CMP")]
        public Laser.Vendor Vendor { get; set; }
        public object Clone()
        {
            LaserSetting setting = new LaserSetting();
            setting.Vendor = this.Vendor;
            setting.EasySerialPort = (EasySerialPort)this.EasySerialPort.Clone();
            return setting;
        }
    }

    public class ScaleSetting : ICloneable
    {
        [CompareAtt("CMP")]
        public EasySerialPort EasySerialPort { get; set; }
        [CompareAtt("CMP")]
        public Scale.Vendor Vendor { get; set; }
        public object Clone()
        {
            ScaleSetting setting = new ScaleSetting();
            setting.Vendor = this.Vendor;
            setting.EasySerialPort = (EasySerialPort)this.EasySerialPort.Clone();
            return setting;
        }
    }

    public class ProportionerSetting : ICloneable
    {
        [CompareAtt("CMP")]
        public EasySerialPort EasySerialPort { get; set; }
        [CompareAtt("CMP")]
        public int Channel1 { get; set; }
        [CompareAtt("CMP")]
        public Proportioner.ControlType ControlType1 { get; set; }
        [CompareAtt("CMP")]
        public int Channel2 { get; set; }
        [CompareAtt("CMP")]
        public Proportioner.ControlType ControlType2 { get; set; }
        public object Clone()
        {
            ProportionerSetting setting = (ProportionerSetting)this.MemberwiseClone();
            setting.EasySerialPort = (EasySerialPort)this.EasySerialPort.Clone();
            return setting;
        }
    }

    public class HeaterSetting
    {
        public EasySerialPort EasySerialPort { get; set; }
        public HeaterControllerMgr.Vendor Vendor { get; set; }
        public void SetParam()
        {
            if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika)
            {
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[0])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                        HeaterPrmMgr.Instance.FindBy(0).Standard[0], 0, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                        HeaterPrmMgr.Instance.FindBy(0).High[0], 0, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                        HeaterPrmMgr.Instance.FindBy(0).Low[0], 0, HeaterControllerMgr.Instance.FindBy(0)));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[1])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                        HeaterPrmMgr.Instance.FindBy(0).Standard[1], 1, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                        HeaterPrmMgr.Instance.FindBy(0).High[1], 1, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                        HeaterPrmMgr.Instance.FindBy(0).Low[1], 1, HeaterControllerMgr.Instance.FindBy(0)));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[2])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                        HeaterPrmMgr.Instance.FindBy(0).Standard[2], 2, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                        HeaterPrmMgr.Instance.FindBy(0).High[2], 2, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                        HeaterPrmMgr.Instance.FindBy(0).Low[2], 3, HeaterControllerMgr.Instance.FindBy(0)));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[3])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                        HeaterPrmMgr.Instance.FindBy(0).Standard[3], 3, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                        HeaterPrmMgr.Instance.FindBy(0).High[3], 3, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                        HeaterPrmMgr.Instance.FindBy(0).Low[3], 3, HeaterControllerMgr.Instance.FindBy(0)));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[4])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                        HeaterPrmMgr.Instance.FindBy(0).Standard[4], 4, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                        HeaterPrmMgr.Instance.FindBy(0).High[4], 4, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                        HeaterPrmMgr.Instance.FindBy(0).Low[4], 4, HeaterControllerMgr.Instance.FindBy(0)));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[5])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                        HeaterPrmMgr.Instance.FindBy(0).Standard[5], 5, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                        HeaterPrmMgr.Instance.FindBy(0).High[5], 5, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                        HeaterPrmMgr.Instance.FindBy(0).Low[5], 5, HeaterControllerMgr.Instance.FindBy(0)));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[6])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                        HeaterPrmMgr.Instance.FindBy(0).Standard[6], 6, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                        HeaterPrmMgr.Instance.FindBy(0).High[6], 6, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                        HeaterPrmMgr.Instance.FindBy(0).Low[6], 6, HeaterControllerMgr.Instance.FindBy(0)));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[7])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                        HeaterPrmMgr.Instance.FindBy(0).Standard[7], 7, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                        HeaterPrmMgr.Instance.FindBy(0).High[7], 7, HeaterControllerMgr.Instance.FindBy(0)));
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                        HeaterPrmMgr.Instance.FindBy(0).Low[7], 7, HeaterControllerMgr.Instance.FindBy(0)));
                }
            }
            else
            {
                HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                    HeaterPrmMgr.Instance.FindBy(0).Standard[0], 0, HeaterControllerMgr.Instance.FindBy(0)));
                HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                    HeaterPrmMgr.Instance.FindBy(0).High[0], 0, HeaterControllerMgr.Instance.FindBy(0)));
                HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                    HeaterPrmMgr.Instance.FindBy(0).Low[0], 0, HeaterControllerMgr.Instance.FindBy(0)));
                HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.设置温度漂移值,
                    HeaterPrmMgr.Instance.FindBy(0).Offset[0], 0, HeaterControllerMgr.Instance.FindBy(0)));

                HeaterControllerMgr.Instance.FindBy(1).Fire(new HeaterMessage(HeaterMsg.设置标准温度值,
                   HeaterPrmMgr.Instance.FindBy(1).Standard[0], 0, HeaterControllerMgr.Instance.FindBy(1)));
                HeaterControllerMgr.Instance.FindBy(1).Fire(new HeaterMessage(HeaterMsg.设置温度上限值,
                    HeaterPrmMgr.Instance.FindBy(1).High[0], 0, HeaterControllerMgr.Instance.FindBy(1)));
                HeaterControllerMgr.Instance.FindBy(1).Fire(new HeaterMessage(HeaterMsg.设置温度下限值,
                    HeaterPrmMgr.Instance.FindBy(1).Low[0], 0, HeaterControllerMgr.Instance.FindBy(1)));
                HeaterControllerMgr.Instance.FindBy(1).Fire(new HeaterMessage(HeaterMsg.设置温度漂移值,
                    HeaterPrmMgr.Instance.FindBy(1).Offset[0], 0, HeaterControllerMgr.Instance.FindBy(1)));
            }
        }

        public void StartHeating()
        {
            if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika)
            {
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[0])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.开始加热,
                        HeaterControllerMgr.Instance.FindBy(0),0));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[1])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.开始加热,
                        HeaterControllerMgr.Instance.FindBy(0), 1));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[2])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.开始加热,
                        HeaterControllerMgr.Instance.FindBy(0), 2));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[3])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.开始加热,
                        HeaterControllerMgr.Instance.FindBy(0), 3));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[4])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.开始加热,
                        HeaterControllerMgr.Instance.FindBy(0), 4));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[5])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.开始加热,
                        HeaterControllerMgr.Instance.FindBy(0), 5));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[6])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.开始加热,
                        HeaterControllerMgr.Instance.FindBy(0), 6));
                }
                if (HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel[7])
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.开始加热,
                        HeaterControllerMgr.Instance.FindBy(0), 7));
                }
            }
        }

       
    }

    public class LightSetting
    {
        public  LightVendor Vendor { get; set; } = LightVendor.Anda;

        //public OPTPrm prm { get; set; }

        [CompareAtt("CMP")]
        public EasySerialPort EasySerialPort { get; set; }
    }
}
