using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Grammar
{
    public class ArrayDrawCmd : DrawCmdLine
    {
        private List<DoPatternDrawCmd> doPatterns = new List<DoPatternDrawCmd>();
        private DrawPattern[] patterns;
        private PointF[] originPoints;
        private bool enable;
        private bool isClick;
        public ArrayDrawCmd(DrawPattern pattern,PointF[] originPoints,bool enable)
        {
            this.patterns = new DrawPattern[originPoints.Length];
            this.originPoints = new PointF[originPoints.Length];
            this.enable = enable;
            for (int i = 0; i < this.originPoints.Length; i++)
            {
                this.originPoints[i] = DrawingUtils.Instance.CoordinateTrans(originPoints[i]);
                this.patterns[i] = pattern.Clone() as DrawPattern;
                this.doPatterns.Add(new DoPatternDrawCmd(patterns[i], originPoints[i],this.enable));
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

        public override RectangleF Rect => this.CalcaulateRect();


        public override void Setup(PointF origin)
        {            
            PointF Canvaseorigin = DrawingUtils.Instance.CoordinateTrans(origin);

            if(this.IsSelected)
            {
                for (int i = 0; i < this.doPatterns.Count; i++)
                {
                    this.doPatterns[i].IsSelected=true;
                    this.doPatterns[i].Setup(Canvaseorigin);
                }
            }
            else
            {
                for (int i = 0; i < this.doPatterns.Count; i++)
                {
                    this.doPatterns[i].IsSelected = false;
                }

                if (!this.enable)
                {
                    for (int i = 0; i < this.doPatterns.Count; i++)
                    {
                        this.doPatterns[i].SetupDisable(Canvaseorigin);
                    }
                }
                else
                {
                    if (this.isClick)
                    {
                        for (int i = 0; i < this.doPatterns.Count; i++)
                        {
                            this.doPatterns[i].IsClick = true;
                        }
                        this.isClick = false;
                    }
                    for (int i = 0; i < this.doPatterns.Count; i++)
                    {
                        this.doPatterns[i].Setup(Canvaseorigin);
                    }
                }
            }
                 
        }
        public override void SetupDisable(PointF origin)
        {
            PointF Canvaseorigin = DrawingUtils.Instance.CoordinateTrans(origin);
            for (int i = 0; i < this.doPatterns.Count; i++)
            {
                this.doPatterns[i].SetupDisable(Canvaseorigin);
            }
        }
        public override void DrawingPanel()
        {
            foreach (var item in doPatterns)
            {
                item.DrawingPanel();
            }
        }
        public override object Clone()
        {
            ArrayDrawCmd arrayCmdLine = MemberwiseClone() as ArrayDrawCmd;
            arrayCmdLine.doPatterns = doPatterns;
            arrayCmdLine.patterns = patterns.Clone() as DrawPattern[];
            arrayCmdLine.originPoints = originPoints;
            return arrayCmdLine;
        }

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {

        }
        private RectangleF CalcaulateRect()
        {
            float[] topPostion = new float[this.doPatterns.Count];
            float[] leftPostion = new float[this.doPatterns.Count];
            float[] bottomPostion = new float[this.doPatterns.Count];
            float[] rightPostion = new float[this.doPatterns.Count];
            for (int i = 0; i < this.doPatterns.Count; i++)
            {
                topPostion[i] = this.doPatterns[i].Rect.Top;
                leftPostion[i] = this.doPatterns[i].Rect.Left;
                bottomPostion[i] = this.doPatterns[i].Rect.Bottom;
                rightPostion[i] = this.doPatterns[i].Rect.Right;
            }

            return new RectangleF(new PointF(leftPostion.Min(), topPostion.Min()), new SizeF(rightPostion.Max() - leftPostion.Min(), bottomPostion.Max() - topPostion.Min()));
        }

        public override bool IsHitter(PointF point)
        {
            for (int i = 0; i < this.doPatterns.Count; i++)
            {
                if (this.doPatterns[i].IsHitter(point)) 
                {
                    this.IsSelected = !this.IsSelected;
                    return true;
                }
            }
            return false;
        }

        public override bool IsContain(RectangleF rect)
        {
            for (int i = 0; i < this.doPatterns.Count; i++)
            {
                if (!this.doPatterns[i].IsContain(rect))
                {
                    return false;
                }
            }
            this.IsSelected = !this.IsSelected;
            return true;
        }
    }
}
