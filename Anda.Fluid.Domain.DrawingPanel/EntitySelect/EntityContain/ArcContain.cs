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
    public class ArcContain : IContainable
    {
        public bool IsContain(RectangleF mouseRect, DirectiveDraw entity)
        {
            ArcDraw arc = entity as ArcDraw;
            if (arc.degree == 360f || arc.degree == -360f)
            {
                return Geometry.RectContainCircle(mouseRect, arc.centerPosition, arc.radius);
            }
            else
            {
                return Geometry.RectContainArc(mouseRect, arc.centerPosition, arc.startPosition, arc.endPosition);
            }
        }
    }
}
