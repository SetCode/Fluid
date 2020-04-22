using Anda.Fluid.Infrastructure.International;
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
    public partial class OperateSetting : FormEx
    {
        public OperateSetting()
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            this.ReadLanguageResources();                     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                DataSetting.Default.IsReStart = true;
            }
            else
            {
                DataSetting.Default.IsReStart = false;
            }

            DataSetting.Save();
        }

        private void SettingTest_Load(object sender, EventArgs e)
        {
            DataSetting.Load();
            if (DataSetting.Default.IsReStart)
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
