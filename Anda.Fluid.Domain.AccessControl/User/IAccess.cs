using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.AccessControl.User
{
    /// <summary>
    /// 权限管理界面文本汉化接口
    /// </summary>
    interface IAccess
    {
        int GetLength();
        string GetDescription(int index);
    }
}
