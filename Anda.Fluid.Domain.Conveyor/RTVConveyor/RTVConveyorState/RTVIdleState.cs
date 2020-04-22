using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState
{
    public class RTVIdleState : StateBase
    {
        //private RTVConveyorStateMachine rtvConveyorStateMachine;
        public override string GetName => RTVConveyorStateEnum.空闲.ToString();

        public RTVIdleState(RTVConveyorStateMachine rtvConveyorStateMachine):base(rtvConveyorStateMachine)
        {
            this.stateMachine = rtvConveyorStateMachine;
        }

        public override void EnterState()
        {
            DoType.下层轨道求板信号.Set(true);
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
            else if (DiType.下层轨道放板信号.Sts().Is(StsType.High))
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.求板);
            }
        }
    }
}
