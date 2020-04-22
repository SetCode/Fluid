using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors.Heater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public class WorkingExit : WorkingSiteState
    {
        public WorkingExit(WorkingSiteStateMachine workingStateMachine) : base(workingStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return WorkingSiteStateEnum.作业结束.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState = WorkingSiteStateEnum.作业结束;
            Machine.Instance.HeaterController1.Opeate(OperateHeaterController.程序结束运行时);
            // 点胶结束重置当前工作站条码信息
            FlagBitMgr.Instance.FindBy(this.workingStateMachine.ConveyorNo).ModelLevel.Auto.CurrentBarcode = "";
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            
        }
    }
}
