using Anda.Fluid.Drive.Motion.CardFramework.Crd;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.Command
{
    public class CommandMoveTrcTime : CommandMoveTrc
    {
        private DateTime currTime = DateTime.Now;
        private DateTime startTime = DateTime.Now;

        public CommandMoveTrcTime(Axis axisX, Axis axisY, MoveTrcPrm trcPrm, IList<ICrdable> crds, int milliSec)
           : base(axisX, axisY, trcPrm, crds)
        {
            this.MilliSec = milliSec;
        }

        public int MilliSec { get; private set; }

        protected override void CallEx()
        {
            this.currTime = DateTime.Now;
            this.startTime = DateTime.Now;
        }

        protected override bool GuardTime()
        {
            this.currTime = DateTime.Now;
            if(this.currTime - this.startTime > TimeSpan.FromMilliseconds(MilliSec))
            {
                return true;
            }
            return false;
        }
    }
}
