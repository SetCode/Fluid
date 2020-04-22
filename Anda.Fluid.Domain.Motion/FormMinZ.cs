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

namespace Anda.Fluid.Domain.Motion
{
    public partial class FormMinZ : JogFormBase
    {
        public FormMinZ()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.txtMinZ.ReadOnly = true;
            this.btnTeach.Click += BtnTeach_Click;

            if (Machine.Instance.Robot != null)
            {
                this.ShowMinZ();
            }
        }

        private void BtnTeach_Click(object sender, EventArgs e)
        {
            Machine.Instance.Robot.DefaultPrm.MinZ = Machine.Instance.Robot.PosZ;
            Machine.Instance.Robot.SaveDefaultPrm();
            this.ShowMinZ();

        }

        private void ShowMinZ()
        {
            this.txtMinZ.Text = Machine.Instance.Robot.DefaultPrm.MinZ.ToString();
        }
    }
}
