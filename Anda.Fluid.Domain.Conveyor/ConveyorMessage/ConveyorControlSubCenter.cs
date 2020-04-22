using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorMessage
{
    public class ConveyorControlSubCenter 
    {
        public void SendMessage(ConveyorControlMsg message)
        {
            ConveyorMsgCenter.Instance.PassMessage(this, message);
        }
        public void ReciveMessage(object message)
        {
           
        }
    }
    public enum ConveyorControlMsg
    {
        轨道1启用,
        轨道2启用,
        轨道1和轨道2同时启用,
        轨道1和轨道2都不启用,

        轨道1卡板解决,
        轨道2卡板解决,

        下层轨道启用,
        下层轨道停用,

        轨道1手动SMEMA进板,
        轨道1手动SMEMA出板,

        轨道2手动SMEMA进板,
        轨道2手动SMEMA出板
    }
}
