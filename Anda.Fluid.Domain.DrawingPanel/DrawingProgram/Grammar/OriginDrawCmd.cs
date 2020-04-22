
using DrawingPanel.DrawingProgram.Executant;
using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Grammar
{
    public class OriginDrawCmd : DrawCmdLine
    {
        private LineDraw xAxis, yAxis;

        private bool enable;
        private bool isClick = false;

        /// <summary>
        /// 原点指令（位置，半径，原点颜色，坐标轴颜色）
        /// </summary>
        public OriginDrawCmd(bool enable)
        {
            this.enable = enable;

            this.xAxis = new LineDraw(new PointF(0, 0), new PointF(50, 0), Properties.Settings.Default.TrackWidth * 0.5f, Properties.Settings.Default.TrackNormalColor, true,false);
            this.yAxis = new LineDraw(new PointF(0, 0), new PointF(0, -50), Properties.Settings.Default.TrackWidth * 0.5f, Properties.Settings.Default.TrackNormalColor, true,false);

        }


        public override bool IsClick
        {
            get
            {
                return this.isClick;
            }

            set
            {
                this.isClick = value;
            }
        }

        public override RectangleF Rect => new RectangleF(new PointF(this.yAxis.Rect.Left, this.yAxis.Rect.Top),
            new SizeF(this.xAxis.Rect.Right-this.yAxis.Rect.Left, this.xAxis.Rect.Bottom-this.yAxis.Rect.Top));

        public override void Setup(PointF origin)
        {
            if (this.IsSelected)
            {
                this.SetTrack(origin, Properties.Settings.Default.TrackSelectedColor);
            }
            else if (!this.enable)
            {
                this.SetTrack(origin, Properties.Settings.Default.TrackDisableColor);
            }
            else
            {
                if (this.isClick)
                {
                    this.SetTrack(origin, Properties.Settings.Default.TrackClickColor);
                    this.isClick = false;
                }
                else
                {
                    this.SetTrack(origin, Properties.Settings.Default.TrackNormalColor);
                }

            }
           
        }
        public override void SetupDisable(PointF origin)
        {
            this.SetTrack(origin, Properties.Settings.Default.TrackDisableColor);
        }
        public override void DrawingPanel()
        {
            this.xAxis.Execute();
            this.yAxis.Execute();

        }
        public override object Clone()
        {
            OriginDrawCmd originCmdLine = MemberwiseClone() as OriginDrawCmd;
            originCmdLine.xAxis = this.xAxis.Clone() as LineDraw;
            originCmdLine.yAxis = this.yAxis.Clone() as LineDraw;           
            return originCmdLine;
        }

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {
            PointF CanvasPosition = DrawingUtils.Instance.CoordinateTrans(ModelOrigin);

            this.xAxis = new LineDraw(CanvasPosition, new PointF(CanvasPosition.X + Properties.Settings.Default.TrackWidth * 0.5f * 10, CanvasPosition.Y), Properties.Settings.Default.TrackWidth * 0.5f, backColor, true,false);
            this.yAxis = new LineDraw(CanvasPosition, new PointF(CanvasPosition.X, CanvasPosition.Y - Properties.Settings.Default.TrackWidth * 0.5f * 10), Properties.Settings.Default.TrackWidth * 0.5f, backColor, true,false);
        }
        public override bool IsHitter(PointF point)
        {
            return false;
        }

        public override bool IsContain(RectangleF rect)
        {
            return false;
        }
    }
}
