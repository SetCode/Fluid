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
    /// Description	:窗口扩展类(可以在此处增加扩展通用窗口方法)
    /// Author  	:liyi
    /// Date		:2019/05/30
    ///</summary>   
    public class FormEx : Form
    {
        private bool skipButton = false;
        private bool skipRadioButton = false;
        private bool skipCheckBox = false;
        private bool skipLabel = false;
        /// <summary>
        /// 递归读取容器控件中的子控件语言资源
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
                else if (Utils.isResourceControl(subControl, this.skipButton, this.skipRadioButton, this.skipCheckBox, this.skipLabel))
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
        /// 保存容器型控件的所有子孙控件中需要语言切换的控件
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
                if (Utils.isContainer(subControl))
                {
                    SaveContainerSubControlText(subControl, controlsText);
                }
                else if (Utils.isResourceControl(subControl, this.skipButton, this.skipRadioButton, this.skipCheckBox, this.skipLabel))
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
        /// 保存窗体所有控件name-text键值对到语言数据结构
        /// </summary>
        public virtual void SaveLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.skipButton = skipButton;
            this.skipRadioButton = skipRadioButton;
            this.skipCheckBox = skipCheckBox;
            this.skipLabel = skipLabel;
            Dictionary<string, List<string>> controlsText = new Dictionary<string, List<string>>();
            if (LanguageHelper.Instance.ContainForm(this.GetType().Name))
            {
                controlsText = LanguageHelper.Instance.ReadLanguageResources(this.GetType().Name);
            }
            if (!controlsText.ContainsKey(this.GetType().Name))
            {
                if (!this.Text.Equals(""))
                {
                    //添加窗体标题文本
                    controlsText.Add(this.GetType().Name, new List<string>(new string[] { this.Text }));
                }
            }
            //添加普通控件文本
            foreach (Control control in this.Controls)
            {
                if (controlsText.ContainsKey(control.Name) && !Utils.isContainer(control))
                {
                    continue;
                }
                List<string> textList = new List<string>();
                if (Utils.isResourceControl(control, skipButton, skipRadioButton, skipCheckBox, skipLabel))
                {
                    if (control.Text.Equals(""))
                    {
                        continue;
                    }
                    textList.Add(control.Text);
                    controlsText.Add(control.Name, textList);
                }
                else if (Utils.isContainer(control))
                {
                    this.SaveContainerSubControlText(control, controlsText);
                }
            }
            //保存到相关数据结构中
            LanguageHelper.Instance.SaveLanguageResources(this.GetType().Name, controlsText);
        }
        /// <summary>
        /// 从语言数据结构中读取窗体所有控件显示文本
        /// </summary>
        public virtual void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            this.skipButton = skipButton;
            this.skipRadioButton = skipRadioButton;
            this.skipCheckBox = skipCheckBox;
            this.skipLabel = skipLabel;
            if (LanguageHelper.Instance.ContainForm(this.GetType().Name))
            {
                Dictionary<string, List<string>> controlsText = LanguageHelper.Instance.ReadLanguageResources(this.GetType().Name);
                if (controlsText.ContainsKey(this.GetType().Name))
                {
                    if (!controlsText[this.GetType().Name][0].Equals(""))
                    {
                        this.Text = controlsText[this.GetType().Name][0];
                    }
                }
                foreach (Control control in this.Controls)
                {
                    if (controlsText.ContainsKey(control.Name))
                    {
                        if (Utils.isResourceControl(control, skipButton, skipRadioButton, skipCheckBox, skipLabel))
                        {
                            if (!controlsText[control.Name][0].Equals(""))
                            {
                                control.Text = controlsText[control.Name][0];
                            }
                        }
                    }
                    if (Utils.isContainer(control))
                    {
                        ReadContainerSubControlTest(control, controlsText);
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
            Dictionary<string, List<string>> controlsText = LanguageHelper.Instance.ReadLanguageResources(this.GetType().Name);
            if (controlsText.ContainsKey(key))
            {
                return controlsText[key][0];
            }
            return "";
        }
        /// <summary>
        /// 添加某些特殊键值对
        /// 提供给子类添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public List<string> ReadKeyListFromResources(string key)
        {
            Dictionary<string, List<string>> controlsText = LanguageHelper.Instance.ReadLanguageResources(this.GetType().Name);
            if (controlsText.ContainsKey(key))
            {
                return controlsText[key];
            }
            return null;
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
        /// 保存propertyGrid控件的显示对象的语言资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        public void SaveProportyGridLngText<T>(T param)
        {
            PropertyDescriptorCollection appSetingAttributes = TypeDescriptor.GetProperties(param);
            foreach (PropertyDescriptor item in appSetingAttributes)
            {
                this.SaveKeyValueToResources(item.Name, item.DisplayName);
                this.SaveKeyValueToResources(item.Category, item.Category);
            }
        }
        /// <summary>
        /// 当前窗体是否有语言资源
        /// </summary>
        /// <returns></returns>
        public bool HasLngResources()
        {
            return LanguageHelper.Instance.ContainForm(this.GetType().Name);
        }
    }
}
