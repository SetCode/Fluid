using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Drive.Vision.Halcon;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.Semantics
{
    [Serializable]
    public class BlobsCmd : SupportDirectiveCmd
    {
        public BlobsTool BlobsTool { get; private set; } 

        public string SavePath { get; set; } 

        public PointD Position { get; private set; } 

        public BlobsCmd(RunnableModule runnableModule, BlobsCmdLine measurecmdLine) : base(runnableModule, measurecmdLine)
        {
            var structure = runnableModule.CommandsModule.program.ModuleStructure;
            this.Position = structure.ToMachine(runnableModule, measurecmdLine.PosInPattern);
            this.BlobsTool = measurecmdLine.BlobsTool;
            this.SavePath = measurecmdLine.SavePath;
        }

        public override Directive ToDirective(CoordinateCorrector coordinateCorrector)
        {
            return new Blobs(this, coordinateCorrector);
        }
    }
}
