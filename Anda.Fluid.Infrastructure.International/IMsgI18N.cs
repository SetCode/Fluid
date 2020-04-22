using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.International
{
    /// <summary>
    /// Author : liyi
    /// Description : 非UI界面类消息函数语言文本接口
    /// </summary>
    public interface IMsgI18N
    {
        void SaveMsgLanguageResource();

        void ReadMsgLanguageResource();
    }
}
