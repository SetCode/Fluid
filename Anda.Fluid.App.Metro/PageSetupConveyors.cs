using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Anda.Fluid.Domain.Conveyor.Forms;

namespace Anda.Fluid.App.Metro
{
    public partial class PageSetupConveyors : MetroSetUserControl
    {
        public PageSetupConveyors()
        {
            InitializeComponent();
            this.ShowBorder = false;

            ConveyorSettingForm form = new ConveyorSettingForm(2);
            form.TopLevel = false;
            form.Parent = this;
            form.FormBorderStyle = FormBorderStyle.None;
            form.ForeColor = Color.Black;
            form.StartPosition = FormStartPosition.CenterParent;
            form.Show();
        }
    }
}
