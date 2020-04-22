using csDmc1000;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Anda.Fluid.Drive.Conveyor.LeadShine.IO
{
    internal class InPut
    {
        private DateTime pulseStartTime;
        public InPut(DiEnum diName)
        {
            this.Name = diName;
        }
        public DiEnum Name { get; private set; }
        private IOSts PreSts { get; set; }
        public IOSts CurrSts { get; set; }

        public void UpdateSts()
        {
            if (this.IsRisingOrFalling())
            {
                this.UpdatePulse();
            }
            else
            {                
                this.PreSts = this.CurrSts;

                this.ReadCurrSts();
            }           

        }
        private bool IsRisingOrFalling()
        {
            if (this.CurrSts == IOSts.IsFalling || this.CurrSts == IOSts.IsRising)
            {
                return true;
            }
            else
                return false;
        }

        private void UpdatePulse()
        {
            if (DateTime.Now - this.pulseStartTime > TimeSpan.FromMilliseconds(20))
            {
                if (this.CurrSts == IOSts.IsFalling)
                {
                    this.CurrSts = IOSts.Low;
                }
                else if (this.CurrSts == IOSts.IsRising)
                {
                    this.CurrSts = IOSts.High;
                }
            }

        }
        
        private void ReadCurrSts()
        {
            int state = Dmc1000.d1000_in_bit((int)this.Name);

            //将读取到的状态取反
            if (state == 0)
            {
                state = 1;
            }
            else
            {
                state = 0;
            }

            if (state == 0) 
            {
                if (this.PreSts == IOSts.High)
                {
                    this.CurrSts = IOSts.IsFalling;
                    this.pulseStartTime = DateTime.Now;
                }
                else
                {
                    this.CurrSts = IOSts.Low;
                }
            }
            else 
            {
                if (this.PreSts == IOSts.Low)
                {
                    this.CurrSts = IOSts.IsRising;
                    this.pulseStartTime = DateTime.Now;
                }
                else
                {
                    this.CurrSts = IOSts.High;
                }               
            }

        }
    }
    public enum DiEnum
    {
        轨道1进板感应 = 1,
        轨道1出板感应,
        轨道2进板感应,
        轨道2出板感应,

        轨道1上游设备有板,
        轨道1下游设备求板,
        轨道2上游设备有板,       
        轨道2下游设备求板,

        轨道1预热站到位,
        轨道1工作站到位,
        轨道1成品站到位,

        轨道2预热站到位,
        轨道2工作站到位,
        轨道2成品站到位,

        擦拭转动检测,
        擦拭无尘布检测,
       


        轨道1预热站真空,
        轨道1工作站真空,
        轨道1成品站真空,

        轨道2预热站真空,
        轨道2工作站真空,
        轨道2成品站真空,

        轨道1工作站顶升气缸顶升到位 = 29,
        轨道1工作站顶升气缸回退到位,
        轨道1工作站阻挡气缸阻挡到位,
        轨道1工作站阻挡气缸回退到位,

    }

    /// <summary>
    /// Y轴限位信号的状态
    /// </summary>
    internal class AxisYLimitInput
    {
        /// <summary>
        /// 负极限信号状态
        /// </summary>
        internal IOSts N_LimitSts { get; private set; }

        /// <summary>
        /// 正极限信号状态
        /// </summary>
        internal IOSts P_LimitSts { get; private set; }

        /// <summary>
        /// 原点信号状态
        /// </summary>
        internal IOSts OrgSts { get; private set; }

        public void Upstate()
        {
            int state = Dmc1000.d1000_get_axis_status(2);

            int NLimitBit = (state >> 0) & 1;
            int PLimitBit = (state >> 1) & 1;
            int OrgBit = (state >> 2) & 1;

            if (NLimitBit == 1)
            {
                this.N_LimitSts = IOSts.High;
            }
            else
            {
                this.N_LimitSts = IOSts.Low;
            }

            if (PLimitBit == 1)
            {
                this.P_LimitSts = IOSts.High;
            }
            else
            {
                this.P_LimitSts = IOSts.Low;
            }

            if (OrgBit == 1)
            {
                this.OrgSts = IOSts.High;
            }
            else
            {
                this.OrgSts = IOSts.Low;
            }
        }
    }
}
