using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Proportionor
{
    /// <summary>
    /// 比例阀接口
    /// </summary>
    public interface IProportional : IConnectable , IAlarmSenderable, IUpdatable
    {
        ComCommunicationSts CommunicationOK { get; }
        int Channel { get; set; }
        ushort CurrentValue { get; }
        bool SetValue(ushort value);
    }
}
