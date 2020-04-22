using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.DigitalGage
{
    public class DigitalGage : EntityBase<int>,IAlarmSenderable
    {
        public object Obj => this;
        public string Name => this.GetType().Name;

        public IDigitalGagable DigitalGagable;
        public DigitalGage(int key) : base(key)
        { }

        public DigitalGage LoadSetting(IDigitalGagable digitalGagable)
        {
            if (digitalGagable==null)
            {
                return null;
            }
            this.DigitalGagable = digitalGagable;
            return this;
        }

        public Result Init()
        {
            this.DigitalGagable.Disconnect();
            if (!this.DigitalGagable.Connect(TimeSpan.FromSeconds(1)))
            {
                AlarmServer.Instance.Fire(this,AlarmInfoSensors.SerialPortOpenAlarm);
                return Result.FAILED;
            }
            return Result.OK;
        }
    }
}
