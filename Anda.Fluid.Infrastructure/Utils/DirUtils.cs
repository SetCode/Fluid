using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class DirUtils
    {
        public static bool CreateDir(string dir)
        {
            try
            {
                if(Directory.Exists(dir))
                {
                    return true;
                }
                Directory.CreateDirectory(dir);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
