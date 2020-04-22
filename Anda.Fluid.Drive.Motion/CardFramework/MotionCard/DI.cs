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

    public class DI : EntityBase<int>
    {
        private int ValidValue = 1;
        private int InvalidValue = 0;

        public DI(int key, IIOExecutable executable, DIPrm prm)
            : base(key)
        {
            this.IOExecutor = executable;
            this.Prm = prm;
            this.DiId = this.Prm.DiId;
            this.Name = this.Prm.Name;
        }

        public IIOExecutable IOExecutor { get; private set; }

        public short DiId { get; private set; }

        public string Name { get; set; }

        public DIPrm Prm { get; private set; }

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

        public Sts Status { get; private set; } = new Sts();

        public void Update()
        {
            try
            {
                if (this.IOExecutor == null)
                {
                    return;
                }

                UpdataValidValue();
                if (this.IOExecutor.GetDiBit(this.IOExecutor.DiData, this.DiId) == ValidValue)
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
