
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
    public class DotDrawCmd:DrawCmdLine
    {
        private DotDraw dot;
        private PointF centerPosition;
        private bool enable;
        private bool isClick = false;
        /// <summary>
        /// 点指令
        /// </summary>
        /// <param name="centerPosition"></param>
        public DotDrawCmd(PointF centerPosition,bool enable)
        {
            this.centerPosition = DrawingUtils.Instance.CoordinateTrans(centerPosition);
            this.enable = enable;
            this.SetTrack(new PointF(0, 0), Properties.Settings.Default.TrackNormalColor);
        }
        public override bool IsClick
        {
            get { return this.isClick; }
            set { this.isClick = value; }
        }

        public override RectangleF Rect => this.dot.Rect;

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
                if(this.isClick)
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
            this.dot.Execute();
        }

        protected override void SetTrack(PointF modelOrigin, Color backColor)
        {
            PointF CanvasOrigion = DrawingUtils.Instance.CoordinateTrans(modelOrigin);

            this.dot = new DotDraw(new PointF(CanvasOrigion.X + centerPosition.X, CanvasOrigion.Y + centerPosition.Y)
               , Properties.Settings.Default.TrackWidth * 0.5f, backColor, Color.Black);
        }
        public override object Clone()
        {
            DotDrawCmd dotCmdLine = MemberwiseClone() as DotDrawCmd;
            dotCmdLine.dot = dot.Clone() as DotDraw;
            dotCmdLine.centerPosition = centerPosition;
            return dotCmdLine;
        }

        public override bool IsHitter(PointF point)
        {
            if(this.dot.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }

        public override bool IsContain(RectangleF rect)
        {
            if(this.dot.IsContain(rect))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }
            return false;
        }
    }
}
