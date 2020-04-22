using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    /// <summary>
    /// IO配置接口
    /// </summary>
    public interface IIOSetupable
    {
        IOPrm IOPrm { get; }
        void SetupDIPrm();
        void SetupDOPrm();
        void SetupIO();
        bool LoadIOPrm();
        bool SaveIOPrm();
    }

    /// <summary>
    /// IO参数，包括DI和DO
    /// </summary>
    public class IOPrm
    {
        /// <summary>
        /// DI参数集合
        /// </summary>
        public DIPrmMgr DIPrmMgr { get; set; } = new DIPrmMgr();

        /// <summary>
        /// DO参数集合
        /// </summary>
        public DOPrmMgr DOPrmMgr { get; set; } = new DOPrmMgr();
    }
}
