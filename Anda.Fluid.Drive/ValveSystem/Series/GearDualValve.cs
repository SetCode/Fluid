using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Drive.ValveSystem.Series
{
    public class GearDualValve : DualValve
    {
        private GearValve gearValve1, gearValve2;
        public GearDualValve(Card card, Valve valve1, Valve valve2) : base(card, valve1, valve2)
        {
            this.gearValve1 = (GearValve)valve1;
            this.gearValve2 = (GearValve)valve2;
        }

        public override Result FluidArc(SvValveFludLineParam svValveArcParam, PointD center, short clockwize, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidArc(PointD accStartPos, PointD arcStartPos, PointD arcEndPos, PointD decEndPos, PointD center, short clockwize, double vel, PointD[] points, double intervalSec, PointD simulStartPos, PointD[] simulPoints, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidLine(SvValveFludLineParam primaryLineParam, SvValveFludLineParam simulLineParam, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, double acc)
        {
            throw new NotImplementedException();
        }

        public override Result FluidLine(PointD accStartPos, PointD lineStartPos, PointD lineEndPos, PointD decEndPos, double vel, PointD[] points, double intervalSec, PointD simulStartPos, PointD[] simulPoints, double acc)
        {
            throw new NotImplementedException();
        }

        public override int GetSprayMills(short cycle)
        {
            throw new NotImplementedException();
        }

        public override short SprayCycle(short cycle)
        {
            throw new NotImplementedException();
        }

        public override short SprayCycle(short cycle, short offTime)
        {
            throw new NotImplementedException();
        }

        public override short SprayCycleAndWait(short cycle)
        {
            throw new NotImplementedException();
        }

        public override short Spraying()
        {
            throw new NotImplementedException();
        }

        public override short SprayOff()
        {
            throw new NotImplementedException();
        }

        public override short SprayOne()
        {
            throw new NotImplementedException();
        }

        public override short SprayOneAndWait()
        {
            throw new NotImplementedException();
        }

        public override Result SuckBack(double suckBackTime)
        {
            throw new NotImplementedException();
        }

        protected override short CmpStart(short source, PointD[] points)
        {
            throw new NotImplementedException();
        }

        protected override void SimulCmp2dStart(short cmp2dSrc, short cmp2dMaxErr, PointD[] points)
        {
            throw new NotImplementedException();
        }
    }
}
