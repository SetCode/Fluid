using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Dialogs.CmdLineFineTune.MVC
{
    public interface IFineTuneViewable
    {
        IFineTuneControlable Controller { get; }

        /// <summary>
        /// 当模型发生改变时，视图跟着变化
        /// </summary>
        /// <param name="model"></param>
        void UpdateByModelChange(IFineTuneModelable model);

        /// <summary>
        /// 当选择的点发生改变时，视图跟着变化
        /// </summary>
        /// <param name="model"></param>
        void UpdateBySelectedChange(IFineTuneModelable model);
    }
}
