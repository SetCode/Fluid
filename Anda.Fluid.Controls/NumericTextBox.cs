using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Anda.Fluid.Controls
{
    /// <summary>
    /// 只允许输入数字的TextBox
    /// </summary>
    public abstract class NumericTextBox : TextBox
    {
        /// <summary>
        /// 是否允许有小数位
        /// </summary>
        private bool hasDecimalDigits = true;

        /// <summary>
        /// 小数位数限制
        /// </summary>
        protected int decimalDigitsNumber = 3;

        /// <summary>
        /// 是否是无符号类型的数字
        /// </summary>
        private bool unsigned = false;

        /// <summary>
        /// 当前输入的内容是否是有效的
        /// </summary>
        public abstract bool IsValid
        {
            get;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unsigned">是否是有符号的</param>
        /// <param name="hasDecimalDigits">是否有小数位</param>
        /// <param name="decimalDigitsNumber">小数位数</param>
        public NumericTextBox(bool unsigned, bool hasDecimalDigits, int decimalDigitsNumber)
        {
            this.unsigned = unsigned;
            this.hasDecimalDigits = hasDecimalDigits;
            this.decimalDigitsNumber = decimalDigitsNumber;
            setNormalBgColor();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // 只能输入 负号、数字、点、Backspace
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') && e.KeyChar != (char)('-'))
            {
                e.Handled = true;
            }
            // 负号
            else if (e.KeyChar == '-')
            {
                // 非负数禁止输入负号
                if (unsigned)
                {
                    e.Handled = true;
                }
                // 否则只能是第一个字符，且只能有一个
                if (SelectionStart != 0 || (Text.Length > 0 && Text[0] == '-'))
                {
                    e.Handled = true;
                }
            }
            // 小数点
            else if (e.KeyChar == '.')
            {
                if (decimalDigitsNumber == 0)
                {
                    e.Handled = true;
                }
                else
                {
                    // 小数点只能输入一次
                    if (Text.IndexOf('.') != -1)
                    {
                        e.Handled = true;
                    }
                    // 第一位不能是小数点
                    else if (SelectionStart == 0)
                    {
                        e.Handled = true;
                    }
                    // 第一位是负号，第二个不能是小数点
                    else if (Text.Length > 0 && Text[0] == '-' && SelectionStart == 1)
                    {
                        e.Handled = true;
                    }
                }
            }
            // 数字
            else if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                if (e.KeyChar == '0')
                {
                    // 非负数第一个不能是0
                    if (SelectionStart == 0 && unsigned)
                    {
                        e.Handled = true;
                    }
                }
                // 小数点后位数限制
                if (Text.Length > 0)
                {
                    int index = Text.IndexOf('.');
                    if (index > -1 && Text.Length - 1 - index >= decimalDigitsNumber 
                        && SelectionLength == 0 && SelectionStart > index)
                    {
                        e.Handled = true;
                    }
                }
            }
            // 第一位是0，第二位必须是小数点
            if (Text.Length > 0 && Text[0] == '0' && SelectionStart == 1)
            {
                if (e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (!e.Handled)
            {
                base.OnKeyPress(e);
            }
        }

        /// <summary>
        /// 设置正常的背景色
        /// </summary>
        protected void setNormalBgColor()
        {
            BackColor = Color.White;
        }

        /// <summary>
        /// 设置出错时的背景色
        /// </summary>
        protected void setErrorBgColor()
        {
            BackColor = Color.FromArgb(255, 192, 192);
        }
    }
}
