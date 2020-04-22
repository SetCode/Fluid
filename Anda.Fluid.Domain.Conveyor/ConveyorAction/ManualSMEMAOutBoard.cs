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
    internal class ManualSMEMAOutBoard
    {
        public void Execute(int conveyorNo)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //气缸松开
                ConveyorController.Instance.SetWorkingSiteStopper(conveyorNo, false);
                ConveyorController.Instance.SetWorkingSiteLift(conveyorNo, false);
                Thread.Sleep(1000);

                ConveyorController.Instance.ConveyorForward(conveyorNo);

                DateTime startTime = DateTime.Now;
                while (!ConveyorController.Instance.SingleSiteExitSensor(conveyorNo).Is(StsType.High))
                {
                    TimeSpan timeSpan = DateTime.Now - startTime;
                    if (timeSpan >= TimeSpan.FromSeconds(20))
                    {
                        goto end1;
                    }
                    Thread.Sleep(1);
                }
                end1:

                while (!ConveyorController.Instance.DownstreamAskBoard(conveyorNo).Value)
                {
                    ConveyorController.Instance.ConveyorAbortStop(conveyorNo);
                    Thread.Sleep(1);
                    continue;
                }

                ConveyorController.Instance.ConveyorForward(conveyorNo);

                DateTime stuckTime = DateTime.Now;
                while (DateTime.Now - stuckTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.FindBy
                (conveyorNo).DownStreamStuckTime))
                {
                    if (ConveyorController.Instance.SingleSiteExitSensor(conveyorNo).Is(StsType.IsFalling))
                    {
                        ConveyorController.Instance.ConveyorAbortStop(conveyorNo);
                        ConveyorController.Instance.InStoreSignalling(conveyorNo, false);
                        return;
                    }
                    Thread.Sleep(1);
                }
                ConveyorController.Instance.ConveyorAbortStop(conveyorNo);
                ConveyorController.Instance.InStoreSignalling(conveyorNo, false);
                MessageBox.Show("可能发生卡板");
              
            }));
        }
    }
}
