using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Infrastructure.Alarming;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public class ExtMdl : EntityBase<int>, IAlarmSenderable
    {
        private int diData, doData;

        private bool IsFirst = true;
        public ExtMdl(int key, short cardId, IExtMdlExecutable extMdlExecutor, short mdlId, string name, string configFile)
            : base(key)
        {
            this.CardId = cardId;
            this.MdlId = mdlId;
            this.Name = name;
            this.ExtMdlExecutor = extMdlExecutor;
            this.ConfigFile = configFile;
        }

        public IExtMdlExecutable ExtMdlExecutor { get; private set; }

        public short CardId { get; private set; }

        public short MdlId { get; private set; }

        public string Name { get; private set; }

        public string ConfigFile { get; private set; }

        object IAlarmSenderable.Obj => this;

        string IAlarmSenderable.Name => this.Name;

        public void Updata()
        {
            short rtn = this.ExtMdlExecutor.GetDi(this.CardId, this.MdlId, out this.diData);
            rtn = this.ExtMdlExecutor.GetDo(this.CardId, this.MdlId, out this.doData);
            this.ExtMdlExecutor.DiData = this.diData;
            this.ExtMdlExecutor.DoData = this.doData;
        }

        public short Init()
        {
            short rtn = this.ExtMdlExecutor.Open(this.CardId);
            if (rtn != 0) return rtn;
            rtn = this.ExtMdlExecutor.Reset(this.CardId);
            if (rtn != 0) return rtn;
            if (this.IsFirst)
            {                
                rtn = this.ExtMdlExecutor.LoadConfig(this.CardId, this.ConfigFile);
                this.IsFirst = false;
            }
            return rtn; 
        }

        public short Close()
        {
            return this.ExtMdlExecutor.Close(this.CardId);
        }
    }
}
