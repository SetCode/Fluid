using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision
{
    [Serializable]
    public abstract class VisionFindPrmBase
    {
        public bool IsCreated { get; set; }        
       
        public bool IsFromFile { get; set; }

        /// <summary>
        /// 在Pattern中的拍照位置，需要保存
        /// </summary>
        public PointD PosInPattern { get; set; } = new PointD();

        /// <summary>
        /// 机械拍照位置，运行时动态使用，不保存
        /// </summary>
        [NonSerialized]
        public PointD PosInMachine = new PointD();

        /// <summary>
        /// 拍照到位等待时间
        /// </summary>
        [CompareAtt("CMP")]
        public int SettlingTime { get; set; } = 50;

        /// <summary>
        /// 相机增益
        /// </summary>
        public int Gain { get; set; } = 400;

        /// <summary>
        /// 相机曝光
        /// </summary>
        public int ExposureTime { get; set; } = 3000;

        /// <summary>
        /// 光源类型
        /// </summary>
        //public LightType LightType { get; set; } = LightType.Coax;

        public ExecutePrm ExecutePrm { get; set; } = new ExecutePrm();

        /// <summary>
        /// 图像宽度
        /// </summary>
        public int ImgWidth { get; set; }

        /// <summary>
        /// 图像高度
        /// </summary>
        public int ImgHeight { get; set; }

        /// <summary>
        /// 图像数据
        /// </summary>
        [NonSerialized]
        public byte[] ImgData;

        public Bitmap Bmp;
        /// <summary>
        /// 搜索区域左上X
        /// </summary>
        public int SearchTopLeftX { get; set; }

        /// <summary>
        /// 搜索区域左上Y
        /// </summary>
        public int SearchTopLeftY { get; set; }

        /// <summary>
        /// 搜索区域宽度
        /// </summary>
        [CompareAtt("CMP")]
        public int SearchWidth { get; set; }

        /// <summary>
        /// 搜索区域高度
        /// </summary>
        [CompareAtt("CMP")]
        public int SearchHeight { get; set; }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <returns></returns>
        public abstract bool Execute();
    }
}
