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
    public partial class AbortOrRunForm : Form
    {
        public AbortOrRunForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }
        /// <summary>
        /// 停止按钮按下后  No表示不是暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        /// <summary>
        /// 暂停按钮按下后 continue  yes表示暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
    }
}
