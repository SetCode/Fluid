using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Linq;

namespace Anda.Fluid.Infrastructure.International
{
    public enum LanguageType
    {
        zh_CN,
        en_US
    }

    ///<summary>
    /// Description	:窗体语言文本数据结构 <Form/UserControl,<Control,text>>
    /// Author  	:liyi
    /// Date		:2019/05/30
    ///</summary>   
    [JsonObject(MemberSerialization.OptIn)]
    public class LanguageHelper
    {
        /// <summary>
        /// 当前资源文件路径
        /// </summary>
        private string path = "";
        private string msgPath = "";
        /// <summary>
        /// 当前语言类型
        /// </summary>
        public string CurLang { get; set; } = "en_US";
        private LanguageHelper()
        {
            path = System.Windows.Forms.Application.StartupPath + "/LanguageResources/" + LanguageType.en_US.ToString() + ".lng";
            msgPath = System.Windows.Forms.Application.StartupPath + "/LanguageResources/" + LanguageType.en_US.ToString() + "_msg" + ".lng";
        }
        
        private static readonly LanguageHelper instance = new LanguageHelper();
        public static LanguageHelper Instance => instance;
        /// <summary>
        /// 语言资源存储数据结构
        /// </summary>
        [JsonProperty]
        private Dictionary<string, Dictionary<string, List<string>>> LanguageResources = new Dictionary<string, Dictionary<string, List<string>>>();
        [JsonProperty]
        private Dictionary<string, Dictionary<int, string>> MsgLngResources = new Dictionary<string, Dictionary<int, string>>();

        private List<IMsgI18N> obServerList = new List<IMsgI18N>();

        /// <summary>
        /// 切换当前语言资源
        /// </summary>
        /// <param name="languageType"></param>
        public void SWitchLanguage(LanguageType languageType)
        {
            CurLang = languageType.ToString();
            path = System.Windows.Forms.Application.StartupPath + "/LanguageResources/" + CurLang + ".lng";
            msgPath = System.Windows.Forms.Application.StartupPath + "/LanguageResources/" + CurLang + "_msg" + ".lng";
            if (this.Load())
            {
                foreach (IMsgI18N item in this.obServerList)
                {
                    item.ReadMsgLanguageResource();
                }
            }
        }

