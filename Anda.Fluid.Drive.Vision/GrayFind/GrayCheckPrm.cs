using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.GrayFind
{
    [Serializable]
    public class GrayCheckPrm : VisionFindPrmBase, ICloneable
    {
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
        /// 检测区域
        /// </summary>
        [NonSerialized]
        public byte[] CheckData;

        /// <summary>
        /// 检测区域宽度
        /// </summary>
        [NonSerialized]
        [CompareAtt("CMP")]
        public int CheckWidth;

        /// <summary>
        /// 检测区域长度
        /// </summary>
        [NonSerialized]
        [CompareAtt("CMP")]
        public int CheckHeight;

        /// <summary>
        /// 可接受灰度容差
        /// </summary>
        [CompareAtt("CMP")]
        public int AcceptTolerance { get; set; } = 30;

        public override bool Execute()
        {
            if (this.CheckData == null)
            {
                return false;
            }

            int rtn = GrayCheckThm.Check(this.ModelData, this.ModelWidth, this.ModelHeight,
                this.CheckData, this.CheckWidth, this.CheckHeight, this.AcceptTolerance);
            if (rtn != 0)
            {
                return false;
            }
            return true;
        }

        public byte[] GetROI(byte[] img, int imgWidth, int imgHeight)
        {
            byte[] roiImageData = new byte[ModelWidth * ModelHeight];
            int rtn = ModelFindThm.GetRoiImageData(img, imgWidth, imgHeight, ModelTopLeftX,
                ModelTopLeftY, ModelWidth, ModelHeight, roiImageData);
            return roiImageData;
        }
        public object Clone()
        {
            return (GrayCheckPrm)this.MemberwiseClone();
        }
    }
}
