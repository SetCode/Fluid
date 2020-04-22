using Anda.Fluid.Infrastructure.Alarming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.Main
{
    public partial class AlarmForm : Form
    {
        private AlarmControl alarmControl;

        public AlarmForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormClosing += FormAlarm_FormClosing;
        }

        private void FormAlarm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.panel1.Controls.Clear();
        }

        public AlarmForm AddControl(AlarmControl c)
        {
            this.alarmControl = c;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(c);
            c.Dock = DockStyle.Fill;
            return this;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.alarmControl?.ClearAlarms();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
