using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;
using System;

namespace Anda.Fluid.Drive.GlueManage
{
    /// <summary>
    /// Author: liyi
    /// Date:   2019/09/07
    /// Description:胶水管控参数类
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class GlueManagePrm : EntityBase<int>
    {
        public GlueManagePrm(int key) : base(key)
        {

        }
        /// <summary>
        /// 是否启用胶水管控
        /// </summary>
        [JsonProperty]
        public bool UseGlueManage { get; set; } = false;
        /// <summary>
        /// 胶水寿命，单位minute
        /// </summary>
        [JsonProperty]
        public double GlueLife { get; set; } = 1440;
        /// <summary>
        /// 胶水剩余寿命，单位minute
        /// </summary>
        [JsonProperty]
        public double GlueRemainLife { get; set; } = 1440;

        /// <summary>
        /// 寿命到期预警时间，单位minute
        /// </summary>
        [JsonProperty]
        public double LifeWarningTime { get; set; } = 30;

        /// <summary>
        /// 胶水总重量，单位mg
        /// </summary>
        [JsonProperty]
        public double TotalWeight { get; set; } = 30000;
        /// <summary>
        /// 胶水剩余重量，单位mg
        /// </summary>
        [JsonProperty]
        public double RemainWeight { get; set; } = 30000;
        /// <summary>
        /// 报警百分比，单位%
        /// </summary>
        [JsonProperty]
        public double WarningPercentage { get; set; } = 15;
        /// <summary>
        /// 胶水出库时间
        /// </summary>
        [JsonProperty]
        public DateTime GlueDeliverTime { get; set; }
        /// <summary>
        /// 胶水回温时间，单位minute
        /// </summary>
        [JsonProperty]
        public double GlueThawTime { get; set; } = 120;
        /// <summary>
        /// 胶水SN
        /// </summary>
        [JsonProperty]
        public string GlueSN { get; set; } = "";
        /// <summary>
        /// 胶水SN长度
        /// </summary>
        [JsonProperty]
        public int GlueSNLength { get; set; } = 0;
        /// <summary>
        /// 胶水型号
        /// </summary>
        [JsonProperty]
        public string GlueType { get; set; } = "";
        /// <summary>
        /// 是否启用胶水型号管控
        /// </summary>
        [JsonProperty]
        public bool UseGlueType { get; set; } = false;
        /// <summary>
        /// 胶水管控方式
        /// 0：本地管控
        /// 1：联网管控
        /// </summary>
        [JsonProperty]
        public int ManageType { get; set; } = 0;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
