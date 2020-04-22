using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.SVO
{
    internal interface IClickable
    {
        void DoNext();
        void DoPrev();
        void DoTeach();
        void DoDone();
        void DoHelp();
        void DoCancel();
    }
}
