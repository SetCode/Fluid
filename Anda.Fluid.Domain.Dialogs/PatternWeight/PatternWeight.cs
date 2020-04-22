using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Dialogs.PatternWeight
{
    public class PatternWeight
    {
        private Valve valve;

        public void DoPatternWeight()
        {
            Machine.Instance.Robot.MoveSafeZAndReply();
            this.valve.MoveToScaleLoc();
            //打胶前

            //打胶

            //打胶后

        }
        /// <summary>
        /// 
        /// </summary>
        public void getRunnableMoudleByPattern()
        {

        }
    }
}
