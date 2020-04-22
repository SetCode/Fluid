using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.International
{

    ///<summary>
    /// Description	:语言切换控件类型判断工具类
    /// Author  	:liyi
    /// Date		:2019/06/01
    ///</summary>   
    public static class Utils
    {
        /// <summary>
        /// 判断指定控件是否是需要处理的容器控件
        /// Contain：GroupBox、TabControl、Panel
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool isContainer(Control control)
        {
            if (control is GroupBox || control is TabControl || control is TableLayoutPanel || control is TabPage)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断指定控件是否是需要加载资源的单文本控件
        /// 包含：Button、RadioButton、CheckBox、Label
        /// </summary>
        /// <param name="control">传入控件</param>
        /// <param name="skipButton">是否跳过Button</param>
        /// <param name="skipRadioButton">是否跳过RadioButton</param>
        /// <param name="skipCheckBox">是否跳过CheckBox</param>
        /// <param name="skipLabel">是否跳过Label</param>
        /// <returns></returns>
        public static bool isResourceControl(Control control, bool skipButton, bool skipRadioButton, bool skipCheckBox, bool skipLabel)
        {
            if (control is Button)
            {
                if (!skipButton)
                {
                    return true;
                }
            }
            else if (control is RadioButton)
            {
                if (!skipRadioButton)
                {
                    return true;
                }
            }
            else if (control is CheckBox)
            {
                if (!skipCheckBox)
                {
                    return true;
                }
            }
            else if (control is Label)
            {
                if (!skipLabel)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 是否是多文本控件
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool isMultiTextControl(Control control)
        {
            if (control is ComboBox)
            {
                return true;
            }
            return false;
        }
    }
}
