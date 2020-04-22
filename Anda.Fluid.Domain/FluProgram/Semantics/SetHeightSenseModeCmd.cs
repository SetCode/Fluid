using Anda.Fluid.Domain.FluProgram.Grammar;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class SetHeightSenseModeCmd : Command
    {
        public SetHeightSenseModeCmd(RunnableModule runnableModule, SetHeightSenseModeCmdLine setHeightSenseModeCmdLine) : base(runnableModule)
        {
            // TODO
        }
    }
}