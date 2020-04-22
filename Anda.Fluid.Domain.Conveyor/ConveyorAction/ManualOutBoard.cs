using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorAction
{
    internal class ManualOutBoard
    {
        public void Execute(int conveyorNo)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                //气缸松开
                ConveyorController.Instance.SetWorkingSiteStopper(conveyorNo, false);
                ConveyorController.Instance.SetWorkingSiteLift(conveyorNo, false);
                Thread.Sleep(1000);

                this.ConveyorRun(conveyorNo);

                DateTime startTime = DateTime.Now;
                while (!ConveyorController.Instance.SingleSiteExitSensor(conveyorNo).Is(StsType.High))
                {
                    TimeSpan timeSpan = DateTime.Now - startTime;
                    if (timeSpan >= TimeSpan.FromSeconds(20))
                    {
                        goto end;
                    }
                    Thread.Sleep(1);
                }
                end:
                ConveyorController.Instance.ConveyorAbortStop(conveyorNo);
            }));
        }

        private void ConveyorRun(int conveyorNo)
        {
            switch (ConveyorPrmMgr.Instance.FindBy(conveyorNo).BoardDirection)
            {
                case BoardDirection.LeftToRight:
                    ConveyorController.Instance.ConveyorForward(conveyorNo);
                    break;
                case BoardDirection.RightToLeft:
                    ConveyorController.Instance.ConveyorForward(conveyorNo);
                    break;
                case BoardDirection.LeftToLeft:
                    ConveyorController.Instance.ConveyorBack(conveyorNo);
                    break;
                case BoardDirection.RightToRight:
                    ConveyorController.Instance.ConveyorBack(conveyorNo);
                    break;
            }
        }
    }
}
