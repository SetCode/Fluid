using Anda.Fluid.Infrastructure.Algo;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.LoadTrajectory
{
    public class Trajectory
    {     
        public List<TrajPoint> PointList = new List<TrajPoint>();
       
        public List<TrajPoint> PointsModified = new List<TrajPoint>();

        public List<PointD> PointsOptimized = new List<PointD>();
        public List<PointD> CurPoints;
        public PointD Mark1;
        public PointD Mark2;

        private List<CompProperty> compList;
        public double Distance;
       
        public PointD Offset = new PointD();
        public double MinX = -500;
        public double MaxX = 500;
        public double MinY = -500;
        public double MaxY = 500;
        public Trajectory()
        {

        }
        public Trajectory(List<CompProperty> compList)
        {
            this.compList = compList;
        }

        public Result Load()
        {
            Result res = Result.OK;
            this.PointList.Clear();
            foreach (CompProperty compProperty in this.compList)
            {                
                compProperty.GetPoints(CADImport.Instance.curLib);                           
                if (this.PointList.Count == 0)
                {
                    if (compProperty.points.Count > 0)
                    {
                        PointD p = new PointD();
                        p.X = compProperty.points[0].point.X;
                        p.Y= compProperty.points[0].point.Y;
                        TrajPoint trPoint = new TrajPoint()
                        {
                            Desig = compProperty.Desig,
                            Comp = compProperty.Comp,
                            Mid = p,
                            Rotation = compProperty.Rotation,
                            LayOut = compProperty.LayOut,
                            Weight = compProperty.points[0].Weight,
                            IsWeight = compProperty.points[0].IsWeight,
                            NumShots = compProperty.points[0].NunShots
                        };
                        this.PointList.Add(trPoint);
                        
                    }
                }
                bool findFlag = false;
                foreach (GlueDot compP in compProperty.points)
                {
                    findFlag = false;
                    foreach (TrajPoint pL in this.PointList)
                    {
                        if ((compP.point == pL.Mid))
                        {
                            findFlag = true;
                            break;
                        }
                    }
                    if (findFlag == false)
                    {
                        PointD p = new PointD();
                        p.X = compP.point.X ;
                        p.Y = compP.point.Y ;
                        TrajPoint trPoint = new TrajPoint()
                        {
                            Desig = compProperty.Desig,
                            Comp = compProperty.Comp,
                            Mid = p,
                            Rotation = compProperty.Rotation,
                            LayOut = compProperty.LayOut,
                            Weight = compP.Weight,
                            IsWeight = compP.IsWeight,
                            NumShots= compP.NunShots
                        };
                        this.PointList.Add(trPoint);
                    }
                }
            }
            this.CurPoints = this.GetPts();
            this.PointsModified.Clear();
            this.PointsModified.AddRange(this.PointList);
            this.GetDistance();
            return res;

        }
      
        public void PointsSelecte()
        {   
            foreach (var item in this.PointList)
            {
                if (!(item.Mid.X >= MinX && item.Mid.X <= MaxX) && (item.Mid.Y >= MinY && item.Mid.Y <= MaxY))
                {
                    this.PointList.Remove(item);
                }                
            }
            this.PointsModified.Clear();
            this.PointsModified.AddRange(this.PointList);

        }
        public  void PointsOffset()
        {
            foreach (var p in this.PointList)
            {                             
                p.Mid.X += this.Offset.X;
                p.Mid.Y += this.Offset.Y;               
            }
            this.PointsModified.Clear();
            this.PointsModified.AddRange(this.PointList);
        }
        /// <summary>
        /// 坐标系翻转
        /// </summary>
        /// <param name="revsX"></param>
        /// <param name="revsY"></param>
        public void Reverse(bool revsX, bool revsY)
        {
            foreach (var p in this.PointList)
            {
                if (revsX)
                {
                    double x = (0 - p.Mid.X); 
                    p.Mid.X = x;                
                }
                if (revsY)
                {
                    double y = -p.Mid.Y;
                    p.Mid.Y = y;
                } 
            }
            this.PointsModified.Clear();
            this.PointsModified.AddRange(this.PointList);
        }

        public void Reverse(Dictionary<int ,string > axisMap)
        {
            double r11 = 1, r12 = 1;
            double r21 = 1, r22 = 1;
            foreach (var item in axisMap.Keys)
            {
                if (axisMap[item] == Axislabel.X.ToString())
                {
                    getCos(Axislabel.X.ToString(), item,out r11,out r21);
                    
                }
                else if (axisMap[item] == Axislabel.Y.ToString())
                {
                    getCos(Axislabel.Y.ToString(), item, out r22, out r12);
                   
                }
            }
           
            foreach (var p in this.PointList)
            {
                double px = p.Mid.X;
                double py = p.Mid.Y;
                p.Mid.X = px * r11 + py * r12;
                p.Mid.Y = px * r21 + py * r22;
                 
            }
            this.PointsModified.Clear();
            this.PointsModified.AddRange(this.PointList);


        }
        private void getMatrix(Dictionary<int, string> axisMap)
        {
            double r11 = 1, r12 = 1;
            double r21 = 1, r22 = 1;
            foreach (var item in axisMap.Keys)
            {
                if (axisMap[item]==Axislabel.X.ToString())
                {
                    getCos(Axislabel.X.ToString(), item, out r11, out r21);
                }
                else if (axisMap[item] == Axislabel.Y.ToString())
                {
                    getCos(Axislabel.Y.ToString(), item, out r12, out r22);
                }
            }
        }
        private void getCos(string axisName,int index,out double xx, out double xy)
        {
            xx = 1;
            xy = 1;
            if (axisName == Axislabel.X.ToString())
            {
                switch (index)
                {
                    case 1:
                        xx = Math.Cos(0);
                        xy = Math.Cos(0-Math.PI / 2);
                        break;
                    case 2:
                        xx = Math.Cos(Math.PI / 2);
                        xy = Math.Cos(Math.PI / 2 - Math.PI / 2);
                        break;
                    case -1:
                        xx = Math.Cos(Math.PI);
                        xy = Math.Cos(Math.PI - Math.PI / 2);
                        break;
                    case -2:
                        xx = Math.Cos(Math.PI * 3 / 2);
                        xy = Math.Cos(Math.PI * 3 / 2 - Math.PI / 2);
                        break;
                    default:
                        xx = Math.Cos(0);
                        xy = Math.Cos(0 - Math.PI / 2);
                        break;
                }
            }
            else if (axisName == Axislabel.Y.ToString())
            {
                switch (index)
                {
                    case 1:
                        xx = Math.Cos(Math.PI * 3 / 2);//yy
                        xy = Math.Cos(Math.PI * 3 / 2 + Math.PI / 2);
                        break;
                    case 2:
                        xx = Math.Cos(0);//yy
                        xy = Math.Cos(0 + Math.PI / 2);
                        break;
                    case -1:
                        xx = Math.Cos(Math.PI / 2);
                        xy = Math.Cos(Math.PI / 2 + Math.PI / 2);
                        break;
                    case -2:
                        xx = Math.Cos(Math.PI);
                        xy = Math.Cos(Math.PI  + Math.PI / 2);
                        break;
                    default:
                        xx = Math.Cos(0);//yy
                        xy = Math.Cos(0 + Math.PI / 2);
                        break;
                }
            }
           
        }

        public void TrajOptimize(MarkPoint mark1, MarkPoint mark2)
        {            
            List<PointD> points = new List<PointD>();
            foreach (var p in this.PointList)
            {
                if (mark1 != null)
                {
                    if (!mark1.IsSpray && mark1.Mark == p.Mid)
                    {
                        continue;
                    }
                }
                if (mark2!=null)
                {
                    if (!mark2.IsSpray && p.Mid == mark2.Mark)
                    {
                        continue;
                    }
                }
                points.Add(p.Mid);
            }
            points = this.setFirstPoint(points, mark1,  mark2);
            double[] data = new double[points.Count * 2];
            for (int i = 0; i < points.Count; i++)
            {
                data[i * 2] = points[i].X;
                data[i * 2 + 1] = points[i].Y;
            }
            int[] routeIndexArr = new int[points.Count];
            OptimalRoute.initializeAll();
            OptimalRoute.autoRunAntColonyx86(data, points.Count, routeIndexArr);            
            this.PointsOptimized.Clear();
            for (int i = 0; i < routeIndexArr.Length; i ++)
            {
                this.PointsOptimized.Add (points[routeIndexArr[i]]);
            }          
            this.CurPoints = this.PointsOptimized;
            this.GetDistance();
            this.sortPoints(this.PointsOptimized);            
        }
        private List<PointD> setFirstPoint(List<PointD> points, MarkPoint mark1, MarkPoint mark2)
        {
            if (points==null || points.Count<=0)
                return null;
            List<PointD> res = new List<PointD>();
            res.AddRange(points);
            if (mark2 == null)
            {
                if (mark1 == null)
                {
                    PointD firstPoint = this.nearestTo(points, new PointD(0,0));
                    res.Clear();
                    res.Add(firstPoint);
                    points.Remove(firstPoint);
                    res.AddRange(points);
                    
                }
                else if (mark1.IsSpray)
                {
                    res.Clear();
                    res.Add(mark1.Mark);
                    points.Remove(mark1.Mark);
                    res.AddRange(points);
                    
                }
                else if (!mark1.IsSpray)
                {
                    PointD firstPoint = this.nearestTo(points,mark1.Mark);
                    res.Clear();
                    res.Add(firstPoint);
                    points.Remove(firstPoint);
                    res.AddRange(points);
                    
                }             


            }
            else
            {
                if (mark2.IsSpray)
                {
                    res.Clear();
                    res.Add(mark2.Mark);
                    points.Remove(mark2.Mark);
                    res.AddRange(points);
                    
                }
                else
                {
                    PointD firstPoint = this.nearestTo(points,mark2.Mark);
                    res.Clear();
                    res.Add(firstPoint);
                    points.Remove(firstPoint);
                    res.AddRange(points);
                  
                }
                
            }
            return res; 
        }

        private PointD nearestTo(List<PointD> points, PointD p)
        {
            if (points == null)
                return null;
            if (points.Count <= 0)
                return null;
            double minlength = 0;
            minlength = points[0].DistanceTo(new PointD(0, 0));
            PointD firstPoint = points[0];
            foreach (var item in points)
            {
                double length = item.DistanceTo(p);
                if (length < minlength)
                {
                    minlength = length;
                    firstPoint = item;
                }
            }
            return firstPoint;
        }
        private void sortPoints(List<PointD> points)
        {
            this.PointsModified.Clear();
            foreach (PointD p in points)
            {
                foreach (var item in this.PointList)
                {
                    if (item.Mid==p)
                    {
                        this.PointsModified.Add(item);
                    }
                }                       
            }

        }


        public void GetDistance()
        {
            this.Distance = 0;
            if (this.CurPoints.Count<=0)
            {
                return;
            }
            for (int i=0;i<this.CurPoints.Count-1;i++)
            {
                Distance+=this.CurPoints[i].DistanceTo(this.CurPoints[i+1]);
            }
            
        }
        public List<PointD> GetPts()
        {
            List<PointD> pts = new List<PointD>();
            pts.Clear();
            foreach (var item in this.PointsModified)
            {
                pts.Add(item.Mid);
            }
            return pts;
        }


        
    }
}
