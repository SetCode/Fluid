using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.Infrastructure.UI
{
    public partial class SettingFormBase : FormEx
    {
        public SettingFormBase()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public event Action OnSaveClicked;
        public event Action OnResetClicked;
        public event Action OnResetAllClicked;

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.OnSaveClicked?.Invoke();
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.OnResetClicked?.Invoke();
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            this.OnResetAllClicked?.Invoke();
        }
    }
}
