using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.SVO
{
    public class SvoSetting
    {
        private static readonly SvoSetting instance = new SvoSetting();
        private SvoSetting()
        {
            DataSetting.Load();
        }
        public static SvoSetting Instance => instance;

        public void SetFormRestart()
        {
            DataSetting.Default.IsReStart = true;
        }
        public void SetFormNotRestart()
        {
            DataSetting.Default.IsReStart = false;
        }
        public bool FormIsRestart()
        {
            if(DataSetting.Default.IsReStart)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Save()
        {
            DataSetting.Save();
        }
    }
}
