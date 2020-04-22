using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    internal class IOSetupAD16_Gts4x2 : IOSetupBase
    {
        public override void SetupDIPrm()
        {
            // AD-16 4x2 setup DI
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.对刀仪, cardKey0, 1, DiType.对刀仪.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.气压检测, cardKey0, 2, DiType.气压检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道通信, cardKey0, 4, DiType.轨道通信.ToString()));
            //this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶阀温度检测, cardKey0, 6, DiType.胶阀温度检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应2, cardKey0, 13, DiType.胶量感应2.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应1, cardKey0, 14, DiType.胶量感应1.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.门禁, cardKey0, 15, DiType.门禁.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.急停, cardKey0, 16, DiType.急停.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.进板检测, cardKey1, 11, DiType.进板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.定位1检测, cardKey1, 12, DiType.定位1检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.定位2检测, cardKey1, 13, DiType.定位2检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.出板检测, cardKey1, 14, DiType.出板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.前设备放板信号, cardKey1, 15, DiType.前设备放板信号.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.后设备求板信号, cardKey1, 16, DiType.后设备求板信号.ToString()));

            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.阀1温度报警, cardKey0, 6, DiType.阀1温度报警.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.阀2温度报警, cardKey0, 8, DiType.阀2温度报警.ToString()));
        }

        public override void SetupDOPrm()
        {
            // AD-16 4x2 setup DO   
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
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪加热2, cardKey0, 14, DoType.胶枪加热2.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.胶枪加热1, cardKey0, 15, DoType.胶枪加热1.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.真空清洗, cardKey0, 16, DoType.真空清洗.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.求板信号, cardKey1, 1, DoType.求板信号.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.放板信号, cardKey1, 2, DoType.放板信号.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.运输反转, cardKey1, 3, DoType.运输反转.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.运输正转, cardKey1, 4, DoType.运输正转.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热开, cardKey1, 7, DoType.预热开.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热阻挡, cardKey1, 8, DoType.预热阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热顶升, cardKey1, 9, DoType.预热顶升.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热吹气, cardKey1, 10, DoType.预热吹气.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作阻挡, cardKey1, 11, DoType.工作阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作顶升, cardKey1, 12, DoType.工作顶升.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作吹气, cardKey1, 13, DoType.工作吹气.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.出板阻挡, cardKey1, 14, DoType.出板阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.出板顶升, cardKey1, 15, DoType.出板顶升.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.出板吹气, cardKey1, 16, DoType.出板吹气.ToString()));

        }
    }
}
