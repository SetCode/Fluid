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
    public class DotContain : IContainable
    {
        public bool IsContain(RectangleF mouseRect, DirectiveDraw entity)
        {
            DotDraw dot = entity as DotDraw;
            return Geometry.RectContainCircle(mouseRect, dot.centerPosition, dot.radius);
        }
    }
}
