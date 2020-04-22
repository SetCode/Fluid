using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.LoadTrajectory
{
    /// <summary>
    /// SolderPaste 锡膏  Adh红胶
    /// </summary>
    public enum Technology
    {
        SolderPaste,
        Adh
    }
   
    public enum HeadName
    {
        Design,
        Comp,
        X,
        Y,
        Rot,
        LayOut
    }

    public class CompProperty : ICloneable
    {
        public CompProperty(string desig, string comp, PointD mid, double rotation)
        {
            this.desig = desig;
            this.comp = comp;
            this.mid = mid;
            this.rotation = rotation;
        }       

        public CompProperty(string desig, string comp, PointD mid, double rotation, string layout) : this(desig, comp, mid, rotation)
        {
            this.layout = layout;
        }

        private string desig;
        public string Desig => this.desig;
        private string comp;
        public string Comp
        {
            get { return comp; }
            set { comp = value; }
        }
        public string CompStand;
        private PointD mid = new PointD();
        public PointD Mid
        { get { return this.mid; } set { this.mid = value; } }

        private double rotation;
        public double Rotation => this.rotation;
        private string layout;
        public string LayOut => this.layout;

        public Technology tech = Technology.Adh;

        //public List<PointD> points = new List<PointD>();
        public List<GlueDot> points = new List<GlueDot>();
        public void GetPoints(ComponentLib lib)
        {
            this.points.Clear();
            bool isFind = false;
            PointD pRotated;
            ComponentEx cmp = new ComponentEx(0);
            if (lib == null || lib.FindAll().Count == 0)
                return;
            foreach (ComponentEx item in lib.FindAll())
            {
                if(item.component.Name.Contains(this.Comp) || this.Comp.Contains(item.component.Name))               
                {
                    isFind = true;
                    cmp = item;
                    break;
                }
            }
            if (isFind)
            {
                if (tech==Technology.Adh)
                {
                    if (cmp.component.AdhPoints.Count > 0)
                    {                       
                        foreach (GlueDot p in cmp.component.GetPoints(Technology.Adh))
                        {
                            GlueDot dot = new GlueDot();
                            dot.IsWeight = p.IsWeight;
                            dot.Weight = p.Weight;
                            dot.Radius = p.Radius;
                            dot.NunShots = p.NunShots;
                            dot.index = p.index;
                            dot.point = new PointD(p.point);                            
                            this.RotateComp(dot.point, this.rotation, out pRotated);
                            dot.point = pRotated + this.mid;
                            this.points.Add(dot);
                        }
                    }
                    else
                    {
                        GlueDot dot = new GlueDot();                       
                        dot.point = new PointD(this.mid);
                        this.points.Add(dot);
                    }
                }
               
               
            }
            else
            {
                GlueDot dot = new GlueDot();
                dot.point = new PointD(this.mid);
                this.points.Add(dot);
            }
        }
        private void RotateComp(PointD pOriginal, double angle, out PointD pRotated)
        {
            
            pRotated = new PointD();
            double rad = angle / (180 / Math.PI);
            pRotated.X = pOriginal.X * Math.Cos(rad) - pOriginal.Y * Math.Sin(rad);
            pRotated.Y = pOriginal.X * Math.Sin(rad) + pOriginal.Y * Math.Cos(rad);
        }

        public object Clone()
        {
            return this.MemberwiseClone() as CompProperty;
        }

    }
}
