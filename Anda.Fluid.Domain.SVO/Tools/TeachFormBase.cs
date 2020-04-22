using Anda.Fluid.Domain.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.SVO
{
    internal partial class TeachFormBase : JogFormBase
    {
        private IClickable clickable;
        public TeachFormBase()
        {
            InitializeComponent();

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterParent;

            clickable = this as IClickable;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.clickable.DoPrev();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.clickable.DoNext();
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            this.clickable.DoTeach();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.clickable.DoDone();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            this.clickable.DoHelp();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clickable.DoCancel();
        }

    }
}
