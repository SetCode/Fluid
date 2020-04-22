using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Domain.FluProgram;

namespace Anda.Fluid.App.CustomDataUI
{
    public partial class DefaultDataControl : CustomControlBase
    {
        public DefaultDataControl()
        {
            InitializeComponent();
            this.ReadLanguageResources();
        }

        public override void LoadParam(FluidProgram program)
        {
            
        }

        public override void SetParam(FluidProgram program)
        {
            
        }
    }
}
