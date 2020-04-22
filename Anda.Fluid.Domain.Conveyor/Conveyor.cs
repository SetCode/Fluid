using Anda.Fluid.Domain.Conveyor.ConveyorState;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Domain.Conveyor.RTVConveyor;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Conveyor.LeadShine;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Tasker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor
{
    public class Conveyor 
    {
        public Conveyor(int key)
        {
            this.StateMachine = new IntegralStateMachine(key);
        }
        public IntegralStateMachine StateMachine { get; private set; }


        public void InitStates()
        {
            this.StateMachine.Region(IntegralStateEnum.初始状态, new ConveyorState.UILevelState.Initial(this.StateMachine));
            this.StateMachine.Region(IntegralStateEnum.等待运行, new ConveyorState.UILevelState.WaitingToRun(this.StateMachine));

            this.StateMachine.Region(IntegralStateEnum.Offline模式, new ConveyorState.UILevelState.OfflineModel(this.StateMachine));

            this.StateMachine.Region(IntegralStateEnum.Demo模式, new ConveyorState.UILevelState.DemoModel(this.StateMachine));
            this.StateMachine.Region(IntegralStateEnum.运行Demo, new ConveyorState.ModelLevelState.DemoModel.DemoRun(this.StateMachine));

            this.StateMachine.Region(IntegralStateEnum.PassThrough模式, new ConveyorState.UILevelState.PassThroughModel(this.StateMachine));
            this.StateMachine.Region(IntegralStateEnum.运行PassThrough, new ConveyorState.ModelLevelState.PassThroughModel.PassRun(this.StateMachine));

            this.StateMachine.Region(IntegralStateEnum.Auto模式, new ConveyorState.UILevelState.AutoModel(this.StateMachine));
            this.StateMachine.Region(IntegralStateEnum.Auto起始状态, new ConveyorState.ModelLevelState.AutoModel.AutoEnter(this.StateMachine));
            this.StateMachine.Region(IntegralStateEnum.轨道检查, new ConveyorState.ModelLevelState.AutoModel.CheckResidue(this.StateMachine));
            this.StateMachine.Region(IntegralStateEnum.轨道复位, new ConveyorState.ModelLevelState.AutoModel.WidthReset(this.StateMachine));
            this.StateMachine.Region(IntegralStateEnum.运行子站, new ConveyorState.ModelLevelState.AutoModel.RunSubsites(this.StateMachine));
            this.StateMachine.Region(IntegralStateEnum.运行结束, new ConveyorState.ModelLevelState.AutoModel.AutoExit(this.StateMachine));
            this.StateMachine.Region(IntegralStateEnum.卡板, new ConveyorState.ModelLevelState.AutoModel.IntegralStuck(this.StateMachine));

            this.StateMachine.SetDefault(IntegralStateEnum.初始状态);
        }

        public void UpdateUpStreamSignal()
        {
            if (ConveyorController.Instance.UpstreamPutBoard(this.StateMachine.ConveyorNo).Value)
            {
                FlagBitMgr.Instance.FindBy(this.StateMachine.ConveyorNo).ModelLevel.Auto.UpStreamHaveBoard = true;
            }
        }

        public void UpdateDownSreamSignal()
        {
            if (ConveyorController.Instance.DownstreamAskBoard(this.StateMachine.ConveyorNo).Is(Infrastructure.StsType.High))
            {
                FlagBitMgr.Instance.FindBy(this.StateMachine.ConveyorNo).ModelLevel.Auto.DownStreamAskBoard = true;
            }
        }
    }
    public class ConveyorMgr : TaskLoop
    {
        private readonly static ConveyorMgr instance = new ConveyorMgr();
        private Conveyor conveyor1, conveyor2;
        private RTVDownConveyor rtvDownConveyor;

        public  Conveyor Conveyor1 => this.conveyor1;
        public Conveyor Conveyor2 => this.conveyor2;

        public RTVDownConveyor RTVDownConveyor => this.rtvDownConveyor;
        private ConveyorMgr()
        {
            this.conveyor1 = new Conveyor(0);
            this.conveyor2 = new Conveyor(1);
            this.rtvDownConveyor = new RTVDownConveyor();
        }
        public static ConveyorMgr Instance => instance;
        public void Setup()
        {
            ConveyorPrmMgr.Instance.Load();
            this.conveyor1.InitStates();
            this.conveyor2.InitStates();
            this.rtvDownConveyor.InitStates();
            this.Start();

            ConveyorController.Instance.ConveyorInit();
        }

        public void Unload()
        {
            this.Stop();

            ConveyorController.Instance.LeadShineClose();
        }

        protected override void Loop()
        {
            this.conveyor1.StateMachine.UpdateSate();
            this.conveyor2.StateMachine.UpdateSate();
            this.rtvDownConveyor.stateMachine.UpdateSate();

            ConveyorMachine.Instance.Update();

            //this.conveyor1.UpdateUpStreamSignal();
            //this.conveyor1.UpdateDownSreamSignal();
            //this.conveyor2.UpdateUpStreamSignal();
            //this.conveyor2.UpdateDownSreamSignal();
        }
    }
}
