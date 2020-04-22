using System;
//using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using static System.Math;

using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Spatial.Units;
using MathNet.Spatial.Euclidean;


namespace Anda.Fluid.Domain.FluProgram.CoordiConverter
{
    /// <summary>
    /// 坐标系
    /// </summary>
    public class TransformMatrix : DenseMatrix, IEquatable<TransformMatrix>
    {
        /// <summary>
        /// 父坐标系
        /// </summary>
        public TransformMatrix Parent { get; set; }
        public string Remarks { get; set; }

        /// <summary>
        /// 无参数则生成单位矩阵
        /// </summary>
        public TransformMatrix() : base(3)
        {
            this.SetRow(0, new[] { 1.0, 0, 0 });
            this.SetRow(1, new[] { 0, 1.0, 0 });
            this.SetRow(2, new[] { 0, 0, 1.0 });
            Parent = null;
        }

        public TransformMatrix(TransformMatrix parent) : base(3)
        {
            this.SetRow(0, new[] { 1.0, 0, 0 });
            this.SetRow(1, new[] { 0, 1.0, 0 });
            this.SetRow(2, new[] { 0, 0, 1.0 });
            Parent = parent;
        }

        /// <summary>
        /// Mark点对构造坐标系
        /// Suffix: o for old, n for new;
        /// 输入点坐标均为基于parent坐标系描述而非基于根坐标系
        /// 而相机直接获取的坐标是基于根坐标系的
        /// 因此需要经过从根坐标系到parent坐标系的转换后
        /// 才能输入到这个函数中建立新的坐标系。
        /// </summary>
        /// <param name="Mark1o"></param>
        /// <param name="Mark1n"></param>
        /// <param name="Mark2o"></param>
        /// <param name="Mark2n"></param>
        /// <param name="hasRotated"></param>
        /// <param name="hasScale"></param>
        /// <param name="parent"></param>
        public TransformMatrix(Point2D Mark1o, Point2D Mark1n, Point2D Mark2o, Point2D Mark2n,
            bool hasRotated = false, bool hasScale = false, TransformMatrix parent = null)
            : base(3)
        {
            double dxo = Mark1o.X - Mark2o.X;
            double dyo = Mark1o.Y - Mark2o.Y;
            double dxn = Mark1n.X - Mark2n.X;
            double dyn = Mark1n.Y - Mark2n.Y;

            double a, b, c, d, e;
            if (hasScale)
            {
                if (hasRotated)
                {
                    e = dxo * dxo + dyo * dyo;
                    a = (dyn * dyo + dxn * dxo) / e;
                    b = (dxn * dyo - dxo * dyn) / e;
                }
                else
                {
                    b = 0;
                    a = (dxn / dxo + dyn / dyo) / 2;//(dxn/dxo) is enough
                }
            }
            else
            {
                if (hasRotated)
                {
                    double xoxn = dxo * dxn;
                    double yoyn = dyo * dyn;
                    double xoyn = dxo * dyn;
                    double xnyo = dxn * dyo;
                    e = System.Math.Sqrt(Pow(xoxn, 2) + Pow(yoyn, 2) + Pow(xoyn, 2) + Pow(xnyo, 2));

                    a = (yoyn + xoxn) / e;
                    b = (xnyo - xoyn) / e;
                }
                else
                {
                    a = 1; b = 0;
                }
            }

            c = (Mark1n.X + Mark2n.X - a * (Mark1o.X + Mark2o.X) - b * (Mark1o.Y + Mark2o.Y)) / 2;
            d = (Mark1n.Y + Mark2n.Y - a * (Mark1o.Y + Mark2o.Y) + b * (Mark1o.X + Mark2o.X)) / 2;

            this.SetRow(0, new[] { a, b, c });
            this.SetRow(1, new[] { -b, a, d });
            this.SetRow(2, new[] { 0, 0, 1.0 });

            this.Parent = parent;
        }

        /// <summary>
        /// 单Mark点构造，无旋转无缩放
        /// </summary>
        /// <param name="Markold"></param>
        /// <param name="Marknew"></param>
        public TransformMatrix(Point2D Markold, Point2D Marknew, TransformMatrix parent = null) : base(3)
        {
            double dtx = Marknew.X - Markold.X;
            double dty = Marknew.Y - Markold.Y;
            this.SetRow(0, new[] { 1.0, 0, dtx });
            this.SetRow(1, new[] { 0, 1.0, dty });
            this.SetRow(2, new[] { 0, 0, 1.0 });
            Parent = parent;
        }

