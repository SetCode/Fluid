using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Sensors;

namespace Anda.Fluid.Drive.Sensors.HeightMeasure
{
    /// <summary>
    /// 激光尺接口无效类
    /// </summary>
    public class LaserableDisable :LaserableCom,  Ilaserable, IAlarmSenderable
    {
        public LaserableDisable(EasySerialPort easySerialPort) :base(easySerialPort)
        {
            this.CommunicationOK = ComCommunicationSts.DISABLE;
        }
        public ComCommunicationSts CommunicationOK { get; private set; }
       
        public string CmdReadValue => "";

        public double ErrorValue => 0;

        public string Name => this.GetType().Name;

        public object Obj => this;

        public Laser.Vendor Vendor => Laser.Vendor.Disable;

        public bool Connect(TimeSpan timeout)
        {
            return false;
        }

        public void Disconnect()
        {

        }

        public int ReadValue(TimeSpan timeout, out double value)
        {
            value = 0;
            this.CommunicationOK = ComCommunicationSts.DISABLE;
            return 0;
        }

        public void Update()
        {
            this.CommunicationOK = ComCommunicationSts.DISABLE;
        }
    }
}
