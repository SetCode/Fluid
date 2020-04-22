using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Drive.Motion;

namespace Anda.Fluid.Drive.SetupCard
{
    /// <summary>
    /// 4轴Gts卡 x 2，双阀
    /// </summary>
    internal class CardSetupGts4x2 : ICardSetupable
    {
        public RobotXYZ Setup()
        {
            //setup card
            Card card1 = new Card((int)CardType.Card0, new GtsExecutor(), 0, 4, CardType.Card0.ToString(), SettingsPath.PathGts + "\\" + "GTS1.cfg");
            CardMgr.Instance.Add(card1);
            Card card2 = new Card((int)CardType.Card1, new GtsExecutor(), 1, 4, CardType.Card1.ToString(), SettingsPath.PathGts + "\\" + "GTS2.cfg");
            CardMgr.Instance.Add(card2);

            ////setup ExtMdl
            //ExtMdl extMdl = new ExtMdl((int)CardType.ExtMdl, 0, new GtsExtMdlExecutor(), 0, CardType.ExtMdl.ToString(), SettingsPath.PathGts + "\\" + "extmdl.cfg");
            //ExtMdlMgr.Instance.Add(extMdl);

            //setup axes
            if (!AxisPrmMgr.Instance.Load())
            {
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(1)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(2)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(3)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(4)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(5)));
            }
            Axis axisX = new Axis((int)AxisType.X轴, card1, 1, AxisType.X轴.ToString(), AxisPrmMgr.Instance.FindBy(1));
            Axis axisY = new Axis((int)AxisType.Y轴, card1, 2, AxisType.Y轴.ToString(), AxisPrmMgr.Instance.FindBy(2));
            Axis axisZ = new Axis((int)AxisType.Z轴, card2, 1, AxisType.Z轴.ToString(), AxisPrmMgr.Instance.FindBy(3));
            Axis axisA = new Axis((int)AxisType.A轴, card1, 3, AxisType.A轴.ToString(), AxisPrmMgr.Instance.FindBy(4));
            Axis axisB = new Axis((int)AxisType.B轴, card1, 4, AxisType.B轴.ToString(), AxisPrmMgr.Instance.FindBy(5));
            AxisMgr.Instance.Add(axisX);
            AxisMgr.Instance.Add(axisY);
            AxisMgr.Instance.Add(axisZ);
            AxisMgr.Instance.Add(axisA);
            AxisMgr.Instance.Add(axisB);
            return new RobotXYZ(axisX, axisY, axisA, axisB, axisZ);
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
            Card card2 = CardMgr.Instance.FindBy((int)CardType.Card1);
            rtn = card2.Close();
            rtn = card2.Init();
            if (rtn != 0)
            {
                b = false;
                AlarmServer.Instance.Fire(card2, AlarmInfoMotion.FatalCardInit);
            }

            ////init extMdl ??? 4x2 需要初始化扩展模块么？
            //ExtMdl extMdl = ExtMdlMgr.Instance.FindBy((int)CardType.ExtMdl);
            //Log.Dprint("initing extMdl...");
            //rtn = extMdl.Close();
            //rtn = extMdl.Init();
            //if (rtn != 0)
            //{
            //    b = false;
            //    AlarmServer.Instance.Fire(card2, AlarmInfoMotion.FatalExtMdlInit);
            //}

            return b;
        }
    }
}
