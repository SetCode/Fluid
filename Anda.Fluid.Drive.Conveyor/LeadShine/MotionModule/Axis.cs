using Anda.Fluid.Drive.Conveyor.LeadShine.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Conveyor.LeadShine.MotionModule
{
    internal class Axis
    {
        private int axisNo;
        private const int pulse2mm = 2000;
        public Axis(int axisNo)
        {
            this.axisNo = axisNo;
        }

        public AxisState State { get; set; } = AxisState.Disable;

        public bool HomeIsDone { get; set; } = false;

        public double Pos { get; private set; } = 0; 
        
        public MotionCommand StopMove()
        {
            return new StopCommand(this.axisNo);
        }

        public MotionCommand MoveHome(double startVel, double maxVel, double accTime)
        {
            return new MoveHomeCommand(this.axisNo, (int)startVel, -(int)maxVel * pulse2mm, accTime);
        }

        public MotionCommand PosMove(double pos, double startVel, double maxVel, double accTime)
        {
            double realPos = pos;// - this.Pos;
            return new PosMoveCommand(this.axisNo, (int)realPos * pulse2mm, (int)startVel, (int)maxVel * pulse2mm, accTime);
        }

        public MotionCommand JogMove(bool isForwad, double startVel, double maxVel, double accTime)
        {
            if (isForwad)
            {
                return new JogMoveCommand(this.axisNo, (int)startVel, (int)maxVel * pulse2mm, accTime);
            }
            else
            {
                return new JogMoveCommand(this.axisNo, (int)startVel, -(int)maxVel * pulse2mm, accTime);
            }

        }

        public MotionCommand SetPos(double pos)
        {
            return new SetPosCommand(this.axisNo, pos);
        }

        public void UpdatePos()
        {
            double pos = csDmc1000.Dmc1000.d1000_get_command_pos(this.axisNo) * (1 / pulse2mm);
            this.Pos = pos;
        }
    }
    internal enum AxisState
    {
        Disable,
        Idle,
        Running
    }
}
