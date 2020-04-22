using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.Command
{
    internal class ChangeSpeedCommand : MotionCommand
    {
        private int axisNo;
        private int newSpeed;
        public ChangeSpeedCommand(int axisNo,int newSpeed)
        {
            this.axisNo = axisNo;
            this.newSpeed = newSpeed;
        }
        public override void Run()
        {
            int result = csDmc1000.Dmc1000.d1000_change_speed(this.axisNo, this.newSpeed);

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
