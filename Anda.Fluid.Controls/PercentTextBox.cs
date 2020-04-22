using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Controls
{
    public class PercentTextBox : NumericTextBox
    {
        private bool isValid = false;
        private double max = 100d;
        private double min = 0d;
        public PercentTextBox() : base(true, true, 2)
        {
        }

        public override bool IsValid
        {
            get { return this.isValid;}
        }

        public double Value { get; private set; } = 0d;

        protected override void OnTextChanged(EventArgs e)
        {
            try
            {
                double value = Math.Round(double.Parse(Text), decimalDigitsNumber);
                if (value > this.max || value < this.min)
                {
                    setErrorBgColor();
                    isValid = false;
                }
                else
                {
                    setNormalBgColor();
                    isValid = true;
                }              
            }
            catch (Exception)
            {
                setErrorBgColor();
                isValid = false;
            }
            base.OnTextChanged(e);
        }
    }
}
