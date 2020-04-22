using Anda.Fluid.Drive.Motion.CardFramework.CardExecutor;
using Anda.Fluid.Drive.Motion.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.ActiveItems
{
    public class RobotHomePrm : ICloneable
    {
        [Browsable(false)]
        public MoveHomePrm HomePrmX { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrmY { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrmZ { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrmR { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrmU { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrmA { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrmB { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrm5 { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrm6 { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrm7 { get; set; }

        [Browsable(false)]
        public MoveHomePrm HomePrm8 { get; set; }

        [Browsable(false)]
        public EscapeLmtPrm EscapeLmtPrmZ { get; set; }

        public RobotHomePrm Default()
        {
            //axis x move home param
            this.HomePrmX = new MoveHomePrm()
            {
                Mode = HomeMode.Limit,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 10,
                Acc = 5,
                HomeOffset = 1,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 50
            };
            //axis y move home param
            this.HomePrmY = new MoveHomePrm()
            {
                Mode = HomeMode.Limit,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 10,
                Acc = 5,
                HomeOffset = 1,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 50
            };
            //axis z move home param
            this.HomePrmZ = new MoveHomePrm()
            {
                Mode = HomeMode.Limit,
                MoveDir = 1,
                IndexDir = -1,
                VelHigh = 50,
                VelLow = 5,
                Acc = 5,
                HomeOffset = -1,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 10
            };
            //axis R move home param
            this.HomePrmR = new MoveHomePrm()
            {
                Mode = HomeMode.Home_Index,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 10,
                Acc = 3,
                HomeOffset = 0,
                SearchHomeDis = 0,
                SearchIndexDis = 300,
                EscapeStep = 10
            };
            //axis U move home param
            this.HomePrmU = new MoveHomePrm()
            {
                Mode = HomeMode.Home_Index,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 10,
                Acc = 3,
                HomeOffset = 0,
                SearchHomeDis = 0,
                SearchIndexDis = 300,
                EscapeStep = 10
            };
            //axis z escape positive limit param before move home
            this.EscapeLmtPrmZ = new EscapeLmtPrm()
            {
                Lmt = LmtType.Positive,
                Step = -5,
                Vel = 20,
                Acc = 5
            };
            this.HomePrmA = new MoveHomePrm()
            {
                Mode = HomeMode.Limit,
                MoveDir = 1,
                IndexDir = -1,
                VelHigh = 50,
                VelLow = 5,
                Acc = 5,
                HomeOffset = -1,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 10
            };
            this.HomePrmB = new MoveHomePrm()
            {
                Mode = HomeMode.Limit,
                MoveDir = 1,
                IndexDir = -1,
                VelHigh = 50,
                VelLow = 5,
                Acc = 5,
                HomeOffset = -1,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 10
            };
            this.HomePrm5 = new MoveHomePrm()
            {
                Mode = HomeMode.Limit,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 10,
                Acc = 5,
                HomeOffset = 1,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 50
            };
            this.HomePrm6 = new MoveHomePrm()
            {
                Mode = HomeMode.Limit,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 10,
                Acc = 5,
                HomeOffset = 1,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 50
            };
            this.HomePrm7 = new MoveHomePrm()
            {
                Mode = HomeMode.Limit,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 10,
                Acc = 5,
                HomeOffset = 1,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 50
            };
            this.HomePrm8 = new MoveHomePrm()
            {
                Mode = HomeMode.Limit,
                MoveDir = -1,
                IndexDir = 1,
                VelHigh = 100,
                VelLow = 10,
                Acc = 5,
                HomeOffset = 1,
                SearchHomeDis = 0,
                SearchIndexDis = 200,
                EscapeStep = 50
            };
            return this;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
