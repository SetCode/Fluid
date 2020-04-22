using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;
using System.ComponentModel;


namespace Anda.Fluid.Drive.ValveSystem.Prm
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JtValvePrm:ICloneable
    {
        private const string CategoryCmp2d = "二维比较参数";
        private const string CategoryValve = "阀参数";
        private const string CategoryMotion = "运动参数";
        private const string CategoryPurge = "清洗参数";
        private const string CategoryCycle = "寿命参数";
        private const string CategoryScrape = "擦纸带参数";

        public JtValvePrm()
        {

        }
        #region 2d比较参数

        //[JsonProperty]
        //[Category(CategoryCmp2d)]
        //[DisplayName("比较源")]
        //[Description("0：规划器，1：编码器")]
        //[DefaultValue(1)]
        //public Cmp2dSrcType Cmp2dSrc { get; set; }

        [JsonProperty]
        [Category(CategoryCmp2d)]
        [DisplayName("最大误差")]
        [Description("比较范围最大误差，单位Pulse")]
        [DefaultValue(300)]
        public int Cmp2dMaxErr { get; set; }

        [JsonProperty]
        [Category(CategoryCmp2d)]
        [DisplayName("最优阈值")]
        [Description("最优点计算阈值")]
        [DefaultValue(10)]
        public int Cmp2dThreshold { get; set; }

        #endregion

        #region 运动参数

        [JsonProperty]
        [Category(CategoryMotion)]
        [DisplayName("控制模式")]
        [Description("")]
        [DefaultValue(0)]
        public ValveFluidMode FluidMode { get; set; }

        [JsonProperty]
        [Category(CategoryMotion)]
        [DisplayName("运动模式")]
        [Description("")]
        [DefaultValue(0)]
        public ValveMoveMode MoveMode { get; set; }

        #endregion

        #region 阀参数

        [JsonProperty]
        [Category(CategoryValve)]
        [DisplayName("开胶时间")]
        [Description("微秒")]
        [DefaultValue(3000)]
        public int OnTime { get; set; }

        [JsonProperty]
        [Category(CategoryValve)]
        [DisplayName("关胶时间")]
        [Description("微秒")]
        [DefaultValue(3000)]
        public int OffTime { get; set; }

        #endregion

        #region 清洗参数

        //[JsonProperty]
        //[Category(CategoryPurge)]
        //[DisplayName("打胶间隔")]
        //[Description("打第一点后间隔多久打第二点，单位s")]
        //[DefaultValue(1)]
        //public double PurgeInterval { get; set; }

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("启用真空吸转")]
        [Description("启用True,不启用False")]
        [DefaultValue(true)]
        public bool EnablePrePurgeVaccum { get; set; }

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("吸转圈数")]
        [Description("")]
        [DefaultValue(3)]
        public int PrePurgeCount { get; set; }

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("圈半径")]
        [Description("")]
        [DefaultValue(1)]
        public double PrePurgeRadius { get; set; }

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("清洗速度")]
        [Description("")]
        [DefaultValue(40)]
        public double PrePurgerSpeed { get; set; } = 40;

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("开胶延时")]
        [Description("转完后延时多久开胶，单位s")]
        [DefaultValue(1)]
        public double PrePurgeDelay { get; set; }

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("排胶点数")]
        [Description("")]
        [DefaultValue(20)]
        public int PrimeDotsCount { get; set; }

        #endregion

        #region 寿命参数

        [JsonProperty]
        [Category(CategoryCycle)]
        [DisplayName("启用寿命检测")]
        [Description("启用True,不启用False")]
        [DefaultValue(true)]
        public bool EnableLimitChecking { get; set; }

        [JsonProperty]
        [Category(CategoryCycle)]
        [DisplayName("当前次数")]
        [Description("")]
        [DefaultValue(0)]
        public int CurrentCycleCount { get; set; }

        [JsonProperty]
        [Category(CategoryCycle)]
        [DisplayName("使用寿命")]
        [Description("")]
        [DefaultValue(10000)]
        public int CycleCountLimit { get; set; }

        #endregion

        # region 擦纸带参数
        [JsonProperty]
        [Category(CategoryScrape)]
        [DisplayName("启用擦纸带")]
        [Description("启用True，不启用False")]
        public bool ScrapeEnable { get; set; } = false;

        [JsonProperty]
        [Category(CategoryScrape)]
        [DisplayName("擦纸带时长")]
        [Description("单位s")]
        [DefaultValue(1)]
        public double ScrapeTime { get; set; }
        # endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
