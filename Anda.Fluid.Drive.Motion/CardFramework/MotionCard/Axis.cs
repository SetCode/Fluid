using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Alarming;

namespace Anda.Fluid.Drive.Motion.CardFramework.MotionCard
{
    public class Axis : EntityBase<int>, IAlarmSenderable
    {
        private int stsData;
        private int pulse;

        public Axis(int key, Card card, short axisId, string name, AxisPrm prm)
           : base(key)
        {
            this.Card = card;
            this.CardKey = card.Key;
            this.CardId = card.CardId;
            this.AxisId = axisId;
            this.Name = name;
            this.Prm = prm;
        }

        public bool Enabled { get; set; } = true;

        public Card Card { get; private set; } = null;

        public int CardKey { get; private set; }

        public short CardId { get; private set; }

        public short AxisId { get; private set; }

        public string Name { get; set; }

        public AxisPrm Prm { get; set; }

        object IAlarmSenderable.Obj => this;

        string IAlarmSenderable.Name => this.Name;


        #region Status

        public AxisState State { get; private set; } = new AxisState();

        public int StsData => this.stsData;

        public Sts IsAlarm { get; private set; } = new Sts();

        public Sts IsError { get; private set; } = new Sts();

        public Sts IsPLmt { get; private set; } = new Sts();

        public Sts IsNLmt { get; private set; } = new Sts();

        public Sts IsServoOn { get; private set; } = new Sts();

        public Sts IsMoving { get; private set; } = new Sts();

        public Sts IsSmoothStop { get; private set; } = new Sts();

        public Sts IsAbruptStop { get; private set; } = new Sts();

        public Sts IsHomeError { get; private set; } = new Sts();

        public Sts IsArrived { get; private set; } = new Sts();

        public int Pulse => this.pulse;

        public double Pos { get; private set; }

        #endregion

        #region Event
        /// <summary>
        /// 使用事件触发限位
        /// </summary>
        public bool useActionLimitTrigger { get; set; } = false;
        public bool useProfilePos { get; set; } = false;
        public event Action LimitTrigger;
        #endregion


        #region Update

        public void Update()
        {
            if(!this.Enabled)
            {
                return;
            }
            this.UpdateStatus();
            this.UpdatePos();
        }
        private void UpdateStatus()
        {
            if (this.Card == null)
            {
                return;
            }

            try
            {
                //this.Card.Executor.GetAxisSts(this.CardId, this.AxisId, out this.stsData);
                this.stsData = this.Card.AxesData[this.AxisId - 1];
            }
            catch
            {

            }

            if ((this.stsData & 1) == 1)
            {
                this.IsAlarm.Update(true);                
            }
            else
            {
                this.IsAlarm.Update(false);
            }

            if ((this.stsData >> 1 & 1) == 1)
            {
                this.IsError.Update(true);
            }
            else
            {
                this.IsError.Update(false);
            }


            if (useActionLimitTrigger)
            {
                // 其他限位触发判断
                this.LimitTrigger?.Invoke();
            }
            else
            {
                if ((this.stsData >> 2 & 1) == 1)
                {
                    this.IsPLmt.Update(true);
                }
                else
                {
                    this.IsPLmt.Update(false);
                }
                if ((this.stsData >> 3 & 1) == 1)
                {
                    this.IsNLmt.Update(true);
                }
                else
                {
                    this.IsNLmt.Update(false);
                }
            }
            

            if ((this.stsData >> 4 & 1) == 1)
            {
                this.IsServoOn.Update(true);
            }
            else
            {
                this.IsServoOn.Update(false);
            }

            if ((this.stsData >> 5 & 1) == 1)
            {
                this.IsMoving.Update(true);
            }
            else
            {
                this.IsMoving.Update(false);
            }

            if ((this.stsData >> 6 & 1) == 1)
            {
                this.IsSmoothStop.Update(true);
            }
            else
            {
                this.IsSmoothStop.Update(false);
            }

            if ((this.stsData >> 7 & 1) == 1)
            {
                this.IsAbruptStop.Update(true);
            }
            else
            {
                this.IsAbruptStop.Update(false);
            }

            if (this.Card.Executor.GetDiBit(this.Card.Executor.DiArriveData, this.AxisId) == 1)
            {
                this.IsArrived.Update(true);
            }
            else
            {
                this.IsArrived.Update(false);
            }
        }

        private void UpdatePos()
        {
            if (this.Card == null)
            {
                return;
            }

            try
            {
                //this.Card.Executor.GetEncPos(this.CardId, this.AxisId, out this.pulse);
                // 使用规划器坐标
                if (useProfilePos)
                {
                    this.Card.Executor.GetPrfPos(this.CardId, this.AxisId,out this.pulse);
                }
                else
                {
                    this.pulse = this.Card.AxesPulse[this.AxisId - 1];
                }
            }
            catch (Exception)
            {

            }

            this.Pos = Math.Round(this.pulse * this.Prm.Pulse2Mm, 3);
        }


        #endregion


        public short Init()
        {
            try
            {
                short rtn = this.Card.Executor.SetStopDec(this.CardId, this.AxisId,
                    this.Prm.SmoothDec, this.Prm.AbruptDec);
                if (rtn != 0) return rtn;
                return 0;
            }
            catch
            {
                return -1;
            }
        }


        #region Execute

        public short Servo(bool b)
        {
            try
            {
                if (b)
                {
                    return this.Card.Executor.AxisOn(this.CardId, this.AxisId);
                }
                else
                {
                    return this.Card.Executor.AxisOff(this.CardId, this.AxisId);
                }
            }
            catch (Exception)
            {
                return -10;
            }
          
        }

        public short ClrSts()
        {
            return this.Card.Executor.ClrAxisSts(this.CardId, this.AxisId);
        }

        public short ZeroPos()
        {
            return this.Card.Executor.ZeroPos(this.CardId, this.AxisId);
        }

        public short MoveJog(double vel)
        {
            return this.Card.Executor.MoveJog(this.CardId, this.AxisId, this.ConvertVel2Card(vel), this.Prm.Acc);
        }

        public short MoveJog(double vel,double acc)
        {
            return this.Card.Executor.MoveJog(this.CardId, this.AxisId, this.ConvertVel2Card(vel), acc);
        }
        public short MoveSmoothStop()
        {
            return this.Card.Executor.MoveSmoothStop(this.CardId, this.AxisId);
        }

        public short MoveAbruptStop()
        {
            return this.Card.Executor.MoveAbruptStop(this.CardId, this.AxisId);
        }

        public short SetLimit(double postivePos, double negativePos)
        {
            return this.Card.Executor.SetAxisLimit(this.CardId, this.AxisId, ConvertPos2Card(postivePos), ConvertPos2Card(negativePos));
        }

        public short EncOn()
        {
            return this.Card.Executor.EncOn(this.CardId, this.AxisId);
        }

        public short EncOff()
        {
            return this.Card.Executor.EncOff(this.CardId, this.AxisId);
        }

        #endregion


        #region Convert2Card

        public int ConvertPos2Card(double pos)
        {
            return (int)(pos * this.Prm.Mm2Pulse);
        }

        public double ConvertVel2Card(double vel)
        {
            return vel * this.Prm.Mm2Pulse / 1000;
        }

        public double ConvertAcc2Mm(double acc)
        {
            return acc * this.Prm.Pulse2Mm * 1000000;
        }

        #endregion
    }
}
