using Anda.Fluid.Infrastructure.Trace;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.ASV
{
    [Serializable]
    public class InspectionLine : Inspection
    {
        public InspectionLine(int key) : base(key)
        {

        }

        public InspectionLine()
        {

        }

        [NonSerialized]
        public double PixWidth1;

        [NonSerialized]
        public double PixWidth2;

        [NonSerialized]
        public double PhyWidth1;

        [NonSerialized]
        public double PhyWidth2;

       

        public override bool Execute(byte[] imgData, int imgWidth, int imgHeight)
        {
            if (!base.Execute(imgData, imgWidth, imgHeight))
            {
                return false;
            }
            this.PixWidth1 = 0;
            this.PixWidth2 = 0;
            try
            {
                string[] ss = this.CurrResultStr.Split(',');
                this.PixWidth1 = double.Parse(ss[0]);
                this.PixWidth2 = double.Parse(ss[1]);
                this.IsCurrResultOk = true;
            }
            catch
            {
                this.IsCurrResultOk = false;
            }
            return this.IsCurrResultOk;
        }
      

    }
}
