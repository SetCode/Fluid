using Anda.Fluid.App.Test;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System;
using System.Windows.Forms;

namespace Anda.Fluid.App
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.init();
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BarCode());
            Log.Dispose();
        }
    }
}
