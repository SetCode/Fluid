using MetroSet_UI.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static Anda.Fluid.Infrastructure.International.AccessEnums;

namespace Anda.Fluid.Infrastructure.International.Access
{
    [Serializable]
    public class AccessControlMgr
    {
        
        private readonly static AccessControlMgr instance = new AccessControlMgr();
        private AccessControlMgr()
        { }
        public  static AccessControlMgr Instance = instance;

        public List<ContainerAccess> ContainerAccessList = new List<ContainerAccess>();
        //public AccessTree AccessTree { get; private set; } = new AccessTree();


        public RoleEnums CurrRole = RoleEnums.Operator;
        
        private string pathAccess = Application.StartupPath+ "\\Settings"+ "\\Business" + "\\" + typeof(AccessControlMgr).Name;

        public List<IAccessControllable> accessControls { get; } = new List<IAccessControllable>();

        public void Register(IAccessControllable accessControl)
        {
            if (this.accessControls.Contains(accessControl))
            {
                return;
            }
            this.accessControls.Add(accessControl);
        }
        //public void AddGroupAccessList(List<AccessGroup> groups)
        //{
        //    foreach (AccessGroup item in groups)
        //    {
        //        this.AddGroupAccess(item);
        //    }

        //}
        public void Clear()
        {
            this.ContainerAccessList.Clear();
        }

        public void AddContainerAccess(ContainerAccess containerAccess)
        {
            ContainerAccess accessExist = null;
            accessExist = this.ContainerAccessList.Find(p => p.ContainerName == containerAccess.ContainerName);

            if (accessExist == null)
            {
                this.ContainerAccessList.Add(containerAccess);
            }
            else
            {
                this.UpdateExistAccess(accessExist, containerAccess);
            }

        }


        public ContainerAccess FindContainerAccessByName(string containerName)
        {
            foreach (ContainerAccess item in this.ContainerAccessList)
            {
                if (item.ContainerName == containerName)
                {
                    return item;
                }
            }
            return null;
        }


        public void UpdateExistAccess(ContainerAccess containerExist, ContainerAccess containerAdd)
        {
            if (containerExist == null)
            {
                this.ContainerAccessList.Add(containerAdd);
                return;
            }
            if (this.FindContainerAccessByName(containerExist.ContainerName) == null)
            {
                this.ContainerAccessList.Add(containerAdd);
            }
            else
            {
                if (containerExist.ContainerName == containerAdd.ContainerName)
                {
                    List<ControlAccess> controlAccessList = new List<ControlAccess>();
                    ControlAccess accessTmp = null;
                    foreach (ControlAccess item in containerAdd.ControlAccessList)
                    {
                        accessTmp = containerExist.ControlAccessList.Find(p => { return p.AccessDesrciption == item.AccessDesrciption; });
                        //如果ControlAccess 已经存在
                        if (accessTmp != null)
                        {
                            //默认中有的权限项，accessTmp中可能不存在
                            List<AccessMap> maps = new List<AccessMap>();
                            
                            AccessMap mapTemp = null;
                            //遍历默认的权限AccessMap
                            foreach (AccessMap map in item.Controls)
                            {
                                mapTemp = accessTmp.Controls.Find(p => p.ControlName == map.ControlName);
                                //accessTmp 存在AccessMap
                                if (mapTemp != null)
                                {
                                    maps.Add(mapTemp);
                                }
                                else
                                {
                                    maps.Add(map);
                                }
                            }
                            accessTmp.Controls = maps;
                            controlAccessList.Add(accessTmp);
                        }
                        else//如果不存在
                        {
                            controlAccessList.Add(item);
                        }
                    }
                    containerExist.ControlAccessList = controlAccessList;
                }
            }
        }

        //public void SaveControlAccess()
        //{
        //    Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        //    foreach (Assembly assembly in assemblies)
        //    {
        //        Type[] types = assembly.GetTypes();
        //        foreach (Type type in types)
        //        {
        //            //Base相关的父类窗口不保存文本数据，直接保存子类窗口的文本
        //            if (type.Name.Contains("MainForm"))//主窗口反射实例化异常
        //            {
        //                continue;
        //            }
        //            if (type.Name.Contains("Base"))
        //            {
        //                continue;
        //            }
        //            //if (!(type.IsSubclassOf(typeof(FormEx))) && !(type.IsSubclassOf(typeof(UserControlEx))))
        //            //{
        //            //    continue;
        //            //}                   
        //            if (type.BaseType == typeof(MetroSetForm))
        //            {
        //                try
        //                {
        //                    //object mestroForm = Activator.CreateInstance(type, true);                           

        //                    using (object mestroForm = Activator.CreateInstance(type, true)  )
        //                    {
        //                        if (mestroForm is IAccessControllable)
        //                        {                          
        //                            mestroForm.SetDefaultAccess();
        //                            if (userControlEx.DefaultAccess.controlAccessList.Count > 0)
        //                            {
        //                                this.AddWindowAccess(userControlEx.DefaultAccess);
        //                            }
        //                        }


        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw ex;
        //                }

        //            }
        //            else if (type.BaseType==typeof(FormEx))
        //            {
        //                try
        //                {
        //                    using (FormEx formEx = Activator.CreateInstance(type, true) as FormEx)
        //                    {
        //                        if (formEx is IAccessControllable)
        //                        {                                    
        //                            formEx.initialDefaultAccess();
        //                            formEx.SetDefaultAccess();
        //                            if (formEx.DefaultAccess.controlAccessList.Count > 0)
        //                            {
        //                                this.AddWindowAccess(formEx.DefaultAccess);
        //                            }
        //                        }

        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                    throw;
        //                }
        //            }
        //        }
        //    }
        //    this.Save();
        //}

        public bool Save()
        {           
            try
            {
                FileStream fileStream = new FileStream(this.pathAccess, FileMode.Create);
                XmlSerializer xs = new XmlSerializer(this.ContainerAccessList.GetType());
                xs.Serialize(fileStream, this.ContainerAccessList);
                fileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                string msg = "处理失败，失败原因:\r\n" + ex.Message;

                if (ex.InnerException != null)
                {
                    msg += "\r\n具体原因：\r\n" + ex.InnerException.Message;
                }
                MessageBox.Show(msg);
                return false;
            }
        }

        public bool Load()
        {   
            try
            {
                FileStream fileStream = new FileStream(this.pathAccess, FileMode.Open, FileAccess.Read, FileShare.Read);
                XmlSerializer xs = new XmlSerializer(this.ContainerAccessList.GetType());
                this.ContainerAccessList = xs.Deserialize(fileStream) as List<ContainerAccess>;
                if (this.ContainerAccessList == null)
                {
                    this.ContainerAccessList = new List<ContainerAccess>();
                }
                fileStream.Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        

    }

}
