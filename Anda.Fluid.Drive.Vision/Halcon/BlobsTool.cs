using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace Anda.Fluid.Drive.Vision.Halcon
{
    [Serializable]
    public class BlobsTool : VisionFindPrmBase
    {
        [NonSerialized]
        public HImage CurrentHImage;

        public byte[] ReferenceImageData;

        public List<Blob> List { get; private set; } = new List<Blob>();

        public override bool Execute()
        {
            bool flag = true;
            foreach (var blob in this.List)
            {
                blob.CurrentImage = this.CurrentHImage;
                if (!blob.Execute())
                {
                    flag = false;
                }
            }
            return flag;
        }
    }

    [Serializable]
    public class Blob : HalconTool
    {
        public int MaxArea { get; set; } = 1000;

        public int MinArea { get; set; } = 100;

        public int MinThreshold { get; set; } = 100;

        public int MaxThreshold { get; set; } = 200;

        public bool Filled { get; set; }

        public override bool Execute()
        {
            this.ROI.error = false;
            if (this.CurrentImage == null || this.ROI == null)
            {
                return false;
            }
            try
            {
                HImage reduceImage = this.CurrentImage.ReduceDomain(this.ROI.getRegion());
                HRegion region = reduceImage.Threshold((double)this.MinThreshold, this.MaxThreshold);
                HRegion connectRegion = region.Connection();
                HRegion selectRegionTmp, selectRegion;
                selectRegion = new HRegion();
                selectRegion.GenEmptyRegion();
                for (int i = 1; i < connectRegion.CountObj(); i++)
                {
                    selectRegionTmp = connectRegion[i].SelectShape("area", "and", (double)this.MinArea, (double)this.MaxArea);
                    if (selectRegionTmp != null)
                    {
                        selectRegion = selectRegion.ConcatObj(selectRegionTmp);
                    }
                }
                HRegion fillRegion;
                if (this.Filled)
                {
                    fillRegion = selectRegion.FillUp();
                }
                else
                {
                    fillRegion = selectRegion;
                }
                if (fillRegion.CountObj() > 0)
                {
                    double areaMax = fillRegion.Area.TupleMax();
                    int areaIndex = fillRegion.Area.TupleFind(areaMax);
                    this.ResultRegion = fillRegion[areaIndex + 1];
                }
                else
                {
                    this.ResultRegion = fillRegion;
                }
                if(this.ResultRegion.Area > 0)
                {
                    return true;
                }
                else
                {
                    this.ROI.error = true;
                    return false;
                }
            }
            catch
            {
                this.ROI.error = true;
                return false;
            }
        }
    }
}
