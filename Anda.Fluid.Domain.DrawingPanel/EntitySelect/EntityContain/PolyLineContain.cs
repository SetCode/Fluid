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
    public class PolyLineContain : IContainable
    {
        public bool IsContain(RectangleF mouseRect, DirectiveDraw entity)
        {
            PolyLineDraw polyLine = entity as PolyLineDraw;
            for (int i = 0; i < polyLine.points.Length - 1; i++)
            {
                if (!Geometry.RectContainLine(mouseRect, polyLine.points[i], polyLine.points[i + 1]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
