using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure
{
    public abstract class SettingBase
    {
        public bool Save()
        {
            return JsonUtil.Serialize<SettingBase>(this.GetType().Name, this);
        }

        public bool Load()
        {
            SettingBase obj = JsonUtil.Deserialize<SettingBase>(this.GetType().Name);
            return obj != null;
        }

        public bool ResetToDefault()
        {
            SettingBase obj = SettingUtil.ResetToDefault<SettingBase>(this);
            return obj != null;
        }
    }
}
