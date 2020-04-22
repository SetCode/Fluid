using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.PropertyGridExtension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Scalage
{
    [JsonObject(MemberSerialization.OptIn)]
    [TypeConverter(typeof(PropertySorter))]
    public class ScalePrm:EntityBase<int>
    {
        public ScalePrm(int key)
            :base(key)
        {

        }
        private const string CategoryScalePrms = "天平控制参数";
        private const string CategoryResult = "\t结果参数";
        private const string CategoryScaleCup = "胶杯管理参数";
        private const string CategoryCaliPrms = "天平校准参数";
        

        /// <summary>
        /// 读数延时
        /// </summary>
        [JsonProperty]
        [Category(CategoryScalePrms)]
        [DisplayName("读数延时")]
        [Description("天平控制参数，打胶之后等待天平稳定之后再进行读数的时间，默认值：3000ms")]
        [DefaultValue(3000)]
        [PropertyOrderAttribute(1)]
        public int ReadDelay { get; set; } 


        /// <summary>
        /// 读取重量次数（大于3次）
        /// </summary>
        [JsonProperty]
        [Category(CategoryScalePrms)]
        [DisplayName("天平读数次数")]
        [Description("天平控制参数，天平读数次数，默认值：5")]
        [DefaultValue(5)]
        [PropertyOrderAttribute(2)]
        public int ReadTimes { get; set; } 

        /// <summary>
        /// 单次读取重量超时时间
        /// </summary>
        [JsonProperty]
        [Category(CategoryScalePrms)]
        [DisplayName("单次读取重量超时时间")]
        [Description("天平控制参数，单次读取重量超时时间(ms)，默认值：500ms")]
        [DefaultValue(500)]
        [PropertyOrderAttribute(3)]
        public int SingleReadTimeOut { get; set; }
        /// <summary>
        /// 单次读取串口前延时
        /// </summary>
        [JsonProperty]
        [Category(CategoryScalePrms)]
        [DisplayName("单次读取前延时")]
        [Description("天平控制参数，单次读取前延时时间(ms)，默认值：20ms")]
        [DefaultValue(20)]
        [PropertyOrderAttribute(4)]
        public int SingleReadDelay { get; set; }

        ///// <summary>
        ///// 天平稳定读数超时时间
        ///// </summary>
        //[JsonProperty]
        //[Category(CategoryScalePrms)]
        //[DisplayName("天平稳定读数超时时间")]
        //[Description("天平控制参数,该时间段内天平没有稳定读数，则会触发报警，默认10000ms")]
        //[DefaultValue(10000)]
        //[PropertyOrderAttribute(4)]
        //public int StabilityTimeOut { get; set; }


        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("天平读数")]
        [Description("结果参数,天平读数（mg）")]
        [DefaultValue(0)]
        public double ReadWeight { get; set; } = 0;


        /// <summary>
        /// 胶杯最大容量
        /// </summary>
        [JsonProperty]
        [Category(CategoryScaleCup)]
        [DisplayName("胶杯最大容量")]
        [Description("胶杯管理参数，胶杯最大容量,默认值：50000mg")]
        [DefaultValue(50000)]
        [Browsable(false)]
        public int MaxCapacity { get; set; }

        /// <summary>
        /// 是否管理胶杯
        /// </summary>
        [JsonProperty]
        [Category(CategoryScaleCup)]
        [DisplayName("是否管理胶杯")]
        [Description("胶杯管理参数，如果进行胶杯管理，当胶杯重量超过 胶杯最大承重 * 胶杯超重百分比的重量时，则会触发报警")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool IsMonitorScaleCup { get; set; }

        /// <summary>
        /// 胶杯超重百分比
        /// </summary>
        [JsonProperty]
        [Category(CategoryScaleCup)]
        [DisplayName("胶杯超重百分比(%)")]
        [Description("胶杯管理参数，胶杯超重百分比(%)")]
        [DefaultValue(90)]
        [Browsable(false)]
        public int WarningStartPercentage { get; set; } 

        [JsonProperty]
        [Category(CategoryCaliPrms)]
        [DisplayName("天平校准重量")]
        [Description("天平校准重量（mg），通常是砝码的标准重量")]
        [DefaultValue(50000)]
        
        public int ScaleCalibWeight { get; set; } = 50000;


        public override string ToString()
        {
            return null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void SetReadWeight(double value)
        {
            this.ReadWeight = Math.Round(value, 1);
        }

    }

    public sealed class ScalePrmMgr : EntityMgr<ScalePrm, int>
    {
        private readonly static ScalePrmMgr instance = new ScalePrmMgr();
        private ScalePrmMgr()
        {
            this.Add(new ScalePrm(0));
        }
        public static ScalePrmMgr Instance => instance;
    }
}
