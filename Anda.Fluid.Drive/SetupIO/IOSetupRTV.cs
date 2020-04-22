using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    internal class IOSetupRTV : IOSetupBase
    {
        public override void SetupDIPrm()
        {
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.急停, cardKey0, 1, DiType.急停.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.启动, cardKey0, 2, DiType.启动.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.停止, cardKey0, 3, DiType.停止.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.对刀仪,cardKey0, 4 , DiType.对刀仪.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.前设备放板信号, cardKey0, 5, DiType.前设备放板信号.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.后设备求板信号, cardKey0, 6, DiType.后设备求板信号.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.下层轨道出板检测, cardKey0, 9, DiType.下层轨道出板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.下层轨道进板检测, cardKey0, 10, DiType.下层轨道进板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.进板检测, cardKey0, 11, DiType.进板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.定位2检测, cardKey0, 12, DiType.定位2检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.出板检测, cardKey0, 13, DiType.出板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.前上门门禁, cardKey0, 14, DiType.前上门门禁.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.后上门门禁, cardKey0, 15, DiType.后上门门禁.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.前下门门禁, cardKey0, 16, DiType.前下门门禁.ToString()));

            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.下层轨道放板信号, extMdlKey, 0, DiType.下层轨道放板信号.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.下层轨道求板信号, extMdlKey, 1, DiType.下层轨道求板信号.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.压板下位, extMdlKey, 5, DiType.压板下位.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.压板上位, extMdlKey, 6, DiType.压板上位.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.测高下位, extMdlKey, 7, DiType.测高下位.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.测高上位, extMdlKey, 8, DiType.测高上位.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.待料阻挡下位, extMdlKey, 9, DiType.待料阻挡下位.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.待料阻挡上位, extMdlKey, 10, DiType.待料阻挡上位.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.涂胶挡板下位, extMdlKey, 11, DiType.涂胶挡板下位.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.涂胶挡板上位, extMdlKey, 12, DiType.涂胶挡板上位.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应1, extMdlKey, 13, DiType.胶量感应1.ToString()));
        }

        public override void SetupDOPrm()
        {
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.测高阀, cardKey0, 4, DoType.测高阀.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作阻挡, cardKey0, 3, DoType.工作阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作顶升, cardKey0, 5, DoType.工作顶升.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.绿色信号灯, cardKey0, 6, DoType.绿色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.黄色信号灯, cardKey0, 7, DoType.黄色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.红色信号灯, cardKey0, 8, DoType.红色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.蜂鸣器, cardKey0, 9, DoType.蜂鸣器.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.上层轨道调宽刹车, cardKey0, 10, DoType.上层轨道调宽刹车.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.下层轨道调宽刹车, cardKey0, 11, DoType.下层轨道调宽刹车.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.求板信号, cardKey0, 12, DoType.求板信号.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.放板信号, cardKey0, 13, DoType.放板信号.ToString()));

            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.下层轨道有板信号, extMdlKey, 0, DoType.下层轨道有板信号.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.下层轨道求板信号, extMdlKey, 1, DoType.下层轨道求板信号.ToString()));
        }
    }
}
