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
    public partial class SettingTest : Form
    {
        public SettingTest()
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            InitializeComponent();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                SvoSetting.Instance.SetFormRestart();
            }
            else
            {
                SvoSetting.Instance.SetFormNotRestart();
            }

            SvoSetting.Instance.Save();
        }

        private void SettingTest_Load(object sender, EventArgs e)
        {
            if (SvoSetting.Instance.FormIsRestart())
            {
                this.checkBox1.Checked = true;
            }
            else
            {
                this.checkBox1.Checked = false;
            }

        }
    }
}
