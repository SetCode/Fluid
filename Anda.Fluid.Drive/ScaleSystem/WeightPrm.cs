using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ScaleSystem
{

    /// <summary>
    /// 称重参数类
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class WeightPrm : ICloneable
    {
        private const string CategoryScalePrms = "天平控制参数";
        private const string CategoryControlPrms = "单步执行称重控制参数";
        private const string CategorySimulateProduction = "模拟生产参数";
        private const string CategoryResult = "结果参数";
        private const string CategoryScaleCup = "胶杯管理参数";
        private const string CategoryCaliPrms = "天平校准参数";
        private const string CategoryWeightCalibPrms = "重量校准参数";
        private const string CategoryWorkMode = "工作模式控制参数";

        /// <summary>
        /// 读取重量次数（大于3次）
        /// </summary>
        [JsonProperty]
        [Category(CategoryScalePrms)]
        [DisplayName("天平读数次数")]
        [Description("天平控制参数，天平读数次数，默认值：5")]
        public int ReadTimes { get; set; } = 5;

        /// <summary>
        /// 单词读取重量超时时间
        /// </summary>
        [JsonProperty]
        [Category(CategoryScalePrms)]
        [DisplayName("单次读取重量超时时间")]
        [Description("天平控制参数，单次读取重量超时时间(ms)，默认值：500ms")]
        public int SingleReadTimeOut { get; set; } = 500;

        /// <summary>
        /// 读数延时
        /// </summary>
        [JsonProperty]
        [Category(CategoryScalePrms)]
        [DisplayName("读数延时")]
        [Description("天平控制参数，打胶之后等待天平稳定之后再进行读数的时间，默认值：3000ms")]
        public int ReadDelay { get; set; } = 3000;

        /// <summary>
        /// 天平稳定读数超时时间
        /// </summary>
        [JsonProperty]
        [Category(CategoryScalePrms)]
        [DisplayName("天平稳定读数超时时间")]
        [Description("天平控制参数,该时间段内天平没有稳定读数，则会触发报警，默认10000ms")]
        public int StabilityTimeOut { get; set; } = 10000;

        /// <summary>
        /// 打点次数
        /// </summary>
        [JsonProperty]
        [Category(CategoryControlPrms)]
        [DisplayName("打胶次数")]
        [Description("单步执行称重控制参数，打胶次数,默认值：100")]
        public int SprayDots { get; set; } = 100;

        /// <summary>
        /// 每个“拼版”打的点数
        /// </summary>
        [JsonProperty]
        [Category(CategorySimulateProduction)]
        [DisplayName("每块拼版打胶次数")]
        [Description("模拟生产参数，每块拼版打胶次数,默认值：100")]
        public int ShotDotsEachPanel { get; set; } = 100;

        /// <summary>
        /// “拼版数”
        /// </summary>
        [JsonProperty]
        [Category(CategorySimulateProduction)]
        [DisplayName("总的拼版数")]
        [Description("模拟生产参数，总的拼版数,默认值：5")]
        public int Panels { get; set; } = 5;

        /// <summary>
        /// 每个"拼版"的时间间隔(ms)
        /// </summary>
        [JsonProperty]
        [Category(CategorySimulateProduction)]
        [DisplayName("每个拼版的时间间隔")]
        [Description("模拟生产参数，每个拼版的时间间隔(ms)，默认值：500ms")]
        public int Interval { get; set; } = 500;

        /// <summary>
        /// 胶杯最大容量
        /// </summary>
        [JsonProperty]
        [Category(CategoryScaleCup)]
        [DisplayName("胶杯最大容量")]
        [Description("胶杯管理参数，胶杯最大容量,默认值：50000mg")]
        public int MaxCapacity { get; set; } = 50000;

        /// <summary>
        /// 是否管理胶杯
        /// </summary>
        [JsonProperty]
        [Category(CategoryScaleCup)]
        [DisplayName("是否管理胶杯")]
        [Description("胶杯管理参数，如果进行胶杯管理，当胶杯重量超过 胶杯最大承重 * 胶杯超重百分比的重量时，则会触发报警")]
        public bool IsMonitorScaleCup { get; set; } = false;

        /// <summary>
        /// 胶杯超重百分比
        /// </summary>
        [JsonProperty]
        [Category(CategoryScaleCup)]
        [DisplayName("胶杯超重百分比(%)")]
        [Description("胶杯管理参数，胶杯超重百分比(%)")]
        public int WarningStartPercentage { get; set; } = 90;

        /// <summary>
        /// 天平读数
        /// </summary>
        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("天平读数")]
        [Description("结果参数,天平读数（mg）")]
        public double ReadWeight { get; private set; } = 0;

        /// <summary>
        /// 单点重量
        /// </summary>
        [JsonProperty]
        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("单点重量")]
        [Description("结果参数,单点重量(mg/dot)")]
        public double SingleDotWeight { get; private set; } = 0;

        /// <summary>
        /// 累计重量
        /// </summary>
        [JsonProperty]
        [ReadOnly(true)]
        [Category(CategoryResult)]
        [DisplayName("打胶累计重量")]
        [Description("结果参数,打胶累计重量（mg）")]
        public double CumulativeWeight { get; set; } = 0;

        [JsonProperty]
        [Category(CategoryCaliPrms)]
        [DisplayName("天平校准重量")]
        [Description("天平校准重量（mg），通常是砝码的标准重量")]
        public int ScaleCalibWeight { get; set; } = 50000;
        
        [JsonProperty]
        [Category(CategoryWeightCalibPrms)]
        [DisplayName("标准重量")]
        [Description("标准重量（mg），")]
        public int StandardWeight { get; set; } = 0;

        [JsonProperty]
        [Category(CategoryWeightCalibPrms)]
        [DisplayName("重量偏差")]
        [Description("重量偏差（%），超出标准重量的数值与标准重量的百分比")]
        public int WeightOffset { get; set; } = 0;



        [JsonProperty]
        [Category(CategoryWorkMode)]
        [DisplayName("初次运行是否自动校准")]
        [Description("工作模式控制参数，初次运行是否自动校准，点击开始运行程序后，true为是，false为否，则自动进行称重得到合适的单点重量参数")]
        public bool IsAutoRunFirstStart { get; set; } = false;

        [JsonProperty]
        [Category(CategoryWorkMode)]
        [DisplayName("工作指定次数后是否自动校准")]
        [Description("工作模式控制参数，工作指定次数后自动校准，点击开始运行程序后，true为是，false为否，达到设定工作次数后自动进行称重得到合适的单点重量参数")]
        public bool IsAutoRunAfterTimes { get; set; } = false;

        [JsonProperty]
        [Category(CategoryWorkMode)]
        [DisplayName("工作指定次数")]
        [Description("工作模式控制参数，工作指定次数，用于自动称重校准的次数，满足该次数条件，则会自动的进行自动校准")]
        public int RunningTimes { get; set; } = 5;

        public void SetSingleDotWeight(double value)
        {
            this.SingleDotWeight = value;
        }

        public void SetReadWeight(double value)
        {
            this.ReadWeight = Math.Round(value,1);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
