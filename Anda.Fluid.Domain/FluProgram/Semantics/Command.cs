using Anda.Fluid.Domain.FluProgram.Grammar;
using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public abstract class Command
    {
        private RunnableModule runnableModule;
        /// <summary>
        /// 所属 RunnableModule，
        /// </summary>
        public RunnableModule RunnableModule
        {
            get { return runnableModule; }
        }

        public Command(RunnableModule runnableModule)
        {
            this.runnableModule = runnableModule;
        }


    }
}