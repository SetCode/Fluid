
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
    public class MarkDrawCmd : DrawCmdLine
    {
        private CircleDraw circle;
        private LineDraw upLine, downLine, leftLine, rightLine;

        private PointF position;
        private bool enable;
        private bool isClick = false;
        private MarkType markType;

        /// <summary>
        /// Mark指令（位置，点半径，整体颜色，背景颜色）
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="lineColor"></param>
        /// <param name="backColor"></param>
        public MarkDrawCmd(PointF position,bool enable,MarkType markType)
        {
            this.position = DrawingUtils.Instance.CoordinateTrans(position);
            this.enable = enable;
            this.markType = markType;
            switch (this.markType)
            {
                case MarkType.NormalMark:
                    this.SetTrack(new PointF(0, 0), Properties.Settings.Default.NormalMarkColor);
                    break;
                case MarkType.BadMark:
                    this.SetTrack(new PointF(0, 0), Properties.Settings.Default.BadMarkColor);
                    break;
                case MarkType.CheckDotMark:
                    this.SetTrack(new PointF(0, 0), Properties.Settings.Default.CheckDotColor);
                    break;
            }
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

        public override RectangleF Rect => new RectangleF(new PointF(this.leftLine.Rect.Left, this.upLine.Rect.Top),
            new SizeF(this.rightLine.Rect.Right - this.leftLine.Rect.Left, this.downLine.Rect.Bottom - this.upLine.Rect.Top));

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
                    switch (this.markType)
                    {
                        case MarkType.NormalMark:
                            this.SetTrack(origin, Properties.Settings.Default.NormalMarkColor);
                            break;
                        case MarkType.BadMark:
                            this.SetTrack(origin, Properties.Settings.Default.BadMarkColor);
                            break;
                        case MarkType.CheckDotMark:
                            this.SetTrack(origin, Properties.Settings.Default.CheckDotColor);
                            break;
                    }
                    
                }
               
            }
            
        }
        public override void SetupDisable(PointF origin)
        {
            this.SetTrack(origin, Properties.Settings.Default.TrackDisableColor);
        }
        public override void DrawingPanel()
        {
            this.circle.Execute();
            this.upLine.Execute();
            this.downLine.Execute();
            this.leftLine.Execute();
            this.rightLine.Execute();
        }

        public override object Clone()
        {
            MarkDrawCmd markCmdLine = MemberwiseClone() as MarkDrawCmd;
            markCmdLine.position = position;
            markCmdLine.markType = this.markType;
            markCmdLine.circle = this.circle.Clone() as CircleDraw;
            markCmdLine.upLine = this.upLine.Clone() as LineDraw;
            markCmdLine.downLine = this.downLine.Clone() as LineDraw;
            markCmdLine.leftLine = this.leftLine.Clone() as LineDraw;
            markCmdLine.rightLine = this.rightLine.Clone() as LineDraw;
            return markCmdLine;
        }

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {
            PointF canvasOrigin = DrawingUtils.Instance.CoordinateTrans(ModelOrigin);

            PointF position = new PointF(this.position.X + canvasOrigin.X, this.position.Y + canvasOrigin.Y);

            this.circle = new CircleDraw(new PointF(position.X, position.Y), Properties.Settings.Default.TrackWidth, Properties.Settings.Default.TrackWidth * 0.25f, backColor);
            this.upLine = new LineDraw(new PointF(position.X, position.Y + Properties.Settings.Default.TrackWidth * 0.5f),
                new PointF(position.X, position.Y + Properties.Settings.Default.TrackWidth * (float)1.2), Properties.Settings.Default.TrackWidth * 0.12f, backColor, false,true);
            this.downLine = new LineDraw(new PointF(position.X, position.Y - Properties.Settings.Default.TrackWidth * 0.5f),
                new PointF(position.X, position.Y - Properties.Settings.Default.TrackWidth * (float)1.2), Properties.Settings.Default.TrackWidth * 0.12f, backColor, false, true);
            this.leftLine = new LineDraw(new PointF(position.X - Properties.Settings.Default.TrackWidth * 0.5f, position.Y),
                new PointF(position.X - Properties.Settings.Default.TrackWidth * (float)1.2, position.Y), Properties.Settings.Default.TrackWidth * 0.12f, backColor, false, true);
            this.rightLine = new LineDraw(new PointF(position.X + Properties.Settings.Default.TrackWidth * 0.5f, position.Y),
                new PointF(position.X + Properties.Settings.Default.TrackWidth * (float)1.2, position.Y), Properties.Settings.Default.TrackWidth * 0.12f, backColor, false, true);
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

            if (this.leftLine.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }

            if (this.rightLine.IsHitter(point))
            {
                this.IsSelected = !this.IsSelected;
                return true;
            }

            if (this.circle.IsHitter(point))
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

            if (!this.leftLine.IsContain(rect))
            {
                return false;
            }

            if (!this.rightLine.IsContain(rect))
            {
                return false;
            }

            if (!this.circle.IsContain(rect))
            {
                return false;
            }

            this.IsSelected = !this.IsSelected;
            return true;
        }
    }

    public enum MarkType
    {
        NormalMark,
        BadMark,
        CheckDotMark,
    }
}
