using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Anda.Fluid.App.Test
{
    // MathUtils.CalculateDegree
    // MathUtils.CalculateMiddleAndEndPoint
    public partial class TestCalculateCircleCenterForm : Form
    {
        private PointD[] points = null;

        public TestCalculateCircleCenterForm()
        {
            InitializeComponent();
        }

        private void TestCalculateCircleCenterForm_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "x:" + e.X + ", y:" + e.Y;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Invalidate();
        }

        private void TestCalculateCircleCenterForm_Paint(object sender, PaintEventArgs ea)
        {
            Graphics g = ea.Graphics;
            g.FillEllipse(Brushes.Red, (int)tbP1X.Value, (int)tbP1Y.Value, 8, 8);
            g.FillEllipse(Brushes.Green, (int)tbP2X.Value, (int)tbP2Y.Value, 8, 8);
            g.FillEllipse(Brushes.Blue, (int)tbP3X.Value, (int)tbP3Y.Value, 8, 8);
            // 计算圆心
            //PointD s = logic2draw(new PointD(tbP1X.Value, tbP1Y.Value));
            //PointD m = logic2draw(new PointD(tbP2X.Value, tbP2Y.Value));
            //PointD e = logic2draw(new PointD(tbP3X.Value, tbP3Y.Value));
            PointD s = new PointD(99.603, 47.149);
            PointD m = new PointD(116.783, 90.134);
            PointD e = new PointD(156.078, 78.414);
            PointD c = MathUtils.CalculateCircleCenter(s, m, e);
            double r = MathUtils.Distance(c, s);
            double degree = MathUtils.CalculateDegree(s, m, e, c);
            PointD accStart = calAccStartPosition(s, c, r, degree, 50);
            PointD decelEnd = calDecelEndPosition(e, c, r, degree, 50);
            points = calFixedTotalOfDots(s, c, e, degree < 0, MathUtils.Distance(s, c), 1000);
            c = logic2draw(c);
            g.FillEllipse(Brushes.Black, (int)c.X, (int)c.Y, 8, 8);
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = logic2draw(points[i]);
                g.FillEllipse(Brushes.Black, (int)points[i].X, (int)points[i].Y, 4, 4);
            }
            accStart = logic2draw(accStart);
            s = logic2draw(s);
            e = logic2draw(e);
            decelEnd = logic2draw(decelEnd);
            g.DrawLine(Pens.Red, (float)accStart.X, (float)accStart.Y, (float)s.X, (float)s.Y);
            g.DrawLine(Pens.Blue, (float)e.X, (float)e.Y, (float)decelEnd.X, (float)decelEnd.Y);
        }

        private PointD logic2draw(PointD p)
        {
            return new PointD(p.X, Height - p.Y);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
        }

        private PointD[] calFixedTotalOfDots(PointD s, PointD c, PointD e, bool isClockwise, double r, int shots)
        {
            PointD[] points = null;
            if (shots <= 0)
            {
                points = new PointD[0];
            }
            else
            {
                double startRadian = MathUtils.GetRadianOnCircle(c, s, r);
                double endRadian = MathUtils.GetRadianOnCircle(c, e, r);
                if (shots == 1)
                {
                    points = new PointD[1];
                    if (isClockwise)
                    {
                        if (startRadian > endRadian)
                        {
                            points[0] = MathUtils.GetPointOnCircle(c, r, (startRadian + endRadian) / 2);
                        }
                        else
                        {
                            points[0] = MathUtils.GetPointOnCircle(c, r, (startRadian + endRadian + 2 * Math.PI) / 2);
                        }
                    }
                    else
                    {
                        if (startRadian >= endRadian)
                        {
                            points[0] = MathUtils.GetPointOnCircle(c, r, (startRadian + endRadian + 2 * Math.PI) / 2);
                        }
                        else
                        {
                            points[0] = MathUtils.GetPointOnCircle(c, r, (startRadian + endRadian) / 2);
                        }
                    }
                }
                else
                {
                    points = new PointD[shots];
                    double start = 0;
                    double gap = 0;
                    if (isClockwise)
                    {
                        if (startRadian > endRadian)
                        {
                            start = startRadian;
                            gap = -(start - endRadian) / (shots - 1);
                        }
                        else
                        {
                            start = startRadian + 2 * Math.PI;
                            gap = -(start - endRadian) / (shots - 1);
                        }
                    }
                    else
                    {
                        if (startRadian >= endRadian)
                        {
                            start = startRadian;
                            gap = (endRadian + 2 * Math.PI - start) / (shots - 1);
                        }
                        else
                        {
                            start = startRadian;
                            gap = (endRadian - start) / (shots - 1);
                        }
                    }

                    for (int i = 0; i < shots; i++)
                    {
                        points[i] = MathUtils.GetPointOnCircle(c, r, startRadian + gap * i);
                    }
                }
            }
            return points;
        }

        /// <summary>
        /// 根据加速区间，计算加速起始位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="accDistance"></param>
        /// <returns></returns>
        private PointD calAccStartPosition(PointD s, PointD c, double r, double degree, double accDistance)
        {
            PointD position = new PointD();
            double cos = (s.Y - c.Y) / r;
            double sin = (s.X - c.X) / r;
            if (degree > 0)
            {
                position.X = s.X + accDistance * cos;
                position.Y = s.Y - accDistance * sin;
            }
            else
            {
                position.X = s.X - accDistance * cos;
                position.Y = s.Y + accDistance * sin;
            }
            return position;
        }

        /// <summary>
        /// 根据减速区间，计算减速结束位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="decelDistance"></param>
        /// <returns></returns>
        private PointD calDecelEndPosition(PointD e, PointD c, double r, double degree, double decelDistance)
        {
            PointD position = new PointD();
            double cos = (e.Y - c.Y) / r;
            double sin = (e.X - c.X) / r;
            if (degree > 0)
            {
                position.X = e.X - decelDistance * cos;
                position.Y = e.Y + decelDistance * sin;
            }
            else
            {
                position.X = e.X + decelDistance * cos;
                position.Y = e.Y - decelDistance * sin;
            }
            return position;
        }

    }
}
