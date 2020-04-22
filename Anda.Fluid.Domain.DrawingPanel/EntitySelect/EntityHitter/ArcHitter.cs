using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingPanel.DrawingProgram.Grammar;
using DrawingPanel.Utils.MathUtils;
using DrawingPanel.DrawingProgram.Executant;

namespace DrawingPanel.EntitySelect.EntityHitter
{
    public class ArcHitter : IHittable
    {

        public bool IsHitting(PointF mouseLocationInModel, DirectiveDraw entity)
        {
            ArcDraw arc = entity as ArcDraw;
            return Geometry.PointInArc(mouseLocationInModel, 
                arc.centerPosition, arc.startPosition,arc.endPosition, arc.degree, arc.lineWidth);
        }
    }
}
