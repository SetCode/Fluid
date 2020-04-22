using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MathNet.Spatial.Euclidean;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.FluProgram.CoordiConverter
{
    /// <summary>
    /// 坐标系管理类
    /// created by wzf,2018/11/2
    /// </summary>
    /// 
    public class CordinateSystemManager
    {
       /// <summary>
       /// 坐标系管理数据结构，以坐标系Level为Key，属于同一Level的坐标系存放在List中作为Value
       /// </summary>
        private Dictionary<int, List<CordinateSystem>> CordinateSystemDictionary = new Dictionary<int, List<CordinateSystem>>();

        /// <summary>
        /// 在坐标系管理数据结构中添加坐标系
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public bool AddCordinateSystem(CordinateSystem cs)
        {
            List<CordinateSystem> csl;

            CordinateSystem csParent = GetCordinateSystem(cs.Level - 1, cs.ParentIndex);
            if(csParent != null)
            {
                cs.OTM.Parent  = csParent.TM;
            }

            if (CordinateSystemDictionary.TryGetValue(cs.Level, out csl))
            {
                foreach(var item in csl)
                {
                    if (item.Index == cs.Index) { return false; }
                }

                csl.Add(cs);
            }
            else
            {
                csl = new List<CordinateSystem>();
                csl.Add(cs);
                CordinateSystemDictionary.Add(cs.Level, csl);
            }

            return true;
        }

        /// <summary>
        /// 在坐标系管理数据结构中删除坐标系
        /// </summary>
        /// <param name="csLevel"></param>
        /// <param name="csIndex"></param>
        /// <returns></returns>
        public bool DeleteCordinateSystem(int csLevel, int csIndex)
        {
            List<CordinateSystem> csl;
            int tempLevel = csLevel;
            List<int> tempIndex = new List<int>();
            tempIndex.Add(csIndex);

            if (CordinateSystemDictionary.TryGetValue(tempLevel, out csl))
            {
                foreach (var item in csl)
                {
                    if (item.Index == csIndex)
                    {
                        csl.Remove(item);
                        tempLevel++;
                        break;
                    }
                }
            }

            while (CordinateSystemDictionary.TryGetValue(tempLevel, out csl))
            {
                List<int> pi = new List<int>();
                for (int i = csl.Count -1; i > -1; --i)
                {
                    foreach(var itemIndex in tempIndex)
                    {
                        if(csl[i].ParentIndex == itemIndex)
                        {
                            pi.Add(itemIndex);
                            csl.RemoveAt(i);
                            break;
                        }
                    }
                }

                tempIndex.Clear();
                tempIndex = pi;
                tempLevel++;
            }

            return true;
        }
        //public bool ModifyCordinateSystem(CordinateSystem cs) { return true; }

        /// <summary>
        /// 根据Level和Index设置坐标系的的模板Mark点
        /// </summary>
        /// <param name="csLevel"></param>
        /// <param name="csIndex"></param>
        /// <param name="markPoint1"></param>
        /// <param name="markPoint2"></param>
        /// <returns></returns>
        public bool SetStandardMarkPoint(int csLevel, int csIndex, PointD markPoint1, PointD markPoint2)
        {
            CordinateSystem cs = GetCordinateSystem(csLevel, csIndex);
            if (cs != null)
            {
                cs.StandardMarkPoint1 = markPoint1;
                cs.StandardMarkPoint2 = markPoint2;

                return true;
            }

            return false;
        }
        public bool SetStandardMarkPoint(int csLevel, int csIndex, PointD markPoint)
        {
            CordinateSystem cs = GetCordinateSystem(csLevel, csIndex);
            if (cs != null)
            {
                cs.StandardMarkPoint1 = markPoint;
                return true;
            }

            return false;
        }
        /// <summary>
        /// ????????
        /// </summary>
        /// <param name="csLevel"></param>
        /// <param name="csIndex"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public bool SetOrigin(int csLevel, int csIndex, PointD origin)
        {
            CordinateSystem cs = GetCordinateSystem(csLevel, csIndex);
            if (cs != null)
            {
                cs.Origin = origin;

                return true;
            }

            return false;
        }

        /// <summary>
        ///通过Mark点更新变换矩阵
        /// </summary>
        /// <param name="csLevel"></param>
        /// <param name="csIndex"></param>
        /// <param name="markPoint1"></param>
        /// <param name="markPoint2"></param>
        /// <returns></returns>
        public bool SetCurrentMarkPoint(int csLevel, int csIndex, PointD markPoint1, PointD markPoint2)//
        {
            CordinateSystem cs = GetCordinateSystem(csLevel, csIndex);

            if (cs != null)
            {
                CordinateSystem csParent = null;
                if (csLevel > 1)
                {
                    csParent = GetCordinateSystem(csLevel - 1, cs.ParentIndex);

                    if(csParent == null) { return false; }
                }
                      
                if(csParent != null)
                {
                    Point2D[] mp = { new Point2D(cs.StandardMarkPoint1.X, cs.StandardMarkPoint1.Y), new Point2D(markPoint1.X, markPoint1.Y), new Point2D(cs.StandardMarkPoint2.X, cs.StandardMarkPoint2.Y),
                            new Point2D(markPoint2.X, markPoint2.Y) };
                    Point2D[] ret = cs.OTM.MapFromRoot(mp);

                    TransformMatrix tempTm = new TransformMatrix(ret[0], ret[1], ret[2], ret[3]);
                    cs.TM.ReplacebyMatrix(tempTm);
                    tempTm = null;
                }
                else
                {
                    TransformMatrix tempTm = new TransformMatrix(new Point2D(cs.StandardMarkPoint1.X, cs.StandardMarkPoint1.Y), new Point2D(markPoint1.X, markPoint1.Y), 
                        new Point2D(cs.StandardMarkPoint2.X, cs.StandardMarkPoint2.Y), new Point2D(markPoint2.X, markPoint2.Y));

                    cs.TM.ReplacebyMatrix(tempTm);
                    tempTm = null;
                }
             
                return true;
            }

            return false;
        }

        public bool SetCurrentMarkPoint(int csLevel, int csIndex, PointD markPoint)//
        {
            CordinateSystem cs = GetCordinateSystem(csLevel, csIndex);

            if (cs != null)
            {
                CordinateSystem csParent = null;
                if (csLevel > 1)
                {
                    csParent = GetCordinateSystem(csLevel - 1, cs.ParentIndex);

                    if (csParent == null) { return false; }
                }
                
                if (csParent != null)
                {
                    Point2D[] mp = { new Point2D(cs.StandardMarkPoint1.X, cs.StandardMarkPoint1.Y), new Point2D(markPoint.X, markPoint.Y) };
                    Point2D[] ret = cs.OTM.MapFromRoot(mp);

                    TransformMatrix tempTm = new TransformMatrix(ret[0], ret[1]);
                    cs.TM.ReplacebyMatrix(tempTm);
                    tempTm = null;
                }
                else
                {
                    TransformMatrix tempTm = new TransformMatrix(new Point2D(cs.StandardMarkPoint1.X, cs.StandardMarkPoint1.Y), new Point2D(markPoint.X, markPoint.Y));
                    cs.TM.ReplacebyMatrix(tempTm);
                    tempTm = null;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 根据Level和Index获取坐标系
        /// </summary>
        /// <param name="level"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private CordinateSystem GetCordinateSystem(int level, int index)
        {
            List<CordinateSystem> csl;
            CordinateSystem cs = null;
            if (CordinateSystemDictionary.TryGetValue(level, out csl))
            {
                foreach (var item in csl)
                {
                    if (item.Index == index)
                    {
                        cs = item;
                    }
                }
            }
            else
                cs = null;

            return cs;
        }

        /// <summary>
        /// 将指定坐标系上的点p坐标转换到指定坐标系
        /// </summary>
        /// <param name="level"></param>
        /// <param name="index"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public PointD CordinateTransforming(int fromLevel, int fromIndex, int toLevel, int toIndex, PointD p)
        {
            CordinateSystem fromCs = GetCordinateSystem(fromLevel, fromIndex);
            CordinateSystem toCs = GetCordinateSystem(toLevel, toIndex);

            if (fromCs == null || toCs == null) { return p; }
            Point2D ret = TransformMatrix.MapPoints(fromCs.TM, new Point2D(p.X, p.Y), toCs.TM);

            return new PointD(ret.X, ret.Y);
        }
    }
}
