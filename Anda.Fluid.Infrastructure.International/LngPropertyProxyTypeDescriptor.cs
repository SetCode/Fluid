using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.International
{

    ///<summary>
    /// Description	:propertyGrid显示对象的包装类
    /// Author  	:liyi
    /// Date		:2019/06/10
    ///</summary>   
    public class LngPropertyProxyTypeDescriptor : ICustomTypeDescriptor
    {
        private object _target;
        private string formName;

        public LngPropertyProxyTypeDescriptor(object target,string formName)
        {
            if (target == null) throw new ArgumentNullException("target");
            _target = target;
            this.formName = formName;
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return _target;
        }
        #region 接口实现

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(_target, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(_target, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(_target, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(_target, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(_target, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(_target, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(_target, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(_target, attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(_target, true);
        }

        private PropertyDescriptorCollection _propCache;
        private FilterCache _filterCache;

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(null);
        }
        #endregion

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            bool filtering = (attributes != null && attributes.Length > 0);
            PropertyDescriptorCollection props = _propCache;
            FilterCache cache = _filterCache;

            // Use a cached version if we can
            if (filtering && cache != null && cache.IsValid(attributes))
            {
                return cache.FilteredProperties;
            }
            else if (!filtering && props != null)
            {
                return props;
            }

            // Create the property collection and filter if necessary
            props = new PropertyDescriptorCollection(null);
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(_target, attributes, true))
            {
                string displayNameLng = LanguageHelper.Instance.ReadKeyValueFromResources(formName, prop.Name);
                string categoryLng = LanguageHelper.Instance.ReadKeyValueFromResources(formName, prop.Category);

                props.Add(new LngPropertyDescriptor(prop, displayNameLng,"",categoryLng));
            }

            // Store the computed properties
            if (filtering)
            {
                cache = new FilterCache();
                cache.Attributes = attributes;
                cache.FilteredProperties = props;
                _filterCache = cache;
            }
            else _propCache = props;

            return props;
        }

        private class FilterCache
        {
            public Attribute[] Attributes;
            public PropertyDescriptorCollection FilteredProperties;
            public bool IsValid(Attribute[] other)
            {
                if (other == null || Attributes == null) return false;
                if (Attributes.Length != other.Length) return false;
                for (int i = 0; i < other.Length; i++)
                {
                    if (!Attributes[i].Match(other[i])) return false;
                }
                return true;
            }
        }
    }
}
