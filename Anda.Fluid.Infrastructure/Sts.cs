using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure
{
    public enum StsType
    {
        Low,
        High,
        IsRising,
        IsFalling
    }

    public class Sts
    {
        private bool lastSts;
        private bool currSts;

        public Sts()
        {

        }

        public void Update(bool currSts)
        {
            this.currSts = currSts;
            if(this.currSts && !this.lastSts)
            {
                this.IsRising = true;
            }
            else if(!this.currSts && this.lastSts)
            {
                this.IsFalling = true;
            }
            else
            {
                this.IsRising = false;
                this.IsFalling = false;
            }
            this.lastSts = this.currSts;
        }

        public void Reverse()
        {
            this.Update(!this.currSts);
        }

        public bool Value { get { return this.currSts; } }

        public bool IsRising { get; private set; }

        public bool IsFalling { get; private set; }

        public bool Is(StsType stsType)
        {
            bool b = false;
            switch (stsType)
            {
                case StsType.IsRising:
                    b = this.IsRising;
                    break;
                case StsType.IsFalling:
                    b = this.IsFalling;
                    break;
                case StsType.High:
                    b = this.Value;
                    break;
                case StsType.Low:
                    b = !this.Value;
                    break;
            }
            return b;
        }
    }
}
