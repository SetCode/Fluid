using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using System.ComponentModel;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AxisPrm : EntityBase<int>, ICloneable
    {
        private const string CategoryMotion = "运动参数";
        private const string CategoryHard = "硬件参数";
        

        public AxisPrm(int key)
            : base(key)
        {

        }

        [Category(CategoryHard)]
        [DisplayName("每转脉冲数")]
        [DefaultValue(10000)]
        [Description("pulses")]
        [JsonProperty]
        public int PulsePerLap { get; set; }

        [Category(CategoryHard)]
        [DisplayName("导程")]
        [DefaultValue(10)]
        [Description("mm")]
        [JsonProperty]
        public double Lead { get; set; }

        [ReadOnly(true)]
        public double Pulse2Mm => this.Lead / this.PulsePerLap;

        [ReadOnly(true)]
        public double Mm2Pulse => this.PulsePerLap / this.Lead;


        /// <summary>
        /// 最大运行速度mm/s 
        /// </summary>
        [Category(CategoryMotion)]
        [DisplayName("最大运行速度")]
        [DefaultValue(1000)]
        [Description("mm/s")]
        [JsonProperty]
        public double MaxRunVel { get; set; }

        /// <summary>
        /// 最大手动速度mm/s
        /// </summary>
        [Category(CategoryMotion)]
        [DisplayName("手动最大速度")]
        [DefaultValue(200)]
        [Description("mm/s")]
        [JsonProperty]
        public double MaxManualVel { get; set; }

        [Category(CategoryMotion)]
        [DisplayName("手动高速%")]
        [Description("[0,1]")]
        [DefaultValue(0.4)]
        [JsonProperty]
        public double ManualHigh { get; set; }

        [Category(CategoryMotion)]
        [DisplayName("手动低速%")]
        [Description("[0,1]")]
        [DefaultValue(0.02)]
        [JsonProperty]
        public double ManualLow { get; set; }
        
        ///// <summary>
        ///// 位置偏差
        ///// </summary>
        //[Category(CategoryMotion)]
        //[DisplayName("位置偏差")]
        //[DefaultValue(0.1)]
        //[Description("mm")]
        //[JsonProperty]
        //public double TolerancePos { get; set; }

        /// <summary>
        /// 默认加速度
        /// </summary>
        [Category(CategoryMotion)]
        [DisplayName("默认加速度")]
        [DefaultValue(5)]
        [Description("pulse/ms2")]
        [JsonProperty]
        public double Acc { get; set; }

        /// <summary>
        /// 默认减速度
        /// </summary>
        [Category(CategoryMotion)]
        [DisplayName("默认减速度")]
        [DefaultValue(5)]
        [Description("pulse/ms2")]
        [JsonProperty]
        public double Dec { get; set; }

        /// <summary>
        /// 平滑停止减速度pulse/ms2
        /// </summary>
        [Category(CategoryMotion)]
        [DisplayName("平滑停止减速度")]
        [DefaultValue(5)]
        [Description("pulse/ms2")]
        [JsonProperty]
        public double SmoothDec { get; set; }

        /// <summary>
        /// 紧急停止减速度pulse/ms2
        /// </summary>
        [Category(CategoryMotion)]
        [DisplayName("紧急停止减速度")]
        [DefaultValue(10)]
        [Description("pulse/ms2")]
        [JsonProperty]
        public double AbruptDec { get; set; }
        public object Clone()
        {
            return (AxisPrm)this.MemberwiseClone();
        }
    }

    public sealed class AxisPrmMgr : EntityMgr<AxisPrm, int>
    {
        private readonly static AxisPrmMgr instance = new AxisPrmMgr(SettingsPath.PathMachine);
        private AxisPrmMgr() { }
        private AxisPrmMgr(string dir) : base(dir)
        { }
        public static AxisPrmMgr Instance => instance;
    }
}
