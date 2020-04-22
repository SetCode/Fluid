using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            this.TopLevel = true;
            this.rtbHelp.ReadOnly = true;
            this.rtbHelp.BorderStyle = BorderStyle.None;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private string textHelp = string.Empty;
        private Form parentFrm;
        public HelpForm SetUp(string textHelp, Form parent)
        {
            this.textHelp = textHelp;
            this.parentFrm = parent;
            return this;
        }

        private void Help_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(this.parentFrm.Location.X+ this.parentFrm.Width, this.parentFrm.Location.Y); 
            this.rtbHelp.Text = this.textHelp;
        }
    }
}
