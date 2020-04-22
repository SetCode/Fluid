using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewROI;

namespace Anda.Fluid.Drive.Vision.Halcon
{
    [Serializable]
    public abstract class HalconTool 
    {
        [NonSerialized]
        public HImage CurrentImage;

        [NonSerialized]
        public HRegion ResultRegion;

        public ROI ROI;

        public abstract bool Execute();
    }
}
