using Anda.Fluid.Drive.Vision.ASV;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.ModelFind
{
    [Serializable]
    public class ModelFindPrm : MarkFindPrmBase, ICloneable
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int ModelId;

        /// <summary>
        /// 模板数据
        /// </summary>
        public byte[] ModelData { get; set; }

        /// <summary>
        /// 模板左上X
        /// </summary>
        public int ModelTopLeftX { get; set; }

        /// <summary>
        /// 模板左上Y
        /// </summary>
        public int ModelTopLeftY { get; set; }

        /// <summary>
        /// 模板宽度
        /// </summary>
        [CompareAtt("CMP")]
        public int ModelWidth { get; set; }

        /// <summary>
        /// 模板高度
        /// </summary>
        [CompareAtt("CMP")]
        public int ModelHeight { get; set; }

        /// <summary>
        /// 可接受得分
        /// </summary>
        [CompareAtt("CMP")]
        public double AcceptScore { get; set; } = 0.6;

        /// <summary>
        /// 匹配得分
        /// </summary>
        [NonSerialized]
        [CompareAtt("CMP")]
        public double Score;

        public void CopyTempDataTo(ModelFindPrm other)
        {
            if(other == null)
            {
                other = new ModelFindPrm();
            }
            other.ModelTopLeftX = this.ModelTopLeftX;
            other.ModelTopLeftY = this.ModelTopLeftY;
            other.ModelWidth = this.ModelWidth;
            other.ModelHeight = this.ModelHeight;
            other.SearchTopLeftX = this.SearchTopLeftX;
            other.SearchTopLeftY = this.SearchTopLeftY;
            other.SearchWidth = this.SearchWidth;
            other.SearchHeight = this.SearchHeight;
            other.AcceptScore = this.AcceptScore;
            other.SettlingTime = this.SettlingTime;
            other.Tolerance = this.Tolerance;
        }

        public override bool Init()
        {
            if (this.ModelData == null)
            {
                return false;
            }

            int rtn = ModelFindThm.CreateModel(this.ModelData, this.ModelWidth, this.ModelHeight,
                this.SearchTopLeftX, this.SearchTopLeftY, this.SearchWidth, this.SearchHeight, ref this.ModelId);

            return rtn == 0;
        }

        public override bool Execute()
        {
            if(this.ImgData == null)
            {
                return false;
            }

            if(this.IsUnStandard)
            {
                Inspection inspection = InspectionMgr.Instance.FindBy(InspectionKey);
                if(inspection == null)
                {
                    return false;
                }
                inspection.Execute(this.ImgData, this.ImgWidth, this.ImgHeight);
                if (!inspection.IsCurrResultOk)
                {
                    return false;
                }
                double x, y, x2, y2,angle;
                try
                {
                    string[] ss = inspection.CurrResultStr.Split(',');
                    x = double.Parse(ss[0]);
                    y = double.Parse(ss[1]);
                    if (this.UnStandardType == 0)
                    {
                        angle = double.Parse(ss[2]);
                        Angle = angle;
                    }
                    else
                    {
                        x2 = double.Parse(ss[2]);
                        y2 = double.Parse(ss[3]);
                        MarkInImg2 = new PointD(x2, y2);
                    }
                }
                catch
                {
                    return false;
                }
                MarkInImg = new PointD(x, y);
                
            }
            else
            {
                double score = 0, x = 0, y = 0;
                int rtn = ModelFindThm.Match(this.ImgData, this.ImgWidth, this.ImgHeight, this.ModelId, ref score, ref x, ref y);
                if (rtn != 0)
                {
                    return false;
                }
                this.Score = score;
                this.MarkInImg = new PointD(x, y);
                if (this.Score < this.AcceptScore)
                {
                    return false;
                }
            }
            return true;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
