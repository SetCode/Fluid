using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public sealed class AxisMgr : EntityMgr<Axis, int>
    {
        private readonly static AxisMgr instance = new AxisMgr();
        private AxisMgr() { }
        public static AxisMgr Instance => instance;
    }
}
