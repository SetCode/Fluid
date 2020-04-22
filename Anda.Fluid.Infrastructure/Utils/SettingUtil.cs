using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class SettingUtil
    {
        public static T ResetToDefault<T>(T t)
        {
            Type type = t.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                var instances = property.GetCustomAttributes(typeof(DefaultValueAttribute), true).OfType<DefaultValueAttribute>();
                foreach (var instance in instances)
                {
                    DefaultValueAttribute dva = instance as DefaultValueAttribute;
                    property.SetValue(t, dva.Value);
                }
            }
            return t;
        }
    }
}
