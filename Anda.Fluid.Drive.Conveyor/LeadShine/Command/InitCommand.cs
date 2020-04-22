using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.Command
{
    internal class InitCommand : MotionCommand
    {
        public override void Run()
        {
            int result = csDmc1000.Dmc1000.d1000_board_init();

            if (result == 0)
            {
                this.State = CommmandState.Failed;
                ConveyorMachine.Instance.Enable = false;
            }
            else
            {
                this.State = CommmandState.Succeed;
                ConveyorMachine.Instance.Enable = true;
            }
        }

        public override void UpdateState()
        {
            
        }
    }
}
