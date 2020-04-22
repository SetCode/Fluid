using Anda.Fluid.Infrastructure.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    /// <summary>
    /// 无效的温度控制器类
    /// </summary>
    public class InvalidThermostat : IHeaterControllable
    {
        public ComCommunicationSts CommunicationOK { get; private set; }
        public EasySerialPort EasySerialPort { get; private set; } = null;

        public InvalidThermostat(EasySerialPort easySerialPort)
        {
            this.EasySerialPort = easySerialPort;
        }

        public bool Connect(TimeSpan timeout)
        {
            return true;
        }

        public void Disconnect()
        {
        }

        public bool GetAlarmTemp(out double result, ToleranceType limit, int chanelNo)
        {
            result = 0;
            return true;
        }

        public bool GetTemp(out double result, int chanelNo)
        {
            result = 0;
            return true;
        }

        public bool GetTempOffset(out double result, int chanelNo)
        {
            result = 0;
            return true;
        }

        public bool SetAlarmTemp(double value, ToleranceType limit, int chanelNo)
        {
            return true;
        }


        public bool SetTemp(double value, int chanelNo)
        {
            return false;
        }


        public bool SetTempOffset(double value, int chanelNo)
        {
            return true;
        }

        public bool StartAlarm(int chanelNo)
        {
            return true;
        }

        public bool StartHeating(int chanelNo)
        {
            return true;
        }

        public bool StopHeating(int chanelNo)
        {
            return true;
        }

        public void Update()
        {
            this.CommunicationOK = ComCommunicationSts.DISABLE;
        }
    }
}
