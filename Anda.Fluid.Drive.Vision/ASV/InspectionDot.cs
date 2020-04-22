using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.ASV
{
    [Serializable]
    public class InspectionDot : Inspection
    {
        public InspectionDot(int key)
            : base(key)
        {

        }

        public InspectionDot()
        {

        }

        [NonSerialized]
        public double PixResultX;

        [NonSerialized]
        public double PixResultY;

        [NonSerialized]
        public double PixResultR;

        [NonSerialized]
        public double PhyResultX;

        [NonSerialized]
        public double PhyResultY;
        [NonSerialized]
        public double PhyResultRadius;
    

        public override bool Execute(byte[] imgData, int imgWidth, int imgHeight)
        {
            if (!base.Execute(imgData, imgWidth, imgHeight))
            {
                return false;
            }
            this.PixResultX = 0;
            this.PixResultY = 0;
            this.PixResultR = 0;
            try
            {
                string[] ss = this.CurrResultStr.Split(',');
                this.PixResultX = double.Parse(ss[0]);
                this.PixResultY = double.Parse(ss[1]);
                if(ss.Length >= 3)
                {
                    this.PixResultR = double.Parse(ss[2]);
                }
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
