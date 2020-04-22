using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Grammar
{
    public class SnakeLineDrawCmd : LinesDrawCmd
    {
        /// <summary>
        /// 蛇形线指令（线段集合，线宽，颜色，是否带箭头）
        /// </summary>
        /// <param name = "lines" ></ param >
        /// < param name="linesWidth"></param>
        /// <param name = "linesColor" ></ param >
        /// < param name="isArrowlines"></param>
        public SnakeLineDrawCmd(Line2Points[] lines, bool isArrowlines,bool enable) : base(lines, isArrowlines, enable)
        {

        }
        
    }
}
