using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.FinishedProductSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.PreHeatSite;
using Anda.Fluid.Domain.Conveyor.ConveyorState.SubSitesLevelState.WorkingSite;
using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.Prm
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ConveyorPrm: EntityBase<int>
    {
        public ConveyorPrm(int key):base(key)
        { }
        [JsonProperty]
        public BoardDirection BoardDirection { get; set; } = BoardDirection.LeftToRight;
        [JsonProperty]
        public ConveyorSubsiteMode SubsiteMode { get; set; } = ConveyorSubsiteMode.Triple;
        [JsonProperty]
        public bool EnterIsSMEMA { get; set; } = false;

        [JsonProperty]
        public bool ExitIsSMEMA { get; set; } = false;

        [JsonProperty]
        public bool SMEMAIsPulse { get; set; } = false;

        [JsonProperty]
        public bool SMEMAIsSingleInteraction { get; set; } = false;

        [JsonProperty]
        public int PulseTime { get; set; } = 30;
        [JsonProperty]
        public bool ReceiveSMEMAIsPulse { get; set; } = false;

        [JsonProperty]
        public bool AutoExitBoard { get; set; } = false;
        [JsonProperty]
        public int UpStramStuckTime { get; set; } = 20000;
        [JsonProperty]
        public int DownStreamStuckTime { get; set; } = 20000;

        //这是工作站和预热站加热时间共享的情况，根据需求，现在不做考虑。
        //[JsonProperty]
        //public long HeatingTime { get; set; } = 20000;

        [JsonProperty]
        public double ConveyorWidth { get; set; } = 40;

        [JsonProperty]
        public double Speed { get; set; } = 40;
        [JsonProperty]
        public PreSitePrm PreSitePrm { get; set; } = new PreSitePrm();
        [JsonProperty]
        public WorkingSitePrm WorkingSitePrm { get; set; } = new WorkingSitePrm();
        [JsonProperty]
        public FinishedSitePrm FinishedSitePrm { get; set; } = new FinishedSitePrm();
        [JsonProperty]
        public int CheckTime { get; set; } = 10000;
        [JsonProperty]
        public int LiftUpDelay { get; set; } = 1000;
        [JsonProperty]
        public int StopperUpDelay { get; set; } = 1000;
        [JsonProperty]
        public int BoardLeftDelay { get; set; } = 2000;
        [JsonProperty]
        public int StuckCoefficent { get; set; } = 2;
        [JsonProperty]
        public bool IsWaitOutInExitSensor { get; set; } = false;

        [JsonProperty]
        public double AccTime { get; set; } = 1;

        /// <summary>
        /// 轨道扫码
        /// </summary>
        [JsonProperty]
        public bool ConveyorScan { get; set; } = false;

        [JsonProperty]
        public RTVPrm RTVPrm { get; set; } = new RTVPrm();
        public object Clone()
        {
            ConveyorPrm prm = new ConveyorPrm(this.Key + 10);
            prm = (ConveyorPrm)this.MemberwiseClone();
            prm.PreSitePrm = (PreSitePrm)this.PreSitePrm.Clone();
            prm.WorkingSitePrm = (WorkingSitePrm)this.WorkingSitePrm.Clone();
            prm.FinishedSitePrm = (FinishedSitePrm)this.FinishedSitePrm.Clone();
            prm.RTVPrm = (RTVPrm)this.RTVPrm.Clone();
            return prm;
        }
    }
    
    public enum BoardDirection
    {
        LeftToRight,
        RightToLeft,
        LeftToLeft,
        RightToRight,
    }

    public enum ConveyorSubsiteMode
    {
        Singel,//单站轨道
        Triple,//三站轨道
        PreAndDispense,//预热站+点胶站
        DispenseAndInsulation//点胶站+保温站
    }

}
