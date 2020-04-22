using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using System.Windows.Forms.DataVisualization.Charting;

namespace Anda.Fluid.App.LoadTrajectory
{
    public partial class TrajectoryChart : UserControl
    {
        private List<PointD> points=new List<PointD>();
        private double distance=2;
        private Dictionary<double,PointD> pointDis = new Dictionary<double, PointD>();
        private PointD pointSelected;

        //private PointD mark1 = new PointD();
        //private PointD mark2 = new PointD();
        private MarkPoint mark1 = new MarkPoint();
        private MarkPoint mark2 = new MarkPoint();

        private List<double> xList = new List<double>();
        private List<double> yList = new List<double>();

        private double minX;
        private double maxX;
        private double minY;
        private double maxY;

        private Action<MarkPoint,string> setMark;
        private bool mouseMiddIsClick = false;
        private bool mouseIsMoving = false;
        private Point mouseLastPoint = new Point();
                 
        public TrajectoryChart()
        {            
            InitializeComponent();

            ContextMenuStrip chartCompM = new ContextMenuStrip();
            chartCompM.Items.Add("ResetZoom").Name = "ResetZoom";
            chartCompM.Items.Add("DistanseSetting").Name = "DistanseSetting";
            chartCompM.Items.Add("Mark1").Name = "Mark1";
            chartCompM.Items.Add("Mark2").Name = "Mark2";
            chartCompM.Items.Add("ClearMarks").Name = "ClearMarks";
            chartCompM.ItemClicked += chartCompM_ItemClicked;

            this.chart1.ChartAreas[0].AxisX.RoundAxisValues();
            this.chart1.ChartAreas[0].AxisY.RoundAxisValues();

            this.chart1.ContextMenuStrip = chartCompM;
            this.MouseWheel += MouseWheeled;
        }
        public void Setup(Action<MarkPoint,string> setMark)
        {
            this.setMark = setMark;
        }
        /// <summary>
        /// 滚轮缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseWheeled(object sender, MouseEventArgs e)
        {
            this.Focus();
            if (this.mouseIsMoving)
                return;
            this.chart1.Cursor = Cursors.SizeAll;            
            minSVX = this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ViewMinimum;
            maxSVX = this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ViewMaximum;
            minSVY = this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ViewMinimum;
            maxSVY = this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ViewMaximum;
            double xValue = this.chart1.ChartAreas["ChartArea1"].AxisX.PixelPositionToValue(e.X);
            double yValue = this.chart1.ChartAreas["ChartArea1"].AxisY.PixelPositionToValue(e.Y);          
            double xL = minSVX;
            double xR = maxSVX;
            double yB = minSVY;
            double yU = maxSVY;        
            if (e.Delta > 0)//放大
            {
                xL = xValue - ((xValue - minSVX)* 9 / 10);
                xR = xValue + ((maxSVX - xValue) * 9 / 10);
                yB = yValue - ((yValue - minSVY) * 9 / 10);
                yU = yValue + ((maxSVY - yValue) * 9 / 10);
            }
            else
            {
                xL = xValue - ((xValue - minSVX) * 11 / 10);
                xR = xValue + ((maxSVX - xValue) * 11 / 10);
                yB = yValue - ((yValue - minSVY) * 11 / 10);
                yU = yValue + ((maxSVY - yValue) * 11 / 10);
            }     
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(xL, xR);
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoom(yB, yU);
        }
        private void chartCompM_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            if (item.Name == "ResetZoom")
            {
                chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset(0);
                chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset(0);
            }
            else if (item.Name == "Mark1")
            {
                if (this.pointSelected == null)
                    return;
                this.mark1.Mark = this.pointSelected;
                this.mark1.IsSelected = true;
                this.setMark(this.mark1, "Mark1");
            }
            else if (item.Name == "Mark2")
            {
                if (!this.mark1.IsSelected)
                {
                    MessageBox.Show("Please selecte the mark1 firstly", "", MessageBoxButtons.YesNo);
                }
                if (this.pointSelected == null)
                    return;
                this.mark2.Mark = this.pointSelected;
                this.mark2.IsSelected = true;
                this.setMark(this.mark2, "Mark2");
            }
            else if (item.Name == "ClearMarks")
            {
                this.setMark(null, "Mark1");
                this.setMark(null, "Mark2");
            }
            else if (item.Name == "DistanseSetting")
            {

            }        
        }
        private void TrajectoryChart_Load(object sender, EventArgs e)
        {
            //AddPoints(1000);
        }
        private List<PointD> linePts = new List<PointD>();
        public void AddLinePoints(List<PointD> points,bool clear=false)
        {
            if (clear)
                this.linePts.Clear();
            this.linePts.AddRange(points);
          
        }
        public void ClearLinePoints()
        {
            this.linePts.Clear();
        }
        private List<PointD> discretePts=new List<PointD>();
        /// <summary>
        /// 添加离散点
        /// </summary>
        /// <param name="point"></param>
        public void AddPoints(List<PointD> points,bool clear=false)
        {
            if (clear)
                this.discretePts.Clear();
            this.discretePts.AddRange(points);
        }
        public void ClearPoints()
        {
            this.discretePts.Clear();
        }

