using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.International
{

    ///<summary>
    /// Description	:自定义控件的语言切换父类
    /// Author  	:liyi
    /// Date		:2019/05/30
    ///</summary>   
    public class UserControlEx : UserControl
    {
        private bool skipButton = false;
        private bool skipRadioButton = false;
        private bool skipCheckBox = false;
        private bool skipLabel = false;
        /// <summary>
        /// 读取容器控件中的子控件显示文本
        /// </summary>
        /// <param name="control"></param>
        /// <param name="controlsText"></param>
        private void ReadContainerSubControlTest(Control control, Dictionary<string, List<string>> controlsText)
        {
            if (control is GroupBox || control is TabPage)
            {
                if (controlsText.ContainsKey(control.Name))
                {
                    if (!controlsText[control.Name][0].Equals(""))
                    {
                        control.Text = controlsText[control.Name][0];
                    }
                }
            }
            if (!control.HasChildren)
            {
                return;
            }
            foreach (Control subControl in control.Controls)
            {
                if (Utils.isContainer(subControl))
                {
                    ReadContainerSubControlTest(subControl, controlsText);
                }
                else if (Utils.isResourceControl(subControl,this.skipButton,this.skipRadioButton,this.skipCheckBox,this.skipLabel))
                {
                    if (controlsText.ContainsKey(subControl.Name))
                    {
                        if (!controlsText[subControl.Name][0].Equals(""))
                        {
                            subControl.Text = controlsText[subControl.Name][0];
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取容器型控件的所有子孙控件中需要语言切换的控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="controlsText"></param>
        private void SaveContainerSubControlText(Control control, Dictionary<string, List<string>> controlsText)
        {
            if (control is GroupBox || control is TabPage)
            {
                if (!controlsText.ContainsKey(control.Name))
                {
                    if (!control.Text.Equals(""))
                    {
                        controlsText.Add(control.Name, new List<string> { control.Text });
                    }
                }
            }
            if (!control.HasChildren)
            {
                return;
            }
            foreach (Control subControl in control.Controls)
            {
                if (subControl.HasChildren && Utils.isContainer(subControl))
                {
                    SaveContainerSubControlText(subControl, controlsText);
                }
                else if (Utils.isResourceControl(subControl,this.skipButton,this.skipRadioButton,this.skipCheckBox,this.skipLabel))
                {
                    if (!controlsText.ContainsKey(subControl.Name))
                    {
                        if (!subControl.Text.Equals(""))
                        {
                            controlsText.Add(subControl.Name, new List<string> { subControl.Text });
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 保存自定义控件需要加载的文本控件
        /// </summary>
        /// <param name="skipButton">是否保存Button文本</param>
        /// <param name="skipRadioButton">是否保存RadioButton文本</param>
        /// <param name="skipCheckBox">是否保存CheckBox文本</param>
        /// <param name="skipLabel">是否保存Label文本</param>
        public virtual void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.skipButton = skipButton;
            this.skipRadioButton = skipRadioButton;
            this.skipCheckBox = skipCheckBox;
            this.skipLabel = skipLabel;
            Dictionary<string, List<string>> controlsText = new Dictionary<string, List<string>>();
            foreach (Control control in this.Controls)
            {
                List<string> textList = new List<string>();
                if (controlsText.ContainsKey(control.Name) && !Utils.isContainer(control))
                {
                    continue;
                }
                if (Utils.isResourceControl(control, this.skipButton, this.skipRadioButton, this.skipCheckBox, this.skipLabel))
                {
                    if (!control.Text.Equals(""))
                    {
                        textList.Add(control.Text);
                        controlsText.Add(control.Name, textList);
                    }
                }
                else if (Utils.isContainer(control))
                {
                    SaveContainerSubControlText(control, controlsText);
                }
            }
            LanguageHelper.Instance.SaveLanguageResources(this.GetType().Name, controlsText);
        }
        /// <summary>
        /// 读取自定义控件所有需要加载语言文本
        /// </summary>
        /// <param name="skipButton">是否读取Button文本</param>
        /// <param name="skipRadioButton">是否读取RadioButton文本</param>
        /// <param name="skipCheckBox">是否读取CheckBox文本</param>
        /// <param name="skipLabel">是否读取Label文本</param>
        public virtual void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.skipButton = skipButton;
            this.skipRadioButton = skipRadioButton;
            this.skipCheckBox = skipCheckBox;
            this.skipLabel = skipLabel;
            if (LanguageHelper.Instance.ContainForm(this.GetType().Name))
            {
                Dictionary<string, List<string>> controlsText = LanguageHelper.Instance.ReadLanguageResources(this.GetType().Name);
                foreach (Control control in this.Controls)
                {
                    if (controlsText.ContainsKey(control.Name))
                    {
                        if (Utils.isResourceControl(control, this.skipButton, this.skipRadioButton, this.skipCheckBox, this.skipLabel))
                        {
                            if (!controlsText[control.Name][0].Equals(""))
                            {
                                control.Text = controlsText[control.Name][0];
                            }
                        }
                        else if (Utils.isContainer(control))
                        {
                            ReadContainerSubControlTest(control, controlsText);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 添加某些特殊键值对
        /// 提供给子类添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SaveKeyValueToResources(string key, string value)
        {
            LanguageHelper.Instance.SaveKeyValueToResources(this.GetType().Name, key, value);
        }
        /// <summary>
        /// 添加某些特殊键值对
        /// 提供给子类添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public void SaveKeyListToResources(string key, List<string> values)
        {
            LanguageHelper.Instance.SaveKeyListToResources(this.GetType().Name, key, values);
        }
        /// <summary>
        /// 添加某些特殊键值对
        /// 提供给子类添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public string ReadKeyValueFromResources(string key)
        {
            return LanguageHelper.Instance.ReadKeyValueFromResources(this.GetType().Name, key);
        }
        /// <summary>
        /// 添加某些特殊键值对
        /// 提供给子类添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public List<string> ReadKeyListFromResources(string key)
        {
            return LanguageHelper.Instance.ReadKeyListFrommResources(this.GetType().Name,key);
        }
        /// <summary>
        /// 判断当前窗口是否有某资源
        /// </summary>
        /// <param name="keyName">资源名称</param>
        /// <returns></returns>
        public bool ContainKey(string keyName)
        {
            if (HasLngResources())
            {
                return LanguageHelper.Instance.FormLngContainKey(this.GetType().Name, keyName);
            }
            return false;
        }
        /// <summary>
        /// 保存propertyGrid控件显示的语言资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        public void SaveProportyGridLngText<T>(T param)
        {
            PropertyDescriptorCollection appSetingAttributes = TypeDescriptor.GetProperties(param);
            foreach (PropertyDescriptor item in appSetingAttributes)
            {
                this.SaveKeyValueToResources(item.Name,item.DisplayName);
                this.SaveKeyValueToResources(item.Category, item.Category);
            }
        }
        ToolTip ToolTip = new ToolTip();
        public void ReadDisplayTip(object sender, EventArgs e)
        {
            try
            {
            Control Control = (Control)sender;
            string ParentName = Control.Parent.ToString();
            string TypeName = ParentName.Substring(Control.Parent.CompanyName.Length + 1);
            if (LanguageHelper.Instance.ContainForm(TypeName))
            {
                Dictionary<string, List<string>> controlsText = LanguageHelper.Instance.ReadLanguageResources(TypeName);
                if (controlsText.Count != 0)
                {
                    Control.Focus();
                    ToolTip.ShowAlways = true;
                    ToolTip.SetToolTip(Control, controlsText[Control.Name][0]);
                }
                }
            }
            catch (Exception)
            {
            }
        }
        public void DisopTip(object sender, EventArgs e)
        {
            ToolTip.ShowAlways = false;

        }
        /// <summary>
        /// 当前窗口是否有语言资源
        /// </summary>
        /// <returns></returns>
        public bool HasLngResources()
        {
            return LanguageHelper.Instance.ContainForm(this.GetType().Name);
        }
    }
}
