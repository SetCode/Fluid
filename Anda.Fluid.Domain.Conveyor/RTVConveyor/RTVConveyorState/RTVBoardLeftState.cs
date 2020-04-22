using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState
{
    public class RTVBoardLeftState : StateBase
    {
        private DateTime startTime;
        public override string GetName => RTVConveyorStateEnum.出板完成.ToString();

        public RTVBoardLeftState(RTVConveyorStateMachine stateMachine):base (stateMachine)
        {

        }
        public override void EnterState()
        {
            this.startTime = DateTime.Now;
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (!FlagBitMgr.Instance.FindBy(0).UILevel.DownConveyorStart)
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.作业结束);
            }
            else if (IsDone()) 
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.空闲);
            }
        }

        private bool IsDone()
        {
            if (DateTime.Now - this.startTime > TimeSpan.FromMilliseconds
                    (2000))
            {
                return true;
            }
            else
                return false;
        }
    }
}
