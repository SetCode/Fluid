using Anda.Fluid.Drive.Sensors.Lighting.Custom;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Anda.Fluid.Drive.Sensors.Lighting
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class ExecutePrm : ICloneable
    {
        public ExecutePrm()
        {

        }

        [JsonProperty]
        public LightType LightType { get; set; } = LightType.Coax;

        [JsonProperty]
        public ExecutePrmOPT PrmOPT = new ExecutePrmOPT();

        public object Clone()
        {
            ExecutePrm ret = this.MemberwiseClone() as ExecutePrm;
            ret.PrmOPT = this.PrmOPT.Clone() as ExecutePrmOPT;
            return ret;
        }

        //[JsonProperty]
        //public ExecutePrmCustom PrmCustom { get; set; } = new ExecutePrmCustom();
    }
}
