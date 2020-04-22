using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.Command
{
    internal abstract class MotionCommand
    {
        public CommmandState State { get; protected set; } = CommmandState.Idle;
        public abstract void Run();
        public abstract void UpdateState();
        public AutoResetEvent AutoEvent { get; private set; } = new AutoResetEvent(false);
    }
    internal enum CommmandState
    {
        Idle,
        Running,
        Succeed,
        Abort,
        Failed,
    }
}
