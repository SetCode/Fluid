
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public class FinishedStuck : FinishedSiteState,IAlarmSenderable
    {
        public FinishedStuck(FinishedSiteStateMachine finishedSiteStateMachine) : base(finishedSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return FinishedSiteStateEnum.卡板.ToString();
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
            FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState = FinishedSiteStateEnum.卡板;
            ConveyorController.Instance.ConveyorAbortStop(this.finishedSiteStateMachine.ConveyorNo);
            Task.Factory.StartNew(new Action(() =>
            {
                AlarmServer.Instance.Fire(this, Alarm.AlarmInfoConveyor.WarnConveyorStuck);
            }));
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if(FlagBitMgr.Instance.FindBy(this.finishedSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.finishedSiteStateMachine.ChangeState(FinishedSiteStateEnum.作业结束);
            }
        }
    }
}
