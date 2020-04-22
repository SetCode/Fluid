using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Grammar
{
    public class DoPatternDrawCmd : DrawCmdLine
    {
        private DrawPattern pattern;
        private PointF position;
        private bool enable;
        private bool isClick = false;
        public DoPatternDrawCmd(DrawPattern pattern,PointF position,bool enable)
        {
            this.pattern = pattern.Clone() as DrawPattern;
            this.position = position;
            this.enable = enable;
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

        public override RectangleF Rect => this.pattern.Rect;

        public override void Setup(PointF origin)
        {
            PointF point = new PointF(this.position.X + origin.X, this.position.Y + origin.Y);
            this.pattern.Origin = point;

            if(this.IsSelected)
            {
                this.pattern.SetupAllSelected(true);
                this.pattern.SetupAll();
            }
            else 
            {
                this.pattern.SetupAllSelected(false);
                if (!this.enable)
                {
                    this.pattern.SetupAllDisable();
                }
                else
                {

                    if (this.isClick)
                    {
                        foreach (var item in pattern.cmdLineList)
                        {
                            item.IsClick = true;
                        }
                        this.pattern.SetupAll();

                        foreach (var item in pattern.cmdLineList)
                        {
                            item.IsClick = false;
                        }

                        this.isClick = false;
                    }
                    else
                    {
                        this.pattern.SetupAll();
                    }

                }
            }
            
           
        }
        public override void SetupDisable(PointF origin)
        {
            PointF point = new PointF(this.position.X + origin.X, this.position.Y + origin.Y);
            this.pattern.Origin = point;
            this.pattern.SetupAllDisable();
        }
        public override void DrawingPanel()
        {
            for (int i = 0; i < this.pattern.CmdLineList.Count; i++)
            {
                if (i == 0)
                {

                }
                else
                {
                    this.pattern.CmdLineList[i].DrawingPanel();
                }
            }
            PointF p = this.pattern.Origin;
        }
        public override object Clone()
        {
            DoPatternDrawCmd doPatternCmdLine = MemberwiseClone() as DoPatternDrawCmd;
            doPatternCmdLine.pattern = pattern.Clone() as DrawPattern;
            doPatternCmdLine.position = position;
            return doPatternCmdLine;
        }

        protected override void SetTrack(PointF ModelOrigin, Color backColor)
        {
            
        }

        public override bool IsHitter(PointF point)
        {
            for (int i = 1; i < this.pattern.cmdLineList.Count; i++)
            {
                if (this.pattern.cmdLineList[i] != null && this.pattern.cmdLineList[i].IsHitter(point))
                {
                    this.IsSelected = !this.IsSelected;
                    return true;
                }
            }
            return false;
        }

        public override bool IsContain(RectangleF rect)
        {
            for (int i = 1; i < this.pattern.cmdLineList.Count; i++)
            {
                if (this.pattern.cmdLineList[i] != null && !this.pattern.cmdLineList[i].IsContain(rect))
                {
                    return false;
                }
            }
            this.IsSelected = !this.IsSelected;
            return true; ;
        }
    }
}
