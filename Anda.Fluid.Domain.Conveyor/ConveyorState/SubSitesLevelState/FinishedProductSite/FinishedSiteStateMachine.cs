using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite
{
    public class FinishedSiteStateMachine:SubSiteStateMachine
    {
        public FinishedSiteStateMachine(int conveyorNo):base(conveyorNo)
        {

        }
        public override void Setup()
        {
            this.Region(FinishedSiteStateEnum.起始状态, new FinishedEnter(this));
            this.Region(FinishedSiteStateEnum.求板, new FinishedAsk(this));
            this.Region(FinishedSiteStateEnum.直接出板, new BoardDirectLeaving(this));
            this.Region(FinishedSiteStateEnum.进板中, new BoardEntering(this));
            this.Region(FinishedSiteStateEnum.加热中, new FinishedHeating(this));
            this.Region(FinishedSiteStateEnum.出板中, new BoardLeaving(this));
            this.Region(FinishedSiteStateEnum.出板完成, new BoardLeft(this));
            this.Region(FinishedSiteStateEnum.卡板, new FinishedStuck(this));
            this.Region(FinishedSiteStateEnum.作业结束, new FinishedExit(this));

            if (FlagBitMgr.Instance.FindBy(this.ConveyorNo).ModelLevel.Auto.FinishedSiteHaveBoard)
            {
                this.SetDefault(FinishedSiteStateEnum.加热中);
            }
            else
            {
                this.SetDefault(FinishedSiteStateEnum.起始状态);
            }
        }
    }
}
