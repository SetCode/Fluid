using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public sealed class DIMgr : EntityMgr<DI, int>
    {
        private readonly static DIMgr instance = new DIMgr(SettingsPath.PathMachine);
        private DIMgr() { }
        private DIMgr(string dir) : base(dir)
        { }
        public static DIMgr Instance { get { return instance; } }
    }
}
