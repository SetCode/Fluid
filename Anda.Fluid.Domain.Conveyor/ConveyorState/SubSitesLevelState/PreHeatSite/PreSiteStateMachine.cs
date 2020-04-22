using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite
{
    public class PreSiteStateMachine:SubSiteStateMachine
    {

        public PreSiteStateMachine(int conveyorNo):base(conveyorNo)
        {

        }
        public override void Setup()
        {
            this.Region(PreSiteStateEnum.起始状态, new PreEnter(this));
            this.Region(PreSiteStateEnum.空闲, new PreIdle(this));
            this.Region(PreSiteStateEnum.求板, new PreAsk(this));
            this.Region(PreSiteStateEnum.板进入, new BoardEntering(this));
            this.Region(PreSiteStateEnum.板到达, new BoardArrived(this));
            this.Region(PreSiteStateEnum.加热中, new PreHeating(this));
            this.Region(PreSiteStateEnum.出板中, new BoardLeaving(this));
            this.Region(PreSiteStateEnum.出板完成, new BoardLeft(this));
            this.Region(PreSiteStateEnum.卡板, new PreStuck(this));
            this.Region(PreSiteStateEnum.作业结束, new PreExit(this));

            if (FlagBitMgr.Instance.FindBy(this.ConveyorNo).ModelLevel.Auto.PreSiteHaveBoard)
            {
                this.SetDefault(PreSiteStateEnum.加热中);
            }
            else
            {
                this.SetDefault(PreSiteStateEnum.起始状态);
            }
        }
    }
}
