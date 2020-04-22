using Anda.Fluid.Domain.Settings;
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

namespace Anda.Fluid.App
{
    public partial class ProgramSettingMotionForm : FormEx
    {
        public ProgramSettingMotionForm(MotionSettings settings)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.propertyGrid1.SelectedObject = settings;
            this.ReadLanguageResources();
        }
    }
}
