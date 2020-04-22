using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class CommandMoveInc : CommandMovePos
    {
        public CommandMoveInc(Axis[] axes, MovePosPrm[] movePosPrms)
            : base(axes, movePosPrms)
        {
            for (int i = 0; i < axes.Length; i++)
            {
                this.MovePosPrms[i].Pos += this.Axes[i].Pos;
            }
        }
        public CommandMoveInc(Axis axis, MovePosPrm movePosPrm)
            : this(new Axis[] { axis }, new MovePosPrm[] { movePosPrm })
        {

        }
        
    }
}
