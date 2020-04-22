using Anda.Fluid.Drive;
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

namespace Anda.Fluid.Domain.Conveyor.Forms
{
    public partial class ConveyorControlForm : FormEx
    {
        private ConveyorControl conveyorControl1;
        public ConveyorControlForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.conveyorControl1 = new Anda.Fluid.Domain.Conveyor.Forms.ConveyorControl();
            this.conveyorControl1.SetUp();
            this.conveyorControl1.Parent = this;
            this.conveyorControl1.Location = new Point(1, 1);

            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                this.Size = new Size(this.Width, this.Height + 30);
            }
            this.ReadLanguageResources();
        }

        private void ConveyorControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
