using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.Prm
{
    public class ConveyorPrmMgr: EntityMgr<ConveyorPrm,int>
    {
        private static ConveyorPrmMgr instance = new ConveyorPrmMgr(SettingsPath.PathMachine);
        private ConveyorPrmMgr()
        {
            this.Add(new ConveyorPrm(0));
            this.Add(new ConveyorPrm(1));
        }
        private ConveyorPrmMgr(string dir) : base(dir)
        {
            this.Add(new ConveyorPrm(0));
            this.Add(new ConveyorPrm(1));
        }
        public static ConveyorPrmMgr Instance => instance;
    }
}
