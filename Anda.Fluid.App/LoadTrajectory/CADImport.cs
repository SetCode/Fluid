using Anda.Fluid.Infrastructure.Common;
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
    public class CADImport
    {
        private static readonly CADImport instance = new CADImport();
        private CADImport()
        {

        }
        public static CADImport Instance => instance;

        private Dictionary<string, PatternInfo> pathPatternDic = new Dictionary<string, PatternInfo>();
        public Dictionary<string, PatternInfo> PathPatternDic=>this.pathPatternDic;

        public static string[] libStrArray;
        public ComponentLib curLib;
        public string PathLib = string.Empty;
        public string CurrTrajPath;

        public  double Unit2MM = 1;
        private string directory { get { return Path.GetDirectoryName(this.CurrTrajPath)+ "\\ComponetMap.txt"; } }
        
        public  bool OnLoadASYMTEKLib()
        {
            //string libPath = Application.StartupPath + "\\Trajectory\\ASYMTEK.LIB";
            string libPath = Application.StartupPath + "\\ASYMTEK.LIB";
            bool ret = CsvUtil.ReadLines(libPath, out libStrArray);
            if (ret == false)
                return ret;            
            return ret;
        }

        public void AddFilePath(string trajPath)
        {
            if(String.IsNullOrEmpty(trajPath))
            {
                return;
            }
            this.CurrTrajPath = trajPath;
            if (PathPatternDic.ContainsKey(trajPath))
            {
                this.RemovePatternInfo(trajPath);
            }
            PatternInfo patterInfo = new PatternInfo(trajPath);
            //获取文本
            Result res = patterInfo.GetText();
            if (res != Result.OK)
            {
                MessageBox.Show("加载轨迹文件失败");
                return;
            }            
            PathPatternDic.Add(trajPath, patterInfo);            
        }

        public PatternInfo GetCurPatternInfo()
        {
            return this.GetPatternInfo(this.CurrTrajPath);
        }
        public PatternInfo GetPatternInfo(string trajPath)
        {
            if (String.IsNullOrEmpty(trajPath))
            {
                return null;
            }
            if (this.PathPatternDic.ContainsKey(trajPath))
            {
                return PathPatternDic[trajPath];
            }
            return null;
            
        }
        public Result RemovePatternInfo(string trajPath)
        {
            if (this.PathPatternDic.ContainsKey(trajPath))
            {
                PathPatternDic.Remove(trajPath);
                return Result.OK;
            }
            return Result.FAILED;
        }

  
    }
}
