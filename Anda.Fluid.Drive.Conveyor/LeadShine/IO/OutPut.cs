using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.IO
{
    internal class OutPut
    {
        public OutPut(DoEnum doName)
        {
            this.Name = doName;
        }

        public DoEnum Name { get; private set; }
        public IOSts CurrSts { get; set; }

        public void UpdateSts()
        {
            int state = csDmc1000.Dmc1000.d1000_get_outbit((int)this.Name);

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
                this.CurrSts = IOSts.Low;
            }
            else if (state == 1)
            {
                this.CurrSts = IOSts.High;
            }
        }

        public int SetSts(bool sts)
        {
            if (sts)
            {
                if (this.CurrSts == IOSts.High)
                {
                    return 0;
                }
                else
                {
                    return csDmc1000.Dmc1000.d1000_out_bit((int)this.Name, 0);
                }
            }
            else
            {
                if (this.CurrSts == IOSts.Low)
                {
                    return 0;
                }
                else
                {
                    return csDmc1000.Dmc1000.d1000_out_bit((int)this.Name, 1);
                }
            }
        }
    }
    public enum DoEnum
    {
        轨道1求板=1,
        轨道1放板,
        轨道2求板,       
        轨道2放板,

        轨道1预热站阻挡,
        轨道1预热站顶升,
        轨道1工作站阻挡,
        轨道1工作站顶升,
        轨道1成品站阻挡,
        轨道1成品站顶升,

        轨道2预热站阻挡,
        轨道2预热站顶升,
        轨道2工作站阻挡,
        轨道2工作站顶升,
        轨道2成品站阻挡,
        轨道2成品站顶升,      

        轨道1预热站真空,
        轨道1工作站真空,
        轨道1成品站真空,

        轨道2预热站真空,
        轨道2工作站真空,
        轨道2成品站真空,
    }
}
