using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive.Conveyor.LeadShine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.ModelLevelState.AutoModel
{
    public class WidthReset : IntegralState
    {
        private bool taskIsBreak;
        private bool taskIsDone;
        public WidthReset(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.轨道复位.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.轨道复位;

            if (ConveyorMsgCenter.Instance.ConveyorState == ConveyorControlMsg.轨道1和轨道2同时启用)
            {
                if (this.integralStateMachine.ConveyorNo == 1)
                {
                    this.taskIsDone = true;
                    return;
                }

            }

            Task.Factory.StartNew(new Action(() =>
            {
                ConveyorMachine.Instance.MoveHome(10);
                ConveyorMachine.Instance.AxisYMovePos(FlagBitMgr.Instance.ConveyorWidth, ConveyorPrmMgr.Instance.FindBy(0).Speed, ConveyorPrmMgr.Instance.FindBy(0).AccTime);
                this.taskIsDone = true;
            }));

        }

        public override void ExitState()
        {
            this.taskIsBreak = false;
            this.taskIsDone = false;
        }

        public override void UpdateState()
        {
            if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.Terminate)
            {
                this.taskIsBreak = true;
                this.integralStateMachine.ChangeState(IntegralStateEnum.运行结束);
            }
            else if (this.taskIsDone)
            {
                this.integralStateMachine.ChangeState(IntegralStateEnum.运行子站);               
            }
        }
    }
}