        public void ShowPoint(PointD point)
        {
            this.chart1.Series["Series3"].Points.Clear();
            this.chart1.Series["Series3"].Points.AddXY(point.X, point.Y);
            this.Invalidate();
        }
        public void DrawChart()
        {
            this.points.Clear();
            this.points.AddRange(this.linePts);
            this.points.AddRange(this.discretePts);
            this.setRect(this.points);
            this.DrawLine(this.linePts);
            this.DrawDiscretePts(this.discretePts);
        }
        public void DrawLine(List<PointD> points)
        {
            this.chart1.Series["Series1"].Points.Clear();
            if (points == null || points.Count <= 0)
            {
                this.Invalidate();
                return;
            }
            foreach (PointD point in points)
            {
                this.chart1.Series["Series1"].Points.AddXY(point.X, point.Y);
            }
            this.Invalidate();
        }
        public void DrawDiscretePts(List<PointD> points)
        {
            this.chart1.Series["Series2"].Points.Clear();
            if (points == null || points.Count <= 0)
            {
                this.Invalidate();
                return;
            }
            foreach (PointD point in points)
            {
                this.chart1.Series["Series2"].Points.AddXY(point.X, point.Y);
            }
            this.Invalidate();
        }
        private void setRect(List<PointD> points)
        {
            if (points == null || points.Count <= 0)
                return;
            this.xList.Clear();
            this.yList.Clear();
            foreach (PointD point in points)
            {
                xList.Add(point.X);
                yList.Add(point.Y);
            }
            this.minX = xList.Min()-10;
            this.maxX = xList.Max()+10;
            this.minY = yList.Min()-10;
            this.maxY = yList.Max()+10;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = this.maxX;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = this.minX;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = this.maxY;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = this.minY;

            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(this.minX, this.maxX);
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoom(this.minY, this.maxY);
            double lengthX = this.maxX - this.minX;
            double lengthY = this.maxY - this.minY;
            if (lengthX < lengthY)
            {
                distance = lengthX / 100;
            }
            else
            {
                distance = lengthY / 100;
            }
            
        }
        
        private void chart1_MouseEnter(object sender, EventArgs e)
        {   
            this.chart1.Cursor = Cursors.Cross;
            minSVX = this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ViewMinimum;
            maxSVX = this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ViewMaximum;
            minSVY = this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ViewMinimum;
            maxSVY = this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ViewMaximum;           
        }
             
        private double minSVX;
        private double maxSVX;
        private double minSVY;
        private double maxSVY;

