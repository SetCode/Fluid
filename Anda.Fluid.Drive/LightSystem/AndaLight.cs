using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision.CameraFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.LightSystem
{
    public class AndaLight:ILighting
    {
        public LightVendor lightVendor { get; } = LightVendor.Anda;
        public LightType CurrType { get; private set; } = LightType.Ring;

        public void None()
        {
            this.SetLight(LightType.None);
        }

        public void Coax()
        {
            this.SetLight(LightType.Coax);
        }

        public void Ring()
        {
            this.SetLight(LightType.Ring);
        }

        public void Both()
        {
            this.SetLight(LightType.Both);
        }

        public void ResetToLast()
        {
            this.SetLight(this.CurrType);
        }

        public void SetLight(LightType lightType)
        {           
            switch (lightType)
            {
                case LightType.None:
                    DoType.同轴光.Set(false);
                    DoType.环形光.Set(false);
                    break;
                case LightType.Coax:
                    DoType.同轴光.Set(true);
                    DoType.环形光.Set(false);
                    break;
                case LightType.Ring:
                    DoType.同轴光.Set(false);
                    DoType.环形光.Set(true);
                    break;
                case LightType.Both:
                    DoType.同轴光.Set(true);
                    DoType.环形光.Set(true);
                    break;
            }
           
        }
        public void SetLight(ExecutePrm prm)
        {
            this.CurrType = prm.LightType;
            this.SetLight(this.CurrType);
            if (prm.LightType != LightType.None)
            {
                this.CurrType = prm.LightType;
            }
            Machine.Instance.Light.ExecutePrm = prm;
        }

    }
  
}
