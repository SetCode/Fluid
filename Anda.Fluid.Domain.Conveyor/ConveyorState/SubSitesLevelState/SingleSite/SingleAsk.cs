using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class SingleAsk : SingleSiteState
    {
        private DateTime startTime;
        public SingleAsk(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.求板.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.求板;

            this.startTime = DateTime.Now;

            ConveyorController.Instance.ConveyorForward(this.singleSiteStateMachine.ConveyorNo);

            ConveyorController.Instance.SetWorkingSiteStopper(this.singleSiteStateMachine.ConveyorNo,true);

        }

        public override void ExitState()
        {

        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.作业结束);
            }
            else if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).EnterIsSMEMA)
            {
                if (this.IsStuck())
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.卡板);
                }
                else if (ConveyorController.Instance.SingleSiteEnterSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    this.Entering(true);
                }
            }
            else if (FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UILevel.SelectedMode == UILevel.RunMode.PassThrough)
            {
                if (ConveyorController.Instance.SingleSiteEnterSensor(this.singleSiteStateMachine.ConveyorNo).Is(StsType.High))
                {
                    this.Entering(true);
                }
            }
            else
            {
                this.Entering(false);
            }

        }
        private bool IsStuck()
        {
            //如果是SMEMA模式下的单边信号交互，则不会判断卡板，一直转动轨道
            if (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).SMEMAIsSingleInteraction
                && ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).EnterIsSMEMA)
            {
                return false;
            }
            else
            {
                if (DateTime.Now - this.startTime > TimeSpan.FromMilliseconds
                    (ConveyorPrmMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).UpStramStuckTime))
                {
                    return true;
                }
                else
                    return false;
            }           
        }

        private void Entering(bool sendAskSignalling)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV && ConveyorPrmMgr.Instance.FindBy(0).RTVPrm.IOEnable) 
            {
                if(DiType.涂胶挡板上位.Sts().Is(StsType.High))
                {
                    if (sendAskSignalling)
                    {
                        ConveyorController.Instance.AskSignalling(this.singleSiteStateMachine.ConveyorNo, false);
                    }
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.进板中);
                }
                if (DateTime.Now - startTime > TimeSpan.FromSeconds(ConveyorPrmMgr.Instance.FindBy(0).RTVPrm.IOStuckTime))
                {
                    this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.气缸卡住);
                }
            }
            else
            {
                if (sendAskSignalling)
                {
                    ConveyorController.Instance.AskSignalling(this.singleSiteStateMachine.ConveyorNo, false);
                }
                this.singleSiteStateMachine.ChangeState(SingleSiteStateEnum.进板中);
            }


        }
    }
}
