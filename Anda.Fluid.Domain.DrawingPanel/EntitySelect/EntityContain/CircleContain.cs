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
    public class CircleContain : IContainable
    {
        public bool IsContain(RectangleF mouseRect, DirectiveDraw entity)
        {
            CircleDraw circle = entity as CircleDraw;
            return Geometry.RectContainCircle(mouseRect, circle.centerPosition, circle.radius);
        }
    }
}
