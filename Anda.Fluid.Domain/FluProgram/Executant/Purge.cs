using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    //Edite By Shawn
    [Serializable]
    public class Purge : Directive
    {
        public Purge(PurgeCmd purgeCmd)
        {
            this.Valve = purgeCmd.Valve;
            Program = purgeCmd.RunnableModule.CommandsModule.Program;
        }

        public override Result Execute()
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            Result ret = Machine.Instance.Valve1.DoPurgeAndPrime();
            if (Machine.Instance.Setting.ValveSelect == ValveSelection.双阀)
            {
                ret = Machine.Instance.Valve2.DoPurgeAndPrime();
            }
            return ret;
        }
    }
}
