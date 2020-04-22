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
    public class DIPrm : EntityBase<int>
    {
        [JsonProperty]
        public int CardKey { get; set; }

        [JsonProperty]
        public short DiId { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// 设定是否有效电平为高电平
        /// </summary>
        [DefaultValue(true)]
        [JsonProperty]
        public bool HeightLevelValid { get; set; }

        public DIPrm(int key, int cardKey, short diId, string name, bool heightLevelValid = true)
            : base(key)
        {
            this.CardKey = cardKey;
            this.DiId = diId;
            this.Name = name;
            this.HeightLevelValid = heightLevelValid;
        }

        public DIPrm()
        {

        }
    }

    public sealed class DIPrmMgr : EntityMgr<DIPrm,int>
    {
      
    }
}
