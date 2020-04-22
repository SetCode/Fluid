using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.MathTools
{
    /// <summary>
    /// 银宝山特殊轨迹计算数学工具
    /// </summary>
    public static class SymbolLineMathTools
    {
        #region public
        /// <summary>
        /// 获取直线角度 结果范围[0,π]
        /// </summary>
        /// <param name="start">直线起点</param>
        /// <param name="end">直线终点</param>
        /// <returns></returns>
        public static double GetLineAngle(PointD start, PointD end)
        {
            VectorD vector = end - start;
            double angle = Math.Atan2(vector.Y, vector.X) * 180 / Math.PI;
            angle = (angle + 360) % 360;
            return angle;
        }
        /// <summary>
        /// 获取圆弧上某一点沿指定方向的切线角度
        /// </summary>
        /// <param name="center">圆弧圆心</param>
        /// <param name="point1">圆弧上一点</param>
        /// <param name="clockwise">0:顺时针，1逆时针</param>
        /// <returns></returns>
        public static double GetDirectOnArc(PointD center, PointD point1, int clockwise)
        {
            double angle = GetLineAngle(center, point1);
            angle = (angle + 270 - clockwise * 180) % 360;
            return angle;
        }
        /// <summary>
        /// 获取直线与直线拐角过渡圆弧的圆心、起点、终点
        /// 返回结果[起点、圆心、终点]
        /// </summary>
        /// <param name="start">直线1起点</param>
        /// <param name="intersection">两线交点</param>
        /// <param name="end">直线2终点</param>
        /// <param name="r">过渡半径</param>
        /// <returns>过渡圆弧的起点、圆心、终点</returns>
        public static List<PointD> GetLineTransitionArc(PointD start, PointD intersection, PointD end, double r)
        {
            List<PointD> arcPoints = new List<PointD>();

            // 角平分线与对边交点
            PointD point1 = GetAPointOnAngleBiscetor(start, intersection, end);

            // 两线夹角
            double angle = GetAngleOfTwoLine(start, intersection, end);

            // 圆心到拐点距离
            double len1 = r / Math.Sin(angle / 2);

            // 求得过渡圆弧半径为r时，圆心坐标
            VectorD vector1 = GetVectorOnLineByLength(intersection, point1, len1);
            PointD arcCenter = intersection + vector1;

            // 圆弧起点(终点)到拐点距离
            double len2 = Math.Sqrt(len1 * len1 - r * r);

            // 求圆弧起点
            VectorD vector2 = GetVectorOnLineByLength(intersection, start, len2);
            PointD arcStart = intersection + vector2;

            // 求圆弧终点
            VectorD vector3 = GetVectorOnLineByLength(intersection, end, len2);
            PointD arcEnd = intersection + vector3;
            arcPoints.Add(arcStart);
            arcPoints.Add(arcCenter);
            arcPoints.Add(arcEnd);
            return arcPoints;
        }
        /// <summary>
        /// 获取直线到圆弧的过渡圆弧
        /// </summary>
        /// <param name="lineStart">线起点</param>
        /// <param name="intersection">线与圆弧交点</param>
        /// <param name="oriArcCenter">圆弧圆心</param>
        /// <param name="transitionAngle">过渡舍去的圆弧角度，单位度</param>
        /// <returns>过渡圆弧的起点、圆心、终点</returns>
        public static List<PointD> GetLineToArcTransitionArc(PointD lineStart, PointD intersection, PointD oriArcCenter, double transitionAngle, double oriArcClockwise)
        {
            List<PointD> points = new List<PointD>();

            // 获取圆弧上过渡的切点(即过渡圆弧的终点)
            VectorD vOriArcStart = intersection - oriArcCenter;
            if (oriArcClockwise == 0)
            {
                transitionAngle = -transitionAngle;
            }
            VectorD temp = vOriArcStart.Rotate(transitionAngle);
            PointD newArcStart = oriArcCenter + temp;

            // 做圆弧切点切线，与原直线相交得到新的拐角交点
            VectorD tempV1 = temp.Rotate(90);
            PointD inflexionLinePoint1 = newArcStart + tempV1 * 10;
            PointD inflexionLinePoint2 = newArcStart - tempV1 * 10;

            PointD newIntersection = GetIntersection(lineStart, intersection, inflexionLinePoint1, inflexionLinePoint2);

            // 得到新的拐角角度
            double newAngle = GetAngleOfTwoLine(lineStart, newIntersection, newArcStart);

            // 求得新的拐角圆弧半径
            VectorD tempV2 = newArcStart - newIntersection;
            double r = Math.Tan(newAngle / 2) * tempV2.Length;

            points = GetLineTransitionArc(lineStart, newIntersection, newArcStart, r);
            return points;
        }
        /// <summary>
        /// 以圆弧上用于过渡的角度获取圆弧
        /// </summary>
        /// <param name="lineEnd"></param>
        /// <param name="intersection"></param>
        /// <param name="oriArcCenter"></param>
        /// <param name="transitionAngle"></param>
        /// <param name="oriArcClockwise"></param>
        /// <returns>过渡圆弧的起点、圆心、终点</returns>
        public static List<PointD> GetArcToLineTransitionArc(PointD lineEnd, PointD intersection,PointD oriArcCenter,double transitionAngle,double oriArcClockwise)
        {
            List<PointD> result = new List<PointD>();
            if (oriArcClockwise == 1)
            {
                result = GetLineToArcTransitionArc(lineEnd, intersection, oriArcCenter, transitionAngle, 0);
            }
            else
            {
                result = GetLineToArcTransitionArc(lineEnd, intersection, oriArcCenter, transitionAngle, 1);
            }
            result.Reverse();
            return result;
        }
        /// <summary>
        /// 以半径为基准作为获取过渡圆弧
        /// </summary>
        /// <param name="lineStart">线起点</param>
        /// <param name="intersection">线与原始圆弧交点</param>
        /// <param name="oriArcCenter">原始圆弧圆心</param>
        /// <param name="transitionR">过渡圆弧半径</param>
        /// <param name="oriArcClockwise">原始圆弧方向</param>
        /// <returns>过渡圆弧的起点、圆心、终点</returns>
        public static List<PointD> GetLineToArcTransitionArcByR(PointD lineStart, PointD intersection, PointD oriArcCenter, double transitionR, double oriArcClockwise)
        {
            List<PointD> points = new List<PointD>();
            PointD transitionArcCenter, newLineEnd, newArcStart;
            double angle = GetAngleOfTwoLine(lineStart, intersection, oriArcCenter);
            VectorD vArcS = intersection - oriArcCenter;
            double oriR = vArcS.Length;
            VectorD vArcSRotate1;
            if (oriArcClockwise == 1)
            {
                vArcSRotate1 = vArcS.Rotate(-90);
            }
            else
            {
                vArcSRotate1 = vArcS.Rotate(90);
            }

            if (angle != Math.PI)
            {
                //求原始圆弧半径
                double len1;
                if (GetAngleOfTwoLine(lineStart, intersection, (intersection + vArcSRotate1)) > (Math.PI / 2))
                {
                    len1 = oriR * Math.Sin(Math.PI - angle) + transitionR;
                }
                else
                {
                    len1 = oriR * Math.Sin(Math.PI - angle) - transitionR;
                }
                double len2 = Math.Sqrt((oriR + transitionR) * (oriR + transitionR) - len1 * len1);
                PointD footPoint = MathUtils.GetFootPoint(oriArcCenter, lineStart, intersection);
                VectorD vLenTemp = (footPoint - oriArcCenter);
                VectorD vLen1 = vLenTemp.ScaleBy(len1 / vLenTemp.Length);
                VectorD vLine = lineStart - intersection;
                VectorD vLen2 = vLine.ScaleBy(len2 / vLine.Length);
                transitionArcCenter = oriArcCenter + vLen1 + vLen2;
                newLineEnd = MathUtils.GetFootPoint(transitionArcCenter, lineStart, intersection);
                newArcStart = transitionArcCenter + (oriArcCenter - transitionArcCenter).ScaleBy(transitionR / (oriR + transitionR));

            }
            else
            {
                double len1 = transitionR;
                double len2 = Math.Sqrt((oriR + transitionR) * (oriR + transitionR) - len1 * len1);
                VectorD vLen2 = vArcS.ScaleBy(len2 / oriR);
                newLineEnd = oriArcCenter + vLen2;
                VectorD vLen1 = vArcSRotate1.ScaleBy(transitionR / vArcSRotate1.Length);
                transitionArcCenter = newLineEnd + vLen1;
                newArcStart = transitionArcCenter + (oriArcCenter - transitionArcCenter).ScaleBy(transitionR / (transitionR + oriR));
            }
            points.Add(newLineEnd);
            points.Add(transitionArcCenter);
            points.Add(newArcStart);
            return points;
        }

        public static PointD GetScalePointOnLine(PointD lineStart, PointD lineEnd, double scale)
        {
            VectorD vLine = lineEnd - lineStart;
            PointD result = lineStart + vLine.ScaleBy(scale);
            return result;
        }

        /// <summary>
        /// 计算过渡圆弧方向（顺时针或者逆时针）
        /// 0：顺时针，1：逆时针
        /// </summary>
        /// <returns></returns>
        public static int GetArcDirect(PointD start, PointD center, PointD end)
        {
            //过渡圆弧只有劣弧
            int clockwise = 0;
            VectorD vStart = start - center;
            VectorD vEnd = end - center;
            // 使用向量叉乘判断起点到终点的方向（只有劣弧不需要考虑平行的情况）
            double temp = vStart.CrossProduct(vEnd);
            if (temp > 0)
            {
                clockwise = 1;
            }
            return clockwise;
        }

        #endregion

        #region private
        /// <summary>
        /// 返回两线段交点 （实际情况不需要考虑不相交情况）
        /// </summary>
        /// <param name="line1Start">线1起点</param>
        /// <param name="line1End">线1终点</param>
        /// <param name="line2Start">线2起点</param>
        /// <param name="line2End">线2终点</param>
        /// <returns></returns>
        private static PointD GetIntersection(PointD line1Start,PointD line1End,PointD line2Start,PointD line2End)
        {
            double x1 = line1Start.X;
            double y1 = line1Start.Y;
            double x2 = line1End.X;
            double y2 = line1End.Y;
            double x3 = line2Start.X;
            double y3 = line2Start.Y;
            double x4 = line2End.X;
            double y4 = line2End.Y;

            double t = ((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1)) / ((x2 - x1) * (y3 - y4) - (x3 - x4) * (y2 - y1));
            VectorD temp = line2End - line2Start;
            PointD result = line2Start + temp.ScaleBy(t);
            return result;
        }

        /// <summary>
        /// 获取角平分线上一点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="middle"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static PointD GetAPointOnAngleBiscetor(PointD start, PointD middle, PointD end)
        {
            double len1 = start.DistanceTo(middle);
            double len2 = middle.DistanceTo(end);
            double scale = len1 / (len1 + len2);

            // 角平分线与对边交点
            PointD point1 = new PointD();
            VectorD vector1 = end - start;
            point1 = start + vector1.ScaleBy(scale);
            return point1;
        }

        /// <summary>
        /// 返回两线夹角，弧度制结果，范围[0，π]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="middle"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static double GetAngleOfTwoLine(PointD start, PointD middle, PointD end)
        {
            double dx1 = start.X - middle.X;
            double dy1 = start.Y - middle.Y;
            double dx2 = end.X - middle.X;
            double dy2 = end.Y - middle.Y;

            double c = Math.Sqrt(dx1 * dx1 + dy1 * dy1) * Math.Sqrt(dx2 * dx2 + dy2 * dy2);
            double angle = Math.Acos((dx1 * dx2 + dy1 * dy2) / c);
            return angle;
        }
        /// <summary>
        /// 取两点所在直线上的指定长度的向量
        /// </summary>
        /// <param name="lineStart">线起点</param>
        /// <param name="lineEnd">线终点</param>
        /// <param name="length">向量长度</param>
        /// <returns></returns>
        private static VectorD GetVectorOnLineByLength(PointD lineStart, PointD lineEnd, double length)
        {
            double scale = length / lineStart.DistanceTo(lineEnd);
            VectorD vectorOld = lineEnd - lineStart;
            return vectorOld.ScaleBy(scale);
        }

        #endregion
    }
}
