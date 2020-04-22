using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState
{
    public class RTVBoardArrivedState : StateBase
    {
        private DateTime pulseTime;
        public override string GetName => RTVConveyorStateEnum.板到位.ToString();

        public RTVBoardArrivedState(RTVConveyorStateMachine stateMachine):base(stateMachine)
        {

        }

        public override void EnterState()
        {
            AxisType.Axis7.MoveStop();
            DoType.下层轨道有板信号.Set(true);
        }

        public override void ExitState()
        {
            DoType.下层轨道有板信号.Set(false);
        }

        public override void UpdateState()
        {
            if (!FlagBitMgr.Instance.FindBy(0).UILevel.DownConveyorStart)
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.作业结束);
            }
            if (DiType.下层轨道出板检测.Sts().Is(Infrastructure.StsType.IsFalling))
            {
                this.pulseTime = DateTime.Now;
            }
            else if (DiType.下层轨道求板信号.Sts().Is(Infrastructure.StsType.High))
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.出板中);
            }
            else if (DiType.下层轨道出板检测.Sts().Is(Infrastructure.StsType.Low) && IsDelay())
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.空闲);
            }
        }


        private bool IsDelay()
        {
            if (DateTime.Now - pulseTime >= TimeSpan.FromMilliseconds(3000))
            {
                return true;
            }
            else
                return false;
        }
    }
}
