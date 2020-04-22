using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.International
{
    /// <summary>
    /// Author:liyi
    /// Date:2019/06/10
    /// propertyGrid显示对象的属性的包装类
    /// </summary>
    public class LngPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor prop;

        public LngPropertyDescriptor(PropertyDescriptor prop) : base(prop)
        {
            this.prop = prop;
        }
        
        public LngPropertyDescriptor(PropertyDescriptor prop, string displayName,string description,string category) :base(prop)
        {
            this.prop = prop;
            this.customCateGory = category;
            this.customDisplayName = displayName;
            this.customDescription = description;
        }
        #region 重写界面显示属性
        private string customCateGory = "";

        public string CustomCateGory
        {
            get { return customCateGory = ""; }
            set { customCateGory = value; }
        }

        private string customDisplayName = "";

        public string CustomDisplayName
        {
            get { return customDisplayName = ""; }
            set { customDisplayName = value; }
        }

        private string customDescription = "";

        public string CustomDescription
        {
            get { return customDescription = ""; }
            set { customDescription = value; }
        }


        public override string Category
        {
            get
            {
                if (customCateGory == "")
                {
                    return base.Category;
                }
                else
                {
                    return customCateGory;
                }
            }
        }

        public override string DisplayName
        {
            get
            {
                if (customDisplayName == "")
                {
                    return base.DisplayName;
                }
                else
                {
                    return customDisplayName;
                }
            }
        }
        #endregion

        public override string Description
        {
            get
            {
                if (customDescription == "")
                {
                    return base.Description;
                }
                else
                {
                    return customDescription;
                }
            }
        }
        #region 抽象方法实现
        public override bool Equals(object obj)
        {
            LngPropertyDescriptor other = obj as LngPropertyDescriptor;
            return other != null && other.prop.Equals(prop);
        }

        public override int GetHashCode() { return prop.GetHashCode(); }

        public override bool IsReadOnly { get { return prop.IsReadOnly; } }
        public override void ResetValue(object component) { }
        public override bool CanResetValue(object component) { return false; }
        public override bool ShouldSerializeValue(object component) { return true; }

        public override object GetValue(object component) { return prop.GetValue(component); }

        public override void SetValue(object component, object value)
        {
            prop.SetValue(component, value);
            OnValueChanged(component, EventArgs.Empty);
        }
        public override Type ComponentType
        {
            get
            {
                return prop.ComponentType;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return prop.PropertyType;
            }
        }
        #endregion
    }
}
