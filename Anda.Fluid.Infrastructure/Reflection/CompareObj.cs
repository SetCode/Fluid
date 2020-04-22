using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Reflection
{   
    public class CompareObj
    {
        private static string tagParent = string.Empty;
        public static ObjTree objtree;
        public static string parentName;

        public static bool CompareProperty(Object newObj, Object oldObj, string chilPrty, string parentPrty, bool hasCompareAttr = false, int level = 0)
        {
            //return true;
            try
            {
                if (newObj == null || oldObj==null)
                {
                    return false;
                }
                Type newType = newObj.GetType();
                Type oldType = oldObj.GetType();                
                PropertyInfo[] newPropties = newType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                PropertyInfo[] oldPropties = oldType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);               
                if (String.IsNullOrEmpty(chilPrty))
                {
                    parentName = parentPrty +"\\"+ newType.Name;
                    objtree = new ObjTree();
                    objtree.AddProperty(parentName, null,0);
                }
                if (newPropties.Length == 0 || oldPropties.Length == 0)
                {
                    return false;
                }
                #region //修改
                if (newPropties.Length == oldPropties.Length)
                {
                    for (int i = 0; i < newPropties.Length; i++)
                    {
                        PropertyInfo newPInfo = newPropties[i];
                        PropertyInfo oldPInfo = oldPropties[i];
                        if (newPInfo == null || oldPInfo == null)
                        {
                            continue;
                        }
                        if (hasCompareAttr)
                        {
                            CompareAtt pAtt = newPInfo.GetCustomAttribute(typeof(CompareAtt)) as CompareAtt;
                            if (pAtt == null)
                            {
                                continue;
                            }
                        }                                            
                        object valueNew = null;
                        string nameNew = string.Empty;
                        object valueOld = null;
                        string nameOld = string.Empty;                      
                        if (newPInfo.PropertyType.IsClass)
                        {                            
                            Type proType = newPInfo.PropertyType;
                            nameNew = newPInfo.Name;
                            nameOld = oldPInfo.Name;
                            if (typeof(string).Equals(proType))
                            {         
                                valueNew = newPInfo.GetValue(newObj);
                                
                                valueOld = oldPInfo.GetValue(oldObj);                               
                                if (valueNew == null || valueOld == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (!valueNew.Equals(valueOld))
                                    {                                      
                                        string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew, valueOld, valueNew);
                                        Logger.DEFAULT.Info(LogCategory.SETTING,newType.Name,strOutPut);
                                        oldPInfo.SetValue(oldObj, valueNew);
                                    }
                                }
                               
                            }
                            else 
                            {                                
                                object ObjNewInner = newPInfo.GetValue(newObj); 
                                object ObjOldInner = oldPInfo.GetValue(oldObj);
                                if (ObjNewInner == null || ObjOldInner == null)
                                {
                                    continue;
                                }
                                if (level == 0)
                                {
                                    objtree.AddProperty(nameNew, parentName, level + 1);
                                    CompareProperty(ObjNewInner, ObjOldInner,nameNew, parentName, hasCompareAttr,  level + 1);
                                }
                                else
                                {
                                    objtree.AddProperty(nameNew, parentPrty, level + 1);
                                    CompareProperty(ObjNewInner, ObjOldInner, nameNew,parentPrty, hasCompareAttr, level + 1);
                                }
                                
                            }
                        }
                        else if (newPInfo.PropertyType.IsValueType || newPInfo.PropertyType.IsEnum)
                        {
                            nameNew = newPInfo.Name;
                            valueNew = newPInfo.GetValue(newObj);
                            nameOld = oldPInfo.Name;
                            valueOld = oldPInfo.GetValue(oldObj);
                           
                            if (valueNew == null || valueOld == null)
                            {
                               continue;
                            }
                            else
                            {
                                if (!valueNew.Equals(valueOld))
                                {                                   
                                    string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew,valueOld, valueNew);
                                    if (!string.IsNullOrEmpty(chilPrty))
                                    {
                                        Logger.DEFAULT.Info(LogCategory.SETTING, objtree.NodePath(chilPrty), strOutPut);
                                    }
                                    else
                                    {
                                        Logger.DEFAULT.Info(LogCategory.SETTING, objtree.NodePath(objtree.Root.PropertyName), strOutPut);
                                    }                                    
                                    oldPInfo.SetValue(oldObj, valueNew);

                                }
                            }

                        }


                    }
                }
                #endregion
               
                return true;


            }
            catch(Exception ex)
            {
                //throw ex;                
                return false;
            }
        }

        public static bool CompareField(Object newObj, Object oldObj, string chilPrty, string parentPrty, bool hasCompareAttr = false, int level = 0)
        {
            //return true;
            try
            {
                if (newObj == null || oldObj == null)
                {
                    return false;
                }
                Type newType = newObj.GetType();
                Type oldType = oldObj.GetType();
                FieldInfo[] newFields = newType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo[] oldFields = oldType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);    
                           
                if (String.IsNullOrEmpty(chilPrty))
                {
                    parentName = parentPrty +"\\"+newType.Name;
                    objtree = new ObjTree();
                    objtree.AddProperty(parentName, null, 0);
                }
                if (newFields.Length==0 || oldFields.Length==0)
                {
                    return false;
                }

                #region //修改
                if (newFields.Length == oldFields.Length)
                {

                    for (int i = 0; i < newFields.Length; i++)
                    {
                        FieldInfo newFInfo = newFields[i];
                        FieldInfo oldFInfo = oldFields[i];
                        if (newFInfo == null || oldFInfo==null)
                        {
                            continue;
                        }
                        if (hasCompareAttr)
                        {
                            CompareAtt pAtt = newFInfo.GetCustomAttribute(typeof(CompareAtt)) as CompareAtt;
                            if (pAtt == null)
                            {
                                continue;
                            }
                        }
                        object valueNew = null;
                        string nameNew = string.Empty;
                        object valueOld = null;
                        string nameOld = string.Empty;
                        if (newFInfo.FieldType.IsClass)
                        {
                            Type FldType = newFInfo.FieldType;
                            nameNew = newFInfo.Name;
                            nameOld = oldFInfo.Name;
                            if (typeof(string).Equals(FldType))
                            {
                                valueNew = newFInfo.GetValue(newObj);
                                valueOld = oldFInfo.GetValue(oldObj);
                                if (valueNew == null || valueOld == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (!valueNew.Equals(valueOld))
                                    {
                                        string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew, valueOld, valueNew);
                                        Logger.DEFAULT.Info(LogCategory.SETTING, newType.Name, strOutPut);
                                        oldFInfo.SetValue(oldObj, valueNew);
                                    }
                                }

                            }
                            else
                            {
                                object ObjNewInner = newFInfo.GetValue(newObj);
                                object ObjOldInner = oldFInfo.GetValue(oldObj);
                                if (ObjNewInner == null || ObjOldInner==null)
                                {
                                    continue;
                                }
                                if (level == 0)
                                {
                                    objtree.AddProperty(nameNew, parentName, level + 1);
                                    CompareField(ObjNewInner, ObjOldInner, nameNew, parentName, hasCompareAttr, level + 1);
                                }
                                else
                                {
                                    objtree.AddProperty(nameNew, parentPrty, level + 1);
                                    CompareField(ObjNewInner, ObjOldInner, nameNew, parentPrty, hasCompareAttr, level + 1);
                                }

                            }
                        }
                        else if (newFInfo.FieldType.IsValueType || newFInfo.FieldType.IsEnum)
                        {
                            nameNew = newFInfo.Name;
                            valueNew = newFInfo.GetValue(newObj);
                            nameOld = oldFInfo.Name;
                            valueOld = oldFInfo.GetValue(oldObj);
                            if (valueNew == null || valueOld == null)
                            {
                                continue;
                            }
                            else
                            {
                                if (!valueNew.Equals(valueOld))
                                {
                                    string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew, valueOld, valueNew);
                                    if (!string.IsNullOrEmpty(chilPrty))
                                    {
                                        Logger.DEFAULT.Info(LogCategory.SETTING, objtree.NodePath(chilPrty), strOutPut);
                                    }
                                    else
                                    {
                                        Logger.DEFAULT.Info(LogCategory.SETTING, objtree.NodePath(objtree.Root.PropertyName), strOutPut);
                                    }
                                    oldFInfo.SetValue(oldObj, valueNew);

                                }
                            }

                        }


                    }
                }
                #endregion

                return true;


            }
            catch (Exception ex)
            {
                //throw ex;                
                return false;
            }
        }

        public static bool CompareMember(Object newObj, Object oldObj, string chilPrty, string parentPrty, bool hasCompareAttr = false, int level = 0)
        {
            //return true;
            try
            {
                if (newObj == null || oldObj == null)
                {
                    return false;
                }
                Type newType = newObj.GetType();
                Type oldType = oldObj.GetType();
                MemberInfo[] newMembers=newType.FindMembers(MemberTypes.Property|MemberTypes.Field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null,null);           
                MemberInfo[] oldMembers = newType.FindMembers(MemberTypes.Property | MemberTypes.Field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, null);
                if (String.IsNullOrEmpty(chilPrty))
                {
                    parentName = parentPrty + "\\" + newType.Name;
                    objtree = new ObjTree();
                    objtree.AddProperty(parentName, null, 0);
                }
                if (newMembers.Length == 0 || oldMembers.Length == 0)
                {
                    return false;
                }
                #region //修改
                if (newMembers.Length == oldMembers.Length)
                {

                    for (int i = 0; i < newMembers.Length; i++)
                    {

                        MemberInfo newMInfo = newMembers[i];                       
                        MemberInfo oldMInfo = oldMembers[i];
                        if (newMInfo == null || oldMInfo == null)
                        {
                            continue;
                        }
                        if (hasCompareAttr)
                        {
                            CompareAtt pAtt = newMInfo.GetCustomAttribute(typeof(CompareAtt)) as CompareAtt;
                            if (pAtt == null)
                            {
                                continue;
                            }
                        }
                        object valueNew = null;
                        string nameNew = string.Empty;
                        object valueOld = null;
                        string nameOld = string.Empty;
                        MemberTypes mType = newMInfo.MemberType;
                        if (mType == MemberTypes.Property)
                        {
                            PropertyInfo newPInfo = newMInfo as PropertyInfo;
                            PropertyInfo oldPInfo = oldMInfo as PropertyInfo;
                            if (newPInfo.PropertyType.IsClass)
                            {
                                Type proType = newPInfo.PropertyType;
                                nameNew = newPInfo.Name;
                                nameOld = oldPInfo.Name;
                                if (typeof(string).Equals(proType))
                                {
                                    valueNew = newPInfo.GetValue(newObj);
                                    valueOld = oldPInfo.GetValue(oldObj);
                                    if (valueNew == null || valueOld == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (!valueNew.Equals(valueOld))
                                        {
                                            string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew, valueOld, valueNew);
                                            Logger.DEFAULT.Info(LogCategory.SETTING, newType.Name, strOutPut);
                                            oldPInfo.SetValue(oldObj, valueNew);
                                        }
                                    }

                                }
                                else
                                {
                                    object ObjNewInner = newPInfo.GetValue(newObj);
                                    object ObjOldInner = oldPInfo.GetValue(oldObj);
                                    if (ObjNewInner==null || ObjOldInner==null)
                                    {
                                        continue;
                                    }
                                    if (level == 0)
                                    {
                                        objtree.AddProperty(nameNew, parentName, level + 1);
                                        CompareMember(ObjNewInner, ObjOldInner, nameNew, parentName, hasCompareAttr, level + 1);
                                    }
                                    else
                                    {
                                        objtree.AddProperty(nameNew, parentPrty, level + 1);
                                        CompareMember(ObjNewInner, ObjOldInner, nameNew, parentPrty, hasCompareAttr, level + 1);
                                    }

                                }
                            }
                            else if (newPInfo.PropertyType.IsValueType || newPInfo.PropertyType.IsEnum)
                            {
                                nameNew = newPInfo.Name;
                                valueNew = newPInfo.GetValue(newObj);
                                nameOld = oldPInfo.Name;
                                valueOld = oldPInfo.GetValue(oldObj);
                                if (valueNew == null || valueOld == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (!valueNew.Equals(valueOld))
                                    {
                                        string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew, valueOld, valueNew);
                                        if (!string.IsNullOrEmpty(chilPrty))
                                        {
                                            Logger.DEFAULT.Info(LogCategory.SETTING, objtree.NodePath(chilPrty), strOutPut);
                                        }
                                        else
                                        {
                                            Logger.DEFAULT.Info(LogCategory.SETTING, objtree.NodePath(objtree.Root.PropertyName), strOutPut);
                                        }
                                        oldPInfo.SetValue(oldObj, valueNew);

                                    }
                                }

                            }




                        }
                        else if(mType == MemberTypes.Field)
                        {
                            FieldInfo newFInfo = newMInfo as FieldInfo;
                            FieldInfo oldFInfo = oldMInfo as FieldInfo;
                            if (newFInfo.FieldType.IsClass)
                            {

                                Type FldType = newFInfo.FieldType;
                                nameNew = newFInfo.Name;
                                nameOld = oldFInfo.Name;
                                if (typeof(string).Equals(FldType))
                                {
                                    valueNew = newFInfo.GetValue(newObj);
                                    valueOld = oldFInfo.GetValue(oldObj);
                                    if (valueNew == null || valueOld == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (!valueNew.Equals(valueOld))
                                        {
                                            string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew, valueOld, valueNew);
                                            Logger.DEFAULT.Info(LogCategory.SETTING, newType.Name, strOutPut);
                                            oldFInfo.SetValue(oldObj, valueNew);
                                        }
                                    }

                                }
                                else
                                {
                                    object ObjNewInner = newFInfo.GetValue(newObj);
                                    object ObjOldInner = oldFInfo.GetValue(oldObj);
                                    if (ObjNewInner == null || ObjOldInner==null)
                                    {
                                        continue;
                                    }
                                    if (level == 0)
                                    {
                                        objtree.AddProperty(nameNew, parentName, level + 1);
                                        CompareMember(ObjNewInner, ObjOldInner, nameNew, parentName, hasCompareAttr, level + 1);
                                    }
                                    else
                                    {
                                        objtree.AddProperty(nameNew, parentPrty, level + 1);
                                        CompareMember(ObjNewInner, ObjOldInner, nameNew, parentPrty, hasCompareAttr, level + 1);
                                    }

                                }
                            }
                            else if (newFInfo.FieldType.IsValueType || newFInfo.FieldType.IsEnum)
                            {
                                nameNew = newFInfo.Name;
                                valueNew = newFInfo.GetValue(newObj);
                                nameOld = oldFInfo.Name;
                                valueOld = oldFInfo.GetValue(oldObj);
                                if (valueNew == null || valueOld == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (!valueNew.Equals(valueOld))
                                    {
                                        string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew, valueOld, valueNew);
                                        if (!string.IsNullOrEmpty(chilPrty))
                                        {
                                            Logger.DEFAULT.Info(LogCategory.SETTING, objtree.NodePath(chilPrty), strOutPut);
                                        }
                                        else
                                        {
                                            Logger.DEFAULT.Info(LogCategory.SETTING, objtree.NodePath(objtree.Root.PropertyName), strOutPut);
                                        }
                                        oldFInfo.SetValue(oldObj, valueNew);

                                    }
                                }

                            }
                        }
                        

                    }
                }
                #endregion

                return true;


            }
            catch (Exception ex)
            {
                //throw ex;                
                return false;
            }
        }


        public static bool CompareProperty(Object newObj, Object oldObj, bool hasCompareAttr = false)
        {
            //return true;
            try
            {
                if (newObj == null || oldObj == null)
                {
                    return false;
                }

                Type newType = newObj.GetType();
                Type oldType = oldObj.GetType();
                PropertyInfo[] newPropties = newType.GetProperties();
                PropertyInfo[] oldPropties = oldType.GetProperties();              
                #region //修改
                if (newPropties.Length == oldPropties.Length)
                {
                    for (int i = 0; i < newPropties.Length; i++)
                    {
                        PropertyInfo newPInfo = newPropties[i];
                        PropertyInfo oldPInfo = oldPropties[i];
                        if (hasCompareAttr)
                        {
                            CompareAtt pAtt = newPInfo.GetCustomAttribute(typeof(CompareAtt)) as CompareAtt;
                            if (pAtt == null)
                            {
                                continue;
                            }
                        }
                        object valueNew = null;
                        string nameNew = string.Empty;
                        object valueOld = null;
                        string nameOld = string.Empty;
                        if (newPInfo.PropertyType.IsClass)
                        {
                            Type proType = newPInfo.PropertyType;
                            nameNew = newPInfo.Name;
                            nameOld = oldPInfo.Name;
                            if (typeof(string).Equals(proType))
                            {
                                valueNew = newPInfo.GetValue(newObj);
                                valueOld = oldPInfo.GetValue(oldObj);
                                if (valueNew == null || valueOld == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (!valueNew.Equals(valueOld))
                                    {
                                        string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew, valueOld, valueNew);
                                        Logger.DEFAULT.Info(LogCategory.SETTING, newType.Name, strOutPut);
                                        oldPInfo.SetValue(oldObj, valueNew);
                                    }
                                }

                            }
                            else
                            {
                                object ObjNewInner = newPInfo.GetValue(newObj);
                                object ObjOldInner = oldPInfo.GetValue(oldObj);
                                CompareProperty(ObjNewInner, ObjOldInner, hasCompareAttr);
                             

                            }
                        }
                        else if (newPInfo.PropertyType.IsValueType || newPInfo.PropertyType.IsEnum)
                        {
                            nameNew = newPInfo.Name;
                            valueNew = newPInfo.GetValue(newObj);
                            nameOld = oldPInfo.Name;
                            valueOld = oldPInfo.GetValue(oldObj);

                            if (valueNew == null || valueOld == null)
                            {
                                continue;
                            }
                            else
                            {
                                if (!valueNew.Equals(valueOld))
                                {
                                    string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", nameNew, valueOld, valueNew);
                                    Logger.DEFAULT.Info(LogCategory.SETTING, newType.Name, strOutPut);
                                    oldPInfo.SetValue(oldObj, valueNew);

                                }
                            }

                        }


                    }
                }
                #endregion

                return true;


            }
            catch (Exception ex)
            {
                //throw ex;                
                return false;
            }
        }

        
        public static void CompareValue<T>(T newValue,  T oldValue)
        {
            try
            {
                if (!newValue.Equals(oldValue))
                {
                    string strOutPut = string.Format("{0}: oldValue: {1} ->  newValue: {2}", typeof(T).Name, oldValue, newValue);
                    Logger.DEFAULT.Info(LogCategory.SETTING, strOutPut);
                  
                }                    

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class ObjTree
    {
        private ObjNode root;
        /// <summary>
        /// 根节点
        /// </summary>
        public ObjNode Root { get { return root; } }
        // 所有节点
        private List<ObjNode> allNodesList = new List<ObjNode>();
        // RunnableModule与对应的节点
        public Dictionary<string, ObjNode> Map { get; private set; } = new Dictionary<string, ObjNode>();

        /// <summary>
        /// 添加 RunnableModule，按照先访问根节点，再访问子节点顺序遍历添加
        /// </summary>
        public void AddProperty(string propertyName, string parentPropertyName, int level)
        {
            //Log.Print("AddModule Begine origin:" + module.Origin+ module.CommandsModule.Name);
            if (propertyName == null)
            {
                throw new Exception("module can not be null.");
            }
            if (root == null)
            {
                if (!String.IsNullOrEmpty(parentPropertyName))
                {
                    throw new Exception("Please add root node firstly.");
                }
                //Log.Print("Add root module");
                root = new ObjNode(null, propertyName, level);
                allNodesList.Add(root);
                Map.Add(propertyName, root);
                return;
            }
            // 子节点的parentModule不能为null
            if (String.IsNullOrEmpty(parentPropertyName))
            {
                throw new Exception("Parent of property  is null.");
            }
            ObjNode parentNode = GetNode(parentPropertyName);
            if (parentNode == null)
            {
                throw new Exception("Parent runnable module [" + parentNode.PropertyName + "] is not found in runnable module tree.");
            }
            ObjNode node = new ObjNode(parentNode, propertyName, level);
            parentNode.Children.Add(node);
            allNodesList.Add(node);
            Map.Add(propertyName, node);
            //Log.Print("AddModule Done:" + module.CommandsModule.Name);

        }

        public ObjNode GetNode(string propertyName)
        {
            if (!Map.ContainsKey(propertyName))
            {
                return null;
            }
            return Map[propertyName];
        }

        public string NodePath(string propertyName)
        {
            string path = string.Empty;
            ObjNode node =GetNode(propertyName);
            path = node.PropertyName+"\\";
            for (int i = 0; i < node.Level; i++)
            {
                if (node.Parent != null)
                {
                    node = node.Parent;
                    path = node.PropertyName +"\\"+ path;
                }
            }
            return path;
        }

        /// <summary>
        /// 清空pattern树
        /// </summary>
        public void Clear()
        {
            Log.Print("module tree clear.");
            root = null;
            allNodesList.Clear();
            Map.Clear();
        }

        /// <summary>
        /// pattern树是否为空
        /// </summary>
        public bool IsEmpty
        {
            get { return root == null; }
        }

    }

    public class ObjNode
    {
        private ObjNode parent;
        /// <summary>
        /// 父节点
        /// </summary>
        public ObjNode Parent { get { return parent; } }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<ObjNode> Children = new List<ObjNode>();

        public string PropertyName = string.Empty;
        private int level;
        public int Level { get { return level; } }

        public ObjNode(ObjNode parent, string propertyName, int level)
        {
            this.parent = parent;
            this.PropertyName = propertyName;
            this.level = level;
        }

    }
     
}
