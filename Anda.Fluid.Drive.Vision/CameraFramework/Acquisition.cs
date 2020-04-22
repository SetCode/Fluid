using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Tasker;

namespace Anda.Fluid.Drive.Vision.CameraFramework
{
    public sealed class Acquisition : TaskLoop
    {
        private readonly static Acquisition instance = new Acquisition();
        private Acquisition()
        {

        }
        public static Acquisition Instance => instance;

        protected override void Loop()
        {
            foreach (var item in CameraMgr.Instance.FindAll())
            {
                item.Update();
            }
        }
    }
}
