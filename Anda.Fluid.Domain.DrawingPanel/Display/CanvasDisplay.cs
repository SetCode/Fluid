using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace DrawingPanel.Display
{
    public partial class CanvasDisplay : UserControl
    {
        private bool drawSelectBox = false;
        private Point mouseClickLocation = new Point();
        private Point mouseCurrLocation = new Point();
        private PointF centerContentPoint = new PointF(0, 0);

        //根据所需绘制的图像边界计算需要的缩放量
        private float innerScale = 0;
        //根据所需绘制的图像边界计算需要平移的X和Y距离
        private float xInitTrans = 0, yInitTrans = 0;

        internal Action<int, int> SetScroll;

        public CanvasDisplay()
        {
            InitializeComponent();

            this.MouseWheel += this.CanvasDisplay_MouseWheel;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        }
        public RectangleF Rect { get; internal set; } = new RectangleF();
        public float RectX { get; internal set; } = 0;
        public float RectY { get; internal set; } = 0;
        public float RectWidth { get; internal set; } = 0;
        public float RectHeight { get; internal set; } = 0;

        public bool OnlyLook { get; set; } = false;
        public float OuterScale { get; set; } = 1;
        public float XTransDis { get; set; } = 0;
        public float YTransDis { get; set; } = 0;


        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);

                e.Graphics.Clear(Color.White);
                this.DrawBackGround(e);
                this.DrawGrid(e);

                //建立虚拟画布
                Image img = new Bitmap(this.Width, this.Height);
                Graphics virtualCanvaas = Graphics.FromImage(img);
                virtualCanvaas.ResetTransform();

                //确定所需绘制的图像区域位置和大小
                //RectangleF rect = new RectangleF(this.RectX, this.RectY, this.RectWidth, this.RectHeight);

                //根据workpiece边界和鼠标滚轮的放大系数来确定需要放大的倍数
                this.CalculateInnerScale(this.Rect);
                float scale = this.innerScale * this.OuterScale / 2f;
                virtualCanvaas.ScaleTransform(scale, scale);

                //根据workpiece边界和外部操作来确定需要平移的量
                this.CalculateInitTrans(this.Rect);
                //virtualCanvaas.TranslateTransform(this.XTransDis + this.xInitTrans+this.Rect.Width/2, this.YTransDis + this.yInitTrans+ this.Rect.Height / 2);
                virtualCanvaas.TranslateTransform(this.XTransDis + this.xInitTrans+this.Width / (10 * scale), this.YTransDis + this.yInitTrans + this.Height / (2.25f * scale));
              
                //双缓冲，先将绘制图形到虚拟画布上，再绘制到控件上        
                //设置虚拟画布
                DrawProgram.Instance.Graphics = virtualCanvaas;
                //将图像画在虚拟画布上
                DrawProgram.Instance.Drawing();
                //再绘制到控件上
                e.Graphics.DrawImage(img, new Point(0, 0));

                //绘制选择框
                this.DrawSelectBox(e);

                //释放内存（无法自动释放）
                img.Dispose();
                virtualCanvaas.Dispose();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 绘制渐进色背景
        /// </summary>
        private void DrawBackGround(PaintEventArgs e)
        {
            Brush brush = new LinearGradientBrush(this.ClientRectangle, Properties.Settings.Default.TopLeftColor,
                Properties.Settings.Default.RightBottomColor, LinearGradientMode.ForwardDiagonal);
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }

        /// <summary>
        /// 绘制网格
        /// </summary>
        /// <param name="e"></param>
        private void DrawGrid(PaintEventArgs e)
        {
            if (Properties.Settings.Default.GridOn)
                return;

            Pen pen = new Pen(Color.Gray);

            int gridX = this.Width / Properties.Settings.Default.GridInterval + 1;
            int gridY = this.Height / Properties.Settings.Default.GridInterval + 1;
            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    e.Graphics.DrawLine(pen, new Point(this.ClientRectangle.X + i * Properties.Settings.Default.GridInterval, this.ClientRectangle.Y),
                        new Point(this.ClientRectangle.X + i * Properties.Settings.Default.GridInterval, this.ClientRectangle.Bottom));
                    e.Graphics.DrawLine(pen, new Point(this.ClientRectangle.X, this.ClientRectangle.Y + j * Properties.Settings.Default.GridInterval),
                        new Point(this.ClientRectangle.Right, this.ClientRectangle.Y + j * Properties.Settings.Default.GridInterval));
                }

            }

        }

        /// <summary>
        /// 绘制透明选择框
        /// </summary>
        /// <param name="e"></param>
        private void DrawSelectBox(PaintEventArgs e)
        {
            if (this.drawSelectBox)
            {
                Brush tranBrush = new SolidBrush(Color.FromArgb(125, Properties.Settings.Default.SelectedBoxColor));
                SizeF newsize = new SizeF((float)(Math.Abs(this.mouseCurrLocation.X - this.mouseClickLocation.X)), (float)(Math.Abs(this.mouseCurrLocation.Y - this.mouseClickLocation.Y)));

                Point topLeft = new Point(Math.Min(mouseCurrLocation.X, mouseClickLocation.X), Math.Min(mouseCurrLocation.Y, mouseClickLocation.Y));

                e.Graphics.FillRectangle(tranBrush, new RectangleF(topLeft, newsize));

            }
        }

        private void CanvasDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseClickLocation = e.Location;
                this.drawSelectBox = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (this.OnlyLook)
                {
                    return;
                }
                DrawProgram.Instance.EditDrawCmd();
            }
        }

        private void CanvasDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseCurrLocation = e.Location;
                this.Invalidate();
            }
        }

        private void CanvasDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseCurrLocation = e.Location;
                this.drawSelectBox = false;

                Point topLeftInCanvas = new Point(Math.Min(mouseCurrLocation.X, mouseClickLocation.X), Math.Min(mouseCurrLocation.Y, mouseClickLocation.Y));
                Point bottomRightInCanvas = new Point(Math.Max(mouseCurrLocation.X, mouseClickLocation.X), Math.Max(mouseCurrLocation.Y, mouseClickLocation.Y));

                PointF topLeft = this.CanvasToModel(topLeftInCanvas);
                PointF bottomRight = this.CanvasToModel(bottomRightInCanvas);
                RectangleF rect = new RectangleF(topLeft, new SizeF(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y));

                if (mouseCurrLocation.X - mouseClickLocation.X < 5 && mouseCurrLocation.Y - mouseClickLocation.Y < 5)
                {
                    DrawProgram.Instance.HitterCmdLine(topLeft);
                }
                else
                {
                    DrawProgram.Instance.ContainCmdLine(rect);
                }

            }
            this.Invalidate();
        }

        private void CanvasDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            if (DrawProgram.Instance.WorkPiece == null)
                return;
            float zoomDelta = 1.25f * (float)Math.Abs(e.Delta) / 120.0f;
            if (e.Delta < 0)
            {
                if (this.OuterScale >= 1)
                {
                    this.OuterScale = (this.OuterScale / zoomDelta) >= 1 ? (this.OuterScale / zoomDelta) : 1;
                }
            }
            else
            {
                this.OuterScale = this.OuterScale * zoomDelta;
            }


            //通知滚动条改变尺寸
            if (this.OuterScale == 1)
            {
                this.SetScroll?.Invoke(0, 0);
            }
            else
            {
                int hScrollBarMaximum = Convert.ToInt32((this.Rect.Width * this.innerScale * this.OuterScale) * 9 / (this.Width) + 1);
                int vScrollBarMaximum = Convert.ToInt32((this.Rect.Height * this.innerScale * this.OuterScale) * 9 / (this.Height) + 1);
                this.SetScroll?.Invoke(hScrollBarMaximum, vScrollBarMaximum);
            }

            this.Invalidate();

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!this.Focused)
                return false;

            switch (keyData)
            {
                case Keys.Escape:
                    DrawProgram.Instance.EscIsClick();
                    this.Invalidate();
                    break;
                case Keys.F:
                    DrawProgram.Instance.RelateScriptEditor();
                    break;
                case Keys.Left:
                    break;
                case Keys.Up:
                    break;
                case Keys.Right:
                    break;
                case Keys.Down:
                    break;
                case Keys.Delete:
                    break;
            }
            return true;
        }

        /// <summary>
        /// 将鼠标位置转换为轨迹图位置
        /// </summary>
        /// <param name="canvasLocation"></param>
        /// <returns></returns>
        private PointF CanvasToModel(Point canvasLocation)
        {
            PointF locationInModel = new PointF();
            locationInModel.X = canvasLocation.X / (this.innerScale * this.OuterScale) - this.xInitTrans - this.XTransDis;
            locationInModel.Y = canvasLocation.Y / (this.innerScale * this.OuterScale) - this.yInitTrans - this.YTransDis;
            return locationInModel;
        }

        /// <summary>
        /// 计算点在缩放平移后的轨迹图中的位置
        /// </summary>
        private PointF ModelPosition(PointF positionInModel)
        {
            PointF positionInScaleModel = new PointF();
            positionInScaleModel.X = positionInModel.X * this.OuterScale * this.innerScale + this.xInitTrans + this.XTransDis;
            positionInScaleModel.Y = positionInModel.Y * this.OuterScale * this.innerScale + this.yInitTrans + this.YTransDis;

            return positionInScaleModel;
        }

        /// <summary>
        /// 根据所需绘制的图像边界计算需要缩放的大小
        /// </summary>
        private void CalculateInnerScale(RectangleF rect)
        {
            //计算X ,Y方向容下所有轨迹需要的缩放比例
            if (rect.Width == 0 || rect.Height == 0)
            {
                this.innerScale = 0;
            }
            else
            {
                //float xScale = this.Width * 0.75f / (rect.Width * 1.05f);
                //float yScale = this.Height * 0.75f / (rect.Height * 1.05f);
                float xScale = this.Width * 1.05f / (rect.Width * 1.05f);
                float yScale = this.Height * 1.05f / (rect.Height * 1.05f);
                this.innerScale = Math.Min(xScale, yScale);
            }

        }

        private void CanvasDisplay_SizeChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CanvasWidth = this.Width;
            Properties.Settings.Default.CanvasHeight = this.Height;
        }

        private void CalculateInitTrans(RectangleF rect)
        {
            this.xInitTrans = -rect.Left;
            this.yInitTrans = -rect.Top;
        }

        public void SetColorAndWidth()
        {
            DrawProgram.Instance.SetTrackColorAndWidth();
        }

    }
}
