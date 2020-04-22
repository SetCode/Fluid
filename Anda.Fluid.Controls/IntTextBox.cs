using System;
using System.Drawing;

namespace Anda.Fluid.Controls
{
    public class IntTextBox : NumericTextBox
    {
        private bool isValid = false;
        public override bool IsValid
        {
            get { return isValid; }
        }

        private int value = 0;
        public int Value
        {
            get
            {
                return value;
            }
        }

        public IntTextBox() : this(false)
        {
        }

        public IntTextBox(bool unsigned) : base(unsigned, false, 0)
        {
        }

        protected override void OnTextChanged(EventArgs e)
        {
            try
            {
                value = int.Parse(Text);
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
