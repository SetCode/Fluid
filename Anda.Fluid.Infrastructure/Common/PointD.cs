using Anda.Fluid.Infrastructure.Reflection;
using System;
using System.Text;

namespace Anda.Fluid.Infrastructure.Common
{
    [Serializable]
    public class PointD : IEquatable<PointD>, ICloneable
    {
        [CompareAtt("CMP")]
        public double X;

        [CompareAtt("CMP")]
        public double Y;

        public PointD()
        {
            X = 0;
            Y = 0;
        }

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public PointD(PointD point)
        {
            X = point.X;
            Y = point.Y;
        }

        public void CopyFrom(PointD point)
        {
            X = point.X;
            Y = point.Y;
        }

        public static PointD Origin => new PointD(0, 0);

        public static PointD operator +(PointD left, PointD right)
        {
            return new PointD(left.X + right.X, left.Y + right.Y);
        }

        public static PointD operator +(PointD point, VectorD vector)
        {
            return new PointD(point.X + vector.X, point.Y + vector.Y);
        }

        public static PointD operator -(PointD left, VectorD right)
        {
            return new PointD(left.X - right.X, left.Y - right.Y);
        }

        public static VectorD operator -(PointD left, PointD right)
        {
            return new VectorD(left.X - right.X, left.Y - right.Y);
        }

        public VectorD VectorTo(PointD otherPoint)
        {
            return otherPoint - this;
        }

        public double DistanceTo(PointD otherPoint)
        {
            var vector = this.VectorTo(otherPoint);
            return vector.Length;
        }

        public VectorD ToVector()
        {
            return new VectorD(this.X, this.Y);
        }
        /// <summary>
        /// 旋转点位
        /// </summary>
        /// <param name="rotateCenter"></param>
        /// <param name="rotateAngle"></param>
        /// <returns></returns>
        public PointD Rotate(PointD rotateCenter,double rotateAngle)
        {
            PointD temp = new PointD(this);
            PointD Result = new PointD(this);
            temp.X -= rotateCenter.X;
            temp.Y -= rotateCenter.Y;
            rotateAngle = rotateAngle / 180 * Math.PI;
            //新坐标
            Result.X = temp.X * Math.Cos(rotateAngle) - temp.Y * Math.Sin(rotateAngle) + rotateCenter.X;
            Result.Y = temp.X * Math.Sin(rotateAngle) + temp.Y * Math.Cos(rotateAngle) + rotateCenter.Y;

            return Result;
        }

        public bool Equals(PointD other) => this.X.Equals(other.X) && this.Y.Equals(other.Y);

        public static bool operator ==(PointD point1, PointD point2)
        {
            if ((object)point1 == null && (object)point2 == null)
            {
                return true;
            }
            if ((object)point1 == null || (object)point2 == null)
            {
                return false;
            }
            if (point1.X != point2.X)
            {
                return false;
            }
            if (point1.Y != point2.Y)
            {
                return false;
            }
            return true;
        }

        public static bool operator !=(PointD point1, PointD point2)
        {
            return !(point1 == point2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is PointD
                && this == obj as PointD;
        }

        public override string ToString()
        {
            return new StringBuilder().Append("[").Append(X.ToString("0.000"))
                .Append(",").Append(Y.ToString("0.000")).Append("]").ToString();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
