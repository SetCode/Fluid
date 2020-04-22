using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Command;
using Anda.Fluid.Drive.Scheduler;
using Anda.Fluid.Drive.Hardware.CardExecutor;
using Anda.Fluid.Drive.Hardware.MotionCard;
using Anda.Fluid.Drive.Hardware.Crd;
using Newtonsoft.Json;

namespace Anda.Fluid.Drive.ActiveItems
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AxesXYZ : ActiveItem
    {
        public const string Path = "AxesXYZ";

        public AxesXYZ(Axis axisX, Axis axisY, Axis axisZ)
        {
            this.X = axisX;
            this.Y = axisY;
            this.Z = axisZ;
            this.XY = new Axis[] { this.X, this.Y };
            this.XYZ = new Axis[] { this.X, this.Y, this.Z };

            this.MoveHomePrmX = new MoveHomePrm()
            {
                Mode = HomeMode.Limit_Index,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 20,
                Acc = 100,
                HomeOffset = 0,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 20
            };
            this.MoveHomePrmY = new MoveHomePrm()
            {
                Mode = HomeMode.Limit_Index,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 20,
                Acc = 100,
                HomeOffset = 0,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 20
            };
            this.MoveHomePrmZ = new MoveHomePrm()
            {
                Mode = HomeMode.Limit_Index,
                MoveDir = 1,
                IndexDir = -1,
                VelHigh = 100,
                VelLow = 20,
                Acc = 100,
                HomeOffset = 0,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 20
            };
            this.EscapeLmtPrmZ = new EscapeLmtPrm()
            {
                Lmt = LmtType.Positive,
                Step = -20,
                Vel = 50,
                Acc = 100
            };

            this.MoveJogPrmX = new MoveJogPrm() { Vel = 50, Acc = 100 };
            this.MoveJogPrmY = new MoveJogPrm() { Vel = 50, Acc = 100 };
            this.MoveJogPrmZ = new MoveJogPrm() { Vel = 20, Acc = 100 };
        }

        public Axis X { get; private set; }
        public Axis Y { get; private set; }
        public Axis Z { get; private set; }
        public Axis[] XY { get; private set; }
        public Axis[] XYZ { get; private set; }

        public double PosX { get { return this.X.Pos.Mm; } }
        public double PosY { get { return this.Y.Pos.Mm; } }
        public double PosZ { get { return this.Z.Pos.Mm; } }

        [JsonProperty]
        public double OrgX { get; set; }
        [JsonProperty]
        public double OrgY { get; set; }
        [JsonProperty]
        public double SafeZ { get; set; }

        [JsonProperty]
        public MoveHomePrm MoveHomePrmX { get; set; }
        [JsonProperty]
        public MoveHomePrm MoveHomePrmY { get; set; }
        [JsonProperty]
        public MoveHomePrm MoveHomePrmZ { get; set; }
        [JsonProperty]
        public EscapeLmtPrm EscapeLmtPrmZ { get; set; }
        [JsonProperty]
        public MoveJogPrm MoveJogPrmX { get; set; }
        [JsonProperty]
        public MoveJogPrm MoveJogPrmY { get; set; }
        [JsonProperty]
        public MoveJogPrm MoveJogPrmZ { get; set; }

        public void Servo(bool b)
        {
            CommandServo command = new CommandServo(this.XYZ, b);
            this.Fire(command);
        }

        public void MoveHome()
        {
            //escape z PLmt
            CommandEscapeLmt commandEscapeLmt = new CommandEscapeLmt(this.Z, this.EscapeLmtPrmZ);
            this.Fire(commandEscapeLmt);
            //move home z
            CommandMoveHome command = new CommandMoveHome(this.Z, this.MoveHomePrmZ);
            this.Fire(command);
            //move home xy
            command = new CommandMoveHome(this.XY, new MoveHomePrm[] { this.MoveHomePrmX, this.MoveHomePrmY });
            this.Fire(command);
        }

        public void MovePosXYZ(MovePosPrm movePosPrmX, MovePosPrm movePosPrmY, MovePosPrm movePosPrmZ)
        {
            //move pos safe z
            MovePosPrm movePosPrmSafeZ = movePosPrmZ;
            movePosPrmSafeZ.Pos = this.SafeZ;
            CommandMovePos command = new CommandMovePos(this.Z, movePosPrmSafeZ);
            this.Fire(command);
            //move pos xy
            command = new CommandMovePos(this.XY, new MovePosPrm[] { movePosPrmX, movePosPrmY });
            this.Fire(command);
            //move pos z
            command = new CommandMovePos(this.Z, movePosPrmZ);
            this.Fire(command);
        }

        public void MovePosXY(MovePosPrm movePosPrmX, MovePosPrm movePosPrmY)
        {
            //move pos xy
            CommandMovePos command = new CommandMovePos(this.XY, new MovePosPrm[] { movePosPrmX, movePosPrmY });
            this.Fire(command);
        }

        public void MovePosX(MovePosPrm movePosPrm)
        {
            CommandMovePos command = new CommandMovePos(this.X, movePosPrm);
            this.Fire(command);
        }

        public void MovePosY(MovePosPrm movePosPrm)
        {
            CommandMovePos command = new CommandMovePos(this.Y, movePosPrm);
            this.Fire(command);
        }

        public void MovePosZ(MovePosPrm movePosPrm)
        {
            CommandMovePos command = new CommandMovePos(this.Z, movePosPrm);
            this.Fire(command);
        }

        public void MoveIncXY(MovePosPrm movePosPrmX, MovePosPrm movePosPrmY)
        {
            CommandMoveInc command = new CommandMoveInc(this.XY, new MovePosPrm[] { movePosPrmX, movePosPrmY });
            this.Fire(command);
        }

        public void MoveIncX(MovePosPrm movePosPrm)
        {
            CommandMoveInc command = new CommandMoveInc(this.X, movePosPrm);
            this.Fire(command);
        }

        public void MoveIncY(MovePosPrm movePosPrm)
        {
            CommandMoveInc command = new CommandMoveInc(this.Y, movePosPrm);
            this.Fire(command);
        }

        public void MoveIncZ(MovePosPrm movePosPrmZ)
        {
            CommandMoveInc command = new CommandMoveInc(this.Z, movePosPrmZ);
            this.Fire(command);
        }

        public void MoveJogXp()
        {
            CommandMoveJog command = new CommandMoveJog(this.X, this.MoveJogPrmX);
            this.Fire(command);
        }

        public void MoveJogXn()
        {
            CommandMoveJog command = new CommandMoveJog(this.X, -this.MoveJogPrmX);
            this.Fire(command);
        }

        public void MoveJogYp()
        {
            CommandMoveJog command = new CommandMoveJog(this.Y, this.MoveJogPrmY);
            this.Fire(command);
        }

        public void MoveJogYn()
        {
            CommandMoveJog command = new CommandMoveJog(this.Y, -this.MoveJogPrmY);
            this.Fire(command);
        }

        public void MoveJogZp()
        {
            CommandMoveJog command = new CommandMoveJog(this.Z, this.MoveJogPrmZ);
            this.Fire(command);
        }

        public void MoveJogZn()
        {
            CommandMoveJog command = new CommandMoveJog(this.Z, -this.MoveJogPrmZ);
            this.Fire(command);
        }

        public void MoveStop()
        {
            SchedulerMotion.Instance.Notify(MsgType.Stop, this);
        }

        public void MoveTrc(MoveTrcPrm moveTrcPrm, IList<ICrdable> crds)
        {
            CommandMoveTrc command = new CommandMoveTrc(this.X, this.Y, moveTrcPrm, crds);
            this.Fire(command);
        }

    }
}
