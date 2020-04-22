using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MathNet.Spatial.Units;
using MathNet.Spatial.Euclidean;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.CoordiConverter
{
    public class CordinateSystem
    {
        public CordinateSystem(int level, int index, int parentIndex, PointD origin)
        {
            this.Level = level;
            this.Index = index;
            this.ParentIndex = parentIndex;         
            this.Origin = origin;

            this.StandardMarkPoint1 = new PointD(-1, -1);
            this.StandardMarkPoint2 = new PointD(-1, -1);

            this.TM = new TransformMatrix();
            this.OTM = new TransformMatrix(new Point2D(origin.X, origin.Y), Angle.FromDegrees(0), 1.0);

            this.TM.Parent = this.OTM;
        }

        public int Level { get; }//坐标系在整个坐标系群中所在的层级
        public int Index { get; }//坐标系在当前层级中的编号
        public int ParentIndex { get; }//父坐标系的编号

        public PointD Origin { get; set; }
        public PointD StandardMarkPoint1 { get; set; }//坐标系的模板Mark点1
        public PointD StandardMarkPoint2 { get; set; }//坐标系的模板Mark点2

        public TransformMatrix TM;//变换矩阵
        public TransformMatrix OTM;
    }
}
