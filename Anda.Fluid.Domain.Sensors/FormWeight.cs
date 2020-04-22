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

namespace Anda.Fluid.Domain.Sensors
{
    public partial class FormWeight : FormEx
    {
        public FormWeight()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.userControlWeight2.Dock = DockStyle.Fill;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.ReadLanguageResources();
        }

        public FormWeight Setup()
        {
            this.userControlWeight2.Setup();
            return this;
        }
    }
}