        /// <summary>
        /// 正向构造，平移-旋转-缩放
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="alpha"></param>
        /// <param name="scaling"></param>
        /// <param name="parent"></param>
        public TransformMatrix(Point2D origin = new Point2D(), Angle alpha = new Angle(), double scaling = 1.0, TransformMatrix parent = null)
           : base(3)
        {
            this.Parent = parent;

            DenseMatrix Mt = new DenseMatrix(3);
            Mt.SetRow(0, new[] { 1.0, 0, -origin.X });
            Mt.SetRow(1, new[] { 0, 1.0, -origin.Y });
            Mt.SetRow(2, new[] { 0, 0, 1.0 });

            DenseMatrix Ms = new DenseMatrix(3);
            Ms.SetRow(0, new[] { 1 / scaling, 0, 0 });
            Ms.SetRow(1, new[] { 0, 1 / scaling, 0 });
            Ms.SetRow(2, new[] { 0, 0, 1.0 });

            DenseMatrix Mr = new DenseMatrix(3);
            Mr.SetRow(0, new[] { Math.Cos(alpha.Radians), -Math.Sin(alpha.Radians), 0 });
            Mr.SetRow(1, new[] { Math.Sin(alpha.Radians), Math.Cos(alpha.Radians), 0 });
            Mr.SetRow(2, new[] { 0, 0, 1.0 });

            DenseMatrix M = Ms * Mr * Mt;
            this.SetRow(0, M.Row(0));
            this.SetRow(1, M.Row(1));
            this.SetRow(2, M.Row(2));

        }

        /// <summary>
        /// 两个已知坐标系间转换构造
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="aim"></param>
        public TransformMatrix(TransformMatrix parent, TransformMatrix aim) : base(3)
        {
            this.Parent = parent;

            if (parent == null || aim == null) { return; }

            TransformMatrix rootA = aim;
            var mb = CreateDiagonal(3, 3, 1.0).Inverse();
            while (rootA.Parent != null)
            {
                mb = rootA.Inverse() * mb;
                rootA = rootA.Parent;
            }

            TransformMatrix rootP = parent;
            var mq = CreateDiagonal(3, 3, 1.0).Inverse();
            while (rootP.Parent != null)
            {
                mq = rootP.Inverse() * mq;
                rootP = rootP.Parent;
            }

            if (rootA == rootP)
            {
                //Console.WriteLine("root equals\n");

                var mm = mb.Inverse() * mq;

                this.SetRow(0, mm.Row(0));
                this.SetRow(1, mm.Row(1));
                this.SetRow(2, mm.Row(2));
                //Console.WriteLine(mm.ToString());
            }
            else
            {
                //Console.WriteLine("root not equal");

                this.SetRow(0, new[] { 0.0, 0, 0 });
                this.SetRow(1, new[] { 0, 0.0, 0 });
                this.SetRow(2, new[] { 0, 0, 1.0 });
            }
        }

        /// <summary>
        /// 坐标系的层级。根坐标系为0.
        /// </summary>
        /// <returns></returns>
        public int GetLevel()
        {
            int level = 0;
            TransformMatrix p = this;
            while (p.Parent != null)
            {
                level++;
                p = p.Parent;
            }
            return level;
        }

        /// <summary>
        /// 根坐标系
        /// </summary>
        /// <returns></returns>
        public TransformMatrix GetRoot()
        {
            TransformMatrix rt = this;
            while (rt.Parent != null)
            {
                rt = rt.Parent;
            }
            return rt;
        }

        /// <summary>
        /// 将点从父坐标系转到当前坐标系
        /// </summary>
        /// <param name="points"></param>
        /// <param name="aim"></param>
        /// <returns></returns>
        public Point2D[] MapFromParent(Point2D[] points)
        {
            //Console.WriteLine("intoMapFunction\n");

            int s = points.Count();
            int i = 0;
            Point2D[] resultPoint = new Point2D[s];
            foreach (Point2D pt in points)
            {
                Vector3D v = new Vector3D(pt.X, pt.Y, 1);
                Vector3D vv = v.TransformBy(this);
                resultPoint[i] = new Point2D(vv.X, vv.Y);
                i++;
            }
            //Console.WriteLine("exitMapFunction\n");

            return resultPoint;
        }
        public Point2D MapFromParent(Point2D pt)
        {
            Vector3D v = new Vector3D(pt.X, pt.Y, 1);
            Vector3D vv = v.TransformBy(this);
            return new Point2D(vv.X, vv.Y);
        }

