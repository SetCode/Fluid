using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Motion
{
    public class JogComboBox : ComboBox
    {
        private bool mouseWheeling;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(mouseWheeling)
            {
                mouseWheeling = false;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            mouseWheeling = true;
            base.OnMouseWheel(e);
        }
    }
}
