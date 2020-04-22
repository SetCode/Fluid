using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Conveyor.ConveyorAction
{
    internal class ManualSMEMAEnterBoard
    {
        public void Execute(int conveyorNo)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //发求板信号
                ConveyorController.Instance.AskSignalling(conveyorNo, true);

                //气缸松开
                ConveyorController.Instance.SetWorkingSiteStopper(conveyorNo, false);
                ConveyorController.Instance.SetWorkingSiteLift(conveyorNo, false);
                DateTime startTime = DateTime.Now;
                while (!ConveyorController.Instance.SingleSiteEnterSensor(conveyorNo).Is(StsType.High))
                {
                    TimeSpan timeSpan = DateTime.Now - startTime;
                    if (timeSpan >= TimeSpan.FromSeconds(20))
                    {
                        MessageBox.Show("可能发生卡板");
                        return;
                    }
                    Thread.Sleep(1);
                }

                //如果工作站电眼或出板电眼感应有板，则反转一段距离再进入
                if (ConveyorController.Instance.SingleSiteArriveSensor(conveyorNo).Is(StsType.High)
                        || ConveyorController.Instance.SingleSiteExitSensor(conveyorNo).Is(StsType.High))
                {
                    ConveyorController.Instance.ConveyorBack(conveyorNo);

                    //回转计时开始
                    DateTime startBackTime = DateTime.Now;
                    while (ConveyorController.Instance.SingleSiteEnterSensor(conveyorNo).Is(StsType.Low)
                            || ConveyorController.Instance.SingleSiteEnterSensor(conveyorNo).Is(StsType.IsFalling))
                    {
                        TimeSpan timeSpan = DateTime.Now - startBackTime;
                        if (timeSpan >= TimeSpan.FromSeconds(10))
                        {
                            MessageBox.Show("可能发生卡板");
                            goto end;
                        }
                        Thread.Sleep(1);
                    }
                    ConveyorController.Instance.ConveyorAbortStop(conveyorNo);

                    Thread.Sleep(2);
                }

                //如果没有感应到有板，则直接正转进入
                ConveyorController.Instance.ConveyorForward(conveyorNo);
                ConveyorController.Instance.SetWorkingSiteStopper(conveyorNo, true);

                DateTime startForwardTime = DateTime.Now;
                while (!ConveyorController.Instance.SingleSiteArriveSensor(conveyorNo).Is(StsType.High))
                {
                    TimeSpan timeSpan = DateTime.Now - startForwardTime;
                    if (timeSpan >= TimeSpan.FromSeconds(10))
                    {
                        MessageBox.Show("可能发生卡板");
                        goto end;
                    }
                    Thread.Sleep(1);
                }

                //电眼感应到位延时
                Thread.Sleep(ConveyorPrmMgr.Instance.FindBy(0).WorkingSitePrm.BoardArrivedDelay);

                ConveyorController.Instance.SetWorkingSiteLift(conveyorNo, true);
                end:
                ConveyorController.Instance.ConveyorAbortStop(conveyorNo);
                //停掉求板信号
                ConveyorController.Instance.AskSignalling(conveyorNo, false);
            }));
        }
    }
}
