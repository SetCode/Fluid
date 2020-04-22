using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Anda.Fluid.Infrastructure.Utils
{
    public class MathUtils
    {
        public static byte Limit(byte value, byte min, byte max)
        {
            return value < min ? min : (value > max ? max : value);
        }

        public static short Limit(short value, short min, short max)
        {
            return value < min ? min : (value > max ? max : value);
        }

        public static int Limit(int value, int min, int max)
        {
            return value < min ? min : (value > max ? max : value);
        }

        public static long Limit(long value, long min, long max)
        {
            return value < min ? min : (value > max ? max : value);
        }

        public static float Limit(float value, float min, float max)
        {
            return value < min ? min : (value > max ? max : value);
        }

        public static double Limit(double value, double min, double max)
        {
            return value < min ? min : (value > max ? max : value);
        }

        public static double Limit(double value, double min, double max1, double max2)
        {
            return Limit(value, min, Math.Min(max1, max2));
        }

        public static int Limit(int value, int min, int max1, int max2)
        {
            return Limit(value, min, Math.Min(max1, max2));
        }

        public static bool Compare(double value, double target, double tolerance)
        {
            if (Math.Abs(target - value) > Math.Abs(tolerance))
            {
                return false;
            }
            return true;
        }

        public static double Distance(int x, int y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        public static double Distance(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        public static double Distance(PointD point1, PointD point2)
        {
            return Distance(point1.X - point2.X, point1.Y - point2.Y);
        }

        /// <summary>
        /// 根据三点坐标计算圆心坐标
        /// </summary>
        /// <param name="start"></param>
        /// <param name="middle"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static PointD CalculateCircleCenter(PointD start, PointD middle, PointD end)
        {
            PointD center = new PointD();
            double a1, a2, b1, b2, c1, c2, temp;
            a1 = start.X - middle.X;
            b1 = start.Y - middle.Y;
            c1 = (Math.Pow(start.X, 2) - Math.Pow(middle.X, 2) + Math.Pow(start.Y, 2) - Math.Pow(middle.Y, 2)) / 2;
            a2 = end.X - middle.X;
            b2 = end.Y - middle.Y;
            c2 = (Math.Pow(end.X, 2) - Math.Pow(middle.X, 2) + Math.Pow(end.Y, 2) - Math.Pow(middle.Y, 2)) / 2;
            temp = a1 * b2 - a2 * b1;
            // 三点共线
            if (temp == 0)
            {
                //共线只有一种情况是合法的： start 和 middle 相等， 此时这三个点描述的是一个完整的圆，而非一段弧线
                center.X = (start.X + middle.X) / 2;
                center.Y = (start.Y + middle.Y) / 2;
            }
            // 三点不共线
            else
            {
                //不共线则求出圆心：
                //center.x = (C1*B2 - C2*B1) / A1*B2 - A2*B1;
                //center.y = (A1*C2 - A2*C1) / A1*B2 - A2*B1;
                center.X = (c1 * b2 - c2 * b1) / temp;
                center.Y = (a1 * c2 - a2 * c1) / temp;
            }
            return center;
        }

        public static PointF CalculateCircleCenter(PointF start, PointF middle, PointF end)
        {
            PointF center = new PointF();
            float a1, a2, b1, b2, c1, c2, temp;
            a1 = start.X - middle.X;
            b1 = start.Y - middle.Y;
            c1 = ((float)Math.Pow(start.X, 2) - (float)Math.Pow(middle.X, 2) + (float)Math.Pow(start.Y, 2) - (float)Math.Pow(middle.Y, 2)) / 2;
            a2 = end.X - middle.X;
            b2 = end.Y - middle.Y;
            c2 = ((float)Math.Pow(end.X, 2) - (float)Math.Pow(middle.X, 2) + (float)Math.Pow(end.Y, 2) - (float)Math.Pow(middle.Y, 2)) / 2;
            temp = a1 * b2 - a2 * b1;
            // 三点共线
            if (temp == 0)
            {
                //共线只有一种情况是合法的： start 和 middle 相等， 此时这三个点描述的是一个完整的圆，而非一段弧线
                center.X = (start.X + middle.X) / 2;
                center.Y = (start.Y + middle.Y) / 2;
            }
            // 三点不共线
            else
            {
                //不共线则求出圆心：
                //center.x = (C1*B2 - C2*B1) / A1*B2 - A2*B1;
                //center.y = (A1*C2 - A2*C1) / A1*B2 - A2*B1;
                center.X = (float)(c1 * b2 - c2 * b1) / temp;
                center.Y = (float)(a1 * c2 - a2 * c1) / temp;
            }
            return center;
        }

        /// <summary>
        /// 根据圆上的三点及圆心，计算圆的角度，逆时针为正，顺时针为负，取值范围：{-360, 360}
        /// </summary>
        /// <param name="start">圆弧起始点</param>
        /// <param name="middle">圆弧中间点</param>
        /// <param name="end">圆弧终点</param>
        /// <param name="center">圆心</param>
        /// <returns></returns>
        public static double CalculateCircleDegree(PointD start, PointD middle, PointD end, PointD center)
        {
            double circleDegree = CalculateDegree(start, middle, end, center);
            circleDegree = circleDegree >= 0 ? 360 : -360;
            return circleDegree;
        }

        /// <summary>
        /// 根据圆弧上的三点及圆心，计算弧的角度，逆时针为正，顺时针为负，取值范围：(-360, 360)
        /// </summary>
        /// <param name="start">圆弧起始点</param>
        /// <param name="middle">圆弧中间点</param>
        /// <param name="end">圆弧终点</param>
        /// <param name="center">圆心</param>
        /// <returns></returns>
        public static double CalculateDegree(PointD start, PointD middle, PointD end, PointD center)
        {
            double startArc = CalculateArc(center, start);
            double middleArc = CalculateArc(center, middle);
            double endArc = CalculateArc(center, end);
            if (startArc == endArc)
            {
                return startArc == middleArc ? 0f : 360f;
            }
            if (startArc < endArc)
            {
                if (middleArc >= startArc && middleArc <= endArc)
                {
                    return endArc - startArc;
                }
                else
                {
                    return endArc - startArc - 360f;
                }
            }
            else // startArc > endArc
            {
                if (middleArc >= endArc && middleArc <= startArc)
                {
                    return endArc - startArc;
                }
                else
                {
                    return 360f - (startArc - endArc);
                }
            }
        }
        /// <summary>
        /// 根据圆弧上的2点、圆心及圆弧方向，计算弧的角度，逆时针为正，顺时针为负，取值范围：(-360, 360)
        /// </summary>
        /// <param name="start">圆弧起点</param>
        /// <param name="end">圆弧终点</param>
        /// <param name="center">圆弧圆心</param>
        /// <param name="clockwise">圆弧方向</param>
        /// <returns></returns>
        public static double CalculateDegree(PointD start, PointD end, PointD center, int clockwise)
        {
            double startArc = CalculateArc(center, start);
            double endArc = CalculateArc(center, end);
            if (startArc < endArc)
            {
                if (clockwise == 1)
                {
                    return endArc - startArc;
                }
                else
                {
                    return endArc - startArc - 360f;
                }
            }
            else // startArc > endArc
            {
                if (clockwise == 0)
                {
                    return endArc - startArc;
                }
                else
                {
                    return 360f - (startArc - endArc);
                }
            }
        }

        /// <summary>
        /// 获取两点构成的直线的角度
        /// X轴正方向为0°，逆时针旋转为正向，角度值范围：[0, 360)
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">终点</param>
        /// <returns></returns>
        public static double CalculateArc(PointD start, PointD end)
        {
            double dx = end.X - start.X;
            double dy = end.Y - start.Y;
            if (dx == 0)
            {
                return dy > 0 ? 90f : (dy == 0 ? 0f : 270f);
            }
            double arc = Math.Atan2(end.Y - start.Y, dx) / Math.PI * 180;
            arc = arc < 0 ? 360 + arc : arc;
            return arc;
        }
        /// <summary>
        /// 计算两点之间的角度 [0-180]
        /// </summary>
        /// <returns></returns>
        public static double CalculateDegree(PointD start, PointD end)
        {
            double dx = end.X - start.X;
            double dy = end.Y - start.Y;
            double length = start.DistanceTo(end);
            if (length == 0)
            {

                return 0;
            }
            double cos = dx / length;
            double arc = Math.Acos(cos) / Math.PI * 180;
            return arc;
        }


        /// <summary>
        /// 根据圆弧起始点、圆心、弧度，计算圆弧中间点、终点
        /// </summary>
        /// <param name="start">圆弧起始点</param>
        /// <param name="center">圆心</param>
        /// <param name="degree">弧度</param>
        /// <returns></returns>
        public static PointD[] CalculateMiddleAndEndPoint(PointD start, PointD center, double degree)
        {
            PointD end = new PointD();
            PointD middle = new PointD();
            if (degree == 360f) // 针对圆的情况
            {
                end.X = start.X;
                end.Y = start.Y;
                middle.X = center.X - (start.X - center.X);
                middle.Y = center.Y - (start.Y - center.Y);
                return new PointD[] { middle, end };
            }
            if (degree == 0f)
            {
                middle.X = end.X = start.X;
                middle.Y = end.Y = start.Y;
                return new PointD[] { middle, end };
            }
            double startArc = CalculateArc(center, start);
            double endArc = normalizeDegree(startArc + degree);
            double dx = Math.Abs(start.X - center.X);
            double dy = Math.Abs(start.Y - center.Y);
            double r = Math.Sqrt(dx * dx + dy * dy);
            PointD point;
            if (endArc == 90f)
            {
                end.X = center.X;
                end.Y = center.Y + r;
            }
            else if (endArc == 270f)
            {
                end.X = center.X;
                end.Y = center.Y - r;
            }
            else
            {
                point = CalculatePointOnArc(center, r, endArc);
                end.X = point.X;
                end.Y = point.Y;
            }
            // 计算 start 和 end 之间的坐标
            double middleArc = (startArc + endArc) / 2f;
            if ((startArc > endArc && degree > 0)
                || (startArc < endArc && degree < 0))
            {
                middleArc = normalizeDegree(middleArc + 180f);
            }
            point = CalculatePointOnArc(center, r, middleArc);
            middle.X = point.X;
            middle.Y = point.Y;
            return new PointD[] { middle, end };
        }

        /// <summary>
        /// 根据圆心和角度（取值：[0, 360)，逆时针为正向），计算在圆上的坐标，发现该方法有错误
        /// </summary>
        /// <param name="center">圆心坐标</param>
        /// <param name="arc">角度</param>
        /// <returns></returns>
        //public static PointD CalculatePointOnArc(PointD center, double r, double arc)
        //{
        //    if (arc == 0f)
        //    {
        //        return new PointD(center.X + r, center.Y);
        //    }
        //    else if (arc == 90f)
        //    {
        //        return new PointD(center.X, center.Y + r);
        //    }
        //    else if (arc == 180f)
        //    {
        //        return new PointD(center.X - r, center.Y);
        //    }
        //    else if (arc == 270f)
        //    {
        //        return new PointD(center.X, center.Y - r);
        //    }
        //    double tan = Math.Tan((arc * Math.PI / 180));
        //    double a = 1 + tan * tan;
        //    double b = 2 * tan * (center.Y - center.X * tan);
        //    double c = (center.Y - center.X * tan) * (center.Y - center.X * tan) - r * r;
        //    double b4ac = b * b - 4 * a * c;
        //    // 无解
        //    if (b4ac < 0)
        //    {
        //        return new PointD(center.X + r, center.Y);
        //    }
        //    // 只有一个解
        //    else if (b4ac == 0)
        //    {
        //        double x = (-b) / (2 * a);
        //        double y = tan * (x - center.X) + center.Y;
        //        return new PointD(x, y);
        //    }
        //    // 有两个解
        //    else
        //    {
        //        double x1 = (-b + Math.Sqrt(b4ac)) / (2 * a);
        //        double y1 = tan * (x1 - center.X) + center.Y;
        //        double x2 = (-b - Math.Sqrt(b4ac)) / (2 * a);
        //        double y2 = tan * (x2 - center.X) + center.Y;
        //        if (arc < 180f)
        //        {
        //            if (y1 > center.Y)
        //            {
        //                return new PointD(x1, y1);
        //            }
        //            else
        //            {
        //                return new PointD(x2, y2);
        //            }
        //        }
        //        else
        //        {
        //            if (y1 < center.Y)
        //            {
        //                return new PointD(x1, y1);
        //            }
        //            else
        //            {
        //                return new PointD(x2, y2);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 根据圆心和角度（取值：[0, 360)，逆时针为正向），计算在圆上的坐标
        /// </summary>
        /// <param name="center">圆心坐标</param>
        /// <param name="arc">角度</param>
        /// <returns></returns>
        public static PointD CalculatePointOnArc(PointD center, double r, double arc)
        {
            PointD point = new PointD();
            point.X = center.X + r * Math.Cos(arc * Math.PI / 180);
            point.Y = center.Y + r * Math.Sin(arc * Math.PI / 180);
            return point;
        }

        /// <summary>
        /// 根据圆弧的起点和圆心，计算圆弧的起点相对于圆心坐标系的角度
        /// </summary>
        /// <param name="arcStart"></param>
        /// <param name="arcCenter"></param>
        /// <returns></returns>
        public static float CalculateStartDegreeOnArc(PointF arcStart, PointF arcCenter)
        {
            float startDegree = 0;
            //先判断是不是中心坐标系坐标轴上的点
            if (arcStart.Y == arcCenter.Y)
            {
                if (arcStart.X > arcCenter.X)
                {
                    startDegree = 0;
                }
                else
                {
                    startDegree = 180;
                }
            }
            else if (arcStart.X == arcCenter.X)
            {
                if (arcStart.Y < arcCenter.Y)
                {
                    startDegree = 270;
                }
                else
                {
                    startDegree = 90;
                }
            }

            //再判断处于四个象限时的情况
            else
            {
                //对边长度
                double oppositeSide = Math.Abs(arcStart.Y - arcCenter.Y);
                //斜边长度
                double hypotenuse = Math.Sqrt(Math.Pow(arcStart.X - arcCenter.X, 2) + Math.Pow(arcStart.Y - arcCenter.Y, 2));
                //初始角度
                startDegree = (float)(Math.Asin(oppositeSide / hypotenuse) * 180 / Math.PI);
                //在第一象限时
                if (arcStart.X > arcCenter.X && arcStart.Y < arcCenter.Y)
                {
                    startDegree = 360 - startDegree;
                }
                //在第二象限时
                else if (arcStart.X < arcCenter.X && arcStart.Y < arcCenter.Y)
                {
                    startDegree = 180 + startDegree;
                }
                //在第三象限时
                else if (arcStart.X < arcCenter.X && arcStart.Y > arcCenter.Y)
                {
                    startDegree = 180 - startDegree;
                }
                //在第四象限时
                else if (arcStart.X > arcCenter.X && arcStart.Y > arcCenter.Y)
                {

                }
            }

            return startDegree;
        }

        /// <summary>
        /// 将角度值转换成[0, 360)之间的值
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        private static double normalizeDegree(double degree)
        {
            int ndegree = (int)degree;
            double ex = degree - ndegree;
            ndegree = ndegree % 360;
            double real = ndegree + ex;
            real = real < 0 ? real + 360 : real;
            return real;
        }



        /// <summary>
        /// 根据起始速度、终点速度、加速度，计算运动距离
        /// </summary>
        /// <param name="startVel"></param>
        /// <param name="endVel"></param>
        /// <param name="accVel"></param>
        /// <returns></returns>
        public static double CalculateDistance(double startVel, double endVel, double accVel)
        {
            double t = (endVel - startVel) / accVel;
            double distance = startVel * t + accVel * t * t * 0.5;
            return distance;
        }


        /// <summary>
        /// 计算圆上的点的弧度，逆时针为正
        /// </summary>
        /// <param name="point">圆上的点</param>
        /// <param name="center">圆心坐标</param>
        /// <param name="r">半径</param>
        /// <returns></returns>
        public static double GetRadianOnCircle(PointD center, PointD point, double r)
        {
            if (point.X == center.X && point.Y == center.Y)
            {
                return 0;
            }
            double radian = Math.Asin((point.Y - center.Y) / r);
            // 第二象限  第三象限  第二象限与第三象限的交界
            if (point.X < center.X)
            {
                radian = Math.PI - radian;
            }
            else if (point.X == center.X)
            {
                // 第三象限与第四象限的交界
                if (point.Y < center.Y)
                {
                    radian = 1.5 * Math.PI;
                }
                // 第一象限与第二象限的交界
                else
                {
                    // 值保持不变
                }
            }
            else
            {
                // 第四象限
                if (point.Y < center.Y)
                {
                    radian = 2 * Math.PI + radian;
                }
                // 第一象限  第一象限与第四象限的交界   
                else
                {
                    // 值保持不变
                }
            }
            return radian;
        }

        /// <summary>
        /// 计算圆上点的坐标
        /// </summary>
        /// <param name="center">圆心坐标</param>
        /// <param name="r">半径</param>
        /// <param name="radian">弧度，逆时针为正</param>
        /// <returns></returns>
        public static PointD GetPointOnCircle(PointD center, double r, double radian)
        {
            PointD point = new PointD();
            // 弧度 -> [0, 2*PI)
            const double PI2 = 2 * Math.PI;
            radian = radian - ((int)(radian / PI2)) * PI2;
            radian = radian < 0 ? radian + PI2 : radian;
            point.X = center.X + r * Math.Cos(radian);
            point.Y = center.Y + r * Math.Sin(radian);
            return point;
        }

        /// <summary>
        /// 已知一条直线上的一个点和直线的斜率(斜率存在且不为0)，求一个点在该直线上的垂足坐标
        /// </summary>
        /// <param name="point"></param>
        /// <param name="linePoint"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static PointD GetVerticalFoot(PointD point, PointD linePoint, double k)
        {
            // 设linePoint为 (a, b), 垂足为(x, y), point为(m, n)
            // 则有： (y-b)/(x-a) = k, (y-n)/(x-m) = -1/k
            // 解方程得 x = (a*k - b + n + m/k) / (k + 1/k), y = kx - ak + b
            double x = (linePoint.X * k - linePoint.Y + point.Y + point.X / k) / (k + 1 / k);
            double y = k * x - linePoint.X * k + linePoint.Y;
            return new PointD(x, y);
        }

        /// <summary>
        /// 判断三个点是否在一条直线上
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static bool IsInOneLine(PointD p1, PointD p2, PointD p3)
        {
            // 无斜率的情况
            if (p1.X == p2.X)
            {
                return p2.X == p3.X;
            }
            else
            {
                // 无斜率的情况
                if (p2.X == p3.X)
                {
                    return false;
                }
                return (p3.Y - p2.Y) / (p3.X - p2.X) == (p2.Y - p1.Y) / (p2.X - p1.X);
            }
        }

        /// <summary>
        /// 判断多条线段是否有首尾点重复
        /// </summary>
        public static List<LineRepetition> CheckLinesPepeat(List<PointD> points)
        {
            bool[] repetition = new bool[points.Count];
            List<LineRepetition> lineRepetition = new List<LineRepetition>();

            for (int i = 0; i < points.Count; i++)
            {
                //规定第一条线段的首点不是重复点
                if (i == 0)
                {
                    repetition[i] = false;
                }
                //选择一个数作为比较数
                PointD comparePoint = points[i];

                //同所有点进行循环比较
                for (int j = i + 1; j < points.Count; j++)
                {
                    //规定第一条线段的尾点不是重复点
                    if (j == 1)
                    {
                        repetition[j] = false;
                    }
                    //如果有相同的点,则认为是重复点
                    else if (comparePoint.Equals(points[j]))
                    {
                        repetition[j] = true;
                    }
                    else
                    {
                        //如果在之前已经被标记为了重复点，则不能标记为false
                        if (repetition[j] != true)
                        {
                            repetition[j] = false;
                        }
                    }
                }
            }

            //添加结果
            for (int i = 0; i < repetition.Length / 2; i++)
            {
                lineRepetition.Add(new LineRepetition(repetition[i * 2], repetition[i * 2 + 1]));
            }

            return lineRepetition;
        }

        /// <summary>
        /// 选择排序（返回值为排序结果和对应的原始位置）
        /// </summary>
        /// <param name="array"></param>
        /// <param name="isDESC">升序false 降序true</param>
        /// <returns></returns>
        public static Tuple<double[], int[]> SelectSort(double[] array, bool isDESC)
        {
            double[] value = array;
            int[] index = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                index[i] = i;
            }
            Tuple<double[], int[]> result = new Tuple<double[], int[]>(value, index);

            double tempValue = 0;
            int tempIndex = 0;

            for (int i = 0; i < result.Item1.Length - 1; i++)
            {
                double minVal = result.Item1[i];
                int minIndex = i;

                for (int j = i + 1; j < result.Item1.Length; j++)
                {
                    if (isDESC)
                    {
                        if (minVal < result.Item1[j])
                        {
                            minVal = result.Item1[j];
                            minIndex = j;
                        }
                    }
                    else
                    {
                        if (minVal > result.Item1[j])
                        {
                            minVal = result.Item1[j];
                            minIndex = j;
                        }
                    }
                }
                tempValue = result.Item1[i];
                result.Item1[i] = result.Item1[minIndex];
                result.Item1[minIndex] = tempValue;

                tempIndex = result.Item2[i];
                result.Item2[i] = result.Item2[minIndex];
                result.Item2[minIndex] = tempIndex;
            }

            return result;
        }
        /// <summary>
        /// 求点 oriPoint 关于linePoint1和 linePoint2 所在直线的对称点
        /// </summary>
        /// <param name="oriPoint">已知点</param>
        /// <param name="linePoint1">对称轴上一点</param>
        /// <param name="linePoint2">对称轴上另一点</param>
        /// <returns></returns>
        public static PointD GetSymmetricPoint(PointD oriPoint, PointD linePoint1, PointD linePoint2)
        {
            PointD result = new PointD();
            double x0 = oriPoint.X;
            double y0 = oriPoint.Y;
            double A = linePoint2.Y - linePoint1.Y;
            double B = linePoint1.X - linePoint2.X;
            double C = linePoint2.X * linePoint1.Y - linePoint1.X * linePoint2.Y;

            double k = -2 * (A * x0 + B * y0 + C) / (A * A + B * B);
            result.X = x0 + k * A;
            result.Y = y0 + k * B;
            return result;
        }

        /// <summary>
        /// 求过一点与直线的垂足
        /// </summary>
        /// <param name="point1">线外一点</param>
        /// <param name="linePoint1">线上点1</param>
        /// <param name="linePoint2">线上点2</param>
        /// <returns></returns>
        public static PointD GetFootPoint(PointD point1, PointD linePoint1, PointD linePoint2)
        {
            double A = linePoint2.Y - linePoint1.Y;
            double B = linePoint1.X - linePoint2.X;
            double C = linePoint2.X * linePoint1.Y - linePoint1.X * linePoint2.Y;
            PointD result = new PointD();
            result.X = ((B * B) * point1.X - A * B * point1.Y - A * C) / (A * A + B * B);
            result.Y = (-A * B * point1.X + A * A * point1.Y - B * C) / (A * A + B * B);
            return result;
        }
        /// <summary>
        /// 求点到直线距离
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="linePoint1"></param>
        /// <param name="linePoint2"></param>
        /// <returns></returns>
        public static double GetDistanceToLine(PointD p1, PointD linePoint1, PointD linePoint2)
        {
            double A = linePoint2.Y - linePoint1.Y;
            double B = linePoint1.X - linePoint2.X;
            double C = linePoint2.X * linePoint1.Y - linePoint1.X * linePoint2.Y;
            return (A * p1.X + B * p1.Y + C) / Math.Sqrt(A * A + B * B);
        }

        /// <summary>
        /// 判断两线段是否相交
        /// </summary>
        /// <param name="line1Start"></param>
        /// <param name="line1End"></param>
        /// <param name="line2Start"></param>
        /// <param name="line2End"></param>
        /// <returns></returns>
        private static bool isColide(PointD line1Start, PointD line1End, PointD line2Start, PointD line2End)
        {
            VectorD v1 = line1End - line1Start;
            VectorD v2 = line2Start - line1Start;
            VectorD v3 = line2End - line1Start;
            double temp = v1.CrossProduct(v3) * v1.CrossProduct(v2);
            return temp < 0.0 || temp > Math.Exp(-6);
        }


        #region 
        /// <summary>
        /// 计算平均值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double CalMean(double[] data)
        {
            //求和
            double sum = 0;
            int dataNums = data.Length; 
            for (int i = 0; i < dataNums; i++)
            {
                sum += data[i];
            }
            //平均值
            return sum / (double)dataNums;
        }
        /// <summary>
        /// 标准差
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double CalVariance(double[] data)
        {
            //平均值
            double mean=CalMean(data);
            double sumSqr = 0;
            double dataLenght = data.Length;
            for (int i = 0; i < dataLenght; i++)
            {
                sumSqr += Math.Pow((data[i] - mean), 2);
            }       
            return Math.Sqrt(sumSqr/dataLenght);
        }
        #endregion
    }
}
