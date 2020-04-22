using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International.Access;
using static Anda.Fluid.Infrastructure.International.AccessEnums;
using System.Xml.Serialization;

namespace Anda.Fluid.Infrastructure.International
{
    [Serializable]
    public class AccessMap
    {

        public string ControlName;
        public RoleEnums RoleLevel = RoleEnums.Operator;
        public AccessMap()
        { }
        public AccessMap(string controlName, RoleEnums roleLevel)
        {
            this.ControlName = controlName;
            this.RoleLevel = roleLevel;
        }
                 

    }
    /// <summary>
    /// 对应最基本控制，例如button
    /// </summary>
    [Serializable]
    public class ControlAccess: ICloneable
    {
        
        public string ContainerName;
        //唯一
        public string AccessDesrciption;

        public List<AccessMap> Controls = new List<AccessMap>();
        public ControlAccess() { }


        public ControlAccess(string name,string accessDescription)
        {
            this.ContainerName = name;
            this.AccessDesrciption = accessDescription;
        }
        public ControlAccess(string name, string accessDescription, string controlName, RoleEnums roleLevel):this(name, accessDescription)
        {
            this.Add(new List<string> { controlName }, roleLevel);
        }
        [XmlIgnore]
        public RoleEnums RoleLevel
        {
            get
            {
                RoleEnums roleLevel = RoleEnums.Operator;
                if (this.Controls==null || this.Controls.Count<=0)
                {
                    roleLevel = RoleEnums.Operator;
                }
                roleLevel=Controls[0].RoleLevel;
                return roleLevel;
             }
            set
            {
                RoleEnums roleLevel = value;
                if (this.Controls == null || this.Controls.Count <= 0)
                {
                    roleLevel = RoleEnums.Operator;
                }
                foreach (var item in this.Controls)
                {
                    item.RoleLevel = roleLevel;
                }
            }
        }

        public ControlAccess Add(List<string> names, RoleEnums roleLevel)
        {
            if (names==null)
            {
                return this;
            }
            foreach (var item in names)
            {
                AccessMap access = this.Controls.Find(p => p.ControlName == item);
                if (access==null)
                {
                    this.Controls.Add(new AccessMap() { ControlName=item,RoleLevel=roleLevel});
                }
            }
            return this;
        }

        public void AddAccessOperator(List<string> names)
        {
            foreach (string item in names)
            {
                AccessMap access = this.Controls.Find(p => p.ControlName == item);
                if (access == null)
                {
                    this.Controls.Add(new AccessMap(item, RoleEnums.Operator));
                }
            }
        }

        public void AddAccessTechnician(List<string> names)
        {
            foreach (string item in names)
            {
                AccessMap access = this.Controls.Find(p => p.ControlName == item);
                if (access == null)
                {
                    this.Controls.Add(new AccessMap(item, RoleEnums.Technician));
                }

            }
        }

        public void AddAccessSupervisor(List<string> names)
        {
            foreach (string item in names)
            {
                AccessMap access = this.Controls.Find(p => p.ControlName == item);
                if (access == null)
                {
                    this.Controls.Add(new AccessMap(item, RoleEnums.Supervisor));
                }

            }
        }
        
        public bool Contains(string controlName)
        {
            foreach (var item in this.Controls)
            {
                if (item.ControlName==controlName)
                {
                    return true;
                } 
            }
            return false;
        }
        public bool IsContanerAccess()
        {
            return this.Contains(this.ContainerName);
        }
        
        public object Clone()
        {
            return MemberwiseClone() as ControlAccess;
        }



    }
    /// <summary>
    /// 对应窗体和用户控件
    /// </summary>
    [Serializable]
    public class ContainerAccess
    {
        public string ContainerName;
        public string ContainerAccessDescription;
        public List<ControlAccess> ControlAccessList = new List<ControlAccess>();
        public ContainerAccess() { }

        public ContainerAccess(string name, string description)
        {
            this.ContainerName = name;
            this.ContainerAccessDescription = description;
            
        }
        public void AddContainerOperator()
        {
            if(this.FindControlAccessByControlName(this.ContainerName)!=null)
            {
                return;
            }
            ControlAccess controlAccess = new ControlAccess(this.ContainerName, this.ContainerAccessDescription, this.ContainerName, RoleEnums.Operator);
            this.AddControlAccess(controlAccess);

        }
        public void AddContainerTechnician()
        {
            if (this.FindControlAccessByControlName(this.ContainerName) != null)
            {
                return;
            }
            ControlAccess controlAccess = new ControlAccess(this.ContainerName, this.ContainerAccessDescription, this.ContainerName, RoleEnums.Technician);
            this.AddControlAccess(controlAccess);
        }
        public List<AccessMap> GetAllAccessMaps()
        {
            List<AccessMap> maps = new List<AccessMap>();
            foreach (ControlAccess item in ControlAccessList)
            {
                maps.AddRange(item.Controls);
            }
            return maps;
        }
        public void AddControlAccess(ControlAccess controlAccess)
        {            
            this.ControlAccessList.Add(controlAccess);
        }
        public ControlAccess GetContainerAccess()
        {
            ControlAccess access = this.FindControlAccessByControlName(this.ContainerName);
            return access;
        }
        /// <summary>
        /// 通过控件Name找到对应的ControlAccess
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public ControlAccess FindControlAccessByControlName(string controlName)
        {
            foreach (ControlAccess item in ControlAccessList)
            {
                if (item.Contains(controlName))
                {
                    return item;
                }
            }
            return null;
        }
        
    }

