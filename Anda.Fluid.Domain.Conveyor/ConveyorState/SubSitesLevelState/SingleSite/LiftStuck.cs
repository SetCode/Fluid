using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class LiftStuck : SingleSiteState, IAlarmSenderable
    {
        public LiftStuck(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.气缸卡住.ToString();
            }
        }

        public string Name
        {
            get
            {
                return "Conveyor";
            }
        }

        public object Obj
        {
            get
            {
                return this;
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.气缸卡住;
            ConveyorController.Instance.ConveyorAbortStop(this.singleSiteStateMachine.ConveyorNo);
            Task.Factory.StartNew(new Action(() =>
            {
                AlarmServer.Instance.Fire(this, Alarm.AlarmInfoConveyor.WarnLiftStuck);

            }));
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
        }
    }
}
