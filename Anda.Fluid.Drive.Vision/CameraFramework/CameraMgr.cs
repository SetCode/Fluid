using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.CameraFramework
{
    public sealed class CameraMgr : EntityMgr<Camera, int>
    {
        private readonly static CameraMgr instance = new CameraMgr();
        private CameraMgr() { }
        
        public static CameraMgr Instance => instance;
    }
    
}
