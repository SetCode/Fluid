using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Sensors
{
    public partial class GageDialog : Form
    {
        public GageDialog()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Machine.Instance.DigitalGage.DigitalGagable.Disconnect();
            if (!Machine.Instance.DigitalGage.DigitalGagable.Connect(TimeSpan.FromSeconds(1)))
            {
                MessageBox.Show("打开电子高度规失败", null, MessageBoxButtons.OKCancel);
                return;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //Read
            double height;
            Machine.Instance.DigitalGage.DigitalGagable.ReadHeight(out height);
            this.textBox1.Text = height.ToString("F3");
        }


    }
}
