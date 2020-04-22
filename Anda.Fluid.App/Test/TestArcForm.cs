using Anda.Fluid.Domain.FluProgram.Executant;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anda.Fluid.App
{
    public partial class TestArcForm : Form
    {
        private PointD start;
        private PointD mid;
        private PointD end;
        private PointD center;
        private double r;
        private double degree;
        private int count = 0;

        public TestArcForm() : this(null)
        {
        }

        public TestArcForm(Arc arc)
        {
            InitializeComponent();
            if (arc != null)
            {
                start = arc.Start;
                mid = arc.Middle;
                end = arc.End;
                center = arc.Center;
                degree = arc.Degree;
                r = MathUtils.Distance(center, start);
            }
        }

        private void TestArcForm_Paint(object sender, PaintEventArgs eventArgs)
        {
            PointD s, m, e, c;
            Graphics g = eventArgs.Graphics;
            if (count == 1)
            {
                s = product2Draw(start);
                g.DrawEllipse(Pens.Black, (float)(s.X), (float)(s.Y), 4, 4);
            }
            else if (count == 2)
            {
                s = product2Draw(start);
                m = product2Draw(mid);
                g.DrawEllipse(Pens.Black, (float)(s.X), (float)(s.Y), 4, 4);
                g.DrawEllipse(Pens.Black, (float)(m.X), (float)(m.Y), 4, 4);
            }
            else if (count == 3)
            {
                s = product2Draw(start);
                m = product2Draw(mid);
                e = product2Draw(end);
                c = product2Draw(center);
                g.DrawEllipse(Pens.Black, (float)(s.X), (float)(s.Y), 4, 4);
                g.DrawEllipse(Pens.Black, (float)(m.X), (float)(m.Y), 4, 4);
                g.DrawEllipse(Pens.Black, (float)(e.X), (float)(e.Y), 4, 4);
                g.DrawEllipse(Pens.Black, (float)(c.X), (float)(c.Y), 4, 4);

                double sin = (start.Y - center.Y) / r;
                double startangle = Math.Asin(sin) * 180 / Math.PI;

                PointD acc = product2Draw(calAccStartPosition(60));
                PointD decel = product2Draw(calDecelEndPosition(60));

                g.DrawLine(Pens.Red, (float)(acc.X), (float)(acc.Y), (float)(s.X), (float)(s.Y));
                g.DrawLine(Pens.Green, (float)(e.X), (float)(e.Y), (float)(decel.X), (float)(decel.Y));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void TestArcForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (count == 0)
            {
                start = draw2Product(new PointD(e.X, e.Y));
            }
            else if (count == 1)
            {
                mid = draw2Product(new PointD(e.X, e.Y));
            }
            else if (count == 2)
            {
                end = draw2Product(new PointD(e.X, e.Y));
                center = MathUtils.CalculateCircleCenter(start, mid, end);
                degree = MathUtils.CalculateDegree(start, mid, end, center);
                r = MathUtils.Distance(center, start);
            }
            count++;
            count = count % 4;
            this.Invalidate();
        }

        private PointD draw2Product(PointD point)
        {
            PointD p = new PointD();
            p.X = point.X;
            p.Y = this.ClientRectangle.Height - point.Y;
            return p;
        }

        private PointD product2Draw(PointD point)
        {
            PointD p = new PointD();
            p.X = point.X;
            p.Y = this.ClientRectangle.Height - point.Y;
            return p;
        }

        /// <summary>
        /// 根据加速区间，计算加速起始位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="accDistance"></param>
        /// <returns></returns>
        private PointD calAccStartPosition(double accDistance)
        {
            PointD position = new PointD();
            double cos = (start.Y - center.Y) / r;
            double sin = (start.X - center.X) / r;
            if (degree > 0)
            {
                position.X = start.X + accDistance * cos;
                position.Y = start.Y - accDistance * sin;
            }
            else
            {
                position.X = start.X - accDistance * cos;
                position.Y = start.Y + accDistance * sin;
            }
            return position;
        }

        /// <summary>
        /// 根据减速区间，计算减速结束位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="decelDistance"></param>
        /// <returns></returns>
        private PointD calDecelEndPosition(double decelDistance)
        {
            PointD position = new PointD();
            double cos = (end.Y - center.Y) / r;
            double sin = (end.X - center.X) / r;
            if (degree > 0)
            {
                position.X = end.X - decelDistance * cos;
                position.Y = end.Y + decelDistance * sin;
            }
            else
            {
                position.X = end.X + decelDistance * cos;
                position.Y = end.Y - decelDistance * sin;
            }
            return position;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            count = 0;
            this.Invalidate();
        }
    }
}
