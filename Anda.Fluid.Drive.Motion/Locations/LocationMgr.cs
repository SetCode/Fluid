using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure;

namespace Anda.Fluid.Drive.Motion.Locations
{
    public sealed class LocationMgr : EntityMgr<Location, string>
    {
        private readonly static LocationMgr instance = new LocationMgr(SettingsPath.PathMachine);
        private LocationMgr()
        {

        }
        private LocationMgr(string dir) : base(dir)
        { }
        public static LocationMgr Instance => instance;
    }
}
