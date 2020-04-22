using Anda.Fluid.Domain.Motion;
using Anda.Fluid.Domain.Settings;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.GenKey;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.Main
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //判断加密
            //if (!GenKeySN.CheckDate())
            //{
            //    MessageBox.Show("AFM软件未注册！");
            //    return;
            //}
            Log.init();
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            Log.Dispose();
        }
    }
}
