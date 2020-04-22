using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Drive.Motion;

namespace Anda.Fluid.Drive.SetupCard
{
    /// <summary>
    /// 4轴Gts卡，U轴：胶阀倾斜
    /// </summary>
    internal class CardSetupGts4_XYZU : ICardSetupable
    {
        public RobotXYZ Setup()
        {
            //setup card
            Card card = new Card((int)CardType.Card0, new GtsExecutor(), 0, 4, CardType.Card0.ToString(), SettingsPath.PathGts + "\\" + "GTS800.cfg");
            CardMgr.Instance.Add(card);

            //setup ExtMdl
            ExtMdl extMdl = new ExtMdl((int)CardType.ExtMdl, card.CardId, new GtsExtMdlExecutor(), 0, CardType.ExtMdl.ToString(), SettingsPath.PathGts + "\\" + "extmdl.cfg");
            ExtMdlMgr.Instance.Add(extMdl);

            //setup axes
            if (!AxisPrmMgr.Instance.Load())
            {
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(1)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(2)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(3)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(4)));
                AxisPrmMgr.Instance.FindBy(4).Lead = 360;
            }
            Axis axisX = new Axis((int)AxisType.X轴, card, 1, AxisType.X轴.ToString(), AxisPrmMgr.Instance.FindBy(1));
            Axis axisY = new Axis((int)AxisType.Y轴, card, 2, AxisType.Y轴.ToString(), AxisPrmMgr.Instance.FindBy(2));
            Axis axisZ = new Axis((int)AxisType.Z轴, card, 3, AxisType.Z轴.ToString(), AxisPrmMgr.Instance.FindBy(3));
            Axis axisU = new Axis((int)AxisType.U轴, card, 4, AxisType.U轴.ToString(), AxisPrmMgr.Instance.FindBy(4));
            AxisMgr.Instance.Add(axisX);
            AxisMgr.Instance.Add(axisY);
            AxisMgr.Instance.Add(axisZ);
            AxisMgr.Instance.Add(axisU);
            return new RobotXYZ(axisX, axisY, axisZ, axisU, RobotAxesStyle.XYZU);
        }

        public bool Init()
        {
            bool b = true;
            //init card
            Log.Dprint("initing motion...");
            Card card1 = CardMgr.Instance.FindBy((int)CardType.Card0);
            short rtn = card1.Close();
            rtn = card1.Init();
            if (rtn != 0)
            {
                b = false;
                AlarmServer.Instance.Fire(card1, AlarmInfoMotion.FatalCardInit);
            }

            //init extMdl
            ExtMdl extMdl = ExtMdlMgr.Instance.FindBy((int)CardType.ExtMdl);
            Log.Dprint("initing extMdl...");
            rtn = extMdl.Close();
            rtn = extMdl.Init();
            if (rtn != 0)
            {
                b = false;
                AlarmServer.Instance.Fire(card1, AlarmInfoMotion.FatalExtMdlInit);
            }
            return b;
        }
    }
}
