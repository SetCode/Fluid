using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Communication;

namespace Anda.Fluid.Drive.Sensors.Proportionor
{
    public class ProportionorDisable : ProportionorCom,IProportional
    {
        //public ProportionorDisable()
        //{
        //    this.CommunicationOK = ComCommunicationSts.DISABLE;
        //}

        public ProportionorDisable(EasySerialPort easySerialPort) : base(easySerialPort)
        {
            this.CommunicationOK = ComCommunicationSts.DISABLE;
        }
        public int Channel { get; set; }

        public ComCommunicationSts CommunicationOK { get; private set; }

        public ushort CurrentValue { get; set; }

        public string Name => this.GetType().Name;

        public object Obj => this;

        public bool Connect(TimeSpan timeout)
        {
            return false;
        }

        public void Disconnect()
        {

        }

        public bool SetValue(ushort value)
        {
            this.CommunicationOK = ComCommunicationSts.DISABLE;
            return true;
        }

        public void Update()
        {
            this.CommunicationOK = ComCommunicationSts.DISABLE;
        }
    }
}
