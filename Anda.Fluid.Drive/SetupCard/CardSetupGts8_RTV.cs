using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion;
using Anda.Fluid.Drive.Motion.ActiveItems;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupCard
{
    /// <summary>
    /// 8轴Gts卡，双阀
    /// </summary>
    internal class CardSetupGts8_RTV : ICardSetupable
    {
        public RobotXYZ Setup()
        {
            //setup card
            Card card = new Card((int)CardType.Card0, new GtsExecutor(), 0, 8, CardType.Card0.ToString(), SettingsPath.PathGts + "\\" + "GTS800.cfg");
            CardMgr.Instance.Add(card);

            //setup ExtMdl
            ExtMdl extMdl = new ExtMdl((int)CardType.ExtMdl, card.CardId, new GtsExtMdlExecutor(), 0, CardType.ExtMdl.ToString(), SettingsPath.PathGts + "\\" + "extmdl.cfg");
            ExtMdlMgr.Instance.Add(extMdl);

            bool flag = true;
            //setup axes
            if (AxisPrmMgr.Instance.Load())
            {
                int[] ii = new int[] { 1, 2, 3, 5, 6, 7, 8 };
                foreach (var i in ii)
                {
                    if(AxisPrmMgr.Instance.FindBy(i) == null)
                    {
                        flag = false;   
                    }
                }
            }
            else
            {
                flag = false;
            }

            if (!flag)
            {
                AxisPrmMgr.Instance.Clear();
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(1)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(2)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(3)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(5)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(6)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(7)));
                AxisPrmMgr.Instance.Add(SettingUtil.ResetToDefault<AxisPrm>(new AxisPrm(8)));
            }
            Axis axisX = new Axis((int)AxisType.X轴, card, 1, AxisType.X轴.ToString(), AxisPrmMgr.Instance.FindBy(1));
            Axis axisY = new Axis((int)AxisType.Y轴, card, 2, AxisType.Y轴.ToString(), AxisPrmMgr.Instance.FindBy(2));
            Axis axisZ = new Axis((int)AxisType.Z轴, card, 3, AxisType.Z轴.ToString(), AxisPrmMgr.Instance.FindBy(3));
            Axis axis5 = new Axis((int)AxisType.Axis5, card, 5, "上层轨道运输", AxisPrmMgr.Instance.FindBy(5));
            Axis axis6 = new Axis((int)AxisType.Axis6, card, 6, "上层轨道调幅", AxisPrmMgr.Instance.FindBy(6));
            Axis axis7 = new Axis((int)AxisType.Axis7, card, 7, "下层轨道运输", AxisPrmMgr.Instance.FindBy(7));
            Axis axis8 = new Axis((int)AxisType.Axis8, card, 8, "下层轨道调幅", AxisPrmMgr.Instance.FindBy(8));
            AxisMgr.Instance.Add(axisX);
            AxisMgr.Instance.Add(axisY);
            AxisMgr.Instance.Add(axisZ);
            AxisMgr.Instance.Add(axis5);
            AxisMgr.Instance.Add(axis6);
            AxisMgr.Instance.Add(axis7);
            AxisMgr.Instance.Add(axis8);
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
