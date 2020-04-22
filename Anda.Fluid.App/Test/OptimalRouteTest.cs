using Anda.Fluid.Infrastructure.Algo;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.Test
{
    public partial class OptimalRouteTest : Form
    {
        private List<PointD> points = new List<PointD>();
        private List<PointD> route = new List<PointD>();

        public OptimalRouteTest()
        {
            InitializeComponent();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Debug.Print("panel1 mouse clicked");
            points.Add(new PointD(e.X, e.Y));
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Debug.Print("panel1 paint");
            var g = e.Graphics;
            foreach (PointD p in points)
            {
                g.FillEllipse(Brushes.Black, (float)p.X, (float)p.Y, 4, 4);
            }
            for (int i = 0; i < route.Count - 1; i++)
            {
                g.DrawLine(Pens.Red, (float)route[i].X, (float)route[i].Y, (float)route[i + 1].X, (float)route[i+1].Y);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            route.Clear();
            int[] routeIndexArr = OptimalRoute.Calculate(points);
            foreach (int index in routeIndexArr)
            {
                route.Add(points[index]);
            }
            panel1.Invalidate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            points.Clear();
            route.Clear();
            panel1.Invalidate();
        }

    }
}