        /// <summary>
        /// 将点从本坐标系转到父坐标系
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point2D MapToParent(Point2D pt)
        {
            Vector3D v = new Vector3D(pt.X, pt.Y, 1);
            Vector3D vv = v.TransformBy(this.Inverse());
            return new Point2D(vv.X, vv.Y);
        }
        public Point2D[] MapToParent(Point2D[] points)
        {
            int s = points.Count();
            int i = 0;
            Point2D[] resultPoint = new Point2D[s];
            foreach (Point2D pt in points)
            {
                Vector3D v = new Vector3D(pt.X, pt.Y, 1);
                Vector3D vv = v.TransformBy(this.Inverse());
                resultPoint[i] = new Point2D(vv.X, vv.Y);
                i++;
            }
            return resultPoint;
        }

        /// <summary>
        /// 将点从本坐标系转到根坐标系
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point2D MapToRoot(Point2D pt)
        {
            TransformMatrix rootP = this;
            var m = CreateDiagonal(3, 3, 1.0).Inverse();
            while (rootP.Parent != null)
            {
                m = rootP.Inverse() * m;
                rootP = rootP.Parent;
            }
            Vector3D v = new Vector3D(pt.X, pt.Y, 1);
            Vector3D vv = v.TransformBy(m);
            return new Point2D(vv.X, vv.Y);
        }
        public Point2D[] MapToRoot(Point2D[] points)
        {
            TransformMatrix rootP = this;
            var m = CreateDiagonal(3, 3, 1.0).Inverse();
            while (rootP.Parent != null)
            {
                m = rootP.Inverse() * m;
                rootP = rootP.Parent;
            }

            int s = points.Count();
            int i = 0;
            Point2D[] resultPoint = new Point2D[s];
            foreach (Point2D pt in points)
            {
                Vector3D v = new Vector3D(pt.X, pt.Y, 1);
                Vector3D vv = v.TransformBy(m);
                resultPoint[i] = new Point2D(vv.X, vv.Y);
                i++;
            }
            return resultPoint;
        }

        /// <summary>
        /// 从根坐标系转到本坐标系
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public Point2D[] MapFromRoot(Point2D[] points)
        {
            TransformMatrix rootP = this;
            var m = CreateDiagonal(3, 3, 1.0).Inverse();
            while (rootP.Parent != null)
            {
                m = rootP.Inverse() * m;
                rootP = rootP.Parent;
            }
            var n = m.Inverse();

            int s = points.Count();
            int i = 0;
            Point2D[] resultPoint = new Point2D[s];
            foreach (Point2D pt in points)
            {
                Vector3D v = new Vector3D(pt.X, pt.Y, 1);
                Vector3D vv = v.TransformBy(n);
                resultPoint[i] = new Point2D(vv.X, vv.Y);
                i++;
            }
            return resultPoint;
        }
        public Point2D MapFromRoot(Point2D pt)
        {
            Point2D[] p = new Point2D[] { pt };
            Point2D[] q = MapFromRoot(p);
            return q[0];
        }

        /// <summary>
        /// 修改变换矩阵
        /// </summary>
        /// <param name="aim"></param>
        /// <returns></returns>
        public bool ReplacebyMatrix(TransformMatrix aim)
        {
            if (aim == null) return false;
            this.SetRow(0, aim.Row(0));
            this.SetRow(1, aim.Row(1));
            this.SetRow(2, aim.Row(2));
            return true;
        }
        public bool ReplacebyMatrix(DenseMatrix aim)
        {
            if (aim == null) return false;
            if (aim.RowCount != 3 || aim.ColumnCount != 3) return false;
            this.SetRow(0, aim.Row(0));
            this.SetRow(1, aim.Row(1));
            this.SetRow(2, aim.Row(2));
            return true;
        }

        /// <summary>
        /// 将一个坐标系下的点转至另一坐标系
        /// </summary>
        /// <param name="fromCd"></param>
        /// <param name="points"></param>
        /// <param name="toCd"></param>
        /// <returns></returns>
        public static Point2D[] MapPoints(TransformMatrix fromCd, Point2D[] points, TransformMatrix toCd)
        {
            TransformMatrix cd = new TransformMatrix(fromCd, toCd);
            return cd.MapFromParent(points);
        }
        public static Point2D MapPoints(TransformMatrix fromCd, Point2D pt, TransformMatrix toCd)
        {
            Point2D[] p = new Point2D[] { pt };
            Point2D[] q = MapPoints(fromCd, p, toCd);
            return q[0];
        }

        public bool Equals(TransformMatrix other)
        {
            double[] ma = this.Values;
            double[] mb = other.Values;

            int len = ma.Count();
            if (mb.Count() != len) { return false; }

            for (int c = 0; c < len; c++)
            { if (ma[c] != mb[c]) return false; }

            if (this.Parent != other.Parent) { return false; }

            return true;
        }

    }
}