        /// <summary>
        /// 保存所有语言切换相关窗体控件资源
        /// </summary>
        public void SaveAllFormLanguageResource()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (var type in types)
                {
                    //Base相关的父类窗口不保存文本数据，直接保存子类窗口的文本
                    if (type.Name.Contains("MainForm"))//主窗口反射实例化异常
                    {
                        continue;
                    }
                    if (type.Name.Contains("Base"))
                    {
                        continue;
                    }
                    if (!(type.IsSubclassOf(typeof(FormEx))) && !(type.IsSubclassOf(typeof(UserControlEx))))
                    {
                        continue;
                    }
                    //继承自扩展自定义控件的自定义控件保存文本数据
                    if (type.BaseType == typeof(UserControlEx))
                    {
                        try
                        {
                            using (UserControlEx userControlEx = Activator.CreateInstance(type,true) as UserControlEx)
                            {
                                userControlEx.SaveLanguageResources();
                            }
                        }
                        catch
                        {
                            Console.WriteLine(string.Format("类型为 {0} 的自定义控件无法保存控件文本数据", type));
                        }
                    }
                    else if (type.BaseType != null)
                    {
                        if (type.BaseType.BaseType == typeof(UserControlEx))
                        {
                            try
                            {
                                using (UserControlEx userControlEx = Activator.CreateInstance(type, true) as UserControlEx)
                                {
                                    userControlEx.SaveLanguageResources();
                                }
                            }
                            catch
                            {
                                Console.WriteLine(string.Format("类型为 {0} 的自定义控件无法保存控件文本数据", type));
                            }
                        }
                        else if (type.BaseType.BaseType.BaseType != null && type.BaseType.BaseType.BaseType == typeof(UserControlEx))
                        {
                            try
                            {
                                using (UserControlEx userControlEx = Activator.CreateInstance(type, true) as UserControlEx)
                                {
                                    userControlEx.SaveLanguageResources();
                                }
                            }
                            catch
                            {
                                Console.WriteLine(string.Format("类型为 {0} 的自定义控件无法保存控件文本数据", type));
                            }
                        }
                    }
                    //保存非父类窗体的文本数据
                    if (type.BaseType == typeof(FormEx))
                    {
                        try
                        {
                            using (FormEx formEx = Activator.CreateInstance(type,true) as FormEx)
                            {
                                formEx.SaveLanguageResources();
                            }
                        }
                        catch
                        {
                            Console.WriteLine(string.Format("类型为 {0} 的窗体无法保存控件文本数据", type));
                        }
                    }
                    else if (type.BaseType != null)
                    {
                        if (type.BaseType.BaseType == typeof(FormEx))
                        {
                            try
                            {
                                using (FormEx formEx = Activator.CreateInstance(type,true) as FormEx)
                                {
                                    formEx.SaveLanguageResources();
                                }
                            }
                            catch
                            {
                                Console.WriteLine(string.Format("类型为 {0} 的窗体无法保存控件文本数据", type));
                            }
                            continue;
                        }
                        else if(type.BaseType.BaseType.BaseType != null && type.BaseType.BaseType.BaseType == typeof(FormEx))
                        {
                            try
                            {
                                using (FormEx formEx = Activator.CreateInstance(type, true) as FormEx)
                                {
                                    formEx.SaveLanguageResources();
                                }
                            }
                            catch
                            {
                                Console.WriteLine(string.Format("类型为 {0} 的窗体无法保存控件文本数据", type));
                            }
                            continue;
                        }
                    }
                }
            }
            foreach (IMsgI18N item in this.obServerList)
            {
                item.SaveMsgLanguageResource();
            }
            this.Save();
        }
        /// <summary>
        /// 添加特殊控件文本到数据结构中
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="controlName"></param>
        /// <param name="values"></param>
        public void SaveKeyValueToResources(string formName,string controlName, string value)
        {
            Dictionary<string, List<string>> controlsText = new Dictionary<string, List<string>>();
            if (LanguageResources.ContainsKey(formName))
            {
                if (LanguageResources[formName].ContainsKey(controlName))
                {
                    return;
                }
                else
                {
                    LanguageResources[formName].Add(controlName, new List<string>(new string[] { value }));
                }
            }
            else
            {
                controlsText.Add(controlName, new List<string>(new string[] { value }));
                LanguageResources.Add(formName, controlsText);
            }
        }
        /// <summary>
        /// 从数据结构中读取特殊控件文本
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public string ReadKeyValueFromResources(string formName, string controlName)
        {
            Dictionary<string, List<string>> controlsText = LanguageHelper.Instance.ReadLanguageResources(formName);
            if (controlsText.ContainsKey(controlName))
            {
                return controlsText[controlName][0];
            }
            return "";
        }
        /// <summary>
        /// 从数据结构中读取特殊控件文本数组
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public List<string> ReadKeyListFrommResources(string formName, string controlName)
        {
            Dictionary<string, List<string>> controlsText = LanguageHelper.Instance.ReadLanguageResources(formName);
            if (controlsText.ContainsKey(controlName))
            {
                return controlsText[controlName];
            }
            return null;
        }
        /// <summary>
        /// 添加特殊控件文本数组到数据结构中
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="controlName"></param>
        /// <param name="values"></param>
        public void SaveKeyListToResources(string formName, string controlName, List<string> values)
        {
            Dictionary<string, List<string>> controlsText = LanguageHelper.Instance.ReadLanguageResources(formName);
            if (LanguageResources.ContainsKey(formName))
            {
                if (LanguageResources[formName].ContainsKey(controlName))
                {
                    if (Enumerable.SequenceEqual(LanguageResources[formName][controlName], values))
                    {
                        return;
                    }
                    else
                    {
                        LanguageResources[formName][controlName] = values;
                    }
                }
                else
                {
                    LanguageResources[formName].Add(controlName, values);
                }
            }
            else
            {
                controlsText.Add(controlName,  values);
                LanguageResources.Add(formName, controlsText);
            }
        }
        /// <summary>
        /// 保存相关窗体或
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="formResources"></param>
        public void SaveLanguageResources(string formName,Dictionary<string,List<string>> formResources)
        {
            //已经存在且不是自定义控件的覆盖处理
            if (!LanguageResources.ContainsKey(formName))
            {
                LanguageResources.Add(formName, formResources);
            }
            else
            {
                foreach (string key in formResources.Keys)
                {   
                    //已保存的控件文本不再重复保存
                    if (LanguageResources[formName].ContainsKey(key))
                    {
                        continue;
                    }
                    LanguageResources[formName].Add(key,formResources[key]);
                }
            }
        }
        /// <summary>
        /// 返回指定窗口或指定自定义控件的控件文本资源
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public Dictionary<string,List<string>> ReadLanguageResources(string formName)
        {
            Dictionary<string, List<string>> formResources = new Dictionary<string, List<string>>();
            if (LanguageResources.ContainsKey(formName))
            {
                foreach (string key in LanguageResources[formName].Keys)
                {
                    formResources.Add(key, LanguageResources[formName][key]);
                }
            }
            return formResources;
        }
        /// <summary>
        /// 检查语言资源数据结构中是否有指定窗体或自定义控件
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public bool ContainForm(string formName)
        {
            return LanguageResources.ContainsKey(formName);
        }

        public void Register(IMsgI18N obServer)
        {
            if (this.obServerList.Contains(obServer))
            {
                return;
            }
            this.obServerList.Add(obServer);
        }

        public void UnRegister(IMsgI18N obServer)
        {
            if (this.obServerList.Contains(obServer))
            {
                this.obServerList.Remove(obServer);
            }
        }

        /// <summary>
        /// 判断某窗口是否含有指定控件资源
        /// </summary>
        /// <param name="formName">窗体类名</param>
        /// <param name="KeyName">资源名称</param>
        /// <returns></returns>
        public bool FormLngContainKey(string formName,string KeyName)
        {
            if (ContainForm(formName))
            {
                if (LanguageResources[formName].ContainsKey(KeyName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 保存消息或提示文本提示信息
        /// </summary>
        /// <param name="className">当前类名</param>
        /// <param name="key">消息值Id</param>
        /// <param name="msgText">消息文本</param>
        public void SaveMsgLngResource(string className,int key,string msgText)
        {
            if (this.MsgLngResources.ContainsKey(className))
            {
                if (this.MsgLngResources[className].ContainsKey(key))
                {
                    this.MsgLngResources[className][key] = msgText;
                }
                else
                {
                    this.MsgLngResources[className].Add(key, msgText);
                }
            }
            else
            {
                Dictionary<int,string> temp = new Dictionary<int, string>();
                temp.Add(key, msgText);
                this.MsgLngResources.Add(className, temp);
            }
        }

        /// <summary>
        /// 读取消息或文本提示信息
        /// </summary>
        /// <param name="className">当前类名</param>
        /// <param name="key">消息Id</param>
        /// <returns></returns>
        public string ReadMsgLngResource(string className,int key)
        {
            if (this.MsgLngResources.ContainsKey(className))
            {
                if (this.MsgLngResources[className].ContainsKey(key))
                {
                    return this.MsgLngResources[className][key];
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        private bool Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this.LanguageResources, Formatting.Indented);
                string json2 = JsonConvert.SerializeObject(this.MsgLngResources, Formatting.Indented);
                System.IO.File.WriteAllText(this.path, json);
                System.IO.File.WriteAllText(this.msgPath, json2);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Load()
        {
            try
            {
                string json = System.IO.File.ReadAllText(this.path);
                string json2 = System.IO.File.ReadAllText(this.msgPath);
                this.LanguageResources = JsonConvert.DeserializeObject<Dictionary<string,Dictionary<string,List<string>>>>(json);
                this.MsgLngResources = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, string>>>(json2);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
