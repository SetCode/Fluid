using Anda.Fluid.Drive.Motion.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.ActiveItems
{
    public interface IActive
    {
        void Update();
        void HandleMsg(CmdMsgType msgType);
    }
}
