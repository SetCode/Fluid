using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Infrastructure.Alarming;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public class Card : EntityBase<int>, IAlarmSenderable
    {
        private int[] axesData, axesPulse;
        private int diData, doData, diArriveData;

        public Card(int key, ICardExecutable executor, short cardId, short axesCount, string name, string configFile)
            : base(key)
        {
            this.Executor = executor;
            this.ExecutorType = executor.ExecutorType;
            this.AxesCount = axesCount;
            this.axesData = new int[this.AxesCount];
            this.CardId = cardId;
            this.Name = name;
            this.ConfigFile = configFile;
        }

        public ICardExecutable Executor { get; private set; } = null;

        public CardExecutorType ExecutorType { get; private set; }

        public short CardId { get; private set; }

        public short AxesCount { get; private set; }

        public string Name { get; set; }

        public string ConfigFile { get; set; }

        public int[] AxesData => this.axesData;

        public int[] AxesPulse => this.axesPulse;

        object IAlarmSenderable.Obj => this;

        string IAlarmSenderable.Name => this.Name;

        public void Update()
        {
            try
            {
                this.Executor.GetDi(this.CardId, 0, out diData);
                this.Executor.GetDo(this.CardId, 0, out doData);
                this.Executor.GetDiArrive(this.CardId, out diArriveData);
                this.Executor.DiData = diData;
                this.Executor.DoData = doData;
                this.Executor.DiArriveData = diArriveData;
                this.Executor.ClrAxesSts(this.CardId, 1, this.AxesCount);
                this.Executor.GetAxesSts(this.CardId, 1, this.AxesCount, out axesData);
                this.Executor.GetEncPos(this.CardId, 1, this.AxesCount, out axesPulse);
            }
            catch 
            {

            }
        }

        public short Init()
        {
            try
            {
                short rtn = this.Executor.Open(this.CardId);
                if (rtn != 0) return rtn;
                rtn = this.Executor.Reset(this.CardId);
                if (rtn != 0) return rtn;
                rtn = this.Executor.LoadConfig(this.CardId, this.ConfigFile);
                //rtn = this.Executor.SetLmtSns(this.CardId);
                if (rtn != 0) return rtn;
                return 0;
            }
            catch(Exception ex)
            {
                return -10;
            }
        }

        public short Close()
        {
            try
            {
                return this.Executor.Close(this.CardId);
            }
            catch (Exception)
            {
                return -10;
            }
        }

    }
}
