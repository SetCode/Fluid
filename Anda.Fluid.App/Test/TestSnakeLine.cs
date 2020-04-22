using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Anda.Fluid.App.Test
{
    public partial class TestSnakeLine : Form
    {
        private List<PointD> points = new List<PointD>();

        private List<LineCoordinate> lineCooridinates = new List<LineCoordinate>();

        public TestSnakeLine()
        {
            InitializeComponent();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (points.Count < 3)
            {
                points.Add(new PointD(e.X, e.Y));
            }
            if (points.Count == 3)
            {
                // 计算
                calculate();
            }
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            foreach (PointD point in points)
            {
                g.FillEllipse(Brushes.Black, (float)point.X, (float)point.Y, 4, 4);
            }
            if (points.Count == 3)
            {
                foreach (LineCoordinate line in lineCooridinates)
                {
                    g.DrawLine(Pens.Red, (float)line.Start.X, (float)line.Start.Y, (float)line.End.X, (float)line.End.Y);
                    g.FillEllipse(Brushes.Blue, (float)line.End.X, (float)line.End.Y, 6, 6);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            points.Clear();
            lineCooridinates.Clear();
            panel1.Invalidate();
        }

        private void calculate()
        {
            int num = int.Parse(tbNum.Text);
            if (num < 2)
            {
                throw new Exception("error");
            }
            PointD p1 = points[0];
            PointD p2 = points[1];
            PointD p3 = points[2];
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            if (dx == 0 && dy == 0)
            {
                throw new Exception("error");
            }
            PointD start = null, end = null;
            double gap = 0;
            if (dx == 0)
            {
                gap = Math.Abs(p3.X - p1.X) / (num - 1);
                gap = p3.X > p1.X ? gap : -gap;
                for (int i = 0; i < num; i++)
                {
                    if (i % 2 == 0)
                    {
                        start = new PointD(p1.X + gap * i, p1.Y);
                        end = new PointD(p1.X + gap * i, p2.Y);
                    }
                    else
                    {
                        start = new PointD(p1.X + gap * i, p2.Y);
                        end = new PointD(p1.X + gap * i, p1.Y);
                    }
                    lineCooridinates.Add(new LineCoordinate(start, end));
                }
            }
            else if (dy == 0)
            {
                gap = Math.Abs(p1.Y - p3.Y) / (num - 1);
                gap = p3.Y > p1.Y ? gap : -gap;
                for (int i = 0; i < num; i++)
                {
                    if (i % 2 == 0)
                    {
                        start = new PointD(p1.X, p1.Y + gap * i);
                        end = new PointD(p2.X, p1.Y + gap * i);
                    }
                    else
                    {
                        start = new PointD(p2.X, p1.Y + gap * i);
                        end = new PointD(p1.X, p1.Y + gap * i);
                    }
                    lineCooridinates.Add(new LineCoordinate(start, end));
                }
            }
            else
            {
                double k = (p1.Y - p2.Y) / (p1.X - p2.X);
                // 求p3 到直线(p1, p2)的垂足
                PointD vfoot = MathUtils.GetVerticalFoot(p3, p1, k);
                lineCooridinates.Add(new LineCoordinate(new PointD(p1), new PointD(p2)));
                // p3到直线(p1, p2)的垂线与当前直线的交点
                PointD p = new PointD();
                for (int i = 1; i < num; i++)
                {
                    p.X = vfoot.X + (p3.X - vfoot.X) * i / (num - 1);
                    p.Y = vfoot.Y + (p3.Y - vfoot.Y) * i / (num - 1);
                    if (i % 2 == 0)
                    {
                        start = MathUtils.GetVerticalFoot(p1, p, k);
                        end = MathUtils.GetVerticalFoot(p2, p, k);
                    }
                    else
                    {
                        start = MathUtils.GetVerticalFoot(p2, p, k);
                        end = MathUtils.GetVerticalFoot(p1, p, k);
                    }
                    lineCooridinates.Add(new LineCoordinate(start, end));
                }

            }
        }
    }
}
