using Anda.Fluid.Infrastructure.Common;
using CADImportV1._0.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.CADImport
{
    
    public class CreatePatternPrm
    {
        public static CreatePatternPrm DEFAULT=new CreatePatternPrm();
        public string PatternPath;
        public CompProperty Comp1;//系统
        public CompProperty Comp2;//系统
        public PointD Comp1Pos;//系统 改为机械
        public PointD Comp2Pos;//系统 改为机械
    }
}
