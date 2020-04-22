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
    internal partial class LeadShineDiCtl : UserControl
    {
        private InPut inPut;
        public LeadShineDiCtl()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.txtId.ReadOnly = true;
            this.txtName.ReadOnly = true;
        }

        public void SetUp(InPut inPut)
        {
            this.inPut = inPut;
            this.txtId.Text = ((int)inPut.Name).ToString();
            this.txtName.Text = inPut.Name.ToString();
        }

        public void UpDateDi()
        {
            if (this.inPut.CurrSts == IOSts.High || this.inPut.CurrSts == IOSts.IsRising)
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
