using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    internal class IOSetupAD19 : IOSetupBase
    {
        public override void SetupDIPrm()
        {
            // AD19 setup DI
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.门禁, cardKey0, 1, DiType.门禁.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.急停, cardKey0, 2, DiType.急停.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.对刀仪, cardKey0, 3, DiType.对刀仪.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.气压检测, cardKey0, 4, DiType.气压检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应1, cardKey0, 5, DiType.胶量感应1.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应2, cardKey0, 6, DiType.胶量感应2.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.排风检测, cardKey0, 7, DiType.排风检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.双阀切换气缸1, cardKey0, 9, DiType.双阀切换气缸1.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.双阀切换气缸2, cardKey0, 10, DiType.双阀切换气缸2.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.阀1左倾斜, cardKey0, 11, DiType.阀1左倾斜.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.阀1右倾斜, cardKey0, 12, DiType.阀1右倾斜.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.阀1前倾斜, cardKey0, 13, DiType.阀1前倾斜.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.阀1后倾斜, cardKey0, 14, DiType.阀1后倾斜.ToString()));

        }

        public override void SetupDOPrm()
        {
            // AD19 setup DO
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.蜂鸣器, cardKey0, 1, DoType.蜂鸣器.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.红灯闪亮, cardKey0, 2, DoType.红灯闪亮.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.黄灯闪亮, cardKey0, 3, DoType.黄灯闪亮.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.绿灯闪亮, cardKey0, 4, DoType.绿灯闪亮.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.绿色信号灯, cardKey0, 5, DoType.绿色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.蓝灯闪亮, cardKey0, 6, DoType.蓝灯闪亮.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.真空清洗, cardKey0, 7, DoType.真空清洗.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.排风电机, cardKey0, 8, DoType.排风电机.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.主气压电磁阀, cardKey0, 9, DoType.主气压电磁阀.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.阀1胶桶气压控制, cardKey0, 10, DoType.阀1胶桶气压控制.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.阀2胶桶气压控制, cardKey0, 11, DoType.阀2胶桶气压控制.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.机械测高气动, cardKey0, 12, DoType.机械测高气动.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.同轴光, cardKey0, 13, DoType.同轴光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.环形光, cardKey0, 14, DoType.环形光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.飞测, cardKey0, 15, DoType.飞测.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.飞拍, cardKey0, 16, DoType.飞拍.ToString()));

        }
    }
}
