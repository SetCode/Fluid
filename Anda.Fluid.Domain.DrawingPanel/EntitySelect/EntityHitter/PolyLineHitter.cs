using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingPanel.DrawingProgram.Executant;
using DrawingPanel.Utils.MathUtils;

namespace DrawingPanel.EntitySelect.EntityHitter
{
    public class PolyLineHitter : IHittable
    {
        public bool IsHitting(PointF mouseLocationInModel, DirectiveDraw entity)
        {
            PolyLineDraw polyLine = entity as PolyLineDraw;
            for (int i = 0; i < polyLine.points.Length-1; i++)
            {
                if (Geometry.PointInLine(mouseLocationInModel, polyLine.points[i], polyLine.points[i + 1], polyLine.lineWidth))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
