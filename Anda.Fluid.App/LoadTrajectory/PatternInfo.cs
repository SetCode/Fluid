using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.LoadTrajectory
{
    public class Head
    {
        public string Text;
        public int Index;
        public HeadName StandName;
        
    }
    public class PatternInfo
    {
        public List<char> seperatorTxtList = new List<char>() { '\t' };
        public List<string> speratorTxtShow = new List<string>() { "\\t" };
        public List<char> seperatorCsvList = new List<char>() { ',' };
        public List<string> speratorCsvShow = new List<string>() { ','.ToString()};        
        private string trajPath;
        public Trajectory Trajectory=new Trajectory() ;
        public string TrajPath => trajPath;
        
        public string[] Lines;
        private string extension;

        public string[] Heads;
        public List<Head> HeadSelected= new List<Head>();
        private int designIndex = -1;
        private int compIndex = -1;
        private int midXIndex = -1;
        private int midYIndex = -1;
        private int rotationIndex = -1;
        private int layOutIndex = -1;
        public List<string> layOuts = new List<string>();
        public string SelectelayOut;

        public Technology Technololy = Technology.Adh;
        public List<CompProperty> CompList = new List<CompProperty>();

        public List<CompProperty> CompListStanded = new List<CompProperty>();
        //用户元器件类型
        public List<string> UserComps = new List<string>();
        //用户元器件库
        public ComponentLib LibUser = new ComponentLib();
        private string csvpath { get { return Path.GetDirectoryName(this.trajPath) + "\\"+Path.GetFileNameWithoutExtension(this.trajPath)+"_Componet.csv"; } }

        public bool IsUserCompLoaded = false;

        //public  PointD Mark1;
        public MarkPoint Mark1;
        //public PointD Mark2;
        public MarkPoint Mark2;

        public double UnitScale = 1;

        public bool IsPatternCreated = false;
        
        public string PatternName { get { return Path.GetFileName(trajPath); } set{} } 
        public PatternInfo(string trajPath)
        {
            this.trajPath = trajPath;
        }
        /// <summary>
        /// 获取文本内容 信息
        /// </summary>
        /// <returns></returns>
        public Result GetText()
        {
            Result res=Result.OK;
            if (String.IsNullOrEmpty(this.TrajPath))
                return Result.FAILED;
            //获取拓展文件
            this.extension = Path.GetExtension(this.TrajPath);
            //获取所有的内容
            bool ret = CsvUtil.ReadLines(this.TrajPath, out Lines);
            if (ret == false)
            {
                res = Result.FAILED;
            }
            return res;
        }

        #region 分割字符串
        public Result AddSeperator(char sep)
        {
            if (extension.Contains(".txt"))
            {
                this.seperatorTxtList.Clear();               
                this.seperatorTxtList.Add(sep);               
            }
            else if (extension.Contains(".csv"))
            {
                this.seperatorCsvList.Clear();
                this.seperatorCsvList.Add(sep);
                
            }
            else
            {
                MessageBox.Show("输入的文件类型为{0},请确保导入的文件的类型为(*.txt,*.csv)", extension);
                return Result.FAILED;
            }
            return Result.OK;
        }
        public Result DelSeperator(char sep)
        {
            if (extension.Contains(".txt"))
            {
                if (this.seperatorTxtList.Contains(sep))
                {
                    this.seperatorTxtList.Remove(sep);
                }
            }
            else if (extension.Contains(".csv"))
            {
                if (this.seperatorCsvList.Contains(sep))
                {
                    this.seperatorCsvList.Remove(sep);
                }
            }
            else
            {
                MessageBox.Show("输入的文件类型为{0},请确保导入的文件的类型为(*.txt,*.csv)", extension);
                return Result.FAILED;
            }
            return Result.OK;
        }
        public List<char> GetSeperator()
        {
            if (extension.Contains(".txt"))
            {
                return this.seperatorTxtList;
            }
            else if (extension.Contains(".csv"))
            {
                return this.seperatorCsvList;
            }
            else
            {
                MessageBox.Show("输入的文件类型为{0},请确保导入的文件的类型为(*.txt,*.csv)", extension);
                return null;
            }

        }
        public Result AddSeperatorShow(string sep)
        {
            if (extension.Contains(".txt"))
            {
                this.speratorTxtShow.Clear();
                this.speratorTxtShow.Add(sep);

            }
            else if (extension.Contains(".csv"))
            {
                this.speratorCsvShow.Clear();
                this.speratorCsvShow.Add(sep);

            }
            else
            {
                MessageBox.Show("输入的文件类型为{0},请确保导入的文件的类型为(*.txt,*.csv)", extension);
                return Result.FAILED;
            }
            return Result.OK;
        }
        public Result DelSeperatorShow(string sep)
        {
            if (extension.Contains(".txt"))
            {
                if (this.speratorTxtShow.Contains(sep))
                {
                    this.speratorTxtShow.Remove(sep);
                }
            }
            else if (extension.Contains(".csv"))
            {
                if (this.speratorCsvShow.Contains(sep))
                {
                    this.speratorCsvShow.Remove(sep);
                }
            }
            else
            {
                MessageBox.Show("输入的文件类型为{0},请确保导入的文件的类型为(*.txt,*.csv)", extension);
                return Result.FAILED;
            }
            return Result.OK;
        }
        public List<string> GetSeperatorShow()
        {
            if (extension.Contains(".txt"))
            {
                return this.speratorTxtShow;
            }
            else if (extension.Contains(".csv"))
            {
                return this.speratorCsvShow;
            }
            else
            {
                MessageBox.Show("输入的文件类型为{0},请确保导入的文件的类型为(*.txt,*.csv)", extension);
                return null;
            }

        }
        #endregion
        /// <summary>
        /// 获取所有列名称
        /// </summary>
        /// <returns></returns>
        public Result GetHead()
        {  
            if (String.IsNullOrEmpty(this.Lines[0]) || this.Lines[0].Length <= 0)
            {
                MessageBox.Show("请确认文件第一行是头");
                return Result.FAILED;
            }
            string[] lineSplited = null;
            if (extension.Contains(".txt"))
            {
                lineSplited = this.Lines[0].Split(seperatorTxtList.ToArray());
            }
            else if (extension.Contains(".csv"))
            {
                lineSplited = this.Lines[0].Split(seperatorCsvList.ToArray());
            }
            else
            {
                MessageBox.Show("输入的文件类型为{0},请确保导入的文件的类型为(*.txt,*.csv)", extension);
                return Result.FAILED;
            }
            Heads = lineSplited;            
            return Result.OK;
        }
               
        /// <summary>
        /// 获取第一行的index
        /// </summary>
        private void GetHeadIndexs()
        {
            designIndex = this.GetHeadIndexByName(HeadName.Design);
            compIndex = this.GetHeadIndexByName(HeadName.Comp);
            midXIndex = this.GetHeadIndexByName(HeadName.X);
            midYIndex = this.GetHeadIndexByName(HeadName.Y);
            rotationIndex = this.GetHeadIndexByName(HeadName.Rot);
            layOutIndex = this.GetHeadIndexByName(HeadName.LayOut);
        }
        /// <summary>
        /// 获取列在文件中的 第几列
        /// </summary>
        /// <param name="headName"></param>
        /// <returns></returns>
        private int GetHeadIndexByName(HeadName headName)
        {
            if (this.HeadSelected.Count <= 0)
            {
                return -1;
            }
            foreach (Head h in this.HeadSelected)
            {
                if (h.StandName == headName)
                {
                    return h.Index;
                }
            }
            return -1;
        }

        public Result Load()
        {
            Result ret = Result.OK;
            this.GetHeadIndexs();
            if (String.IsNullOrEmpty(trajPath))
                return ret=Result.FAILED;
            this.extension = Path.GetExtension(trajPath);
            if (this.extension.Contains(".txt"))
            {
                ret= this.LoadTxt();
            }
            else if (this.extension.Contains(".csv"))
            {
                ret = this.LoadCsv();
            }
            else
            {
                MessageBox.Show("输入的文件类型为{0},请确保导入的文件的类型为(*.txt,*.csv)", this.extension);
                ret = Result.FAILED;
            }            
            return ret;
        }

        public Result LoadDefault()
        {
            this.designIndex = 0;
            this.compIndex = 1;
            this.midXIndex = 2;
            this.midYIndex = 3;
            this.rotationIndex = 4;
            this.layOutIndex = 5;
            Result res = this.LoadCsv();
            return res;
        }
        public bool unitFind = false;
        public Result LoadCsv()
        {
            Result res = Result.OK;
            
            int dataStart = 0;
            this.CompList.Clear();
            this.CompListStanded.Clear();
            this.layOuts.Clear();
            if ((midXIndex == -1) || (midYIndex == -1) || ((rotationIndex == -1)))
            {
                return Result.FAILED;
            }
           
            for (int i = dataStart + 1; i < this.Lines.Length; i++)
            {
                if (!String.IsNullOrEmpty(this.Lines[i]))
                {
                    string[] splits = this.Lines[i].Split(this.seperatorCsvList.ToArray());
                    if (splits.Length <3)
                    {
                        continue;
                    }
                    //对分割的字符串数组进行处理 去掉前后的空格 ，将 X Y rot 其他的去掉0-9 . - +
                    string disgn = " ";
                    if (this.designIndex == -1)
                    {
                        disgn = " ";
                    }
                    else
                    {
                        disgn = splits[this.designIndex];
                    }
                    string comp = " ";
                    if (this.compIndex!=-1)
                    {
                        comp = splits[compIndex].Trim().Replace('"', ' ').Trim();
                    }                     
                    if (!unitFind)
                    {
                        if (splits[midXIndex].Contains("mm") && splits[midYIndex].Contains("mm"))
                        {
                            this.UnitScale = 1;
                            unitFind = true;
                        }
                        else if (splits[midXIndex].Contains("mil") && splits[midYIndex].Contains("mil"))
                        {
                            this.UnitScale = 0.0254;
                            unitFind = true;
                        }
                        //Log.Dprint(i.ToString());
                        if (!unitFind)
                        {
                            if (i == this.Lines.Length - 2)
                            {
                                MessageBox.Show("there is no unit flay in this trajectory file");
                                return Result.FAILED;
                            }
                            continue;
                        }
                    }
                               
                    double midX = this.getValue(splits[midXIndex]);
                    double midY = this.getValue(splits[midYIndex]);
                    double rotation = this.getValue(splits[rotationIndex]);
                    string layout ;
                    if (this.layOutIndex == -1)
                    {
                        layout = " ";
                    }
                    else
                    {
                        layout = splits[layOutIndex];
                    }
                    this.AddlayOuts(layout);//获得layout
                    PointD p = new PointD(midX* this.UnitScale, midY* this.UnitScale);
                    CompProperty compProperty = new CompProperty(disgn, comp, p, rotation, layout);
                    CompList.Add(compProperty);
                }

            }
            
            this.CompListStanded.AddRange(this.CompList);
            return res;
        }

        public Result LoadTxt()
        {
            Result res = Result.OK;            
            int dataStart = 0;
            this.CompList.Clear();
            this.CompListStanded.Clear();
            this.layOuts.Clear();
            if ( (midXIndex == -1) || (midYIndex == -1) || ((rotationIndex == -1)))
            {
                return Result.FAILED;
            }         
            
            for (int i = dataStart + 1; i < this.Lines.Length; i++)
            {
                if (!String.IsNullOrEmpty(this.Lines[i]))
                {
                    string[] splits = this.Lines[i].Split(this.seperatorTxtList.ToArray());
                    if (splits.Length < 3)
                    {
                        continue;
                    }
                    //对分割的字符串数组进行处理 去掉前后的空格 ，将 X Y rot 其他的去掉0-9 . - +
                    string disgn = " ";
                    if (this.designIndex == -1)
                    {
                        disgn = " ";
                    }
                    else
                    {
                        disgn = splits[this.designIndex];
                    }
                    string comp = " ";
                    if (this.compIndex != -1)
                    {
                        comp = splits[compIndex].Trim();
                    }
                    if (!unitFind)
                    {
                        if (splits[midXIndex].Contains("mm") && splits[midYIndex].Contains("mm"))
                        {
                            this.UnitScale = 1;
                            unitFind = true;
                        }
                        else if (splits[midXIndex].Contains("mil") && splits[midYIndex].Contains("mil"))
                        {
                            this.UnitScale = 0.0254;
                            unitFind = true;
                        }
                        //Log.Dprint(i.ToString());
                        if (!unitFind)
                        {
                            if (i == this.Lines.Length - 2)
                            {
                                MessageBox.Show("there is no unit flay in this trajectory file");
                                return Result.FAILED;
                            }
                            continue;
                        }
                    }                     
                    double midX = this.getValue(splits[midXIndex]);
                    double midY = this.getValue(splits[midYIndex]);
                    double rotation = this.getValue(splits[rotationIndex]);
                    string layout;
                    if (this.layOutIndex == -1)
                    {
                        layout = " ";
                    }
                    else
                    {
                        layout = splits[layOutIndex];
                    }
                    this.AddlayOuts(layout);
                    PointD p = new PointD(midX, midY);
                    CompProperty compProperty = new CompProperty(disgn, comp, p, rotation, layout);
                    CompList.Add(compProperty);
                }

            }
            
            this.CompListStanded.AddRange(this.CompList);
            return res;
        }
        private void AddlayOuts(string layout)
        {
            if (this.layOuts.Contains(layout))
            {
                return;
            }
            this.layOuts.Add(layout);
        }

        private double getValue(string str)
        {
            string valueStr = str.Trim();
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                int intc = Convert.ToInt32(c);
                if ((intc >= 48 && intc <= 57) || intc==45 || intc == 42 || intc == 46)
                {
                    sb.Append(c);
                }
            }

            double value = 0;
            double.TryParse(sb.ToString(), out value);
            return value;           
        }

      /// <summary>
      /// 
      /// </summary>
        public void GetLayoutComp()
        {
            this.CompListStanded.Clear(); 
            foreach (CompProperty p in this.CompList)
            {
                if (p.LayOut == this.SelectelayOut)
                {
                    this.CompListStanded.Add(p);
                }
            }            
        }
        /// <summary>
        /// 生成轨迹
        /// </summary>
        public void GenerateTrajectory()
        {
            if (this.CompListStanded==null || this.CompListStanded.Count<=0)
            {
                return;
            }
            this.Trajectory = new Trajectory(this.CompListStanded);
            this.Trajectory.Load();
        }

        public bool SaveComponent()
        {
            try
            {
                if (File.Exists(this.csvpath))
                {
                    File.Delete(this.csvpath);
                }
                string resline= string.Format("{0},{1},{2},{3},{4},{5}", HeadName.Design.ToString(), HeadName.Comp, HeadName.X.ToString(),HeadName.Y.ToString(), HeadName.Rot.ToString(),HeadName.LayOut.ToString());
                CsvUtil.WriteLine(this.csvpath,resline);
                foreach (CompProperty p in this.CompList)
                {
                    resline = string.Format("{0},{1},{2},{3},{4},{5}", p.Desig,p.Comp,Math.Round(p.Mid.X,3),Math.Round(p.Mid.Y,3),Math.Round(p.Rotation,2), p.LayOut);
                    CsvUtil.WriteLine(this.csvpath, resline);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region  加载所有用户元器件
        /// <summary>
        /// 获得用户元器件类型
        /// </summary>
        public void GetUserComps()
        {
            this.UserComps.Clear();
            foreach (CompProperty property in this.CompListStanded)
            {
                this.AddUserComps(property.Comp);
            } 
        }
        public void AddUserComps(string userComp)
        {
            if (this.UserComps.Contains(userComp))
            {
                return;
            }
            this.UserComps.Add(userComp);
        }

        public void GetUserLib()
        {
            int index = 0;
            this.LibUser.Clear();
            foreach (string item in this.UserComps)
            {
                index = this.LibUser.Count;               
                ComponentEx compnt = new ComponentEx(index + 1);
                compnt.component.Name = item;
                compnt.component.Width = 0;
                compnt.component.Height = 0;
                this.LibUser.Add(compnt);
            }
           
        }
        #endregion

    }
}
