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
    public class RTVBoardLeavingState : StateBase
    {
        private DateTime startTime;
        private bool isDoneTime;
        private DateTime doneTime;
        public override string GetName => RTVConveyorStateEnum.出板中.ToString();

        public RTVBoardLeavingState(RTVConveyorStateMachine stateMachine):base(stateMachine)
        {

        }

        public override void EnterState()
        {
            AxisType.Axis7.MoveJog(ConveyorPrmMgr.Instance.FindBy(1).Speed);
            this.startTime = DateTime.Now;
        }

        public override void ExitState()
        {
            AxisType.Axis7.MoveStop();
            this.isDoneTime = false;
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
            else if (DiType.下层轨道求板信号.Sts().Is(Infrastructure.StsType.Low)) 
            {
                if (this.DelayIsDone())
                {
                    this.stateMachine.ChangeState(RTVConveyorStateEnum.出板完成);
                }
            }
        }

        private bool IsStuck()
        {
            if (DateTime.Now - this.startTime > TimeSpan.FromMilliseconds
                    (ConveyorPrmMgr.Instance.FindBy(1).DownStreamStuckTime + ConveyorPrmMgr.Instance.FindBy(0).RTVPrm.DownConveyorTurnTime * 1000)) 
            {
                return true;
            }
            else
                return false;
        }

        private bool DelayIsDone()
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                if (!this.isDoneTime)
                {
                    this.isDoneTime = true;
                    this.doneTime = DateTime.Now;
                    return false;
                }
                if (this.isDoneTime)
                {
                    if (DateTime.Now - this.doneTime > TimeSpan.FromSeconds(ConveyorPrmMgr.Instance.FindBy(0).RTVPrm.DownConveyorTurnTime))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            else
            {
                return true;
            }
        }

    }
}
