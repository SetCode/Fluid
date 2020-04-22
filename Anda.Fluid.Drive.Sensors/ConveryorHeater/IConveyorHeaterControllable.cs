using Anda.Fluid.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Anda.Fluid.Drive.Sensors.ConveryorHeater
{
  public interface IConveyorHeaterControllable : IConnectable, IUpdatable
    {

        bool SetTemp(double value, int chanelNo);
        bool GetTemp(out double result, int chanelNo);
    //    bool SetAlarmTemp(double value, ToleranceType limit, int chanelNo);
    //    bool GetAlarmTemp(out double result, ToleranceType limit, int chanelNo);
        bool SetTempOffset(double value, int chanelNo);
        bool GetTempOffset(out double result, int chanelNo);
        bool StartHeating(int chanelNo);
        bool StopHeating(int chanelNo);
        bool StartAlarm(int chanelNo);

 

        
    }
}
