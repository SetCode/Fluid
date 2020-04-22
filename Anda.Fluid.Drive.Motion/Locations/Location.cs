using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;

namespace Anda.Fluid.Drive.Motion.Locations
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Location : EntityBase<string>, ICloneable
    {
        public Location()
        {

        }

        public Location(string key)
            : this(key, false)
        {
        }

        public Location(string key, bool isSystemLoc)
            : base(key)
        {
            this.IsSystemLoc = isSystemLoc;
        }

        [JsonProperty]
        public bool IsSystemLoc { get; set; } 

        [JsonProperty]
        public double X { get; set; }

        [JsonProperty]
        public double Y { get; set; }

        [JsonProperty]
        public double Z { get; set; }
        public object Clone()
        {
            return (Location)this.MemberwiseClone();
        }
        public override string ToString()
        {
            return new StringBuilder()
                .Append("[").Append(X.ToString("0.000"))
                .Append(",").Append(Y.ToString("0.000"))
                .Append(",").Append(Z.ToString("0.000"))
                .Append("]").ToString();
        }
    }
}
