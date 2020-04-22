using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class PreStuck : PreSiteState,IAlarmSenderable
    {
        public PreStuck(PreSiteStateMachine preSiteStateMachine) : base(preSiteStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return PreSiteStateEnum.卡板.ToString();
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
            FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState = PreSiteStateEnum.卡板;
            ConveyorController.Instance.ConveyorAbortStop(this.preSiteStateMachine.ConveyorNo);
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
            if (FlagBitMgr.Instance.FindBy(this.preSiteStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.preSiteStateMachine.ChangeState(PreSiteStateEnum.作业结束);
            }
        }
    }
}
