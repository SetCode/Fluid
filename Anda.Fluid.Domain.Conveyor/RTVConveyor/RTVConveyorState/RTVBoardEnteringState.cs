using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState
{
    public class RTVBoardEnteringState : StateBase
    {
        private DateTime startTime;
        public override string GetName => RTVConveyorStateEnum.进板中.ToString();
        public RTVBoardEnteringState(RTVConveyorStateMachine stateMachine):base(stateMachine)
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
            else if (this.IsStuck())
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.卡板);
            }

            else if (DiType.下层轨道出板检测.Sts().Is(Infrastructure.StsType.High)) 
            {
                DoType.下层轨道求板信号.Set(false);
                this.stateMachine.ChangeState(RTVConveyorStateEnum.板到位);
            }
        }
        private bool IsStuck()
        {
            if (DateTime.Now - this.startTime > TimeSpan.FromMilliseconds(
                ConveyorPrmMgr.Instance.FindBy(1).UpStramStuckTime))
            {
                return true;
            }
            else
                return false;
        }
    }
}
