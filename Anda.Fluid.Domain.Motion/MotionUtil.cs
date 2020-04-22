using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Motion
{
    public class MotionUtil
    {
        public static void ResetAxisPrm(Axis axis)
        {
            SettingUtil.ResetToDefault<AxisPrm>(axis.Prm);
            AxisType type = (AxisType)axis.Key;
            switch (type)
            {
                case AxisType.X轴:
                    axis.Prm.MaxRunVel = 1000;
                    axis.Prm.MaxManualVel = 200;
                    break;
                case AxisType.Y轴:
                    axis.Prm.MaxRunVel = 1000;
                    axis.Prm.MaxManualVel = 200;
                    break;
                case AxisType.Z轴:
                    axis.Prm.MaxRunVel = 500;
                    axis.Prm.MaxManualVel = 100;
                    break;
                case AxisType.A轴:
                    axis.Prm.MaxRunVel = 500;
                    axis.Prm.MaxManualVel = 100;
                    break;
                case AxisType.B轴:
                    axis.Prm.MaxRunVel = 500;
                    axis.Prm.MaxManualVel = 100;
                    break;
                case AxisType.R轴:
                    axis.Prm.MaxRunVel = 1000;
                    axis.Prm.MaxManualVel = 200;
                    break;
                case AxisType.U轴:
                    axis.Prm.MaxRunVel = 1000;
                    axis.Prm.MaxManualVel = 200;
                    break;
                case AxisType.Axis5:
                    axis.Prm.MaxRunVel = 1000;
                    axis.Prm.MaxManualVel = 200;
                    break;
                case AxisType.Axis6:
                    axis.Prm.MaxRunVel = 1000;
                    axis.Prm.MaxManualVel = 200;
                    break;
                case AxisType.Axis7:
                    axis.Prm.MaxRunVel = 1000;
                    axis.Prm.MaxManualVel = 200;
                    break;
                case AxisType.Axis8:
                    axis.Prm.MaxRunVel = 1000;
                    axis.Prm.MaxManualVel = 200;
                    break;
            }
        }
    }
}
