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
    public class LineHitter : IHittable
    {
        public bool IsHitting(PointF mouseLocationInModel, DirectiveDraw entity)
        {
            LineDraw line = entity as LineDraw;
            return Geometry.PointInLine(mouseLocationInModel, line.startPoint, line.endPoint, line.lineWidth);
        }

    }
}
