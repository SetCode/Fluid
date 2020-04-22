using Anda.Fluid.Drive.Conveyor.LeadShine.Command;
using Anda.Fluid.Drive.Conveyor.LeadShine.IO;
using Anda.Fluid.Drive.Conveyor.LeadShine.MotionModule;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Anda.Fluid.Drive.Conveyor.LeadShine
{
    public class ConveyorMachine
    {
        private static ConveyorMachine instance = new ConveyorMachine();
        private Axis[] axes;
        private ConcurrentQueue<MotionCommand> prioeityCommandQueue, conveyor1CommandQueue,
            conveyor2CommandQueue, axisYCommandQueue;
        private ConveyorMachine()
        {
            this.axes = new Axis[]
            {
                new Axis(0),
                new Axis(1),
                new Axis(2),
            };

            this.prioeityCommandQueue = new ConcurrentQueue<MotionCommand>();
            this.conveyor1CommandQueue = new ConcurrentQueue<MotionCommand>();
            this.conveyor2CommandQueue = new ConcurrentQueue<MotionCommand>();
            this.axisYCommandQueue = new ConcurrentQueue<MotionCommand>();
        }

        public static ConveyorMachine Instance => instance;

        public bool AxisYHomeIsDone => this.axes[2].HomeIsDone;

        public bool Enable { get; set; } = false;

        public void Init()
        {
            this.prioeityCommandQueue.Enqueue(new InitCommand());
        }

        public void Close()
        {
            this.prioeityCommandQueue.Enqueue(new CloseCommand());
        }

        /// <summary>
        /// 会阻塞
        /// </summary>
        public void MoveHome(double maxVel)
        {
            //如果没有回过零，则需要回零
            if (!this.axes[2].HomeIsDone)
            {
                //先查看是否在负极限位置,如果是则反向逃脱1CM
                if (InPutMgr.Instance.AxisYInput.N_LimitSts == IOSts.High)
                {
                    this.AxisYMovePos(10, 10, 0.1);
                }

                MotionCommand moveCommand = this.axes[2].MoveHome(0, maxVel, 0.01);
                this.axisYCommandQueue.Enqueue(moveCommand);

                this.WaitCommand(moveCommand);

                MotionCommand setPosCommand = this.axes[2].SetPos(0);
                this.axisYCommandQueue.Enqueue(setPosCommand);

                this.axes[2].HomeIsDone = true;
            }
            
        }

        public IOSts InquireDiSts(DiEnum diName)
        {
            return InPutMgr.Instance.FindBy(diName).CurrSts;
        }

        public IOSts GetDoSts(DoEnum doName)
        {
            return OutPutMgr.Instance.FindBy(doName).CurrSts;
        }

        public void SetDo(DoEnum doName,bool isHigh)
        {
            OutPutMgr.Instance.FindBy(doName).SetSts(isHigh);
        }

        public void ResetAllDo()
        {
            foreach (var item in OutPutMgr.Instance.OutPutList)
            {
                item.SetSts(false);
            }
        }

        public double GetYPos()
        {
            return this.axes[2].Pos;
        }

        public void Conveyor1JogForwardMove(double maxVel, double accTime)
        {
            MotionCommand command = this.axes[0].JogMove(true, 0, maxVel, accTime);
            this.conveyor1CommandQueue.Enqueue(command);
        }

        public void Conveyor1JogBackMove(double maxVel, double accTime)
        {
            MotionCommand command = this.axes[0].JogMove(false, 0, maxVel, accTime);
            this.conveyor1CommandQueue.Enqueue(command);
        }

        public void Conveyor1Stop()
        {
            MotionCommand command = this.axes[0].StopMove();
            this.prioeityCommandQueue.Enqueue(command);
        }

        public void Conveyor2JogForwardMove(double maxVel, double accTime)
        {
            MotionCommand command = this.axes[1].JogMove(true, 0, maxVel, accTime);
            this.conveyor2CommandQueue.Enqueue(command);
        }

        public void Conveyor2JogBackMove(double maxVel, double accTime)
        {
            MotionCommand command = this.axes[1].JogMove(false, 0, maxVel, accTime);
            this.conveyor2CommandQueue.Enqueue(command);
        }

        public void Conveyor2Stop()
        {
            MotionCommand command = this.axes[1].StopMove();
            this.prioeityCommandQueue.Enqueue(command);
        }

        public void AxisYJogForwardMove(double maxVel, double accTime)
        {
            MotionCommand command = this.axes[2].JogMove(true, 0, maxVel, accTime);
            this.axisYCommandQueue.Enqueue(command);
        }

        public void AxisYJogBackMove(double maxVel, double accTime)
        {
            MotionCommand command = this.axes[2].JogMove(false, 0, maxVel, accTime);
            this.axisYCommandQueue.Enqueue(command);
        }

        public void AxisYStop()
        {
            MotionCommand command = this.axes[2].StopMove();
            this.prioeityCommandQueue.Enqueue(command);
        }

        public void SetAxisYPos0()
        {
            MotionCommand command = this.axes[2].SetPos(0);
            this.prioeityCommandQueue.Enqueue(command);

        }

        /// <summary>
        /// 阻塞等待
        /// </summary>
        /// <param name="pos"></param>
        public void AxisYMovePos(double pos, double maxVel, double accTime)
        {
            MotionCommand command = this.axes[2].PosMove(pos, 0, maxVel, accTime);
            this.axisYCommandQueue.Enqueue(command);
            this.WaitCommand(command);
        }

        public void Update()
        {
            InPutMgr.Instance.UpdateSts();
            OutPutMgr.Instance.UpdateSts();
            axes[2].UpdatePos();

            if (this.prioeityCommandQueue.Count != 0)
            {
                this.priorityQueueUpdate();
            }

            if (this.conveyor1CommandQueue.Count != 0)
            {
                this.AxisQueueUpdate(0);
            }

            if (this.conveyor2CommandQueue.Count != 0)
            {
                this.AxisQueueUpdate(1);
            }

            if (this.axisYCommandQueue.Count != 0)
            {
                this.AxisQueueUpdate(2);
            }          
        }

        private void priorityQueueUpdate()
        {
            MotionCommand command;
            this.prioeityCommandQueue.TryDequeue(out command);
            command.Run();
        }

        private void AxisQueueUpdate(int axisNo)
        {
            MotionCommand command;

            if (axisNo == 0)
            {
                this.conveyor1CommandQueue.TryPeek(out command);
            }
            else if (axisNo == 1) 
            {
                this.conveyor2CommandQueue.TryPeek(out command);
            }   
            else
            {
                this.axisYCommandQueue.TryPeek(out command);
            }

            if (command.State == CommmandState.Idle)
            {
                command.Run();
            }
            else if (command.State == CommmandState.Running)
            {
                command.UpdateState();
            }
            else if (command.State == CommmandState.Failed)
            {
                MotionCommand failedCommand;

                if (axisNo == 0)
                {
                    this.conveyor1CommandQueue.TryDequeue(out failedCommand);
                }
                else if (axisNo == 1)
                {
                    this.conveyor2CommandQueue.TryDequeue(out failedCommand);
                }
                else
                {
                    this.axisYCommandQueue.TryDequeue(out failedCommand);
                }
            }
            else if (command.State == CommmandState.Abort)
            {
                MotionCommand abortCommand;

                if (axisNo == 0)
                {
                    this.conveyor1CommandQueue.TryDequeue(out abortCommand);
                }
                else if (axisNo == 1)
                {
                    this.conveyor2CommandQueue.TryDequeue(out abortCommand);
                }
                else
                {
                    this.axisYCommandQueue.TryDequeue(out abortCommand);
                }
            }
            else if (command.State == CommmandState.Succeed)
            {
                MotionCommand succeedCommand;
                if (axisNo == 0)
                {
                }
                else if (axisNo == 1)
                {
                }
                else
                {
                    this.axisYCommandQueue.TryDequeue(out succeedCommand);
                }
            }
        }

        private void WaitCommand(MotionCommand command)
        {
            command.AutoEvent.WaitOne();
        }
    }

}
