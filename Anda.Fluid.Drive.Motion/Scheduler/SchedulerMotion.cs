using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.Command;

namespace Anda.Fluid.Drive.Motion.Scheduler
{
    public sealed class SchedulerMotion : SchedulerBase<ICommandable>
    {
        private readonly static SchedulerMotion instance = new SchedulerMotion();
        private SchedulerMotion()
        {

        }
        public static SchedulerMotion Instance => instance;

        protected override void UpdateFirst()
        {
            try
            {
                foreach (var item in CardMgr.Instance.FindAll())
                {
                    item.Update();
                }
                foreach (var item in ExtMdlMgr.Instance.FindAll())
                {
                    item.Updata();
                }
                foreach (var item in AxisMgr.Instance.FindAll())
                {
                    item.Update();
                }
                foreach (var item in DIMgr.Instance.FindAll())
                {
                    item.Update();
                }
                foreach (var item in DOMgr.Instance.FindAll())
                {
                    item.Update();
                }
            }
            catch
            {

            }
        }
    }
}
