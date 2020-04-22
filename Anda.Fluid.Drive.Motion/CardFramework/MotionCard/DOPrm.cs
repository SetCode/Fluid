using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public class DOPrm : EntityBase<int>
    {
        [JsonProperty]
        public int CardKey { get; set; }

        [JsonProperty]
        public short DoId { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// 设定有效电平
        /// </summary>
        [DefaultValue(true)]
        [JsonProperty]
        public bool HeightLevelValid { get; set; }

        public DOPrm(int key, int cardKey, short doId, string name, bool heightLevelValid = true)
            : base(key)
        {
            this.CardKey = cardKey;
            this.DoId = doId;
            this.Name = name;
            this.HeightLevelValid = heightLevelValid;
        }

        public DOPrm()
        {

        }
    }
    public sealed class DOPrmMgr : EntityMgr<DOPrm, int>
    {
       
    }
}
