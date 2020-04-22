using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    public class IOSetupAD16_Gts4_单阀双轨:IOSetupBase
    {
        public override void SetupDIPrm()
        {
            // AD-16 4x1 setup DI
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.对刀仪, cardKey0, 1, DiType.对刀仪.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.气压检测, cardKey0, 2, DiType.气压检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道通信, cardKey0, 4, DiType.轨道通信.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.U轴限位, cardKey0, 12, DiType.U轴限位.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应2, cardKey0, 13, DiType.胶量感应2.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应1, cardKey0, 14, DiType.胶量感应1.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.门禁, cardKey0, 15, DiType.门禁.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.急停, cardKey0, 16, DiType.急停.ToString()));

            //AD16 4x1 拓展模块 setup DI
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.进板检测, extMdlKey, 0, DiType.进板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.定位2检测, extMdlKey, 1, "轨道1定位2检测"));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.出板检测, extMdlKey, 2, DiType.出板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道2进板检测, extMdlKey, 3, DiType.轨道2进板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道2定位2检测, extMdlKey, 4, "轨道2定位2检测"));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道2出板检测, extMdlKey, 5, DiType.轨道2出板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.前设备放板信号, extMdlKey, 6, "轨道1前设备放板信号"));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.后设备求板信号, extMdlKey, 7, "轨道1后设备求板信号"));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道2前设备放板信号, extMdlKey, 8, DiType.轨道2前设备放板信号.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道2后设备求板信号, extMdlKey, 9, DiType.轨道2后设备求板信号.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道2右到左定位2检测, extMdlKey, 12, DiType.轨道2右到左定位2检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道1右到左定位2检测, extMdlKey, 15, DiType.轨道1右到左定位2检测.ToString()));
        }

        public override void SetupDOPrm()
        {
            // AD-16 4x1 setup DO
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热阻挡, cardKey0, 1, "轨道1预热阻挡"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热顶升, cardKey0, 2, "轨道1预热顶升"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热吹气, cardKey0, 3, "轨道1预热吹气"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2运输反转, cardKey0, 5, DoType.轨道2运输反转.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2运输正转, cardKey0, 6, DoType.轨道2运输正转.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.滑台气缸升降, cardKey0, 7, DoType.滑台气缸升降.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.环形光, cardKey0, 8, DoType.环形光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.同轴光, cardKey0, 9, DoType.同轴光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.红色信号灯, cardKey0, 10, DoType.红色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.黄色信号灯, cardKey0, 11, DoType.黄色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.绿色信号灯, cardKey0, 12, DoType.绿色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.蜂鸣器, cardKey0, 13, DoType.蜂鸣器.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪加热2, cardKey0, 14, DoType.胶枪加热2.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪加热1, cardKey0, 15, DoType.胶枪加热1.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.真空清洗, cardKey0, 16, DoType.真空清洗.ToString()));

            //AD16 4x1 拓展模块 setup DO
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.求板信号, extMdlKey, 0, "轨道1求板信号"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.放板信号, extMdlKey, 1, "轨道1放板信号"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.运输反转, extMdlKey, 2, "轨道1运输反转"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.运输正转, extMdlKey, 3, "轨道1运输正转"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2放板信号, extMdlKey, 4, DoType.轨道2放板信号.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2求板信号, extMdlKey, 5, DoType.轨道2求板信号.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热开, extMdlKey, 6, DoType.预热开.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2右到左工作阻挡, extMdlKey, 7, DoType.轨道2右到左工作阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2工作顶升, extMdlKey, 8, DoType.轨道2工作顶升.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2工作阻挡, extMdlKey, 9, DoType.轨道2工作阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道1右到左工作阻挡, extMdlKey, 10, "轨道1右到左工作阻挡"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作顶升, extMdlKey, 11, "轨道1工作顶升"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作阻挡, extMdlKey, 12, "轨道1工作工作阻挡"));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2预热阻挡, extMdlKey, 13, DoType.轨道2预热阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2预热顶升, extMdlKey, 14, DoType.轨道2预热顶升.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道2预热吹气, extMdlKey, 15, DoType.轨道2预热吹气.ToString()));

        }

        public override void SetupIO()
        {
            foreach (var item in this.IOPrm.DIPrmMgr.FindAll())
            {
                DIMgr.Instance.Add(new DI(item.Key, base.GetIOExecutor(item.CardKey), this.GetDIPrmBy(item.Key)));
            }
            foreach (var item in this.IOPrm.DOPrmMgr.FindAll())
            {
                DOMgr.Instance.Add(new DO(item.Key, base.GetIOExecutor(item.CardKey), base.GetCardId(item.CardKey), this.GetDOPrmBy(item.Key), 0));
            }
        }
    }
}

