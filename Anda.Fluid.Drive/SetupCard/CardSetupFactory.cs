using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupCard
{
    /// <summary>
    /// 简单工厂模式，卡配置管理
    /// </summary>
    public static class CardSetupFactory
    {
        /// <summary>
        /// 更加机台配置获取卡配置，新增卡需要更改此方法
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static ICardSetupable GetCardSetup(MachineSetting setting)
        {
            ICardSetupable rtn = null;
            switch(setting.CardSelect)
            {
                case CardSelection.Gts8x1:
                    rtn = new CardSetupGts8x1();
                    break;
                case CardSelection.Gts4x2:
                    rtn = new CardSetupGts4x2();
                    break;
                case CardSelection.Gts4x1:
                    if (setting.AxesStyle == Motion.ActiveItems.RobotAxesStyle.XYZR)
                    {
                        rtn = new CardSetupGts4_XYZR();
                    }
                    else if (setting.AxesStyle == Motion.ActiveItems.RobotAxesStyle.XYZU)
                    {
                        rtn = new CardSetupGts4_XYZU();
                    }
                    else if (setting.AxesStyle == Motion.ActiveItems.RobotAxesStyle.XYZ)
                    {
                        rtn = new CardSetupGts4_XYZ();
                    }                   
                    break;
                case CardSelection.ADMC4:
                    rtn = new CardSetupADMC();
                    break;
                case CardSelection.Gts8_RTV:
                    rtn = new CardSetupGts8_RTV();
                    break;
            }
            return rtn;
        }
    }
}
