using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.Command
{
    public enum CommandState
    {
        Idel,
        Running,
        Pause,
        Succeed,
        Failed
    }
}
