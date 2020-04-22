using Anda.Fluid.Domain.Conveyor.BaseClass;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.MachineStates;
using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Conveyor.RTVConveyor.RTVConveyorState
{
    public class RTVStuckState : StateBase, IAlarmSenderable
    {
        private DialogResult result = DialogResult.No;
        public override string GetName => RTVConveyorStateEnum.卡板.ToString();

        public object Obj => this;

        public string Name => "下层轨道卡板";

        public RTVStuckState(RTVConveyorStateMachine sateMachine):base(sateMachine)
        {

        }

        public override void EnterState()
        {
            this.result = DialogResult.No;
            DoType.红色信号灯.Set(true);
            
            Task.Factory.StartNew(new Action(() =>
            {

                if (this.stateMachine.PrevStateName.Equals(RTVConveyorStateEnum.轨道检查.ToString()))
                {
                    this.result = DialogResult.No;
                    this.result = MessageBox.Show("下层轨道有残余板，取出板后关闭此窗口");
                }
                else
                {
                    AxisType.Axis7.MoveStop();                
                    this.result = MessageBox.Show("下层轨道发生卡板，请解决后重新启动");
                }

            }));

        }

        public override void ExitState()
        {
            DoType.红色信号灯.Set(false);
            this.result = DialogResult.No;
        }

        public override void UpdateState()
        {
            if (this.stateMachine.PrevStateName.Equals(RTVConveyorStateEnum.轨道检查.ToString()))
            {
                if (this.result == DialogResult.OK)
                {
                    this.stateMachine.ChangeState(RTVConveyorStateEnum.空闲);
                }
            }
            if (!FlagBitMgr.Instance.FindBy(0).UILevel.DownConveyorStart
                || this.result != DialogResult.No) 
            {
                FlagBitMgr.Instance.FindBy(0).UILevel.DownConveyorStart = false;
                this.stateMachine.ChangeState(RTVConveyorStateEnum.作业结束);
            }
        }
    }
}
