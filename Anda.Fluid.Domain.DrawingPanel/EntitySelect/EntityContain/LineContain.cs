using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingPanel.DrawingProgram.Executant;
using DrawingPanel.Utils.MathUtils;

namespace DrawingPanel.EntitySelect.EntityContain
{
    public class LineContain : IContainable
    {
        public bool IsContain(RectangleF mouseRect, DirectiveDraw entity)
        {
            LineDraw line = entity as LineDraw;
            return Geometry.RectContainLine(mouseRect, line.startPoint, line.endPoint);
        }
    }
}
