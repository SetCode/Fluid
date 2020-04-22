using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Motion.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Anda.Fluid.Drive
{
    public static class MotionExtensions
    {
        public static void MoveHomeAndReply(this AxisType axisType)
        {
            Axis axis = AxisMgr.Instance.FindBy((int)axisType);
            if (axis == null)
            {
                return;
            }
            MoveHomePrm moveHomePrm = null;
            switch (axisType)
            {
                case AxisType.Axis5:
                    moveHomePrm = Machine.Instance.Robot.HomePrm.HomePrm5;
                    break;
                case AxisType.Axis6:
                    moveHomePrm = Machine.Instance.Robot.HomePrm.HomePrm6;
                    break;
                case AxisType.Axis7:
                    moveHomePrm = Machine.Instance.Robot.HomePrm.HomePrm7;
                    break;
                case AxisType.Axis8:
                    moveHomePrm = Machine.Instance.Robot.HomePrm.HomePrm8;
                    break;
            }
            if(moveHomePrm == null)
            {
                return;
            }
            CommandMoveHome command = new CommandMoveHome(axis, moveHomePrm);
            executeCommand(command);
        }

        public static bool MovePosAndReply(this AxisType axisType, double pos, double vel)
        {
            Axis axis = AxisMgr.Instance.FindBy((int)axisType);
            if (axis == null)
            {
                return false;
            }
            MovePosPrm prm = new MovePosPrm()
            {
                Pos = pos,
                Vel = vel,
                Acc = axis.Prm.Acc,
                Dec = axis.Prm.Acc
            };
            CommandMovePos command = new CommandMovePos(axis, prm);
            return executeCommand(command);
        }

        public static bool MoveJog(this AxisType axisType, double vel)
        {
            Axis axis = AxisMgr.Instance.FindBy((int)axisType);
            if (axis == null)
            {
                return false;
            }
            return axis.MoveJog(vel) == 0;
        }

        public static bool MoveStop(this AxisType axisType)
        {
            Axis axis = AxisMgr.Instance.FindBy((int)axisType);
            if (axis == null)
            {
                return false;
            }
            return axis.MoveSmoothStop() == 0;
        }

        private static bool executeCommand(ICommandable command)
        {
            command.Call();
            while (true)
            {
                Thread.Sleep(5);
                command.Update();
                if (command.State == CommandState.Running)
                {
                    continue;
                }
                else if (command.State == CommandState.Succeed || command.State == CommandState.Failed)
                {
                    break;
                }
            }
            return command.State == CommandState.Succeed;
        }
    }
}
