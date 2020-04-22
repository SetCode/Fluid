using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.LoadTrajectory
{
    public class TrajPoint
    {
        private string desig;
        public string Desig
        {
            get { return desig; }
            set { desig = value; }
        }
        private string comp;
        public string Comp
        {
            get { return comp; }
            set { comp = value; }
        }
        public PointD Mid = new PointD();
        public double Rotation;
        private string layout;
        public string LayOut
        {
            get { return this.layout; }
            set { this.layout = value; }
        }

        private double weight;
        public double Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public bool IsWeight;

        public int NumShots=1;
    }
        
}
