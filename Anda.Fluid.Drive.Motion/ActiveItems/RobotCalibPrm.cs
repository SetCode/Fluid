using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Common;
using System.ComponentModel;
using Anda.Fluid.Drive.Motion.Locations;

namespace Anda.Fluid.Drive.Motion.ActiveItems
{
    /// <summary>
    /// 执行七步生产准备程序后得到的参数，注意是 class 不是 struct
    /// </summary>
    public class RobotCalibPrm : ICloneable
    {
        /// <summary>
        /// 此类中的参数是何时保存的（?超过24小时需重新执行校准）
        /// </summary>
        [Browsable(false)]
        public DateTime SavedTime { get; set; }

        /// <summary>
        /// 上一次保存时完成了几项校准
        /// </summary>
        [Browsable(false)]
        [DefaultValue(0)]
        public int SavedItem { get; set; }

        /// <summary>
        /// Z轴安全高度
        /// </summary>
        [DisplayName("Z轴安全高度")]
        [Description("mm")]
        [DefaultValue(0)]
        public double SafeZ { get; set; } = 0;

        [DisplayName("测高时Z轴位置")]
        [Description("mm")]
        [DefaultValue(0)]
        public double MeasureHeightZ { get; set; } = 0;

        [DisplayName("拍Mark时Z轴位置")]
        [Description("mm")]
        [DefaultValue(0)]
        public double MarkZ { get; set; } = 0;


        /// <summary>
        /// 喷嘴1中心与相机中心的偏差
        /// </summary>
        [DisplayName("喷嘴1中心与相机中心偏差")]
        [Description("x,y")]
        public PointD NeedleCamera1 { get; set; } = new PointD();

        /// <summary>
        /// 喷嘴2中心与相机中心的偏差
        /// </summary>
        [DisplayName("喷嘴2中心与相机中心偏差")]
        [Description("x,y")]
        public PointD NeedleCamera2 { get; set; } = new PointD();

        /// <summary>
        /// 测高中心与相机中心的偏差
        /// </summary>
        [DisplayName("测高中心与相机中心偏差")]
        [Description("x,y")]
        public PointD HeightCamera { get; set; } = new PointD();

        /// <summary>
        /// 清洁位（相机位置）
        /// </summary>
        [DisplayName("清洁位")]
        [Description("x,y")]
        public PointD PurgeLoc { get; set; } = new PointD();

        /// <summary>
        /// 清洁位测高点
        /// </summary>
        [DisplayName("清洁位测高点")]
        [Description("x,y")]
        public PointD PurgeHS { get; set; } = new PointD();

        /// <summary>
        /// 激光测量清洗位的高度值
        /// </summary>
        [DisplayName("激光测量清洗位的高度值")]
        [Description("激光读数值")]
        [DefaultValue(0)]
        public double PurgeZbyHS { get; set; }

        /// <summary>
        /// 喷嘴到清洗位中心的间隔高度
        /// </summary>
        [DisplayName("喷嘴到清洗位中心的间隔高度")]
        [Description("间隔高度")]
        [DefaultValue(10)]
        public double PurgeIntervalHeight { get; set; }

        /// <summary>
        /// 点胶阀中心在排胶时的X，Y平面位置（相机位置）
        /// </summary>
        [DisplayName("排胶位置")]
        [Description("x,y")]
        public PointD PrimeLoc { get; set; } = new PointD();

        /// <summary>
        /// 点胶阀中心在排胶时的高度
        /// </summary>
        [DisplayName("排胶高度")]
        [Description("z")]
        [DefaultValue(0)]
        public double PrimeZ { get; set; }

        /// <summary>
        /// 电子秤位置（相机位置）
        /// </summary>
        [DisplayName("电子秤位置")]
        [Description("x,y")]
        public PointD ScaleLoc { get; set; } = new PointD();

        /// <summary>
        /// 电子秤测高点
        /// </summary>
        [DisplayName("电子秤测高点")]
        [Description("x,y")]
        public PointD ScaleHS { get; set; } = new PointD();

        /// <summary>
        /// 激光测量称重位的高度值
        /// </summary>
        [DisplayName("激光测量称重位的高度值")]
        [Description("激光读数值")]
        [DefaultValue(0)]
        public double ScaleZbyHS { get; set; }

        /// <summary>
        /// 喷嘴到称重位中心的间隔高度
        /// </summary>
        [DisplayName("喷嘴到称重位中心的间隔高度")]
        [Description("间隔高度")]
        [DefaultValue(10)]
        public double ScaleIntervalHeight { get; set; }

