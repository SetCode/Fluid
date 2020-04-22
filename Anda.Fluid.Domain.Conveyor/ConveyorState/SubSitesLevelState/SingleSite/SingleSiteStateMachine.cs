using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite
{
    public class SingleSiteStateMachine : SubSiteStateMachine
    {
        public SingleSiteStateMachine(int conveyorNo) : base(conveyorNo)
        {
        }
        public override void Setup()
        {
            this.Region(SingleSiteStateEnum.起始状态, new SingleEnter(this));
            this.Region(SingleSiteStateEnum.空闲, new SingleIdle(this));
            this.Region(SingleSiteStateEnum.求板, new SingleAsk(this));
            this.Region(SingleSiteStateEnum.进板中, new BoardEntering(this));
            this.Region(SingleSiteStateEnum.板到位, new BoardArrived(this));
            this.Region(SingleSiteStateEnum.准备点胶, new ReadyForDispense(this));
            this.Region(SingleSiteStateEnum.点胶中, new Dispensing(this));
            this.Region(SingleSiteStateEnum.点胶完成, new DispenseDone(this));
            this.Region(SingleSiteStateEnum.出板中, new BoardLeaving(this));
            this.Region(SingleSiteStateEnum.反转出板, new Reverse(this));
            this.Region(SingleSiteStateEnum.出板完成, new BoardLeft(this));
            this.Region(SingleSiteStateEnum.作业结束, new SingleExit(this));
            this.Region(SingleSiteStateEnum.卡板, new SingleStuck(this));
            this.Region(SingleSiteStateEnum.气缸卡住, new LiftStuck(this));

            if (FlagBitMgr.Instance.FindBy(this.ConveyorNo).ModelLevel.Auto.IsContinueProduct)
            {
                this.SetDefault(SingleSiteStateEnum.板到位);
            }
            else
            {
                this.SetDefault(SingleSiteStateEnum.起始状态);
            }
        }
    }
}
