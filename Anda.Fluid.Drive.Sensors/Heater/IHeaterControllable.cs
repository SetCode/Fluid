using Anda.Fluid.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    public interface IHeaterControllable : IConnectable, IUpdatable
    {
        ComCommunicationSts CommunicationOK { get; }
        bool SetTemp(double value,int chanelNo);
        bool GetTemp(out double result, int chanelNo);
        bool SetAlarmTemp(double value, ToleranceType limit, int chanelNo);
        bool GetAlarmTemp(out double result, ToleranceType limit, int chanelNo);
        bool SetTempOffset(double value, int chanelNo);
        bool GetTempOffset(out double result, int chanelNo);
        bool StartHeating(int chanelNo);
        bool StopHeating(int chanelNo);
        bool StartAlarm(int chanelNo);
    }
    public enum ValveHeaterType
    {
        停止加热,
        加热中,
        已达目标温度
    }
}
