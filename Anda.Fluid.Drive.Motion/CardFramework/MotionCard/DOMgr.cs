using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public sealed class DOMgr : EntityMgr<DO, int>
    {
        private readonly static DOMgr instance = new DOMgr(SettingsPath.PathMachine);
        private DOMgr() { }
        private DOMgr(string dir) : base(dir) { }
        public static DOMgr Instance { get { return instance; } }
        public DO FindByName(string name)
        {
            foreach (var item in this.list)
            {
                if (item.Prm.Name.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
