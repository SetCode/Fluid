using Anda.Fluid.Domain.FluProgram;
using DrawingPanel.DrawingProgram;
using DrawingPanel.DrawingProgram.Grammar;
using DrawingPanel.Msg;
using DrawingPanel.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel
{
     public class DrawProgram:IDrawingMsgReceiver
    {
        private bool enterWorkpiece = true;
        private int enterPatternNo;

        private static DrawProgram instance = new DrawProgram();
        private DrawWorkPiece workPiece;
        private List<DrawPattern> patterns = new List<DrawPattern>();
        private DrawingParser drawingParser = new DrawingParser();

        private DrawProgram() { }

        public static DrawProgram Instance => instance;

        public Graphics Graphics { get; set; }

        public DrawWorkPiece WorkPiece => this.workPiece;

        public List<DrawPattern> Patterns => this.patterns;

        public RectangleF Rect
        {
            get
            {
                if (enterWorkpiece)
                {
                    if (this.workPiece == null)
                    {
                        return new RectangleF();
                    }
                    else
                    {
                        return this.workPiece.Rect;
                    }
                }
                else
                {
                    if (this.patterns.Count < 1)
                    {
                        return new RectangleF();
                    }
                    else
                    {
                        return this.patterns[enterPatternNo].Rect;
                    }
                }
            }
        }

        /// <summary>
        /// 进行绘图
        /// </summary>
        public void Drawing()
        {

            if (this.workPiece == null)
                return;

            if (enterWorkpiece)
            {
                for (int i = 0; i < this.workPiece.CmdLineList.Count; i++)
                {
                    if (this.workPiece.CmdLineList[i] == null)
                    { }
                    else
                    {
                        this.workPiece.CmdLineList[i].DrawingPanel();
                    }
                   
                }
            }          

            else
            {
                for (int i = 0; i < this.patterns[this.enterPatternNo].cmdLineList.Count; i++)
                {
                    if (this.patterns[this.enterPatternNo].cmdLineList[i] == null) 
                    {

                    }
                    else
                    {
                        this.patterns[this.enterPatternNo].cmdLineList[i].DrawingPanel();
                    }
                    
                }

            }
           //GlobalData.Instance.Graphics.Dispose();
        }

        public void Update(FluidProgram fluidProgram)
        {
            DrawWorkPiece drawWorkpiece;
            List<DrawPattern> drawPatterns;
            drawingParser.ParseFluidProgram(fluidProgram, out drawWorkpiece, out drawPatterns);

            this.patterns = drawPatterns;

            foreach (var item in drawPatterns)
            {
                item.SetupAll();
            }

            this.workPiece = null;
            this.workPiece = drawWorkpiece;
            this.workPiece.SetupAll();
            this.enterPatternNo = patterns.Count - 1;

        }

        public void EnterWorkpiece()
        {
            this.EscIsClick();
            this.enterWorkpiece = true;

        }

        public void EnterPattern(int patternNo)
        {
            this.EscIsClick();
            this.enterWorkpiece = false;
            this.enterPatternNo = patternNo;
        }

        public void ClickCmdLine(bool inWorkpiece, int patternNo, int[] cmdLineNo)
        {
            if (cmdLineNo == null)
                return;

            if(inWorkpiece)
            {
                for (int i = 0; i < cmdLineNo.Length; i++)
                {
                    if(this.workPiece.cmdLineList[cmdLineNo[i] + 1]==null)
                    {

                    }
                    else
                    {
                        this.workPiece.cmdLineList[cmdLineNo[i] + 1].IsClick = true;
                    }
                   
                }
            }
            else
            {
                for (int j = 0; j < cmdLineNo.Length; j++)
                {
                    if(this.patterns[patternNo].cmdLineList[cmdLineNo[j] + 1]==null)
                    {

                    }
                    else
                    {
                        this.patterns[patternNo].cmdLineList[cmdLineNo[j] + 1].IsClick = true;
                    }
                   
                }
            }

            foreach (var item in patterns)
            {
                item.SetupAll();
            }
            this.workPiece.SetupAll();
        }
      
        public void HitterCmdLine(PointF point)
        {
            if (enterWorkpiece)
            {
                if (this.workPiece == null)
                    return;
                for (int i = 1; i < this.workPiece.cmdLineList.Count; i++)
                {
                    if (this.workPiece.cmdLineList[i]!= null && 
                            this.workPiece.cmdLineList[i].IsHitter(point))
                    {

                        foreach (var item in patterns)
                        {
                            item.SetupAll();
                        }
                        this.workPiece.SetupAll();
                        return;
                    }
                }
            }
            else
            {
                if (this.patterns[this.enterPatternNo] == null)
                    return;
                for (int i = 1; i < this.patterns[this.enterPatternNo].cmdLineList.Count; i++)
                {
                    if (this.patterns[this.enterPatternNo].cmdLineList[i] != null &&
                        this.patterns[this.enterPatternNo].cmdLineList[i].IsHitter(point))
                    {

                        foreach (var item in patterns)
                        {
                            item.SetupAll();
                        }
                        this.workPiece.SetupAll();
                        return;
                    }
                }

            }

        }

        public void ContainCmdLine(RectangleF rect)
        {
            if (enterWorkpiece)
            {
                if (this.workPiece == null)
                    return;
                for (int i = 1; i < this.workPiece.cmdLineList.Count; i++)
                {
                    if (this.workPiece.cmdLineList[i] != null)
                    {
                        this.workPiece.cmdLineList[i].IsContain(rect);
                    }
                }
            }
            else
            {
                if (this.patterns[this.enterPatternNo] == null)
                    return;
                for (int i = 1; i < this.patterns[this.enterPatternNo].cmdLineList.Count; i++)
                {
                    if (this.patterns[this.enterPatternNo].cmdLineList[i] != null)
                        
                    {
                        this.patterns[this.enterPatternNo].cmdLineList[i].IsContain(rect);
                    }
                }

            }

            foreach (var item in patterns)
            {
                item.SetupAll();
            }
            this.workPiece.SetupAll();
        }

        public void EscIsClick()
        {
            if (this.workPiece == null)
                return;
            for (int i = 1; i < this.workPiece.cmdLineList.Count; i++)
            {
                if (this.workPiece.cmdLineList[i] != null)
                {
                    this.workPiece.cmdLineList[i].IsSelected = false;
                }
            }
            this.workPiece.SetupAll();

            if (this.patterns == null || this.patterns.Count == 0)
                return;
            for (int i = 1; i < this.patterns[this.enterPatternNo].cmdLineList.Count; i++)
            {
                if (this.patterns[this.enterPatternNo].cmdLineList[i] != null)

                {
                    this.patterns[this.enterPatternNo].cmdLineList[i].IsSelected = false;
                }
            }
            foreach (var item in patterns)
            {
                item.SetupAll();
            }

        }

        public void EditDrawCmd()
        {
            if (this.enterWorkpiece)
            {
                if (this.workPiece == null)
                    return;
                List<int> selectedCmd = new List<int>();
                for (int i = 1; i < this.workPiece.cmdLineList.Count; i++)
                {
                    if (this.workPiece.cmdLineList[i] != null && this.workPiece.cmdLineList[i].IsSelected) 
                    {
                        selectedCmd.Add(i - 1);
                    }
                }
                if (selectedCmd.Count == 1)
                {
                    DrawingMsgCenter.Instance.singleReceiver.EditSingleDrawCmd(selectedCmd[0]);
                }
                else if (selectedCmd.Count > 1)
                {
                    DrawingMsgCenter.Instance.multiRecierver.EditMultiDrawCmd(selectedCmd);
                }
            }
            else
            {
                if (this.patterns[this.enterPatternNo] == null)
                    return;
                List<int> selectedCmd = new List<int>();
                for (int i = 1; i < this.patterns[enterPatternNo].cmdLineList.Count; i++)
                {
                    if (this.patterns[enterPatternNo].cmdLineList[i] != null && this.patterns[enterPatternNo].cmdLineList[i].IsSelected)
                    {
                        selectedCmd.Add(i - 1);
                    }
                }
                if (selectedCmd.Count == 1)
                {
                    DrawingMsgCenter.Instance.singleReceiver.EditSingleDrawCmd(selectedCmd[0]);
                }
                else if (selectedCmd.Count > 1)
                {
                    DrawingMsgCenter.Instance.multiRecierver.EditMultiDrawCmd(selectedCmd);
                }
            }

        }

        /// <summary>
        /// 将选中的轨迹映射到脚本编辑器里
        /// </summary>
        public void RelateScriptEditor()
        {
            if (this.enterWorkpiece)
            {
                if (this.workPiece == null)
                    return;
                List<int> selectedCmd = new List<int>();
                for (int i = 1; i < this.workPiece.cmdLineList.Count; i++)
                {
                    if (this.workPiece.cmdLineList[i] != null && this.workPiece.cmdLineList[i].IsSelected)
                    {
                        selectedCmd.Add(i - 1);
                    }
                }
                DrawingMsgCenter.Instance.relateScriptReceiver.RelateScirptEditor(selectedCmd);
            }
            else
            {
                if (this.patterns[this.enterPatternNo] == null)
                    return;
                List<int> selectedCmd = new List<int>();
                for (int i = 1; i < this.patterns[enterPatternNo].cmdLineList.Count; i++)
                {
                    if (this.patterns[enterPatternNo].cmdLineList[i] != null && this.patterns[enterPatternNo].cmdLineList[i].IsSelected)
                    {
                        selectedCmd.Add(i - 1);
                    }
                }
                DrawingMsgCenter.Instance.relateScriptReceiver.RelateScirptEditor(selectedCmd);
            }
        }

        public void SetTrackColorAndWidth()
        {
            if (this.workPiece == null)
            {
                return;
            }
            foreach (var item in patterns)
            {
                item.SetupAll();
            }
            this.workPiece.SetupAll();
        }
    }
}