    #region AccessTree
    //[Serializable]
    //public class AccessNode:ICloneable
    //{        

    //    public AccessNode Parent;

    //    public List<AccessNode> Children = new List<AccessNode>();       

    //    public ControlAccess ControlAccess;      

    //    public  int level;       
    //    public AccessNode() { }
    //    public AccessNode(AccessNode parent, ControlAccess controlAccess)
    //    {
    //        this.Parent = parent;
    //        this.ControlAccess = controlAccess;
    //        if (parent == null)
    //        {
    //            this.level = 0;
    //        }
    //        else
    //        {
    //            this.level = parent.level + 1;
    //        }
    //    }
    //    public void Add(AccessNode node)
    //    {
    //        if (this.Children.Contains(node))
    //        {
    //            return;
    //        }
    //        this.Children.Add(node);
    //    }
    //    public object Clone()
    //    {
    //        return MemberwiseClone() as AccessNode;
    //    }     
    //}
    //[Serializable]
    //public class AccessTree
    //{        
    //    //root是0层
    //    public AccessNode root;  

    //    public List<AccessNode> controlAccessNodeList = new List<AccessNode>();


    //    public AccessTree()
    //    {
    //    }

    //    //public void Add(ControlAccess parentAccess, ControlAccess childAccess)
    //    //{
    //    //    if (childAccess == null)
    //    //    {
    //    //        throw new Exception("权限节点不可以为空");
    //    //    }
    //    //    if (this.root == null)
    //    //    {
    //    //        root = new AccessNode(null, childAccess);
    //    //        this.controlAccessNodeList.Add(root);

    //    //        return;
    //    //    }
    //    //    if (parentAccess == null)
    //    //    {
    //    //        throw new Exception("子节点的父节点不可以空");
    //    //    }
    //    //    AccessNode parentNode = this.GetNode(parentAccess);
    //    //    if (parentNode==null)
    //    //    {
    //    //        throw new Exception("当前树中没有");
    //    //    }
    //    //    AccessNode childNode =new AccessNode(parentNode, childAccess);
    //    //    parentNode.Add(childNode);
    //    //    this.controlAccessNodeList.Add(childNode);

    //    //}

    //    public void Add(string parentNodeAccessName, ControlAccess childAccess)
    //    {
    //        if (childAccess == null)
    //        {
    //            throw new Exception("权限节点不可以为空");
    //        }
    //        if (this.root == null)
    //        {
    //            root = new AccessNode(null, childAccess);
    //            this.controlAccessNodeList.Add(root);
    //            return;
    //        }
    //        if (parentNodeAccessName == null)
    //        {
    //            throw new Exception("子节点的父节点不可以空");
    //        }
    //        AccessNode parentNode = this.GetNode(parentNodeAccessName);
    //        if (parentNode == null)
    //        {
    //            throw new Exception("当前树中没有");
    //        }
    //        AccessNode childNode = new AccessNode(parentNode, childAccess);
    //        parentNode.Add(childNode);
    //        this.controlAccessNodeList.Add(childNode);

    //    }

    //    private AccessNode GetNode(ControlAccess access)
    //    {
    //        //if (!this.ControlAccessMap.ContainsKey(access))
    //        //{
    //        //    return null;
    //        //}
    //        //return this.ControlAccessMap[access];
    //        return  this.controlAccessNodeList.Find(p => access.AccessName == p.ControlAccess.AccessName);
    //    }

    //    private AccessNode GetNode(string parentNodeAccessName)
    //    {
    //        //if (!this.ControlAccessMap.ContainsKey(access))
    //        //{
    //        //    return null;
    //        //}
    //        //return this.ControlAccessMap[access];
    //        return this.controlAccessNodeList.Find(p => p.ControlAccess.AccessName == parentNodeAccessName);
    //    }
    //    public void Remove()
    //    {

    //    }
    //    [XmlIgnoreAttribute]
    //    public bool IsEmpty
    //    {
    //        get { return this.root == null; }           
    //    }


    //}
    #endregion 

}
