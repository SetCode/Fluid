using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite
{
    public class WorkingSiteStateMachine:SubSiteStateMachine
    {

        public WorkingSiteStateMachine(int conveyorNo):base(conveyorNo)
        {
            
        }
        public override void Setup()
        {
            this.Region(WorkingSiteStateEnum.起始状态, new WorkingEnter(this));
            this.Region(WorkingSiteStateEnum.求板, new WorkingAsk(this));
            this.Region(WorkingSiteStateEnum.板到位, new BoardArrived(this));
            this.Region(WorkingSiteStateEnum.加热中, new WorkingHeating(this));
            this.Region(WorkingSiteStateEnum.准备点胶, new DispenseReady(this));
            this.Region(WorkingSiteStateEnum.点胶中, new Dispensing(this));
            this.Region(WorkingSiteStateEnum.点胶完成, new DispenseDone(this));
            this.Region(WorkingSiteStateEnum.作业结束, new WorkingExit(this));

            if (FlagBitMgr.Instance.FindBy(this.ConveyorNo).ModelLevel.Auto.WorkingSiteHaveBoard)
            {
                this.SetDefault(WorkingSiteStateEnum.点胶完成);
            }
            else
            {
                this.SetDefault(WorkingSiteStateEnum.起始状态);
            }
        }

    }
}
