using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.DrawingProgram.Executant
{
    public abstract class DirectiveDraw:ICloneable
    {
        public abstract object Clone();
        public abstract void Execute();

        public abstract bool IsHitter(PointF point);

        public abstract bool IsContain(RectangleF rect);
    }
}
