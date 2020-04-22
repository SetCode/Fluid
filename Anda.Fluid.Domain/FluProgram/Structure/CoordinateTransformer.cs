using Anda.Fluid.Infrastructure.Common;
using System;

namespace Anda.Fluid.Domain.FluProgram.Structure
{
    public class CoordinateTransformer
    {
        private double a = 0;
        private double b = 0;
        private double tx = 0;
        private double ty = 0;

        private int MarkPointNum = 0;
        public void SetMarkPoint(PointD standardMarkPoint1, PointD standardMarkPoint2, PointD currentMarkPoint1, PointD currentMarkPoint2)
        {
            if (standardMarkPoint1.X == standardMarkPoint2.X && standardMarkPoint1.Y == standardMarkPoint2.Y)
            {
                MarkPointNum = 0;
                return;
            }
            
            double x1 = standardMarkPoint1.X - standardMarkPoint2.X;
            double x2 = currentMarkPoint1.X - currentMarkPoint2.X;
            double y1 = standardMarkPoint1.Y - standardMarkPoint2.Y;
            double y2 = currentMarkPoint1.Y - currentMarkPoint2.Y;

            a = (x1 * x2 + y1 * y2) / (x1 * x1 + y1 * y1);
            b = (x1 * y2 - y1 * x2) / (x1 * x1 + y1 * y1);
            tx = currentMarkPoint1.X - a * standardMarkPoint1.X + b * standardMarkPoint1.Y;
            ty = currentMarkPoint1.Y - b * standardMarkPoint1.X - a * standardMarkPoint1.Y;

            MarkPointNum = 2;
            return;
        }
        public void SetMarkPoint(PointD standardMarkPoint, PointD currentMarkPoint)
        {
            a = 1;
            b = 0;
            tx = currentMarkPoint.X - standardMarkPoint.X;
            ty = currentMarkPoint.Y - standardMarkPoint.Y;

            MarkPointNum = 1;
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="standardMarkPoint"></param>
        /// <param name="standardAngle">参考角度，单位弧度</param>
        /// <param name="currentMarkPoint"></param>
        /// <param name="currentAngle">当前角度，单位弧度</param>
        public void SetMarkPoint(PointD standardMarkPoint, double standardAngle, PointD currentMarkPoint, double currentAngle)
        {
            //standardAngle = standardAngle * Math.PI / 180;
            //currentAngle = currentAngle * Math.PI / 180;

            //PointD standardMarkPoint2 = new PointD(Math.Cos(standardAngle) * 100 + standardMarkPoint.X, Math.Sin(standardAngle) * 100 + standardMarkPoint.Y);
            //PointD currentMarkPoint2 = new PointD(Math.Cos(currentAngle) * 100 + currentMarkPoint.X, Math.Sin(currentAngle) * 100 + currentMarkPoint.Y);

            //this.SetMarkPoint(standardMarkPoint, standardMarkPoint2, currentMarkPoint, currentMarkPoint2);
            this.GetMatrix(standardMarkPoint, standardAngle, currentMarkPoint, currentAngle);
            MarkPointNum = 1;
        }

        private void GetMatrix(PointD standardMarkPoint, double standardAngle, PointD currentMarkPoint, double currentAngle)
        {
            standardAngle = standardAngle * Math.PI / 180;
            currentAngle = currentAngle * Math.PI / 180;
            double deltaTheta = currentAngle - standardAngle;

            double cosTheta = Math.Cos(deltaTheta);
            double sinTheta = Math.Sin(deltaTheta);

            this.a = cosTheta;
            this.b = sinTheta;
            this.tx = currentMarkPoint.X -(cosTheta*standardMarkPoint.X-sinTheta*standardMarkPoint.Y);
            this.ty = currentMarkPoint.Y -(sinTheta*standardMarkPoint.X+cosTheta*standardMarkPoint.Y);

        }

        public PointD Transform(PointD p)
        {
            if (MarkPointNum == 0) return p;

            return new PointD(a * p.X - b * p.Y + tx, b * p.X + a * p.Y + ty);
        }
    }
}
