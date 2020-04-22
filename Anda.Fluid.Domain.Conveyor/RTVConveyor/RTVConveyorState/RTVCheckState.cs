using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState
{
    public class RTVCheckState : StateBase
    {
        private bool taskIsBreak;
        private bool isStuck;
        private bool checkIsDone;
        public override string GetName => RTVConveyorStateEnum.轨道检查.ToString();

        public RTVCheckState(RTVConveyorStateMachine stateMachine):base(stateMachine)
        {

        }

        public override void EnterState()
        {
            //Task.Factory.StartNew(new Action(() =>
            //{
            //    if (DiType.下层轨道进板检测.Sts().Is(StsType.High)
            //    || DiType.下层轨道出板检测.Sts().Is(StsType.High))
            //    {
            //        this.isStuck = true;
            //    }
            //    else
            //    {
            //        AxisType.Axis7.MoveJog(ConveyorPrmMgr.Instance.FindBy(1).Speed);

            //        DateTime startTime = DateTime.Now;
            //        while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(ConveyorPrmMgr.Instance.
            //            FindBy(1).CheckTime))
            //        {
            //            if (this.taskIsBreak)
            //            {
            //                return;
            //            }
            //            if (DiType.下层轨道进板检测.Sts().Is(StsType.High) || DiType.下层轨道出板检测.Sts().Is(StsType.High))
            //            {
            //                AxisType.Axis7.MoveStop();
            //                this.isStuck = true;
            //                return;
            //            }
            //            Thread.Sleep(2);
            //        }

            //    }
            //    this.checkIsDone = true;
            //}));           
        }

        public override void ExitState()
        {
            //this.taskIsBreak = false;
            //this.checkIsDone = false;
            //this.isStuck = false;
            //AxisType.Axis7.MoveStop();
        }

        public override void UpdateState()
        {
            if (!FlagBitMgr.Instance.FindBy(0).UILevel.DownConveyorStart)
            {
                this.taskIsBreak = true;
                this.stateMachine.ChangeState(RTVConveyorStateEnum.作业结束);
            }
            //else if (this.isStuck)
            //{
            //    this.stateMachine.ChangeState(RTVConveyorStateEnum.卡板);
            //}
            else if (DiType.下层轨道出板检测.Sts().Is(StsType.High))
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.板到位);
            }
            else //if (this.checkIsDone)
            {
                this.stateMachine.ChangeState(RTVConveyorStateEnum.空闲);
            }
        }
    }
}
