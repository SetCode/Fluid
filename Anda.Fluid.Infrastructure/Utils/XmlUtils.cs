using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class XmlUtils
    {
        public static bool Serialize<T>(string path, T t)
            where T : class
        {
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(fileStream, t);
                fileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static T Derialize<T>(string path)
            where T : class
        {
            T t = default(T);
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                XmlSerializer xs = new XmlSerializer(typeof(T));
                t = xs.Deserialize(fileStream) as T;
                fileStream.Close();
                return t;

            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

    }
}
