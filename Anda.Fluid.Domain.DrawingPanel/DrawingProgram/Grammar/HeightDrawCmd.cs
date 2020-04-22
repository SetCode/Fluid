
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
    public class HeightDrawCmd : DrawCmdLine
    {
        private PointF position;
        private bool enable;
        private bool isClick = false;

        private LineDraw upArrow, downArrow, upLine, downLine;

        /// <summary>
        /// 测高指令
        /// </summary>
        /// <param name="position"></param>
        public HeightDrawCmd(PointF position,bool enable)
        {
            this.position = DrawingUtils.Instance.CoordinateTrans(position);
            this.enable = enable;
            this.SetTrack(new PointF(0, 0), Properties.Settings.Default.HeightColor);
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

        public override RectangleF Rect => new RectangleF(new PointF(this.upLine.Rect.Left, this.downLine.Rect.Top)
            , new SizeF(this.upLine.Rect.Right - this.upLine.Rect.Left, this.downLine.Rect.Bottom - this.upLine.Rect.Top));

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
                    this.SetTrack(origin, Properties.Settings.Default.HeightColor);
                }
               
            }
         
        }
        public override void SetupDisable(PointF origin)
        {
            PointF centerPosition = new PointF(origin.X + position.X, origin.Y + position.Y);
            this.SetTrack(centerPosition, Properties.Settings.Default.TrackDisableColor);
        }
        public override void DrawingPanel()
        {
            this.upArrow.Execute();
            this.downArrow.Execute();
            this.upLine.Execute();
            this.downLine.Execute();
        }
        public override object Clone()
        {
            HeightDrawCmd heightCmdLine = MemberwiseClone() as HeightDrawCmd;
            heightCmdLine.position = position;
            heightCmdLine.upLine = this.upLine.Clone() as LineDraw;
            heightCmdLine.downLine = this.downLine.Clone() as LineDraw;
            heightCmdLine.upArrow = this.upArrow.Clone() as LineDraw;
            heightCmdLine.downArrow = this.downArrow.Clone() as LineDraw;
            return heightCmdLine;
        }

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {
            PointF canvasOrigion = DrawingUtils.Instance.CoordinateTrans(ModelOrigin);

            PointF centerPosition = new PointF(canvasOrigion.X + position.X, canvasOrigion.Y + position.Y);
            this.upArrow = new LineDraw(centerPosition, new PointF(centerPosition.X, centerPosition.Y + Properties.Settings.Default.TrackWidth * 0.5f * 2), Properties.Settings.Default.TrackWidth * 0.5f * 0.25f, backColor, true, false);
            this.downArrow = new LineDraw(centerPosition, new PointF(centerPosition.X, centerPosition.Y - Properties.Settings.Default.TrackWidth * 0.5f * 2), Properties.Settings.Default.TrackWidth * 0.5f * 0.25f, backColor, true,false);
            this.upLine = new LineDraw(new PointF(centerPosition.X - Properties.Settings.Default.TrackWidth * 0.5f, centerPosition.Y + Properties.Settings.Default.TrackWidth * 0.5f * 2),
                new PointF(centerPosition.X + Properties.Settings.Default.TrackWidth * 0.5f, centerPosition.Y + Properties.Settings.Default.TrackWidth * 0.5f * 2), Properties.Settings.Default.TrackWidth * 0.5f * 0.25f, backColor, false, false);
            this.downLine = new LineDraw(new PointF(centerPosition.X - Properties.Settings.Default.TrackWidth * 0.5f, centerPosition.Y - Properties.Settings.Default.TrackWidth * 0.5f * 2),
                new PointF(centerPosition.X + Properties.Settings.Default.TrackWidth * 0.5f, centerPosition.Y - Properties.Settings.Default.TrackWidth * 0.5f * 2), Properties.Settings.Default.TrackWidth * 0.5f * 0.25f, backColor, false, false);
        }

        public override bool IsHitter(PointF point)
        {
            if (this.upLine.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }

            if (this.downLine.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }

            if (this.upArrow.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }

            if (this.downArrow.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }

            return false;
        }

        public override bool IsContain(RectangleF rect)
        {
            if (!this.upLine.IsContain(rect))
            {
                return false;
            }

            if (!this.downLine.IsContain(rect))
            {
                return false;
            }

            if (!this.upArrow.IsContain(rect))
            {
                return false;
            }

            if (!this.downArrow.IsContain(rect))
            {
                return false;
            }

            this.IsSelected = !this.IsSelected;
            return true;
        }
    }
}
