using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.Custom.DataCentor;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive.Vision.Measure;
using Anda.Fluid.Domain.FluProgram;

namespace Anda.Fluid.Domain.Custom
{
    public class CustomRTV : ICustomary
    {
        public string FileName { get; set; }

        public CustomEnum Custom => CustomEnum.RTV;

        public MachineSelection MachineSelection => MachineSelection.RTV;
      
        private StringBuilder sb = new StringBuilder();
        private Dictionary<string, string> output = new Dictionary<string, string>();

        public string Barcode { get; set; } = "";

        public List<string[]> Results = new List<string[]>();

        private bool saveDone = false;

        public DataBase GetData(DataBase data)
        {
            throw new NotImplementedException();
        }

        public bool ParseBarcode(string text)
        {
            return true;
        }

        public bool TransPoint(List<MarkCmd> marks)
        {
            return true;
        }

        #region data
       
        public void SaveData(string pathDir)
        {
            //如果线宽不正确不保存 
            if (!this.ResultsIsRight())
                return;
            this.SaveResult(pathDir);
        }
    
        public void AppendRecored(string name, string value)
        {
            if (this.output==null)
            {
                this.output = new Dictionary<string, string>();
            }

            if (this.output.ContainsKey(name))
            {
                this.output[name] = value;
            }
            //this.output.Add(name,value);
        }
        private string FormatToString()
        {
            if (this.sb == null)
            {
                this.sb = new StringBuilder();
            }
            foreach (string item in this.output.Keys)
            {
                this.sb.Append(item.ToString() + ": " + this.output[item].ToString()+"\r\n");
            }
            return this.sb.ToString();

        }
        public void ClearRecord()
        {
            if (this.sb!=null)
            {
                this.sb.Clear();
            }
            if (this.output!=null)
            {
                this.output.Clear();
            }
            this.Results.Clear();
            this.Barcode = "";   
        }
        
        public bool SaveOrNot()
        {
            bool isSaveOrNot = true;
            if (!this.output.ContainsKey(MeasureType.线宽.ToString()))
            {
                isSaveOrNot = false;
            }
            double valued = -1;
            if(this.output.ContainsKey(MeasureType.线宽.ToString()))
            {
                if (!double.TryParse(this.output[MeasureType.线宽.ToString()], out valued))
                {
                    isSaveOrNot = false;
                }
                else
                {
                    if (valued <= 0)
                    {
                        isSaveOrNot = false;
                    }
                    else
                    {
                        isSaveOrNot = true;
                    }
                }
            }
            
            return isSaveOrNot;
        }

        

        #endregion

        #region 
        public void ProductionBefore()
        {
            
        }
        public void Production()
        {
            
        }

        public void ProductionAfter()
        {
            
        }
       
        #endregion

        /// <summary>
        /// 添加结果
        /// </summary>
        /// <param name="result"></param>
        public void AddResult(string[] result)
        {
            this.Results.Add(result);
        }

        public void SaveResult(string pathDir)
        {
            string path = pathDir;
            DirUtils.CreateDir(path);
            //this.FileName = this.FileName.Replace(':', '_');
            path = path + "\\" + DateTime.Now.ToString("yyyyMMdd HHmmss");
             StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(this.Barcode + "\r\n");
            stringBuilder.Append(FluidProgram.CurrentOrDefault().RuntimeSettings.CustomParam.RTVParam.Depart + "\r\n");
            stringBuilder.Append(FluidProgram.CurrentOrDefault().RuntimeSettings.CustomParam.RTVParam.ComputerInfo + "\r\n");
            stringBuilder.Append(FluidProgram.CurrentOrDefault().RuntimeSettings.CustomParam.RTVParam.MachineInfo + "\r\n");
            stringBuilder.Append(this.GetBarcodePart(this.Barcode) + "\r\n");
            stringBuilder.Append(FluidProgram.CurrentOrDefault().RuntimeSettings.CustomParam.RTVParam.ProductLineInfo + "\r\n");
            stringBuilder.Append(FluidProgram.CurrentOrDefault().RuntimeSettings.CustomParam.RTVParam.OwkInfo + "\r\n");
            stringBuilder.Append(FluidProgram.CurrentOrDefault().RuntimeSettings.CustomParam.RTVParam.UserInfo + "\r\n");
            stringBuilder.Append(this.GetProgramRunTime() + "\r\n");
            stringBuilder.Append(this.GetProgramEndTime() + "\r\n");
            stringBuilder.Append("No  Width  MaxValue  MinValue  Height  MaxValue  MinValue" + "\r\n");
            for (int i = 0; i < this.Results.Count; i++)
            {
                string s = "";
                s += (1 + i).ToString() + "   ";
                s += this.Results[i][0] + "   ";
                s += this.Results[i][1] + "      ";
                s += this.Results[i][2] + "      ";
                s += this.Results[i][3] + "    ";
                s += this.Results[i][4] + "      ";
                s += this.Results[i][5];
                stringBuilder.Append(s + "\r\n");
            }

            CsvUtil.WriteLine(path, stringBuilder.ToString());
        }

        /// <summary>
        /// 得到二维码的前半段
        /// </summary>
        /// <returns></returns>
        private string GetBarcodePart(string txt)
        {
            try
            {
                string[] results = txt.Split(':');
                if (results[0] == null)
                {
                    return "";
                }
                else
                    return results[0];
            }
            catch (Exception)
            {
                return "";
            }

        }

        private string GetProgramRunTime()
        {
            if (Executor.Instance.ProgramRunTime != null)
            {
                return "Start:" + Executor.Instance.ProgramRunTime.ToLocalTime();
            }
            else
            {
                return "Start:";
            }
        }

        private string GetProgramEndTime()
        {
            if (Executor.Instance.ProgramEndTime != null)
            {
                return "End:" + Executor.Instance.ProgramEndTime.ToLocalTime();
            }
            else
            {
                return "End:";
            }
        }

        private bool ResultsIsRight()
        {
            if (Results.Count == 0)
            {
                this.SaveResult(FluidProgram.CurrentOrDefault().RuntimeSettings.CustomParam.RTVParam.DataLocalPathDir);
                return false;
            }
            foreach (var item in this.Results)
            {
                if (Convert.ToDouble(item[0]) > Convert.ToDouble(item[1]) || Convert.ToDouble(item[0]) < Convert.ToDouble(item[2]))
                {
                    return false;
                }
                if (Convert.ToDouble(item[3]) > Convert.ToDouble(item[4]) || Convert.ToDouble(item[3]) < Convert.ToDouble(item[5]))
                {
                    return false;
                }
            }
            return true;
        }

        public void SkipBoard(List<int> SkipBoards)
        {
            return;
        }
    }
}
