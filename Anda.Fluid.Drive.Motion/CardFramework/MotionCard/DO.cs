using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public class DO : EntityBase<int>
    {
        private int ValidValue = 1;
        private int InvalidValue = 0;

        public DO(int key, IIOExecutable executable, short cardId, DOPrm prm, short mdlId = -1)
            : base (key)
        {
            this.IOExecutor = executable;
            this.CardId = cardId;
            this.MdlId = mdlId;
            this.Prm = prm;
            this.DoId = this.Prm.DoId;
            this.Name = this.Prm.Name;
        }

        public IIOExecutable IOExecutor { get; private set; }
        public short CardId { get; private set; }
        public short MdlId { get; private set; }
        public short DoId { get; private set; }
        public string Name { get; set; }

        public Sts Status { get; private set; } = new Sts();
        public DOPrm Prm { get; set; }

        public void UpdataValidValue()
        {
            if (this.Prm.HeightLevelValid)
            {
                ValidValue = 1;
                InvalidValue = 0;
            }
            else
            {
                ValidValue = 0;
                InvalidValue = 1;
            }
        }

        public short Set(bool b)
        {
            try
            {
                UpdataValidValue();
                int value = b ? ValidValue : InvalidValue;
                short rtn = this.IOExecutor.SetDoBit(this.CardId, this.MdlId, this.DoId, (short)value);
                return rtn;
            }
            catch
            {

                return -10;
            }
        }

        public void Update()
        {
            try
            {
                if (this.IOExecutor == null)
                {
                    return;
                }

                UpdataValidValue();

                if (this.IOExecutor.GetDoBit(this.IOExecutor.DoData, this.DoId) == ValidValue)
                {
                    this.Status.Update(true);
                }
                else
                {
                    this.Status.Update(false);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
