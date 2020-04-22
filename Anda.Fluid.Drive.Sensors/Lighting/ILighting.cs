using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Lighting
{
    public interface ILighting 
    {

        LightVendor lightVendor { get; }
        
        void None();
        void ResetToLast();
        void SetLight(ExecutePrm prm);
    }
  
}
