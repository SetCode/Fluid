using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    internal class IOSetupIjet6 : IOSetupBase
    {
        public override void SetupDIPrm()
        {
            // iJet6 setup DI
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.门禁, cardKey0, 1, DiType.门禁.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.急停, cardKey0, 2, DiType.急停.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.气压检测, cardKey0, 3, DiType.气压检测.ToString(), false));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.定位2检测, cardKey0, 4, DiType.定位2检测.ToString(), false));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.定位1检测, cardKey0, 5, DiType.定位1检测.ToString(), false));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.进板检测, cardKey0, 6, DiType.进板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.出板检测, cardKey0, 8, DiType.出板检测.ToString(), false));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应1, cardKey0, 11, DiType.胶量感应1.ToString(), false));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.对刀仪, cardKey0, 12, DiType.对刀仪.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶阀温度检测, cardKey0, 13, DiType.胶阀温度检测.ToString(), false));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.前设备放板信号, cardKey0, 14, DiType.前设备放板信号.ToString(), false));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.后设备求板信号, cardKey0, 16, DiType.后设备求板信号.ToString(), false));

        }

        public override void SetupDOPrm()
        {
            // iJet6 setup DO
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.绿色信号灯, cardKey0, 1, DoType.绿色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.黄色信号灯, cardKey0, 2, DoType.黄色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.蜂鸣器, cardKey0, 3, DoType.蜂鸣器.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.红色信号灯, cardKey0, 4, DoType.红色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.运输正转, cardKey0, 5, DoType.运输正转.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.真空清洗, cardKey0, 6, DoType.真空清洗.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.运输反转, cardKey0, 7, DoType.运输反转.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.同轴光, cardKey0, 8, DoType.同轴光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.环形光, cardKey0, -1, DoType.环形光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪加热1, cardKey0, 9, DoType.胶枪加热1.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作顶升, cardKey0, 10, DoType.工作顶升.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作阻挡, cardKey0, 11, DoType.工作阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.链条减速, cardKey0, 12, DoType.链条减速.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪一胶阀, cardKey0, 13, DoType.胶枪一胶阀.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.放板信号, cardKey0, 14, DoType.放板信号.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪一气阀, cardKey0, 15, DoType.胶枪一气阀.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.求板信号, cardKey0, 16, DoType.求板信号.ToString()));

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
