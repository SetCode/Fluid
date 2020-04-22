using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class DoCmd : Command
    {
        private RunnableModule associatedRunnableModule;
        /// <summary>
        /// 关联的 RunnableModule
        /// </summary>
        public RunnableModule AssociatedRunnableModule
        {
            get { return associatedRunnableModule; }
        }

        [NonSerialized]
        private MeasureHeightCmd associatedMeasureHeightCmd = null;
        public MeasureHeightCmd AssociatedMeasureHeightCmd
        {
            get { return associatedMeasureHeightCmd; }
        }

        public DoCmd(RunnableModule runnableModule, RunnableModule associatedRunnableModule, MeasureHeightCmd mhCmd) 
            : base(runnableModule)
        {
            this.associatedRunnableModule = associatedRunnableModule;
            this.associatedMeasureHeightCmd = mhCmd;
        }
    }
}