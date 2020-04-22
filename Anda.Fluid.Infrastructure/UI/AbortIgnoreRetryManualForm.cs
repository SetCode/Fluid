using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.UI
{
    public partial class AbortIgnoreRetryManualForm : Form
    {
        public AbortIgnoreRetryManualForm(string text)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.textBox1.Text = text;
            this.textBox1.BackColor = Color.Red;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }
    }
}
