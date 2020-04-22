using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Grammar
{
    public class DoMultiPassDrawCmd : DoPatternDrawCmd
    {
        /// <summary>
        /// 做多组循环，在画图中和DoPattern一样
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="position"></param>
        public DoMultiPassDrawCmd(DrawPattern pattern, PointF position,bool enable) : base(pattern, position, enable)
        {
        }
    }
}
