using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Communication;
using Newtonsoft.Json;

namespace Anda.Fluid.Drive.Sensors.Proportionor
{
    /// <summary>
    /// 安达比例阀
    /// </summary>
    public class ProportionorAnda : ProportionorCom ,IProportional
    {
        public ProportionorAnda(EasySerialPort easySerialPort)
            :base(easySerialPort)
        {
        }

        /// <summary>
        /// 通道号 1-4
        /// </summary>
        public int Channel { get; set; }

        public object Obj => this;

        public string Name => this.GetType().Name;

        public ushort CurrentValue { get; private set; }

        public ComCommunicationSts CommunicationOK { get; private set; }

        /// <summary>
        /// 设置气压值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(ushort value)
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            string cmd = string.Format("L{0} {1}F", this.Channel, value);
            //string cmd = "L" + this.Channel.ToString() + value.ToString().PadLeft(4, '0') + "F";
            bool rtn = this.EasySerialPort.Write(cmd);
            if(rtn)
            {
                this.CurrentValue = value;   
            }
            this.CommunicationOK = rtn ? ComCommunicationSts.OK : ComCommunicationSts.ERROR;
            return rtn;
        }

        public void Update()
        {
            if(!this.EasySerialPort.Connected)
            {
                this.CommunicationOK = ComCommunicationSts.ERROR;
            }
        }
    }
}
