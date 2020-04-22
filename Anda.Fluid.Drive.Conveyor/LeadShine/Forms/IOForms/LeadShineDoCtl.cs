using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive.Conveyor.LeadShine.IO;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.Forms.IOForms
{
    internal partial class LeadShineDoCtl : UserControl
    {
        private OutPut outPut;
        public LeadShineDoCtl()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.txtId.ReadOnly = true;
            this.txtName.ReadOnly = true;

        }

        public void SetUp(OutPut outPut)
        {
            this.outPut = outPut;
            this.txtId.Text = ((int)this.outPut.Name).ToString();
            this.txtName.Text = this.outPut.Name.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.outPut.CurrSts == IOSts.High)
            {
                this.outPut.SetSts(false);
            }
            else if (this.outPut.CurrSts == IOSts.Low)
            {
                this.outPut.SetSts(true);
            }
        }

        public void UpdateDo()
        {
            if (this.outPut.CurrSts == IOSts.High)
            {
                this.pictureBox1.BackColor = Color.Green;
            }
            else
            {
                this.pictureBox1.BackColor = Color.Gray;
            }
        }
    }
}
