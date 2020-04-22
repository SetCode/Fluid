using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anda.Fluid.App
{
    public partial class TestLineForm : Form
    {
        private bool isFirst = true;
        private PointD start = new PointD();
        private PointD end = new PointD();
        private LineCoordinate line;
        private PointD acc = new PointD();
        private bool showAcc = false;
        private PointD decel = new PointD();
        private bool showDecel = false;

        public TestLineForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showAcc = true;
            acc = calAccStartPosition(line, double.Parse(tbDistance.Text));
            panel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showDecel = true;
            decel = calDecelEndPosition(line, double.Parse(tbDistance.Text));
            panel1.Invalidate();
        }

        /// <summary>
        /// 根据加速区间，计算加速起始位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="accDistance"></param>
        /// <returns></returns>
        private PointD calAccStartPosition(LineCoordinate line, double accDistance)
        {
            PointD position = new PointD();
            double lineDistance = MathUtils.Distance(line.Start, line.End);
            double cos = (line.End.X - line.Start.X) / lineDistance;
            double sin = (line.End.Y - line.Start.Y) / lineDistance;
            position.X = line.Start.X - accDistance * cos;
            position.Y = line.Start.Y - accDistance * sin;
            return position;
        }

        /// <summary>
        /// 根据减速区间，计算减速结束位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="decelDistance"></param>
        /// <returns></returns>
        private PointD calDecelEndPosition(LineCoordinate line, double decelDistance)
        {
            PointD position = new PointD();
            double lineDistance = MathUtils.Distance(line.Start, line.End);
            double cos = (line.End.X - line.Start.X) / lineDistance;
            double sin = (line.End.Y - line.Start.Y) / lineDistance;
            position.X = line.End.X + decelDistance * cos;
            position.Y = line.End.Y + decelDistance * sin;
            return position;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (line == null)
            {
                if (!isFirst)
                {
                    g.FillEllipse(Brushes.Black, new RectangleF((float)(start.X), (float)(start.Y), 4, 4));
                }
            }
            else
            {
                g.FillEllipse(Brushes.Black, new RectangleF((float)(line.Start.X), (float)(line.Start.Y), 4, 4));
                g.FillEllipse(Brushes.Black, new RectangleF((float)(line.End.X), (float)(line.End.Y), 4, 4));
                g.DrawLine(Pens.Black, (float)(line.Start.X), (float)(line.Start.Y), (float)(line.End.X), (float)(line.End.Y));
            }
            if (showAcc)
            {
                g.FillEllipse(Brushes.Black, new RectangleF((float)(line.Start.X), (float)(line.Start.Y), 4, 4));
                g.FillEllipse(Brushes.Black, new RectangleF((float)(line.End.X), (float)(line.End.Y), 4, 4));
                g.FillEllipse(Brushes.Black, new RectangleF((float)(acc.X), (float)(acc.Y), 4, 4));
                g.DrawLine(Pens.Black, (float)(acc.X), (float)(acc.Y), (float)(line.Start.X), (float)(line.Start.Y));
            }
            if (showDecel)
            {
                g.FillEllipse(Brushes.Black, new RectangleF((float)(line.Start.X), (float)(line.Start.Y), 4, 4));
                g.FillEllipse(Brushes.Black, new RectangleF((float)(line.End.X), (float)(line.End.Y), 4, 4));
                g.FillEllipse(Brushes.Black, new RectangleF((float)(decel.X), (float)(decel.Y), 4, 4));
                g.DrawLine(Pens.Black, (float)(line.End.X), (float)(line.End.Y), (float)(decel.X), (float)(decel.Y));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            line = null;
            isFirst = true;
            showAcc = false;
            showDecel = false;
            panel1.Invalidate();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (line == null)
            {
                if (isFirst)
                {
                    start.X = e.X;
                    start.Y = e.Y;
                    isFirst = false;
                }
                else
                {
                    end.X = e.X;
                    end.Y = e.Y;
                    line = new LineCoordinate(start, end);
                }
            }
            panel1.Invalidate();
        }
    }
}
