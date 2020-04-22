using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class JsonUtil
    {
        public static bool Serialize<T>(string path, T t)
        {
            try
            {
                //DirUtils.CreateDir(Path.GetDirectoryName(path));
                string json = JsonConvert.SerializeObject(t, Formatting.Indented);
                System.IO.File.WriteAllText(path, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static T Deserialize<T>(string path)
        {
            try
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
