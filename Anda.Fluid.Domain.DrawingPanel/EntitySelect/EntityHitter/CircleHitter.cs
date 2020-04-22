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
    public class CircleHitter : IHittable
    {
        public bool IsHitting(PointF mouseLocationInModel, DirectiveDraw entity)
        {
            CircleDraw circle = entity as CircleDraw;
            return Geometry.PointInCircle(mouseLocationInModel, circle.centerPosition, circle.radius, circle.lineWidth);
        }
    }
}
