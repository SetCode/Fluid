using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs
{
    public interface IOptional
    {
        void DoPrev();
        void DoNext();
        void DoTeach();
        void DoDone();
        void DoCancel();
    }
}