        /// <summary>
        /// 喷嘴中心点与点胶实际位置的偏差
        /// </summary>
        [DisplayName("喷嘴1中心与实际点胶偏差")]
        [Description("x,y")]
        public PointD NeedleJet1 { get; set; } = new PointD();

        [DisplayName("喷嘴2中心与实际点胶偏差")]
        [Description("x,y")]
        public PointD NeedleJet2 { get; set; } = new PointD();

        [DisplayName("激光测量标准值")]
        [Description("激光读数值")]
        [DefaultValue(0)]
        public double StandardHeight { get; set; }

        [DisplayName("Z轴标准值")]
        [Description("与激光测量标准值对应")]
        [DefaultValue(0)]
        public double StandardZ { get; set; }

        [DisplayName("激光测量标准值2")]
        [Description("激光读数值")]
        [DefaultValue(0)]
        public double StandardHeight2 { get; set; }

        [DisplayName("Z轴标准值2")]
        [Description("与激光测量标准值对应")]
        [DefaultValue(0)]
        public double StandardZ2 { get; set; }

        public double HorizontalRatio { get; set; } = 1;

        public double VerticalRatio { get; set; } = 1;

        [DisplayName("擦纸袋位置")]
        [Description("x,y")]
        public Location ScrapeLocation { get; set; } = new Location();

        [DisplayName("阀2相对阀1高度差")]
        public double ValveZOffset2to1 { get; set; } = 0;

        [DisplayName("阀2相对阀1XY偏移")]
        public PointD ValveXYOffset2to1 { get; set; } = new PointD();

        #region 
        public PointD NeedleStartPos { get; set; }
        public PointD NeedleEndPos { get; set; }

        public double NeedlePosZUp = 0;
        public double NeedlePosZDown = 0;

        public double NeedleRotated = 0;

        public double AngleLine = 0;
        public PointD NeedlePosition { get; set; }
        public double NeedlePosZPlasticene { get; set; }
        public double AngleGap = 0;
        public double AngleGapCompensation = 0;
        public int Direct = 1;
        //同心度检测参数和结果
        public PointD TestPosition { get; set; }
        public double TestPosZ { get; set; }
       
        #endregion

        #region 四方位校准时的参数和结果 Edit By 肖旭

        /// <summary>
        /// 对刀仪中心位置
        /// </summary>
        [Browsable(false)]
        public PointD HeightSensorCenter { get; set; } = new PointD();

        public List<AngleHeightPosOffset> AngleHeightPosOffsetList { get; set; } = new List<AngleHeightPosOffset>();
        #endregion

        public RobotCalibPrm Default()
        {
            this.SavedTime = DateTime.Now;
            this.SavedItem = 0;
            this.SafeZ = 0;
            this.NeedleCamera1 = new PointD();
            this.NeedleCamera2 = new PointD();
            this.HeightCamera = new PointD();
            this.PurgeLoc = new PointD();
            this.PurgeZbyHS = 0;
            this.ScaleLoc = new PointD();
            this.ScaleZbyHS = 0;
            this.NeedleJet1 = new PointD();
            this.NeedleJet2 = new PointD();
            return this;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// 四方位阀组在不同倾斜方向、角度和距板高度下的偏差
    /// </summary>
    public class AngleHeightPosOffset
    {
        /// <summary>
        /// 胶阀倾斜方向
        /// </summary>
        public TiltType TiltType { get; set; }

        /// <summary>
        /// 胶阀角度
        /// </summary>
        public double ValveAngle { get; set; }

        /// <summary>
        /// 胶阀距板高度
        /// </summary>
        public double Gap { get; set; }

        /// <summary>
        /// 在当前姿态和角度下的阀组标准高度
        /// </summary>
        public double StandardZ { get; set; }

        /// <summary>
        /// 阀与相机的偏移
        /// </summary>
        public PointD ValveCameraOffset { get; set; }

        /// <summary>
        /// 实际打胶点到阀的偏移
        /// </summary>
        public PointD DispenseOffset { get; set; }
    }

    /// <summary>
    /// 胶阀倾斜类型
    /// </summary>
    public enum TiltType
    {
        /// <summary>
        /// 不倾斜
        /// </summary>
        NoTilt = 0,
        /// <summary>
        /// 左倾斜
        /// </summary>
        LTilt,
        /// <summary>
        /// 右倾斜
        /// </summary>
        RTilt,
        /// <summary>
        /// 前倾斜
        /// </summary>
        FTilt,
        /// <summary>
        /// 后倾斜
        /// </summary>
        BTilt
    }
}
