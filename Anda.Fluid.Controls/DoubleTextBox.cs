using System;

namespace Anda.Fluid.Controls
{
    public class DoubleTextBox : NumericTextBox
    {
        private bool isValid = false;
        public override bool IsValid
        {
            get { return isValid; }
        }

        private double value = 0d;
        public double Value
        {
            get
            {
                return value;
            }
        }

        public DoubleTextBox() : this(false, 3)
        {
        }

        public DoubleTextBox(bool unsigned, int decimalDigitsNumber) : base(unsigned, true, decimalDigitsNumber)
        {
        }

        protected override void OnTextChanged(EventArgs e)
        {
            try
            {
                value = Math.Round(double.Parse(Text), decimalDigitsNumber);
                setNormalBgColor();
                isValid = true;
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
