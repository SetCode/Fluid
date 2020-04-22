using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    /// <summary>
    /// 简单工厂模式，管理IO的配置创建
    /// </summary>
    public static class IOSetupFactory
    {
        /// <summary>
        /// 根据机台配置获取IO配置，新增IO配置，需要修改此方法
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IIOSetupable GetIOSetupable(MachineSetting setting)
        {
            IIOSetupable ioSetupable = null;
            if (setting.MachineSelect == MachineSelection.AD16 || setting.MachineSelect == MachineSelection.iJet7 || setting.MachineSelect == MachineSelection.AFN)
            {
                switch (setting.CardSelect)
                {
                    case CardSelection.Gts8x1:
                        ioSetupable = new IOSetupAD16_Gts8x1();
                        break;
                    case CardSelection.Gts4x2:
                        ioSetupable = new IOSetupAD16_Gts4x2();
                        break;
                    case CardSelection.ADMC4:
                        ioSetupable = new IOSetupAD16_ADMC4();
                        break;
                    case CardSelection.Gts4x1:
                        if (setting.AxesStyle == Motion.ActiveItems.RobotAxesStyle.XYZU || setting.AxesStyle == Motion.ActiveItems.RobotAxesStyle.XYZ) 
                        {
                            if (setting.ConveyorSelect == ConveyorSelection.双轨)
                            {
                                ioSetupable = new IOSetupAD16_Gts4_单阀双轨();
                            }
                            else
                            {
                                ioSetupable = new IOSetupAD16_Gts8x1();
                            }
                        }
                        break;
                }
            }
            else if (setting.MachineSelect == MachineSelection.iJet6)
            {
                ioSetupable = new IOSetupIjet6();
            }
            else if (setting.MachineSelect == MachineSelection.AD19)
            {
                ioSetupable = new IOSetupAD19();
            }
            else if(setting.MachineSelect == MachineSelection.YBSX)
            {
                ioSetupable = new IOSetupYBSX_Gts4();
            }
            else if(setting.MachineSelect == MachineSelection.RTV)
            {
                ioSetupable = new IOSetupRTV();
            }
            else if(setting.MachineSelect == MachineSelection.TSV300)
            {
                ioSetupable = new IOSetupTSV300();
            }
            return ioSetupable;
        } 
    }
}
