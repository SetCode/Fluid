using Anda.Fluid.Domain.FluProgram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App
{
    public partial class SingleDotWtForm : Form
    {
        public SingleDotWtForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Minimum = 0.01M;
            this.numericUpDown1.Maximum = 10.0M;
            this.numericUpDown1.Increment = 0.01M;
            this.numericUpDown1.Value = (decimal)FluidProgram.Current.RuntimeSettings.SingleDropWeight;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FluidProgram.Current.RuntimeSettings.SingleDropWeight = (double)this.numericUpDown1.Value;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
