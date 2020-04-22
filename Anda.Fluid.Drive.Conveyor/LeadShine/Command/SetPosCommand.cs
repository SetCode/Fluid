using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.Command
{
    internal class SetPosCommand : MotionCommand
    {
        private int axisNo;
        private double pos;
        public SetPosCommand(int axisNo,double pos)
        {
            this.axisNo = axisNo;
            this.pos = pos;
        }
        public override void Run()
        {
            int result = csDmc1000.Dmc1000.d1000_set_command_pos(this.axisNo, this.pos);

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
