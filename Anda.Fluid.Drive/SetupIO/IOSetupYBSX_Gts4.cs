using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    internal class IOSetupYBSX_Gts4 : IOSetupBase
    {
        public override void SetupDIPrm()
        {
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.对刀仪, cardKey0, 1, DiType.对刀仪.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.气压检测, cardKey0, 2, DiType.气压检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道通信, cardKey0, 4, DiType.轨道通信.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶阀温度检测, cardKey0, 6, DiType.胶阀温度检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应2, cardKey0, 13, DiType.胶量感应2.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应1, cardKey0, 14, DiType.胶量感应1.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.门禁, cardKey0, 15, DiType.门禁.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.急停, cardKey0, 16, DiType.急停.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.光纤, cardKey0, 12, DiType.光纤.ToString()));
        }

        public override void SetupDOPrm()
        {
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.飞测, cardKey0, 1, DoType.飞测.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.飞拍, cardKey0, 2, DoType.飞拍.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.离子风棒1, cardKey0, 5, DoType.离子风棒1.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.离子风棒2, cardKey0, 6, DoType.离子风棒2.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.轨道通信, cardKey0, 7, DoType.轨道通信.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.环形光, cardKey0, 8, DoType.环形光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.同轴光, cardKey0, 9, DoType.同轴光.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.红色信号灯, cardKey0, 10, DoType.红色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.黄色信号灯, cardKey0, 11, DoType.黄色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.绿色信号灯, cardKey0, 12, DoType.绿色信号灯.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.蜂鸣器, cardKey0, 13, DoType.蜂鸣器.ToString()));
            //this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪加热2, cardKey0, 14, DoType.胶枪加热2.ToString()));
            //this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪加热1, cardKey0, 15, DoType.胶枪加热1.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.真空清洗, cardKey0, 16, DoType.真空清洗.ToString()));

            //阀1胶桶气压控制
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.阀1胶桶气压控制, cardKey0, 14, DoType.阀1胶桶气压控制.ToString()));
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
