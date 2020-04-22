using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Anda.Fluid.Drive.Vision.Measure
{
    public enum MeasureVendor
    {
        ASV
    }

    public enum MeasureType
    {
        圆半径,
        线宽,
        面积,
        胶高
    }
    [Serializable]
    public class MeasurePrm:VisionFindPrmBase,ICloneable
    {
       //[XmlIgnore]
       // public PointD TargetInMachine { get; set; }

        public double Upper = 0;
       
        public double Lower = 0;

        /// <summary>
        /// 胶高上限
        /// </summary>   
        public double ToleranceMax { get; set; } = 10;

        /// <summary>
        /// 胶高下限
        /// </summary> 
        public double ToleranceMin { get; set; } = 0;

        [NonSerialized]
        /// <summary>
        /// 胶高
        /// </summary>
        public double ResHeight = 0;


        [NonSerialized]
        public double PixResult;

        [NonSerialized]
        public double PhyResult = 0;
        

        public MeasureVendor Vendor = MeasureVendor.ASV;

        public MeasureType measureType = MeasureType.线宽;

        /// <summary>
        /// 测量，检测从10开始  ASV
        /// </summary>
        public int InspectionKey = 10;

        

        public override bool Execute()
        {
            if (this.Vendor==MeasureVendor.ASV)
            {
                Inspection inspection= InspectionMgr.Instance.FindBy(this.InspectionKey);
                if (inspection == null)
                {
                    return false;
                }
                inspection.Execute(this.ImgData, this.ImgWidth, this.ImgHeight);
                if (!inspection.IsCurrResultOk)
                {                   
                    return false;
                }
                string[] res = inspection.CurrResultStr.Split(',');
                Logger.DEFAULT.Info(" 结果转换: "+ inspection.CurrResultStr);
                double tmpRes = 0;
                if (!double.TryParse(res[0], out tmpRes))
                {
                    return false;
                }
                this.PixResult = tmpRes;
            }

            return true;
        }

        public bool IsOutofTolerance()
        {
            if (this.PhyResult<this.Lower || this.PhyResult>this.Upper)
            {
                return true;
            }
            if (this.ResHeight < this.ToleranceMin || this.ResHeight > this.ToleranceMax)
            {
                return true;
            }
            return false;
        }

        public bool WidthIsOutofTolerance()
        {
            if (this.PhyResult < this.Lower || this.PhyResult > this.Upper)
            {
                return true;
            }
            return false;
        }
        public static string GetMeasureTypeEn(MeasureType measure)
        {
            string strEn = string.Empty;
            switch (measure)
            {
                case MeasureType.圆半径:
                    strEn = "Radius";
                    break;
                case MeasureType.线宽:
                    strEn = "LineWidth";
                    break;
                case MeasureType.面积:
                    strEn = "Area";
                    break;
                default:
                    break;
            }
            return strEn;
        }

        public object Clone()
        {
            MeasurePrm prm= this.MemberwiseClone() as MeasurePrm;
            prm.ExecutePrm = this.ExecutePrm.Clone() as ExecutePrm;

            return prm;
        }
    }

}
