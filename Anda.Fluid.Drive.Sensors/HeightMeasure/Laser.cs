using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Alarming;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Sensors;
using Anda.Fluid.Infrastructure.Interfaces;

namespace Anda.Fluid.Drive.Sensors.HeightMeasure
{
    public class Laser : EntityBase<int>, IAlarmSenderable
    {
        public enum Vendor
        {
            IL,
            SickOD2,
            Disable
        }

        public Laser(int key)
            : base(key)
        {
          
        }

        public Ilaserable Laserable { get; set; }

        public string Name => this.GetType().Name;

        public object Obj => this;

        public Action MeasureHeightBefore;
        public Action MeasureHeightAfter;

        public Laser LoadSetting(LaserSetting laserSetting)
        {
            switch(laserSetting.Vendor)
            {
                case Vendor.IL:
                    this.Laserable = new LaserableIL(laserSetting.EasySerialPort);
                    break;
                case Vendor.SickOD2:
                    this.Laserable = new LaserableSickOD2(laserSetting.EasySerialPort);
                    break;
                case Vendor.Disable:
                    this.Laserable = new LaserableDisable(laserSetting.EasySerialPort);
                    break;
            }
            return this;
        }

        public Laser SetLaserable(Vendor vendor)
        {
            SensorMgr.Instance.Laser.Vendor = vendor;
            switch(vendor)
            {
                case Vendor.IL:
                    this.Laserable = new LaserableIL(SensorMgr.Instance.Laser.EasySerialPort);
                    break;
                case Vendor.SickOD2:
                    this.Laserable = new LaserableSickOD2(SensorMgr.Instance.Laser.EasySerialPort);
                    break;
                case Vendor.Disable:
                    this.Laserable = new LaserableDisable(SensorMgr.Instance.Laser.EasySerialPort);
                    break;
            }
            return this;
        }

        public static string GetCmdReadValue(Vendor vendor)
        {
            string rtn = string.Empty;
            switch (vendor)
            {
                case Vendor.IL:
                    rtn = LaserableIL.CMD_READ_VALUE;
                    break;
                case Vendor.SickOD2:
                    rtn = LaserableSickOD2.GetCmdReadValue();
                    break;
            }
            return rtn;
        }

        public void Init()
        {
            //open laser
            this.Laserable.Disconnect();
            if (!this.Laserable.Connect(TimeSpan.FromSeconds(1)))
            {
                if (this.Laserable is LaserableDisable == false)
                {
                    AlarmServer.Instance.Fire(this, AlarmInfoSensors.SerialPortOpenAlarm);
                }
                return;     // Ming 20200317
            }
            //test laser
            bool flagTest = false;
            for (int i = 0; i < 3; i++)
            {
                double value;
                if (this.Laserable.ReadValue(TimeSpan.FromSeconds(1), out value) >= 0)
                {
                    flagTest = true;
                    break;
                }
            }
            if (!flagTest)
            {
                AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorLaserState);
            }
        }
    }
}
