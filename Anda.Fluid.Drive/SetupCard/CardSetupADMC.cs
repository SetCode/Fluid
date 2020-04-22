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
    /// 4轴安达卡
    /// </summary>
    internal class CardSetupADMC : ICardSetupable
    {
        public RobotXYZ Setup()
        {
            //setup card
            Card card1 = new Card((int)CardType.Card0, new AdExecutor(), 1, 4, CardType.Card0.ToString(), SettingsPath.PathGts + "\\" + "admc.cfg");
            CardMgr.Instance.Add(card1);
            //setup axes
            if (!AxisPrmMgr.Instance.Load())
            {
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(1)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(2)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(3)));
            }
            Axis axisX = new Axis((int)AxisType.X轴, card1, 1, AxisType.X轴.ToString(), AxisPrmMgr.Instance.FindBy(1));
            Axis axisY = new Axis((int)AxisType.Y轴, card1, 2, AxisType.Y轴.ToString(), AxisPrmMgr.Instance.FindBy(2));
            Axis axisZ = new Axis((int)AxisType.Z轴, card1, 3, AxisType.Z轴.ToString(), AxisPrmMgr.Instance.FindBy(3));
            Axis axisA = new Axis((int)AxisType.A轴, card1, 4, AxisType.A轴.ToString(), AxisPrmMgr.Instance.FindBy(4));
            Axis axisB = new Axis((int)AxisType.B轴, card1, 5, AxisType.B轴.ToString(), AxisPrmMgr.Instance.FindBy(5));
            AxisMgr.Instance.Add(axisX);
            AxisMgr.Instance.Add(axisY);
            AxisMgr.Instance.Add(axisZ);
            AxisMgr.Instance.Add(axisA);
            AxisMgr.Instance.Add(axisB);
            return new RobotXYZ(axisX, axisY, axisZ);
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
            //rtn = card1.Executor.SaveConfig(card1.CardId, "admc.cfg");
            rtn = card1.Executor.StopCrd(1, 1);
            return b;
        }
    }
}
