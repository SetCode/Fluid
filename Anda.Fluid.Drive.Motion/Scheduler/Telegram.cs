using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.Motion.Command;

namespace Anda.Fluid.Drive.Motion.Scheduler
{
    internal class Telegram
    {
        public CmdMsgType Msg { get; set; }
        public bool IsNotifyAll { get; set; }
        public List<IActive> Observers { get; set; }
    }
}