        private double scalex;
        private double scaley;
        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            if (e.Button == MouseButtons.Middle)
            {
                this.mouseIsMoving = true;
                this.mouseMiddIsClick = true;
                this.mouseLastPoint.X = e.X;
                this.mouseLastPoint.Y = e.Y;

                minSVX = this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ViewMinimum;
                maxSVX = this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ViewMaximum;
                minSVY = this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ViewMinimum;
                maxSVY = this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ViewMaximum;
                double lpixelX = this.chart1.ChartAreas["ChartArea1"].AxisX.ValueToPixelPosition(maxSVX) - this.chart1.ChartAreas["ChartArea1"].AxisX.ValueToPixelPosition(minSVX);
                double lpixelY = this.chart1.ChartAreas["ChartArea1"].AxisY.ValueToPixelPosition(maxSVY) - this.chart1.ChartAreas["ChartArea1"].AxisY.ValueToPixelPosition(minSVY);               
                scalex = (maxSVX - minSVX) / lpixelX;
                scaley = (maxSVY - minSVY) / lpixelY;
                this.chart1.Cursor = Cursors.NoMove2D;
            }
            if (e.Button == MouseButtons.Right)
            {
                double xValue = this.chart1.ChartAreas["ChartArea1"].AxisX.PixelPositionToValue(e.X);
                double yValue = this.chart1.ChartAreas["ChartArea1"].AxisY.PixelPositionToValue(e.Y);
                this.pointSelected = new PointD(xValue, yValue);
            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.mouseMiddIsClick = false;
                this.mouseIsMoving = false;
                this.chart1.Cursor = Cursors.Cross;
            }
            if (e.Button == MouseButtons.Right)
            {
                double nearestDis = this.distance;
                List<PointD> selectedList = new List<PointD>();
                foreach (PointD p in this.points)
                {
                    double dist = p.DistanceTo(this.pointSelected);
                    if (dist<= this.distance)
                    {
                        selectedList.Add(p);
                    }
                }
                if (selectedList.Count<=0)
                {
                    this.pointSelected = null;
                    return;
                }
                foreach (PointD p in selectedList)
                {
                    double dist = p.DistanceTo(this.pointSelected);                  
                    if (dist < nearestDis)
                    {
                        this.pointSelected = new PointD(p.X, p.Y);
                        nearestDis = dist;
                        continue;
                    }                   
                }
            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouseMiddIsClick && this.points.Count>0)
            {
                minSVX = this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ViewMinimum;
                maxSVX = this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ViewMaximum;
                minSVY = this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ViewMinimum;
                maxSVY = this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ViewMaximum;
                double minSVXPxl = this.chart1.ChartAreas["ChartArea1"].AxisX.ValueToPixelPosition(minSVX);
                double maxSVXPxl = this.chart1.ChartAreas["ChartArea1"].AxisX.ValueToPixelPosition(maxSVX);
                double minSVYPxl = this.chart1.ChartAreas["ChartArea1"].AxisY.ValueToPixelPosition(minSVY);
                double maxSVYPxl = this.chart1.ChartAreas["ChartArea1"].AxisY.ValueToPixelPosition(maxSVY);

                if ((e.X < maxSVXPxl && e.X > minSVXPxl) && (e.X > maxSVYPxl && e.Y < minSVYPxl))
                    this.chart1.Cursor = Cursors.Cross;
                else
                    this.chart1.Cursor = Cursors.Default;
                return;
            }
            if (e.Button==MouseButtons.Middle)
            {
                if (!this.mouseMiddIsClick)
                {
                    return;
                }
                double dSVx = Math.Round(-(e.X - this.mouseLastPoint.X) * scalex, 3);
                double dSVy = Math.Round(-(e.Y - this.mouseLastPoint.Y) * scaley, 3);
                if ((Math.Round(this.minSVX, 3) + dSVx) < this.minX && dSVx < 0)
                {
                    dSVx = this.minX - minSVX;
                }
                else if ((maxSVX + dSVx) > this.maxX && dSVx > 0)
                {
                    dSVx = this.maxX - maxSVX;
                }
                if ((Math.Round(this.minSVY, 3) + dSVy) < this.minY && dSVy < 0)
                {
                    dSVy = this.minY - minSVY;
                }
                else if ((Math.Round(this.maxSVY, 3) + dSVy) > this.maxY && dSVy > 0)
                {
                    dSVy = this.maxY - maxSVY;
                }
                this.minSVX = Math.Round(this.minSVX, 3) + dSVx;
                this.maxSVX = Math.Round(this.maxSVX, 3) + dSVx;
                this.minSVY = Math.Round(this.minSVY, 3) + dSVy;
                this.maxSVY = Math.Round(this.maxSVY, 3) + dSVy;
                Log.Dprint(minSVX.ToString() + " | " + maxSVX.ToString() + "  |  " + (maxSVX - minSVX).ToString());
                Log.Dprint(scalex.ToString() + scaley.ToString());
                Log.Dprint("|  " + dSVx.ToString() + "  |");
                this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(this.minSVX, this.maxSVX);
                this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoom(this.minSVY, this.maxSVY);
                this.mouseLastPoint.X = e.X;
                this.mouseLastPoint.Y = e.Y;
                this.Invalidate();
            }
            
        }

        private void chart1_MouseHover(object sender, EventArgs e)
        {
            this.chart1.Cursor = Cursors.Cross;
        }
    }
}
