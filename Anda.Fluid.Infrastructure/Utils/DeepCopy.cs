using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class DeepCopy
    {
        public static T CopyByJson<T>(T obj)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
            
        }
        

    }
}
