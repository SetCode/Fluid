using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Msg
{
    public class MsgInfo
    {
        public MsgInfo(string msgName, IMsgSender sender, IMsgReceiver receiver, object[] args)
        {
            this.MsgName = msgName;
            this.Sender = sender;
            this.Receiver = receiver;
            this.Args = args;
        }

        public string MsgName { get; set; }

        public IMsgSender Sender { get; set; }

        public IMsgReceiver Receiver { get; set; }

        public object[] Args { get; set; }
    }
}
