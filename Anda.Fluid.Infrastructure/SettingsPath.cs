using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure
{
    public class SettingsPath
    {
        private static string PathRoot = Application.StartupPath;

        public static string PathSettings = PathRoot + "\\Settings";

        public static string PathMachine = PathSettings + "\\Machine";

        public static string PathBusiness = PathSettings + "\\Business";

        public static string PathGts= PathRoot + "\\GtsConfig";
    }
}
