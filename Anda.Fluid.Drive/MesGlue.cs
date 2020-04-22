using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive
{
    public class MesGlue
    {
        public double WeightPattern;
        public void recordWeight()
        {
            string line;
            //记录拼版重量CSV
            if (File.Exists(MesSettings.FilePathDotWeight))
            {
                //时间	型号	 实际重量(mg)	重量范围(mg)	状态	NG原因
                line = "时间,型号,实际重量(mg),重量范围(mg),状态NG原因";
                CsvUtil.WriteLine(MesSettings.FilePathDotWeight, line);

            }
            else
            {
                line = String.Format("{0},{1},{2},{3},{4}", DateTime.Now.ToString(), "", this);
                CsvUtil.WriteLine(MesSettings.FilePathDotWeight, this.WeightPattern.ToString());
            }


            //记录单点重量CSV

        }
    }
}
