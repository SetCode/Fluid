using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.Utils
{
    /// <summary>
    /// 轨道计时器
    /// </summary>
    internal class ConveyorTimer
    {
        private DateTime dateTime = new DateTime();
        private int decideTime = 0;
        private bool isPause = false;


        /// <summary>
        /// 复位并启动计时器，传入判定时长ms
        /// </summary>
        /// <param name="decideTime"></param>
        public void ResetAndStart(int decideTime)
        {
            this.dateTime = DateTime.Now;
            this.decideTime = decideTime;
            this.isPause = false;
        }

        /// <summary>
        /// 查看计时器的运行时间有没有超过判定时长,超过返回false
        /// </summary>
        public bool IsNormal
        {
            get
            {
                if (isPause)
                {
                    return true;
                }
                else
                {
                  return DateTime.Now - dateTime < TimeSpan.FromMilliseconds(decideTime) ? true : false;
                }
            }
        }
            

        /// <summary>
        /// 暂停计时器
        /// </summary>
        public void Pause()
        {
            this.isPause = true;
        }

        /// <summary>
        /// 启动计时器
        /// </summary>
        public void Start()
        {
            this.isPause = false;
            this.dateTime = DateTime.Now;
        }
    }

    internal class ConveyorTimerMgr
    {
        private static ConveyorTimerMgr instance = new ConveyorTimerMgr();
        private ConveyorTimerMgr()
        {
            this.PreAskTimer = new ConveyorTimer();
            this.PreBoardEnteringTimer = new ConveyorTimer();
            this.PreBoardArrivedTimer = new ConveyorTimer();
            this.PreBoardLeavingTimer = new ConveyorTimer();
        }
        public static ConveyorTimerMgr Instance => instance;

        public ConveyorTimer PreAskTimer { get; private set; }

        public ConveyorTimer PreBoardEnteringTimer { get; private set; }

        public ConveyorTimer PreBoardLeavingTimer { get; private set; }

        public ConveyorTimer PreBoardArrivedTimer { get; private set; }

        public void PauseAll()
        {
            this.PreAskTimer.Pause();
            this.PreBoardEnteringTimer.Pause();
            this.PreBoardArrivedTimer.Pause();
            this.PreBoardLeavingTimer.Pause();
        }

        public void StartAll()
        {
            this.PreAskTimer.Start();
            this.PreBoardEnteringTimer.Start();
            this.PreBoardArrivedTimer.Start();
            this.PreBoardLeavingTimer.Start();
        }

    }
}
