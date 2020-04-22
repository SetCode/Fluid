using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal partial class FindCircle : UserControl
    {
        public FindCircle()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 单点直接找圆心
        /// </summary>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static PointD GetCenter(PointD center)
        {
            return center;
        }
        /// <summary>
        /// 三点求圆心
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static PointD GetCenter(PointD p1,PointD p2,PointD p3)
        {
            //令：
            //a1 = 2 * pt2.x - 2 * pt1.x      b1 = 2 * pt1.y - 2 * pt2.y       c1 = pt1.y² + pt2.x² - pt1.x² - pt2.y²
            //a2 = 2 * pt3.x - 2 * pt2.x      b2 = 2 * pt2.y - 2 * pt3.y       c2 = pt2.y² + pt3.x² - pt2.x² - pt3.y²
            PointD center = new PointD();
            double a1, a2, b1, b2, c1, c2, temp;
            a1 = p1.X - p2.X;
            b1 = p1.Y - p2.Y;
            c1 = Math.Pow(p1.X, 2) - Math.Pow(p2.X, 2) + Math.Pow(p1.Y, 2) - Math.Pow(p2.Y, 2) / 2;
            a2 = p3.X - p2.X;
            b2 = p3.Y - p2.Y;
            c2 = Math.Pow(p3.X, 2) - Math.Pow(p2.X, 2) + Math.Pow(p3.Y, 2) - Math.Pow(p2.Y, 2) / 2;
            temp = a1 * b2 - a2 * b1;
            //判断三点是否共线
            if (temp == 0)
            {
                //共线则将第一个点pt1作为圆心
                center.X = p1.X;
                center.Y = p1.Y;
            }
            else
            {
                //不共线则求出圆心：
                //center.x = (C1*B2 - C2*B1) / A1*B2 - A2*B1;
                //center.y = (A1*C2 - A2*C1) / A1*B2 - A2*B1;
                center.X = (c1 * b2 - c2 * b1) / temp;
                center.Y = (a1 * c2 - a2 * c1) / temp;
            }
            return center;
        }

    }
}
