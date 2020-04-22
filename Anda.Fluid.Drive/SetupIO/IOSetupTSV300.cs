using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    public class IOSetupTSV300 : IOSetupBase
    {
        public override void SetupDIPrm()
        {
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.对刀仪, cardKey0, 1, DiType.对刀仪.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.气压检测, cardKey0, 2, DiType.气压检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.启动, cardKey0, 10, DiType.启动.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.停止, cardKey0, 11, DiType.停止.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.暂停, cardKey0, 12, DiType.暂停.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应1, cardKey0, 14, DiType.胶量感应1.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应2, cardKey0, 13, DiType.胶量感应2.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.急停, cardKey0, 16, DiType.急停.ToString()));
        }

        public override void SetupDOPrm()
        {
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.同轴光, cardKey0, 8, DoType.同轴光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.环形光, cardKey0, 9, DoType.环形光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.红色信号灯, cardKey0, 10, DoType.红色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.黄色信号灯, cardKey0, 11, DoType.黄色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.绿色信号灯, cardKey0, 12, DoType.绿色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.蜂鸣器, cardKey0, 13, DoType.蜂鸣器.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪加热1, cardKey0, 15, DoType.胶枪加热1.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.真空清洗, cardKey0, 16, DoType.真空清洗.ToString()));
        }
    }
}
