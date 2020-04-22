
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.Utils.MathUtils
{
    public static class Geometry
    {
        /// <summary>
        /// 判断点是否在圆弧上
        /// </summary>
        /// <param name="point"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool PointInArc(PointF point, PointF arcCenter, PointF startPoint, PointF endPoint, float degree, float arcWidth)
        {
            float radius = (float)Math.Sqrt(Math.Pow((double)startPoint.X - (double)arcCenter.X, 2) + Math.Pow((double)startPoint.Y - (double)arcCenter.Y, 2));

            if ((radius - arcWidth) < DisP2P(point, arcCenter) && DisP2P(point, arcCenter) < (radius + arcWidth))
            {
                //是圆环的情况
                if (degree == 360 || degree == -360)
                    return true;

                else
                {
                    //将圆弧的起始点、终点和鼠标点击的点坐标都转换为以圆弧中心为基准
                    PointF center = new PointF(0, 0);
                    PointF start = new PointF(startPoint.X - arcCenter.X, startPoint.Y < arcCenter.Y ? Math.Abs(startPoint.Y - arcCenter.Y) : -Math.Abs(startPoint.Y - arcCenter.Y));
                    PointF end = new PointF(endPoint.X - arcCenter.X, endPoint.Y < arcCenter.Y ? Math.Abs(endPoint.Y - arcCenter.Y) : -Math.Abs(endPoint.Y - arcCenter.Y));
                    PointF clickPoint = new PointF(point.X - arcCenter.X, point.Y < arcCenter.Y ? Math.Abs(point.Y - arcCenter.Y) : -Math.Abs(point.Y - arcCenter.Y));

                    double angle1 = LinesAngle(center, start, end, degree);
                    double angle2 = LinesAngle(center, start, clickPoint, degree);

                    if (angle1 > 0 && angle2 > 0 && angle2 < angle1)
                    {
                        return true;
                    }
                    if (angle1 < 0 && angle2 < 0 && angle2 > angle1)
                    {
                        return true;
                    }
                    return false;
                }

            }
            return false;
        }

        /// <summary>
        /// 判断点是否在圆环上
        /// </summary>
        /// <param name="point"></param>
        /// <param name="circleCenter"></param>
        /// <param name="radius"></param>
        /// <param name="circleWidth"></param>
        /// <returns></returns>
        public static bool PointInCircle(PointF point, PointF circleCenter, float radius, float circleWidth)
        {
            if ((radius - circleWidth) < DisP2P(point, circleCenter) && DisP2P(point, circleCenter) < (radius + circleWidth))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断点是否在直线上
        /// </summary>
        /// <param name="point"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <param name="lineWidth"></param>
        /// <returns></returns>
        public static bool PointInLine(PointF point, PointF lineStart, PointF lineEnd, float lineWidth)
        {
            float dis = DisP2Line(point, lineStart, lineEnd);
            if (dis < lineWidth)
            {
                if (point.X < Math.Max(lineStart.X, lineEnd.X) + lineWidth * 0.5 && point.X > Math.Min(lineStart.X, lineEnd.X) - lineWidth * 0.5)
                {
                    if (point.Y < Math.Max(lineStart.Y, lineEnd.Y) + lineWidth * 0.5 && point.Y > Math.Min(lineStart.Y, lineEnd.Y) - lineWidth * 0.5)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断点是否在实心圆上
        /// </summary>
        /// <returns></returns>
        public static bool PointInDot(PointF point, PointF dotCenter, float radius)
        {
            if (DisP2P(point, dotCenter) < radius)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断矩形是否包含圆弧
        /// </summary>
        /// <returns></returns>
        public static bool RectContainArc(RectangleF rect, PointF arcCenter, PointF arcStartPoint, PointF arcEndPoint)
        {
            return rect.Contains(arcCenter) && rect.Contains(arcEndPoint) && rect.Contains(arcStartPoint);
        }

        /// <summary>
        /// 判断矩形是否包含圆
        /// </summary>
        /// <returns></returns>
        public static bool RectContainCircle(RectangleF rect, PointF circleCenter, float radius)
        {
            return rect.Contains(circleCenter) && rect.Contains(new PointF(circleCenter.X - radius, circleCenter.Y))
                && rect.Contains(new PointF(circleCenter.X, circleCenter.Y - radius))
                && rect.Contains(new PointF(circleCenter.X + radius, circleCenter.Y))
                && rect.Contains(new PointF(circleCenter.X, circleCenter.Y + radius));
        }

        public static bool RectContainLine(RectangleF rect, PointF lineStart, PointF lineEnd)
        {
            return rect.Contains(lineStart) && rect.Contains(lineEnd);
        }


        /// <summary>
        /// 点到点距离
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        private static float DisP2P(PointF point1, PointF point2)
        {
            return (float)Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        /// <summary>
        /// 点到线距离
        /// </summary>
        /// <param name="point"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private static float DisP2Line(PointF point, PointF startPoint, PointF endPoint)
        {
            //先根据首尾点计算直线方程系数AX+BY+C=0
            float A = endPoint.Y - startPoint.Y;
            float B = startPoint.X - endPoint.X;
            float C = (endPoint.X - startPoint.X) * startPoint.Y - (endPoint.Y - startPoint.Y) * startPoint.X;

            return (float)(Math.Abs(A * point.X + B * point.Y + C) / (Math.Sqrt(A * A + B * B)));
        }

        /// <summary>
        /// 求起点相同的两条线段的夹角
        /// </summary>
        private static double LinesAngle(PointF start, PointF end1, PointF end2, float degree)
        {
            double line1x = end1.X - start.X;
            double line1y = end1.Y - start.Y;
            double line2x = end2.X - start.X;
            double line2y = end2.Y - start.Y;

            double line1Angle = Math.Atan2(line1y, line1x) * 180 / Math.PI;
            double line2Angle = Math.Atan2(line2y, line2x) * 180 / Math.PI;

            //角度 > 0时为逆时针
            if (degree > 0)
            {
                return line2Angle - line1Angle > 0 ? line2Angle - line1Angle : line2Angle - line1Angle + 360;
            }
            else
            {
                return line2Angle - line1Angle > 0 ? line2Angle - line1Angle - 360 : line2Angle - line1Angle;
            }
        }

    }
}
