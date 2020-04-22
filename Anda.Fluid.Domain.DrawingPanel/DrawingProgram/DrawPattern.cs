
using DrawingPanel.DrawingProgram.Grammar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram
{
    public class DrawPattern : ICloneable
    {
        public List<DrawCmdLine> cmdLineList = new List<DrawCmdLine>();
        public PointF origin;
        public RectangleF Rect => this.CalcaulateRect();

        public DrawPattern():this(new PointF(0,0))
        {

        }
        public DrawPattern(PointF origin)
        {
            this.origin = origin;
            this.Add(new OriginDrawCmd(true));
        }        
        public PointF Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        public List<DrawCmdLine> CmdLineList => this.cmdLineList;

        public void Add(DrawCmdLine cmdline)
        {
            this.cmdLineList.Add(cmdline);            
        }

        /// <summary>
        /// 构建pattern中的所有指令
        /// </summary>
        public void SetupAll()
        {
            foreach (var item in this.cmdLineList)
            {
                if (item == null)
                {

                }
                else
                {
                    item.Setup(this.origin);
                }
                
            }
        }
        public void SetupAllDisable()
        {
            foreach (var item in this.cmdLineList)
            {
                if (item == null)
                {

                }
                else
                {
                    item.SetupDisable(this.origin);
                }

            }
        }

        public void SetupAllSelected(bool isSelected)
        {
            foreach (var item in this.cmdLineList)
            {
                if (item == null)
                {

                }
                else
                {
                    item.IsSelected = isSelected;
                }

            }
        }

        public object Clone()
        {
            DrawPattern pattern = MemberwiseClone() as DrawPattern;
            // deeply copy.
            pattern.cmdLineList = new List<DrawCmdLine>();
            foreach (DrawCmdLine cmdLine in cmdLineList)
            {
                if (cmdLine == null)
                {

                }
                else
                {
                    pattern.cmdLineList.Add(cmdLine.Clone() as DrawCmdLine);
                }
                
            }
            pattern.origin = new PointF();
            pattern.origin.X = origin.X;
            pattern.origin.Y = origin.Y;

            return pattern;
        }

        private  RectangleF CalcaulateRect()
        {
            List<float> topPosition = new List<float>();
            List<float> bottomPosition = new List<float>();
            List<float> leftPosition = new List<float>();
            List<float> rigthPosition = new List<float>();
            for (int i = 0; i < this.cmdLineList.Count; i++)
            {
                if (this.cmdLineList[i] != null)
                {
                    topPosition.Add(this.cmdLineList[i].Rect.Top);
                    bottomPosition.Add(this.cmdLineList[i].Rect.Bottom);
                    leftPosition.Add(this.cmdLineList[i].Rect.Left);
                    rigthPosition.Add(this.cmdLineList[i].Rect.Right);
                }
            }

            return new RectangleF(new PointF(leftPosition.Min(), topPosition.Min()), 
                new SizeF(rigthPosition.Max() - leftPosition.Min(), bottomPosition.Max() - topPosition.Min()));
        }
    }
}
