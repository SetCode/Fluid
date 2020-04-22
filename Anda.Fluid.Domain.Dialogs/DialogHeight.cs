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

namespace Anda.Fluid.Domain.Dialogs
{
    public partial class DialogHeight : DialogBase
    {
        public DialogHeight()
        {
            InitializeComponent();
            this.ReadLanguageResources();
            this.laserControl1.MeasureStarting = p =>
            {
                p.X = Machine.Instance.Robot.PosX;
                p.Y = Machine.Instance.Robot.PosY;
            };
        }
    }
}
