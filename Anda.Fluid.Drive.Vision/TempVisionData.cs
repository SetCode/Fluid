using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision
{
    public class TempVisionData
    {
        private static TempVisionData ins = new TempVisionData();
        private TempVisionData() { }
        public static TempVisionData Ins => ins;


        public ModelFindPrm TempModelFindPrm;


        public void Load()
        {
            ins = JsonUtil.Deserialize<TempVisionData>(typeof(TempVisionData).Name);
            if(ins == null)
            {
                ins = new TempVisionData();
            }
        }

        public bool Save()
        {
            return JsonUtil.Serialize<TempVisionData>(typeof(TempVisionData).Name, ins);
        }
    }
}
