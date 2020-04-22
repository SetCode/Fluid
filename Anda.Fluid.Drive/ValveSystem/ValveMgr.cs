using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;

namespace Anda.Fluid.Drive.ValveSystem
{
    public sealed class ValveMgr : EntityMgr<Valve, ValveType>
    {
        private readonly static ValveMgr instance = new ValveMgr();
        private ValveMgr()
        {

        }
        public static ValveMgr Instance => instance;
    }
}
