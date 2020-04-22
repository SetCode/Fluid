using Anda.Fluid.Infrastructure.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.LoadTrajectory
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GlueDot
    {        
        [JsonProperty]
        public int index;
        [JsonProperty]
        public double Weight;
        [JsonProperty]
        public bool IsWeight=true;
        [JsonProperty]
        public int NunShots=1;
        [JsonProperty]
        public double Radius;
        [JsonProperty]
        public PointD point = new PointD();
    }
}
