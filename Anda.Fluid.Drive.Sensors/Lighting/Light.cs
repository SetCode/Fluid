using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;

namespace Anda.Fluid.Drive.Sensors.Lighting
{
    public class Light : EntityBase<int>
    {
        public Light(int key, ILighting lighting)
            : base(key)
        {
            this.Lighting = lighting;
        }

        public ILighting Lighting { get; set; }
    }
}
