using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.BatchModification
{
    public partial class SymbolLine : UserControl
    {
        public SymbolLine()
        {
            InitializeComponent();

        }
        public double GetPrm()
        {
            return this.tbTransitionR.Value;
        }

        public void SetPrm(double radius)
        {
            this.tbTransitionR.Text = radius.ToString("0.000"); ;
        }
        
    }

    public class SymbolLinePrm
    {
        public double TransitionR;

        public double ToleranceUp;

        public double ToleranceDown;
    }
}
