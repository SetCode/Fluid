using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Anda.Fluid.Drive.ValveSystem.Prm
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SvValvePrm :ICloneable
    {
        private const string CategoryCmp2d = "二维比较参数";
        private const string CategoryValve = "阀参数";
        private const string CategoryMotion = "运动参数";
        private const string CategoryPurge = "清洗参数";
        private const string CategoryCycle = "寿命参数";
        private const string CategoryFloat = "浮动头参数";

        public SvValvePrm()
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
        public int Cmp2dMaxErr { get; set; } = 300;

        [JsonProperty]
        [Category(CategoryCmp2d)]
        [DisplayName("最优阈值")]
        [Description("最优点计算阈值")]
        [DefaultValue(10)]
        public int Cmp2dThreshold { get; set; } = 10;

        #endregion

        #region 运动参数

        [JsonProperty]
        [Category(CategoryMotion)]
        [DisplayName("控制模式")]
        [Description("")]
        [DefaultValue(0)]
        public ValveFluidMode FluidMode { get; set; } = ValveFluidMode.一维比较优先;

        [JsonProperty]
        [Category(CategoryMotion)]
        [DisplayName("运动模式")]
        [Description("")]
        [DefaultValue(0)]
        public ValveMoveMode MoveMode { get; set; } = ValveMoveMode.单次插补;

        #endregion

        #region 阀参数

        [JsonProperty]
        [Category(CategoryValve)]
        [DisplayName("送胶速度")]
        [Description("转/秒")]
        [DefaultValue(50)]
        public int ForwardSpeed { get; set; } = 50;

        [JsonProperty]
        [Category(CategoryValve)]
        [DisplayName("反转速度")]
        [Description("转/秒")]
        [DefaultValue(50)]
        public int ReverseSpeed { get; set; } = 50;

        [JsonProperty]
        [Category(CategoryValve)]
        [DisplayName("回吸时间")]
        [Description("毫秒")]
        [DefaultValue(1000)]
        public int SuckBackTime { get; set; } = 50;
        #endregion

        #region 清洗参数
        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("启用真空吸转")]
        [Description("")]
        [DefaultValue(true)]
        public bool EnablePrePurgeVaccum { get; set; } = true;

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("吸转圈数")]
        [Description("")]
        [DefaultValue(3)]
        public int PrePurgeCount { get; set; } = 3;

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("圈半径")]
        [Description("")]
        [DefaultValue(1)]
        public double PrePurgeRadius { get; set; } = 1;

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("开胶延时")]
        [Description("转完后延时多久开胶，单位s")]
        [DefaultValue(1)]
        public double PrePurgeDelay { get; set; } = 1;

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("真空延时")]
        [Description("s")]
        [DefaultValue(1)]
        public double VacuumDelay { get; set; } = 1;

        [JsonProperty]
        [Category(CategoryPurge)]
        [DisplayName("排胶时间")]
        [Description("毫秒")]
        [DefaultValue(2000)]
        public int PrimeTime { get; set; } = 2000;

        #endregion

        #region 寿命参数

        [JsonProperty]
        [Category(CategoryCycle)]
        [DisplayName("启用寿命检测")]
        [Description("")]
        [DefaultValue(true)]
        public bool EnableLimitChecking { get; set; } = true;

        [JsonProperty]
        [Category(CategoryCycle)]
        [DisplayName("当前次数")]
        [Description("")]
        [DefaultValue(0)]
        public int CurrentCycleCount { get; set; } = 0;

        [JsonProperty]
        [Category(CategoryCycle)]
        [DisplayName("使用寿命")]
        [Description("")]
        [DefaultValue(10000)]
        public int CycleCountLimit { get; set; } = 10000;

        #endregion

        #region 浮动头参数

        [JsonProperty]
        [Category(CategoryFloat)]
        [DisplayName("启用浮动头")]
        [Description("")]
        [DefaultValue(false)]
        public bool EnableFloat { get; set; } = false;

        [JsonProperty]
        [Category(CategoryFloat)]
        [DisplayName("浮动头补偿")]
        [Description("毫米")]
        [DefaultValue(1)]
        public double FloatOffset { get; set; } = 1;
        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SvOrGearValveSpeedWeightValve
    {
        public static Dictionary<int, double> VavelSpeedWeightDic { get; set; } = new Dictionary<int, double>();
    }
}
