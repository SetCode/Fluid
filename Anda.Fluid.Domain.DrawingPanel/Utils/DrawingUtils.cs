
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.Utils
{
    public class DrawingUtils
    {
        private static DrawingUtils instance = new DrawingUtils();
        private  DrawingUtils()
        {

        }
        public static DrawingUtils Instance => instance;

            
        public PointF CoordinateTrans(PointF pointF)
        {
            return this.CoordinateTrans(pointF.X, pointF.Y);
        }
        public PointF CoordinateTrans(float x,float y)
        {
            PointF point = new PointF(0,0);
            point.X = x;
            point.Y = - y;
            return point;
        }
        
    }
}
