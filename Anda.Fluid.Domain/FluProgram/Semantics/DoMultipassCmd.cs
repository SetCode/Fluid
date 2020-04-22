using System;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class DoMultipassCmd : Command
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
        private MeasureHeightCmd associatedMeasureheightCmd = null;
        public MeasureHeightCmd AssociatedMeasureheightCmd
        {
            get { return associatedMeasureheightCmd; }
        }

        public DoMultipassCmd(RunnableModule runnableModule, RunnableModule associatedRunnableModule,MeasureHeightCmd mhCmd) : base(runnableModule)
        {
            this.associatedRunnableModule = associatedRunnableModule;
            this.associatedMeasureheightCmd = mhCmd;
        }
    }
}