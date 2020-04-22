using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.PropertyGridExtension
{

    public class PropertySorter:ExpandableObjectConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(value, attributes);
            ArrayList orderedProperties = new ArrayList();
            foreach (PropertyDescriptor pd in pdc)
            {
                Attribute attribute = pd.Attributes[typeof(PropertyOrderAttribute)];
                if (attribute != null)
                {
                    PropertyOrderAttribute poa = (PropertyOrderAttribute)attribute;
                    orderedProperties.Add(new PropertyOrderPair(pd.Name, poa.Order));
                }
                else
                {
                    orderedProperties.Add(new PropertyOrderPair(pd.Name, 0));
                }
            }
            orderedProperties.Sort();
            ArrayList propertyNames = new ArrayList();
            foreach (PropertyOrderPair pop in orderedProperties)
            {
                propertyNames.Add(pop.Name);
            }

            return pdc.Sort((string[])propertyNames.ToArray(typeof(string)));
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
     public class PropertyOrderAttribute : Attribute
      {
          //
          // Simple attribute to allow the order of a property to be specified
          //
          private int order;
          public PropertyOrderAttribute(int order)
          {
              this.order = order;
          }
  
          public int Order
          {
              get
              {
                  return this.order;
              }
          }
      }

    public class PropertyOrderPair : IComparable
    {
        private int _order;
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public PropertyOrderPair(string name, int order)
        {
            _order = order;
            _name = name;
        }

        public int CompareTo(object obj)
        {
            //
            // 结对对象通过排序顺序的值
            // 平等的价值观得到相同的等级
            //
            int otherOrder = ((PropertyOrderPair)obj)._order;
            if (otherOrder == _order)
            {
                //
                // 如果未指定顺序,按名称排序
                //
                string otherName = ((PropertyOrderPair)obj)._name;
                return string.Compare(_name, otherName);
            }
            else if (otherOrder > _order)
            {
                return -1;
            }
            return 1;
        }

    }
}
