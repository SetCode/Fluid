using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupIO
{
    /// <summary>
    /// IO配置基类，新增IO配置需要继承此类，并重写<see cref="SetupDIPrm"/> 和 <see cref="SetupDOPrm"/>
    /// </summary>
    public abstract class IOSetupBase : IIOSetupable
    {
        protected Card card0;
        protected Card card1;
        protected ExtMdl extMdl;
        protected int cardKey0 = (int)CardType.Card0;
        protected int cardKey1 = (int)CardType.Card1;
        protected int extMdlKey = (int)CardType.ExtMdl;

        public IOPrm IOPrm { get; private set; }

        public IOSetupBase()
        {
            card0 = CardMgr.Instance.FindBy((int)CardType.Card0);
            card1 = CardMgr.Instance.FindBy((int)CardType.Card1);
            extMdl = ExtMdlMgr.Instance.FindBy((int)CardType.ExtMdl);
        }

        protected IIOExecutable GetIOExecutor(int cardKey)
        {
            IIOExecutable executable = null;
            switch (cardKey)
            {
                case (int)CardType.Card0:
                    executable = card0.Executor;
                    break;
                case (int)CardType.Card1:
                    executable = card1.Executor;
                    break;
                case (int)CardType.ExtMdl:
                    executable = extMdl.ExtMdlExecutor;
                    break;
            }
            return executable;
        }

        protected short GetCardId(int cardKey)
        {
            if (cardKey < 2)
            {
                return CardMgr.Instance.FindBy(cardKey).CardId;
            }
            else
            {
                return ExtMdlMgr.Instance.FindBy(cardKey).CardId;
            }
        }

        protected DIPrm GetDIPrmBy(int Key)
        {
            return this.IOPrm.DIPrmMgr.FindBy(Key);
        }

        protected DOPrm GetDOPrmBy(int key)
        {
            return this.IOPrm.DOPrmMgr.FindBy(key);
        }

        public bool LoadIOPrm()
        {
            string path = SettingsPath.PathMachine + "\\" + this.GetType().Name;
            this.IOPrm = JsonUtil.Deserialize<IOPrm>(path);
            if (this.IOPrm == null)
            {
                this.IOPrm = new IOPrm();
                return false;
            }
            return true;
        }

        public bool SaveIOPrm()
        {
            string path = SettingsPath.PathMachine + "\\" + this.GetType().Name;
            return JsonUtil.Serialize<IOPrm>(path, this.IOPrm);
        }

        public abstract void SetupDIPrm();

        public abstract void SetupDOPrm();

        public virtual void SetupIO()
        {
            foreach (var item in this.IOPrm.DIPrmMgr.FindAll())
            {
                DIMgr.Instance.Add(new DI(item.Key, this.GetIOExecutor(item.CardKey), this.GetDIPrmBy(item.Key)));
            }
            foreach (var item in this.IOPrm.DOPrmMgr.FindAll())
            {
                DOMgr.Instance.Add(new DO(item.Key, this.GetIOExecutor(item.CardKey), this.GetCardId(item.CardKey), this.GetDOPrmBy(item.Key), 0));
            }
        }
    }
}
