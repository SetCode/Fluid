using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    public class HeaterControllerMgr:EntityMgr<HeaterController,int>
    {
        public enum Vendor
        {
            Omron,
            Aika,
            Disable
        }
        private readonly static HeaterControllerMgr instance = new HeaterControllerMgr();
        private HeaterControllerMgr()
        {
        }
        public static HeaterControllerMgr Instance => instance;
    }
}
