using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite;
using Anda.Fluid.Domain.Conveyor.Flag;
using Anda.Fluid.Domain.Conveyor.Prm;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Sensors;
using Anda.Fluid.Drive.Sensors.Heater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorState.ModelLevelState.AutoModel
{
    public class RunSubsites : IntegralState
    {
        private List<SubSiteStateMachine> subSiteStateMachineList;
        
        public RunSubsites(IntegralStateMachine integralStateMachine) : base(integralStateMachine)
        {
            this.subSiteStateMachineList = new List<SubSiteStateMachine>();          
        }

        public override string GetName
        {
            get
            {
                return IntegralStateEnum.运行子站.ToString();
            }
        }

        public override void EnterState()
        {
            FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.IntegralState = IntegralStateEnum.运行子站;

            this.AddSubSites();

            this.SetUpSubSites();       
        }

        public override void ExitState()
        {
            
        }

        public override void UpdateState()
        {
            if (this.CanExit())
            {
                this.integralStateMachine.ChangeState(IntegralStateEnum.运行结束);
            }
            else
            {
                this.RunSubSites();
            }
            
        }
        /// <summary>
        /// 添加子站
        /// </summary>
        private void AddSubSites()
        {
            this.subSiteStateMachineList.Clear();
            if (ConveyorPrmMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.Singel)
            {
                this.subSiteStateMachineList.Add(new SingleSiteStateMachine(this.integralStateMachine.ConveyorNo));
            }
            else
            {
                this.subSiteStateMachineList.Add(new PreSiteStateMachine(this.integralStateMachine.ConveyorNo));
                this.subSiteStateMachineList.Add(new WorkingSiteStateMachine(this.integralStateMachine.ConveyorNo));
                this.subSiteStateMachineList.Add(new FinishedSiteStateMachine(this.integralStateMachine.ConveyorNo));
            }
        }
        /// <summary>
        /// 设置子站状态机拥有状态和初始状态
        /// </summary>
        private void SetUpSubSites()
        {           
            for (int i = 0; i < this.subSiteStateMachineList.Count; i++)
            {
                this.subSiteStateMachineList[i].Setup();
            }
        }
        /// <summary>
        /// 运行子站状态机
        /// </summary>
        private void RunSubSites()
        {
            for (int i = 0; i < this.subSiteStateMachineList.Count; i++)
            {
                this.subSiteStateMachineList[i].UpdateSate();
            }
        }

        /// <summary>
        /// 能否离开当前状态，可以离开返回True
        /// </summary>
        /// <returns></returns>
        private bool CanExit()
        {
            if (ConveyorPrmMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).SubsiteMode == ConveyorSubsiteMode.Singel)
            {
                if (FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.SubSitesCurrState.SingleSiteState == SingleSiteStateEnum.作业结束
                    && FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.Terminate)
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            else 
            {
                if ((FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.SubSitesCurrState.PreSiteState == PreSiteStateEnum.作业结束
                    && FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.SubSitesCurrState.WorkingSiteState == WorkingSiteStateEnum.作业结束
                     && FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).State.SubSitesCurrState.FinishedSiteState == FinishedSiteStateEnum.作业结束)
                    && FlagBitMgr.Instance.FindBy(this.integralStateMachine.ConveyorNo).UILevel.Terminate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
