using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.Command
{
    internal class CloseCommand : MotionCommand
    {
        public override void Run()
        {
            int result = csDmc1000.Dmc1000.d1000_board_close();

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
