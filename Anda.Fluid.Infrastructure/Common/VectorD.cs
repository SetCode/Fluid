using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Common
{
    [Serializable]
    public class VectorD : IEquatable<VectorD>, ICloneable
    {
        public double X;

        public double Y;

        public VectorD()
            : this(0, 0)
        {

        }

        public VectorD(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public static VectorD XAxis { get; } = new VectorD(1, 0);

        public static VectorD YAxis { get; } = new VectorD(0, 1);

        public double Length => Math.Sqrt((this.X * this.X) + (this.Y * this.Y));

        public static bool operator ==(VectorD left, VectorD right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(VectorD left, VectorD right)
        {
            return !left.Equals(right);
        }

        public static VectorD operator +(VectorD left, VectorD right)
        {
            return left.Add(right);
        }

        public static PointD operator +(VectorD v, PointD p)
        {
            return new PointD(p.X + v.X, p.Y + v.Y);
        }

        public static VectorD operator -(VectorD left, VectorD right)
        {
            return left.Subtract(right);
        }

        public static VectorD operator -(VectorD v)
        {
            return v.Negate();
        }

        public static VectorD operator *(double d, VectorD v)
        {
            return new VectorD(d * v.X, d * v.Y);
        }

        public static VectorD operator *(VectorD v, double d)
        {
            return d * v;
        }

        public static VectorD operator /(VectorD v, double d)
        {
            return new VectorD(v.X / d, v.Y / d);
        }

        public double DotProduct(VectorD other)
        {
            return (this.X * other.X) + (this.Y * other.Y);
        }

        public double CrossProduct(VectorD other)
        {
            return (this.X * other.Y) - (this.Y * other.X);
        }

        public VectorD Normalize()
        {
            var l = this.Length;
            return new VectorD(this.X / l, this.Y / l);
        }

        public VectorD ScaleBy(double d)
        {
            return new VectorD(d * this.X, d * this.Y);
        }

        public VectorD Negate()
        {
            return new VectorD(-1 * this.X, -1 * this.Y);
        }

        public VectorD Subtract(VectorD v)
        {
            return new VectorD(this.X - v.X, this.Y - v.Y);
        }

        public VectorD Add(VectorD v)
        {
            return new VectorD(this.X + v.X, this.Y + v.Y);
        }

        public PointD ToPoint()
        {
            return new PointD(this.X, this.Y);
        }
        /// <summary>
        /// 旋转角为正值逆时针旋转；负值顺时针旋转
        /// </summary>
        /// <param name="rotateAngle"></param>
        /// <returns></returns>
        public VectorD Rotate(double rotateAngle)
        {
            VectorD temp = new VectorD(this.X, this.Y);
            VectorD Result = new VectorD(this.X, this.Y);
            rotateAngle = rotateAngle / 180 * Math.PI;

            Result.X = temp.X * Math.Cos(rotateAngle) + temp.Y * Math.Sin(rotateAngle);
            Result.Y = temp.Y * Math.Cos(rotateAngle) - temp.X * Math.Sin(rotateAngle);

            return Result;
        }

        public bool Equals(VectorD other) => this.X.Equals(other.X) && this.Y.Equals(other.Y);

        public override bool Equals(object obj)
        {
            if(obj is VectorD)
            {
                VectorD v = obj as VectorD;
                if(this.Equals(v))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public override string ToString()
        {
            return new StringBuilder().Append("[").Append(X.ToString("0.000"))
                .Append(",").Append(Y.ToString("0.000")).Append("]").ToString();
        }

        public object Clone()
        {
            VectorD v = new VectorD();
            v.X = this.X;
            v.Y = this.Y;
            return v;
        }

    }
}
