using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingPanel.DrawingProgram.Executant;
using DrawingPanel.DrawingProgram.Grammar;
using DrawingPanel.Utils.MathUtils;

namespace DrawingPanel.EntitySelect.EntityHitter
{
    public class DotHitter : IHittable
    {

        public bool IsHitting(PointF mouseLocationInModel, DirectiveDraw entity)
        {
            DotDraw dot = entity as DotDraw;
            return Geometry.PointInDot(mouseLocationInModel, dot.centerPosition, dot.radius);
        }
    }
}
