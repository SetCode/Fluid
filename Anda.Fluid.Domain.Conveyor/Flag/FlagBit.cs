using Anda.Fluid.Domain.Conveyor.ConveyorState;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.SingleSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite;
using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.Flag
{
    public class FlagBit:EntityBase<int>
    {
        private UILevel uiLevel;
        private ModelLevel modelLevel;
        private SubSitesLevel subSitesLevel;
        private State state;
        public FlagBit(int key):base(key)
        {
            this.uiLevel = new UILevel();
            this.modelLevel = new ModelLevel();
            this.subSitesLevel = new SubSitesLevel();
            this.state = new State();
        }
        public UILevel UILevel => this.uiLevel;
        public ModelLevel ModelLevel => this.modelLevel;
        public SubSitesLevel SubSitesLevel => this.subSitesLevel;
        public State State => this.state;
    }
    /// <summary>
    /// UI层标志位
    /// </summary>
    public class UILevel
    {

        public bool AutoRun { get; set; } = false;
        public bool DemoRun { get; set; } = false;
        public bool PassRun { get; set; } = false;
        public RunMode SelectedMode { get; set; }
        public bool Terminate { get; set; } = false;

        public bool EnterEditForm { get; set; } = false;
        public bool ExitEditForm { get; set; } = false;

        public bool DownConveyorStart { get; set; } = false;

        /// <summary>
        /// 程序终止
        /// </summary>      
        public enum RunMode
        {
            Auto,
            Demo,
            PassThrough
        }
    }
    /// <summary>
    /// 模式层标志位
    /// </summary>
    public class ModelLevel
    {
        private Auto auto;
        private Demo demo;
        private PassThrough passthrough;
        public ModelLevel()
        {
            this.auto = new Auto();
            this.demo = new Demo();
            this.passthrough = new PassThrough();
        }
        public Auto Auto => this.auto;
        public Demo Demo => this.demo;
        public PassThrough PassThrough => this.passthrough;
    }
    public class Auto
    {
        public bool UpStreamHaveBoard { get; set; } = false;
        public bool DownStreamAskBoard { get; set; } = false;
        public bool PreSiteHaveBoard { get; set; } = false;
        public bool WorkingSiteHaveBoard { get; set; } = false;
        public bool FinishedSiteHaveBoard { get; set; } = false;
        public bool StuckIsSolve { get; set; } = false;
        public bool DispenseStart { get; set; } = false;
        public bool DispenseDone { get; set; } = false;
        public bool CanDispense { get; set; } = false;
        // 是否可以生产轨道检查时发现的治具
        public bool IsContinueProduct { get; set; } = false;
        // 当前轨道条码
        public string CurrentBarcode { get; set; } = "";
        // 预热站条码
        public string PreBarcode { get; set; } = "";
    }
    public class Demo
    {

    }
    public class PassThrough
    {

    }

    /// <summary>
    /// 子站层标志位
    /// </summary>
    public class SubSitesLevel
    {
        public PreSite PreSite { get; set; } = new PreSite();
        public WorkingSite WorkingSite { get; set; } = new WorkingSite();
        public FinishedSite FinishedSite { get; set; } = new FinishedSite();
        public SingleSite SingleSite { get; set; } = new SingleSite();
    }
    public class PreSite
    {
        /// <summary>
        /// 预热站是否正在使用轨道
        /// </summary>
        public bool usingConveyor { get; set; } = false;
        public long EnterBoardCounts  = 0;               
    }
    public class WorkingSite
    {
        //这是工作站和预热站加热时间共享的情况，根据需求，现在不做考虑。
        //public long NeedHeatingTime  = 0;

        public long DispenseBoardCounts  = 0;

    }
    public class FinishedSite
    {
        /// <summary>
        /// 成品站是否正在使用轨道
        /// </summary>
        public bool usingConveyor { get; set; } = false;
        public long ExitBoardCounts  = 0;
    }
    public class SingleSite
    {
        public long EnterBoardCounts = 0;
    }

    /// <summary>
    /// 状态机当前状态
    /// </summary>
    public class State
    {
        public IntegralStateEnum IntegralState { get; set; }
        public SubSitesCurrState SubSitesCurrState { get; set; } = new SubSitesCurrState();
    }
    public class SubSitesCurrState
    {
        public PreSiteStateEnum PreSiteState { get; set; }
        public WorkingSiteStateEnum WorkingSiteState { get; set; }
        public FinishedSiteStateEnum FinishedSiteState { get; set; }
        public SingleSiteStateEnum SingleSiteState { get; set; }

    }

}
