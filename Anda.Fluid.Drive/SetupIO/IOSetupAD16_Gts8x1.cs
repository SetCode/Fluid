using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    internal class IOSetupAD16_Gts8x1 : IOSetupBase
    {
        public override void SetupDIPrm()
        {
            // AD-16 8x1 setup DI
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.对刀仪, cardKey0, 1, DiType.对刀仪.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.气压检测, cardKey0, 2, DiType.气压检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.轨道通信, cardKey0, 4, DiType.轨道通信.ToString()));
            //this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶阀温度检测, cardKey0, 6, DiType.胶阀温度检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应2, cardKey0, 13, DiType.胶量感应2.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.胶量感应1, cardKey0, 14, DiType.胶量感应1.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.门禁, cardKey0, 15, DiType.门禁.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.急停, cardKey0, 16, DiType.急停.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.进板检测, extMdlKey, 10, DiType.进板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.定位1检测, extMdlKey, 11, DiType.定位1检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.定位2检测, extMdlKey, 12, DiType.定位2检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.出板检测, extMdlKey, 13, DiType.出板检测.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.前设备放板信号, extMdlKey, 14, DiType.前设备放板信号.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.后设备求板信号, extMdlKey, 15, DiType.后设备求板信号.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.启动, extMdlKey, 9, DiType.启动.ToString()));

            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.阀1温度报警, cardKey0, 6, DiType.阀1温度报警.ToString()));
            this.IOPrm.DIPrmMgr.Add(new DIPrm((int)DiType.阀2温度报警, cardKey0, 8, DiType.阀2温度报警.ToString()));
        }

        public override void SetupDOPrm()
        {
            // AD-16 8x1 setup DO    
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
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.求板信号, extMdlKey, 0, DoType.求板信号.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.放板信号, extMdlKey, 1, DoType.放板信号.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.运输反转, extMdlKey, 2, DoType.运输反转.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.运输正转, extMdlKey, 3, DoType.运输正转.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热开, extMdlKey, 6, DoType.预热开.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热阻挡, extMdlKey, 7, DoType.预热阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热顶升, extMdlKey, 8, DoType.预热顶升.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.预热吹气, extMdlKey, 9, DoType.预热吹气.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作阻挡, extMdlKey, 10, DoType.工作阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作顶升, extMdlKey, 11, DoType.工作顶升.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.工作吹气, extMdlKey, 12, DoType.工作吹气.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.出板阻挡, extMdlKey, 13, DoType.出板阻挡.ToString()));
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.出板顶升, extMdlKey, 14, DoType.出板顶升.ToString()));
            //临时改为擦纸带
            this.IOPrm.DOPrmMgr.Add(new DOPrm((int)DoType.擦纸带, extMdlKey, 15, DoType.擦纸带.ToString()));

        }
    }
}
