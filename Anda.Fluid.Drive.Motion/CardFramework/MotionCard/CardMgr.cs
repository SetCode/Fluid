using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public sealed class CardMgr : EntityMgr<Card, int>
    {
        private readonly static CardMgr instance = new CardMgr();
        private CardMgr() { }
        public static CardMgr Instance => instance;
    }
}
