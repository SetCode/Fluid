using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.PropertyGridExtension;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure;

namespace Anda.Fluid.Drive.ValveSystem
{
    [JsonObject(MemberSerialization.OptIn)]
    [TypeConverter(typeof(PropertySorter))]
    public class ValveWeightPrm: EntityBase<ValveType>
    {
        public ValveWeightPrm(ValveType key):
            base(key)
        {

        }
       
        private const string CategoryControlPrms = "单步执行称重控制参数";        
        private const string CategorySimulateProduction = "模拟生产参数";
        private const string CategoryResult = "\t结果参数";
        private const string CategoryWeightCalibPrms = "重量校准参数";
        private const string CategoryWorkMode = "工作模式控制参数";


        /// <summary>
        /// 打点次数
        /// </summary>
        [JsonProperty]
        [Category(CategoryControlPrms)]
        [DisplayName("打胶次数")]
        [Description("单步执行称重控制参数，打胶次数,默认值：100")]
        [DefaultValue(100)]
        public int SprayDots { get; set; } 

        /// <summary>
        /// 每个“拼版”打的点数
        /// </summary>
        [JsonProperty]
        [Category(CategorySimulateProduction)]
        [DisplayName("每块拼版打胶次数")]
        [Description("模拟生产参数，每块拼版打胶次数,默认值：100")]
        [DefaultValue(100)]
        [PropertyOrderAttribute(1)]
        public int ShotDotsEachPattern { get; set; } 

        /// <summary>
        /// “拼版数”
        /// </summary>
        [JsonProperty]
        [Category(CategorySimulateProduction)]
        [DisplayName("总的拼版数")]
        [Description("模拟生产参数，总的拼版数,默认值：5")]
        [DefaultValue(5)]
        [PropertyOrderAttribute(2)]
        public int PatternCount { get; set; } 

        /// <summary>
        /// 每个"拼版"的时间间隔(ms)
        /// </summary>
        [JsonProperty]
        [Category(CategorySimulateProduction)]
        [DisplayName("每个拼版的时间间隔")]
        [Description("模拟生产参数，每个拼版的时间间隔(ms)，默认值：500ms")]
        [DefaultValue(500)]
        [PropertyOrderAttribute(3)]
        public int Interval { get; set; } 
               

        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("天平当前读数")]
        [Description("结果参数,天平读数（mg）")]
        [DefaultValue(0)]        
        [PropertyOrderAttribute(1)]
        public double CurrentWeight { get; private set; }

        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("天平上次读数")]
        [Description("天平上次读数(mg)")]
        [DefaultValue(0)]
        [PropertyOrderAttribute(2)]
        public double WeightBeforeSpray { get; private set; }

        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("打胶前后读数差")]
        [Description("打胶前后读数差(mg)")]
        [DefaultValue(0)]
        [PropertyOrderAttribute(3)]
        public double DifferWeight { get; private set; }


        /// <summary>
        /// 单点重量
        /// </summary>
        [JsonProperty]
        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("单点重量")]
        [Description("结果参数,单点重量(mg/dot)")]
        [DefaultValue(0)]
        [PropertyOrderAttribute(4)]
        public double SingleDotWeight { get;  set; }

        /// <summary>
        /// 累计重量
        /// </summary>
        [JsonProperty]
        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("打胶累计重量")]
        [Description("结果参数,打胶累计重量（mg）")]
        [DefaultValue(0)]
        [PropertyOrderAttribute(5)]
        public double CumulativeWeight { get; set; }

        [JsonProperty]
        [Category(CategoryWeightCalibPrms)]
        [DisplayName("单点标准重量")]
        [Description("标准重量（mg），")]
        [DefaultValue(0)]
        public double StandardWeight { get; set; }

        [JsonProperty]
        [Category(CategoryWeightCalibPrms)]
        [DisplayName("单点重量偏差")]
        [Description("重量偏差（%），超出标准重量的数值与标准重量的百分比")]
        [DefaultValue(5)]
        public double WeightOffset { get; set; }

        [JsonProperty]
        [Category(CategoryWeightCalibPrms)]
        [DisplayName("阀1阀2单点重量偏差")]
        [Description("阀1阀2点胶重量偏差的百分比")]
        [DefaultValue(5)]
        public double Percentage { get; set; }


        [JsonProperty]
        [Category(CategoryWorkMode)]
        [DisplayName("初次运行是否自动校准")]
        [Description("工作模式控制参数，初次运行是否自动校准，点击开始运行程序后，true为是，false为否，则自动进行称重得到合适的单点重量参数")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool IsAutoRunFirstStart { get; set; } 

        [JsonProperty]
        [Category(CategoryWorkMode)]
        [DisplayName("工作指定次数后是否自动校准")]
        [Description("工作模式控制参数，工作指定次数后自动校准，点击开始运行程序后，true为是，false为否，达到设定工作次数后自动进行称重得到合适的单点重量参数")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool IsAutoRunAfterTimes { get; set; }

        [JsonProperty]
        [Category(CategoryWorkMode)]
        [DisplayName("工作指定次数")]
        [Description("工作模式控制参数，工作指定次数，用于自动称重校准的次数，满足该次数条件，则会自动的进行自动校准")]
        [DefaultValue(5)]
        [Browsable(false)]
        public int RunningTimes { get; set; }

        public void SetSingleDotWeight(double value)
        {
            this.SingleDotWeight = value;
        }

        public void SetCurrentWeight(double value)
        {
            this.CurrentWeight = Math.Round(value, 4);
        }
        public void SetWeightBeforeSpray(double value)
        {
            this.WeightBeforeSpray = Math.Round(value,4);
        }

        public Result SetDiffWeight()
        {
            Result ret = Result.OK;
            this.DifferWeight = Math.Round(this.CurrentWeight - this.WeightBeforeSpray,3);
            if (this.DifferWeight<=0)
            {
                ret = Result.FAILED;
            }
            return ret;
        }

       
        public override string ToString()
        {
            return "";
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }

    public sealed class ValveWeightPrmMgr : EntityMgr<ValveWeightPrm, ValveType>
    {
        private readonly static ValveWeightPrmMgr instance = new ValveWeightPrmMgr(SettingsPath.PathMachine);
        private ValveWeightPrmMgr()
        {
            
        }
        private ValveWeightPrmMgr(string dir) : base(dir)
        {
        }
        public static ValveWeightPrmMgr Instance => instance;
    }
}
