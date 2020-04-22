using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.ScaleSystem
{
    /// <summary>
    /// 称重管理类
    /// </summary>
    public sealed class WeightMgr : EntityMgr<Weight, int>
    {
        private readonly static WeightMgr instance = new WeightMgr();
        private WeightMgr()
        {

        }
        public static WeightMgr Instance => instance;
    }

}
