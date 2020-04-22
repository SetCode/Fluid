using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingPanel.Data;
using DrawingPanel.Msg;
using Anda.Fluid.Domain.FluProgram;
using System.Diagnostics;

namespace DrawingPanel
{
    public partial class DrawingPanelControl : UserControl,IDrawingMsgReceiver
    {                  
        private PointF centerContentPoint = new PointF(0, 0);
        private Point mouseLastPoint = new Point(0, 0);
        private float scale = 1f;
        private bool mouseMiddIsClick = false;
        private bool isFisrtEnter = true;

        public DrawingPanelControl()
        {
            InitializeComponent();
            this.MouseWheel += this.drawingPanel_MouseWheel;

            this.centerContentPoint.X = this.Width / 2;
            this.centerContentPoint.Y = this.Height / 2;

            GlobalData.Instance.PanelWidth = this.Width;
            GlobalData.Instance.PanelHeight = this.Height;

        }

        private void drawingPanel_MouseWheel(object sender,MouseEventArgs e)
        {
            float newScale = 0;
            if (e.Delta > 0)
            {
                if (this.scale > 4)
                {
                    newScale = this.scale + 2f;
                }
                else
                {
                    newScale = this.scale + 0.5f;
                }
               
            }
            else
            {
                if (this.scale > 4)
                {
                    newScale = this.scale - 1f;
                    newScale = newScale < 0.1f ? 0.1f : newScale;
                }
                else
                {
                    newScale = this.scale - 0.2f;
                    newScale = newScale < 0.1f ? 0.1f : newScale;
                }
               
            }
            if (this.scale == newScale)
                return;

            //计算鼠标对应的内容坐标系的位置
            double mouseContentX = this.centerContentPoint.X + (e.X - this.Width / 2) / this.scale;
            double mouseContentY = this.centerContentPoint.Y + (e.Y - this.Height / 2) / this.scale;

            //鼠标的内容坐标系位置相对于显示控件中心不变，计算画布中心内容坐标系的位置
            this.centerContentPoint.X = (float)mouseContentX + (this.Width / 2 - e.X) / newScale;
            this.centerContentPoint.Y = (float)mouseContentY + (this.Height / 2 - e.Y) / newScale;

            this.scale = newScale;

            this.Invalidate();
        }

        private void drawingPanel_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }
        private void drawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {               
                this.mouseMiddIsClick = true;
                this.mouseLastPoint.X = e.X;
                this.mouseLastPoint.Y = e.Y;
            }
        }
        private void drawingPanel_MouseUp(object sender,MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.mouseMiddIsClick = false;
            }
        }
        private void drawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(!this.mouseMiddIsClick)
                return;

            //求得鼠标拾取点到上一次拾取点之间的距离
            int dx = e.X - mouseLastPoint.X;
            int dy = e.Y - mouseLastPoint.Y;

            //求得在当前缩放系数和拾取点距离变化下，画布中心坐标系的新位置
            centerContentPoint.X -= dx / this.scale;
            centerContentPoint.Y -= dy / this.scale;

            mouseLastPoint.X = e.X;
            mouseLastPoint.Y = e.Y;

            this.Invalidate();
        }
        private void DrawingPanelControl_Paint(object sender, PaintEventArgs e)
        {
            //因为控件大小在初始化之后有变化，所以要重新赋值
            if (isFisrtEnter)
            {
                this.centerContentPoint.X = this.Width / 2;
                this.centerContentPoint.Y = this.Height / 2;
                GlobalData.Instance.PanelWidth = this.Width;
                GlobalData.Instance.PanelHeight = this.Height;

                this.isFisrtEnter = false;
            }

            //建立虚拟画布
            Image img = new Bitmap((int)this.Width, (int)this.Height);
            Graphics virtualCanvaas = Graphics.FromImage(img);
            DrawProgram.Instance.SetGraphics(virtualCanvaas, this.scale);

            //计算鼠标拾取点变化时的画布平移量
            float offsetX = this.Width / 2 - centerContentPoint.X * this.scale;
            float offsetY = this.Height / 2 - centerContentPoint.Y * this.scale;
            virtualCanvaas.TranslateTransform(offsetX, offsetY);                 

            //将图像画在虚拟画布上
            DrawProgram.Instance.Drawing();

            //将虚拟画布上的图像一次性画在显示控件上
            Graphics g = this.CreateGraphics();
            g.Clear(GlobalData.Instance.BackColor);
            g.DrawImage(img, new Point(0, 0));
            img.Dispose();
            g.Dispose();
        }

        public void Update(FluidProgram fluidProgram)
        {
            
        }

        public void EnterWorkpiece()
        {
            this.Invalidate();    
        }

        public void EnterPattern(int patternNo)
        {
            this.Invalidate();
        }

        public void ClickCmdLine(bool inWorkpiece, int patternNo, int[] cmdLineNo)
        {
            this.Invalidate();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.centerContentPoint.X = this.Width / 2;
            this.centerContentPoint.Y = this.Height / 2;
            this.scale = 1;

            this.Invalidate();
        }
    }
}
