using System;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.DomainBase;

namespace Anda.Fluid.Drive.Sensors.ConveryorHeater
{
    class ConveyorHeaterParam : EntityBase<int>, IAlarmSenderable
    {
        public string Name => this.GetType().Name;
        public object Obj => this;

        public enum Vendor
        {
            AiKi,
            Disable
        }
        public ConveyorHeaterParam(int key) : base(key)
        {

        }
    }
}
