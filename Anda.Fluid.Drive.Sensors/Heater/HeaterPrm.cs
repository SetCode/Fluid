using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    [JsonObject(MemberSerialization.OptIn)]
    public class HeaterPrm:EntityBase<int>
    {
        public HeaterPrm(int key)
            :base(key)
        {

        }

        [JsonProperty]
        public double[] Standard { get; set; } = { 50, 50, 50, 50, 50, 50, 50, 50 };

        [JsonProperty]
        public double[] High { get; set; } = { 60, 60, 60, 60, 60, 60, 60, 60 };

        [JsonProperty]
        public double[] Low { get; set; } = { 40, 40, 40, 40, 40, 40, 40, 40 };

        [JsonProperty]
        public double[] Offset { get; set; } = { -2, -2, -2, -2, -2, -2, -2, -2 };

        [JsonProperty]
        public bool[] acitveChanel { get; set; } = { true, false, false, false, false, false, false, false };

        /// <summary>
        /// 持续加热
        /// </summary>
        [JsonProperty]
        public bool IsContinuseHeating { get; set; } = false;

        /// <summary>
        /// 空闲时关闭较热
        /// </summary>
        [JsonProperty]
        public bool CloseHeatingWhenIdle { get; set; } = false;

        /// <summary>
        /// 空闲判定时长
        /// </summary>
        [JsonProperty]
        public int IdleDecideTime { get; set; } = 60;

    }
    public sealed class HeaterPrmMgr : EntityMgr<HeaterPrm, int>
    {
        private readonly static HeaterPrmMgr instance = new HeaterPrmMgr(SettingsPath.PathMachine);
        private HeaterPrmMgr()
        {
            this.Add(new HeaterPrm(0));
            this.Add(new HeaterPrm(1));
        }
        private HeaterPrmMgr(string dir) : base(dir)
        {
            this.Add(new HeaterPrm(0));
            this.Add(new HeaterPrm(1));
        }
        public static HeaterPrmMgr Instance => instance;
    }
}
