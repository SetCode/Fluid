using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.LoadTrajectory
{
    public partial class DialogLibConvert : Form
    {
        private ComponentLib lib = new ComponentLib();
        public DialogLibConvert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lib.SetPath("E:\\ASYMTEK.lib");
            CADImport.Instance.OnLoadASYMTEKLib();
            getType();
            lib.Save();

        }

        private void getType()
        {           
            for (int i = 0; i < CADImport.libStrArray.Length; i++)
            {
                string line = CADImport.libStrArray[i];
                if (line.Contains("[") && line.Contains("]"))
                {
                    
                    int mid = line.LastIndexOf(',');
                    int last = line.LastIndexOf(']');
                    string before = line.Substring(0, mid);
                    string after = line.Substring(mid + 1, last - mid - 1);
                    int redindexStar = i;
                    int redindexEnd = i;
                    int solindexStar = i;
                    int solindexEnd = i;
                    for (int j = i + 1; j < CADImport.libStrArray.Length; j++)
                    {
                            if (CADImport.libStrArray[j].Contains(".adh"))
                            {
                            redindexStar = j;

                            }
                            if (CADImport.libStrArray[j].Contains(".end"))
                            {
                            redindexEnd = j;
                                break;
                            }

                    }
                    for (int j = i + 1; j < CADImport.libStrArray.Length; j++)
                    {
                        if (CADImport.libStrArray[j].Contains(".sol"))
                        {
                            solindexStar = j;

                        }
                        if (CADImport.libStrArray[j].Contains(".end"))
                        {
                            solindexEnd = j;
                            break;
                        }
                    }
                    List<PointD> pointsred = new List<PointD>();
                    pointsred.Clear();
                    for (int k = redindexStar + 1; k < redindexEnd; k++)
                    {

                        PointD p = new PointD();
                        string[] splited = CADImport.libStrArray[k].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        double x = 0;
                        double y = 0;
                        if (double.TryParse(splited[0], out x) && double.TryParse(splited[1], out y))
                        {
                            p.X = x;
                            p.Y = y;
                        }
                       
                        pointsred.Add(p);
                    }

                    List<PointD> pointssol = new List<PointD>();
                    pointssol.Clear();
                    for (int k = solindexStar + 1; k < solindexEnd; k++)
                    {

                        PointD p = new PointD();
                        string[] splited = CADImport.libStrArray[k].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        double x = 0;
                        double y = 0;
                        if (double.TryParse(splited[0], out x) && double.TryParse(splited[1], out y))
                        {
                            p.X = x;
                            p.Y = y;
                        }

                        pointssol.Add(p);
                    }



                    int index = this.lib.FindAll().Count;

                    ComponentEx comp = new ComponentEx(index+1);
                    comp.component.Name = after;
                    foreach (var item in pointsred)
                    {
                        GlueDot dot = new GlueDot();
                        int cout = comp.component.AdhPoints.Count;
                        dot.index = cout + 1;
                        dot.point = new PointD(item.X,item.Y);
                        dot.Weight = (double)0;
                        dot.Radius = (double)0;
                        comp.component.AdhPoints.Add(dot);
                    }
                    foreach (var item in pointssol)
                    {
                        GlueDot dot = new GlueDot();
                        int cout = comp.component.SldPoints.Count;
                        dot.index = cout + 1;
                        dot.point = new PointD(item.X, item.Y);
                        dot.Weight = (double)0;
                        dot.Radius = (double)0;
                        comp.component.SldPoints.Add(dot);
                    }

                    lib.Add(comp);

                }
            }

        }
        
    }
}
