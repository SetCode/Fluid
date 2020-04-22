using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public sealed class ExtMdlMgr : EntityMgr<ExtMdl, int>
    {
        private static readonly ExtMdlMgr instance = new ExtMdlMgr();
        private ExtMdlMgr() { }
        public static ExtMdlMgr Instance => instance;
    }
}
