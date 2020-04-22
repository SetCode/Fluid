using System.Threading;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class ThreadUtils
    {
        /// <summary>
        /// 运行后台线程任务
        /// </summary>
        /// <param name="threadStart"></param>
        public static void RunBackground(ThreadStart threadStart, ThreadPriority priority = ThreadPriority.Normal)
        {
            var t = new Thread(threadStart);
            t.Priority = priority;
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// 运行线程任务
        /// </summary>
        /// <param name="threadStart"></param>
        public static void Run(ThreadStart threadStart, ThreadPriority priority = ThreadPriority.Normal)
        {
            var t = new Thread(threadStart);
            t.Priority = priority;
            t.Start();
        }

    }
}
