using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.Command
{
    internal class StopCommand : MotionCommand
    {
        private int axisNo;
        public StopCommand(int AxisNo)
        {
            this.axisNo = AxisNo;
        }
        public override void Run()
        {
            int result = csDmc1000.Dmc1000.d1000_decel_stop(this.axisNo);

            if (result != 0)
            {
                this.State = CommmandState.Failed;
            }
            else
            {
                this.State = CommmandState.Succeed;
            }
        }

        public override void UpdateState()
        {
           
        }
    }
}
