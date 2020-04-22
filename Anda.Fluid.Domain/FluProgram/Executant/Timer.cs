using System;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System.Threading;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// 两个命令之间的延时，让硬件层在下一个运动动作之前插入一个延时
    /// </summary>
    [Serializable]
    public class Timer : IDirective
    {
        private long waitInMills;
        /// <summary>
        /// 延时（毫秒值）
        /// </summary>
        public long WaitInMills
        {
            get { return waitInMills; }
        }

        public Timer(long waitInMills)
        {
            this.waitInMills = waitInMills;
        }

        public Result Execute()
        {
            Log.Print("wait " + waitInMills);
            var end = DateTime.Now + TimeSpan.FromMilliseconds(waitInMills);
            while ((end - DateTime.Now).TotalMilliseconds > 1)
            {
                Thread.Sleep(1);
            }
            return Result.OK;
        }
    }
}
