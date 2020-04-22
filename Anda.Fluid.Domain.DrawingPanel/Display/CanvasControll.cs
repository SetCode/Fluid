using Anda.Fluid.Domain.FluProgram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingPanel.Msg;


namespace DrawingPanel.Display
{
    public partial class CanvasControll : UserControl, IDrawingMsgReceiver
    {
        private PointF mouseMiddleClickLocation = new PointF();
        private bool mouseMiddleIsClick = false;
        public CanvasControll()
        {
            InitializeComponent();
            this.canvasDisplay1.SetScroll += new Action<int, int>(this.SetScroll);
        }

        public void SetControlMode(bool onlyLook)
        {
            this.canvasDisplay1.OnlyLook = onlyLook;
        }

        private void tsbBackGroundLeftTopColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.TopLeftColor = colorDialog1.Color;
                this.canvasDisplay1.Invalidate();
            }
        }

        private void tsbBackGroundRightBottomColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.RightBottomColor = colorDialog1.Color;
                this.canvasDisplay1.Invalidate();
            }
        }

        private void tsbGridEnable_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.GridOn = !Properties.Settings.Default.GridOn;
            this.canvasDisplay1.Invalidate();
        }

        private void tsbSelectBoxColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.SelectedBoxColor = colorDialog1.Color;
                this.UpdateDraw();
            }
        }

        private void tsmTrackNormalColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.TrackNormalColor = colorDialog1.Color;
                this.UpdateDraw();
            }
        }

        private void tsmTrackSelectedColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.TrackSelectedColor = colorDialog1.Color;
                this.UpdateDraw();
            }
        }

        private void tsmTrackDisableColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.TrackDisableColor = colorDialog1.Color;
                this.UpdateDraw();
            }
        }

        private void tstxtTrackWidth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float width = Convert.ToSingle(this.tstxtTrackWidth.Text);
                Properties.Settings.Default.TrackWidth = width;
                this.UpdateDraw();
            }
            catch (Exception)
            {

            }
        }

        public void Update(FluidProgram fluidProgram)
        {              
            this.canvasDisplay1.Rect = DrawProgram.Instance.Rect;
         
        }

        public void EnterWorkpiece()
        {
            this.canvasDisplay1.Rect = DrawProgram.Instance.Rect;
            this.canvasDisplay1.Invalidate();
        }

        public void EnterPattern(int patternNo)
        {
            this.canvasDisplay1.Rect = DrawProgram.Instance.Rect;
            this.canvasDisplay1.Invalidate();
        }

        public void ClickCmdLine(bool inWorkpiece, int patternNo, int[] cmdLineNo)
        {
            this.canvasDisplay1.Invalidate();
        }

        private void SetScroll(int xScorllMax, int yScorllMax)
        {
            this.hScrollBar1.Maximum = xScorllMax;
            this.vScrollBar1.Maximum = yScorllMax;
        }

        private void tsmTrackClickColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.TrackClickColor = colorDialog1.Color;
                this.UpdateDraw();
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.GridInterval = Convert.ToInt32(this.toolStripComboBox1.SelectedItem.ToString());
                this.canvasDisplay1.Invalidate();
            }
            catch (Exception)
            {

            }
        }

        private void tsmAsvMarkColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.NormalMarkColor = colorDialog1.Color;
                this.UpdateDraw();
            }
        }

        private void tsmBadMarkColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BadMarkColor = colorDialog1.Color;
                this.UpdateDraw();
            }
        }

        private void tsmCheckDotColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.CheckDotColor = colorDialog1.Color;
                this.UpdateDraw();
            }
        }

        private void tsmHeightColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.HeightColor = colorDialog1.Color;
                this.UpdateDraw();
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            float intervalDis = 0;
            if (this.hScrollBar1.Maximum == 0 || this.canvasDisplay1.Rect == null) 
            {
                intervalDis = 0;
            }
            else
            {
                intervalDis = this.canvasDisplay1.Rect.Width / (this.hScrollBar1.Maximum);
            }
            this.canvasDisplay1.XTransDis = - this.hScrollBar1.Value * intervalDis;
            this.canvasDisplay1.Invalidate();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            float intervalDis = 0;
            if (this.hScrollBar1.Maximum == 0 || this.canvasDisplay1.Rect == null)
            {
                intervalDis = 0;
            }
            else
            {
                intervalDis = this.canvasDisplay1.Rect.Height / (this.vScrollBar1.Maximum);
            }
            this.canvasDisplay1.YTransDis = - this.vScrollBar1.Value * intervalDis;
            this.canvasDisplay1.Invalidate();
        }

        private void CanvasControll_Leave(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void canvasDisplay1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                //如果现有鼠标点在点击鼠标时的右边，并且距离大于10，则画布向右移动一下
                if (e.Location.X - mouseMiddleClickLocation.X > 5)
                {
                    if (this.hScrollBar1.Value < this.hScrollBar1.Maximum)
                    {
                        this.hScrollBar1.Value++;
                        this.hScrollBar1_Scroll(sender, new ScrollEventArgs(ScrollEventType.EndScroll, this.hScrollBar1.Value));
                        this.mouseMiddleClickLocation = e.Location;
                    }
                }
                //如果现有鼠标点在点击鼠标时的左边，并且距离大于10，则画布向左移动一下
                else if (e.Location.X - mouseMiddleClickLocation.X < -5)
                {
                    if (this.hScrollBar1.Value > this.hScrollBar1.Minimum)
                    {
                        this.hScrollBar1.Value--;
                        this.hScrollBar1_Scroll(sender, new ScrollEventArgs(ScrollEventType.EndScroll, this.hScrollBar1.Value));
                        this.mouseMiddleClickLocation = e.Location;
                    }
                }
                //如果现有鼠标点在点击鼠标时的上边，并且距离大于10，则画布向上移动一下
                if (e.Location.Y - mouseMiddleClickLocation.Y < -5)
                {
                    if (this.vScrollBar1.Value > this.vScrollBar1.Minimum)
                    {
                        this.vScrollBar1.Value--;
                        this.vScrollBar1_Scroll(sender, new ScrollEventArgs(ScrollEventType.EndScroll, this.vScrollBar1.Value));
                        this.mouseMiddleClickLocation = e.Location;
                    }
                }
                //如果现有鼠标点在点击鼠标时的下边，并且距离大于10，则画布向下移动一下
                else if (e.Location.Y - mouseMiddleClickLocation.Y > 5) 
                {
                    if (this.vScrollBar1.Value < this.vScrollBar1.Maximum)
                    {
                        this.vScrollBar1.Value++;
                        this.vScrollBar1_Scroll(sender, new ScrollEventArgs(ScrollEventType.EndScroll, this.vScrollBar1.Value));
                        this.mouseMiddleClickLocation = e.Location;
                    }
                }
            }
        }

        private void canvasDisplay1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.mouseMiddleIsClick = true;
                this.mouseMiddleClickLocation = e.Location;
            }
        }

        private void canvasDisplay1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.mouseMiddleIsClick = false;
            }
        }

        /// <summary>
        /// 当只是修改轨迹颜色和宽度时调用
        /// </summary>
        private void UpdateDraw()
        {
            this.canvasDisplay1.SetColorAndWidth();
            this.canvasDisplay1.Invalidate();
        }
    }
}
