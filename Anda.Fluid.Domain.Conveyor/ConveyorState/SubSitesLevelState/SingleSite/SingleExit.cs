using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Heater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class SingleExit : SingleSiteState
    {
        public SingleExit(SingleSiteStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return SingleSiteStateEnum.作业结束.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.singleSiteStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState = SingleSiteStateEnum.作业结束;

            Machine.Instance.HeaterController1.Opeate(OperateHeaterController.程序结束运行时);

            ConveyorController.Instance.AskSignalling(this.singleSiteStateMachine.ConveyorNo,false);
            ConveyorController.Instance.InStoreSignalling(this.singleSiteStateMachine.ConveyorNo,false);
        }

        public override void ExitState()
        {

        }

        public override void UpdateState()
        {

        }
    }
}
