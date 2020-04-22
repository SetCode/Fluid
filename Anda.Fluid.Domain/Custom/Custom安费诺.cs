using Anda.Fluid.Domain.Custom.DataCentor;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Drive;
using System.IO;
using Anda.Fluid.Infrastructure.WNetConnect;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Domain.Custom
{
    public class CustomAFN : ICustomary
    {
        public string FileName { get; set; }

        public MachineSelection MachineSelection => MachineSelection.AFN;      
        //public CustomEnum Custom => CustomEnum.AFN;

        private List<ResultAmphnol> patternMarksZero = new List<ResultAmphnol>();

        private ResultAmphnol workpieceMark1Zero = new ResultAmphnol();

        private ResultAmphnol workpieceMark2Zero = new ResultAmphnol();

        private List<ResultAmphnol> patternMarksTransed = new List<ResultAmphnol>();

        private ResultAmphnol workpieceMark1Transed = new ResultAmphnol();

        private ResultAmphnol workpieceMark2Transed = new ResultAmphnol();

        private double toleranceAngle = 2;
        private double tolerancePos = 2;

        private List<int> SkipBoards = new List<int>();

        /// <summary>
        /// 获取最新时间路径
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private string GetBarcodeFilePath(string barcode, string filePath)
        {
            List<string> temp = Directory.GetFiles(filePath).ToList();
            bool hasBarcode = false;
            List<string> tempResult = new List<string>();
            for (int i = 0; i < temp.Count; i++)
            {
                hasBarcode = temp[i].Contains(barcode);
                if (hasBarcode)
                {
                    tempResult.Add(temp[i]);
                }
            }
            string result = "";
            if (tempResult.Count == 1)
            {
                result = tempResult[0];
            }
            else if (tempResult.Count > 1)
            {
                List<DateTime> fileTime = new List<DateTime>();
                for (int i = 0; i < tempResult.Count; i++)
                {
                    string[] res = tempResult[i].Split('_');
                    string[] res2 = res[1].Split('.');
                    // 20191204103614
                    int year = Int32.Parse(res2[0].Substring(0, 4));
                    int month = Int32.Parse(res2[0].Substring(4, 2));
                    int day = Int32.Parse(res2[0].Substring(6, 2));
                    int hour = Int32.Parse(res2[0].Substring(8, 2));
                    int minute = Int32.Parse(res2[0].Substring(10, 2));
                    int second = Int32.Parse(res2[0].Substring(12, 2));
                    DateTime time = new DateTime(year, month, day, hour, minute, second);
                    fileTime.Add(time);
                }
                int newIndex = 0;
                DateTime newTime = fileTime[0];
                for (int i = 1; i < fileTime.Count; i++)
                {
                    if (DateTime.Compare(newTime, fileTime[i]) < 0)
                    {
                        newIndex = i;
                        newTime = fileTime[i];
                    }
                }
                result = tempResult[newIndex];
            }
            return result;
        }

        private string GetMesFilePath(string barcode,string filePath)
        {
            List<string> temp = Directory.GetFiles(filePath).ToList();
            bool hasBarcode = false;
            List<string> tempResult = new List<string>();
            for (int i = 0; i < temp.Count; i++)
            {
                hasBarcode = temp[i].Contains(barcode);
                if (hasBarcode)
                {
                    tempResult.Add(temp[i]);
                }
            }
            string result = "";
            if (tempResult.Count == 1)
            {
                result = tempResult[0];
            }
            else if (tempResult.Count > 1)
            {
                List<DateTime> fileTime = new List<DateTime>();
                for (int i = 0; i < tempResult.Count; i++)
                {
                    string[] res = tempResult[i].Split('_');
                    string[] res2 = res[2].Split('.');
                    // 20191204103614
                    int year = Int32.Parse(res2[0].Substring(0, 4));
                    int month = Int32.Parse(res2[0].Substring(4, 2));
                    int day = Int32.Parse(res2[0].Substring(6, 2));
                    int hour = Int32.Parse(res2[0].Substring(8, 2));
                    int minute = Int32.Parse(res2[0].Substring(10, 2));
                    int second = Int32.Parse(res2[0].Substring(12, 2));
                    DateTime time = new DateTime(year, month, day, hour, minute, second);
                    fileTime.Add(time);
                }
                int newIndex = 0;
                DateTime newTime = fileTime[0];
                for (int i = 1; i < fileTime.Count; i++)
                {
                    if (DateTime.Compare(newTime, fileTime[i]) < 0)
                    {
                        newIndex = i;
                        newTime = fileTime[i];
                    }
                }
                result = tempResult[newIndex];
            }
            return result;
        }

        /// <summary>
        /// 解析Mark文件， 得到相对于workpiece1的坐标patternMarksZero 及MES中需要跳过的拼版
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public bool ParseBarcode(string barcode)
        {
            bool result = GetMESData(barcode);
            if (!result)
            {
                return result;
            }
            result = GetEmarkData(barcode);

            return result;
        }

        /// <summary>
        /// 获取最新的fiducial mark文件
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private bool GetMESData(string barcode)
        {
            string filePath = FluProgram.FluidProgram.Current.RuntimeSettings.CustomParam.AmphnolParam.DataMesPathDir;
            string userName = FluProgram.FluidProgram.Current.RuntimeSettings.CustomParam.AmphnolParam.EmarkUserName;
            string password = FluProgram.FluidProgram.Current.RuntimeSettings.CustomParam.AmphnolParam.EmarkPassword;
            //打开共享文件夹映射路径
            string msg;
            //获取当前最新条码文件（可能存在重复取最新的）
            string barcodeFilePath = "";
            try
            {
                barcodeFilePath = GetMesFilePath(barcode, filePath);
            }
            catch (Exception)
            {
                // 连接不上再重新连接
                int status = WNetConnection.DosConnect(filePath, userName, password, out msg);
                if (status != 0 && status != 1219)
                {
                    MessageBox.Show(msg);
                    return false;
                }
                barcodeFilePath = GetMesFilePath(barcode, filePath);
            }

            if (barcodeFilePath.Length < 2)
            {
                MessageBox.Show("未找到" + barcode + "的mes skip文件。");
                return false;
            }
            string[] textLines;
            FileUtils.ReadLines(barcodeFilePath, out textLines);
            string[] splitString = { "<SkipMark>", "</SkipMark>" };
            string[] res = textLines[0].Split(splitString, StringSplitOptions.RemoveEmptyEntries);
            List<char> chars = res[0].ToList();
            this.SkipBoards.Clear();
            for (int i = 0; i < chars.Count; i++)
            {
                if (chars[i] == '1')
                {
                    this.SkipBoards.Add(i + 1);
                }
            }
            return true;
        }

        /// <summary>
        /// Emark 数据
        /// </summary>
        /// <param name=""></param>
        private bool GetEmarkData(string barcode)
        {
            string filePath = FluProgram.FluidProgram.Current.RuntimeSettings.CustomParam.AmphnolParam.DataEmarkPathDir;
            string userName = FluProgram.FluidProgram.Current.RuntimeSettings.CustomParam.AmphnolParam.EmarkUserName;
            string password = FluProgram.FluidProgram.Current.RuntimeSettings.CustomParam.AmphnolParam.EmarkPassword;
            //打开共享文件夹映射路径
            string msg;
            //获取当前最新条码文件（可能存在重复取最新的）
            string barcodeFilePath = "";
            try
            {
                barcodeFilePath = GetBarcodeFilePath(barcode, filePath);
            }
            catch (Exception)
            {
                // 连接不上再重新连接
                int status = WNetConnection.DosConnect(filePath, userName, password, out msg);
                if (status != 0 && status != 1219)
                {
                    MessageBox.Show(msg);
                    return false;
                }
                barcodeFilePath = GetBarcodeFilePath(barcode, filePath);
            }

            if (barcodeFilePath.Length < 2)
            {
                MessageBox.Show("未找到" + barcode + "的fiducial mark文件。");
                return false;
            }
            string[] textLines;
            FileUtils.ReadLines(barcodeFilePath, out textLines);
            //将解析的pattern点位
            List<ResultAmphnol> patternMarks = new List<ResultAmphnol>();
            ResultAmphnol workpieceMark1 = new ResultAmphnol();
            ResultAmphnol workpieceMark2 = new ResultAmphnol();
            patternMarks.Clear();
            foreach (string line in textLines)
            {
                string[] res = line.Split(',');
                if (res.Length > 3) // 第一行不要
                {
                    continue;
                }
                ResultAmphnol amphnol = new ResultAmphnol();
                amphnol.lable = res[0];
                double x, y;
                x = double.Parse(res[1]);
                y = double.Parse(res[2]);
                amphnol.position = new PointD(x, y);
                patternMarks.Add(amphnol);
                if (amphnol.lable.Contains("P1"))
                {
                    workpieceMark1 = amphnol;
                }
                if (amphnol.lable.Contains("P2"))
                {
                    workpieceMark2 = amphnol;
                }
            }
            //都减去workpieceMark1 的坐标
            this.patternMarksZero.Clear();
            if (workpieceMark1 == null || workpieceMark2 == null)
            {
                MessageBox.Show("解析共享坐标文件错误");
                return false;
            }

            if (Math.Abs(workpieceMark1.position.X) > 450 || Math.Abs(workpieceMark2.position.X) > 450 || Math.Abs(workpieceMark1.position.Y) > 900 || Math.Abs(workpieceMark2.position.Y) > 900)
            {
                MessageBox.Show("获取治具Mark坐标异常，请检查fiducial-mark数据");
                return false;
            }

            this.workpieceMark1Zero = (ResultAmphnol)workpieceMark1.Clone();
            this.workpieceMark1Zero.position = PointZeroRel(workpieceMark1.position, workpieceMark1.position);
            this.workpieceMark2Zero = (ResultAmphnol)workpieceMark2.Clone();
            this.workpieceMark2Zero.position = PointZeroRel(workpieceMark1.position, workpieceMark2.position);
            foreach (ResultAmphnol item in patternMarks)
            {
                ResultAmphnol amphnol = (ResultAmphnol)item.Clone();
                amphnol.position = PointZeroRel(workpieceMark1.position, amphnol.position);
                this.patternMarksZero.Add(amphnol);
            }
            return true;
        }

        /// <summary>
        /// p相对于origion的位置
        /// </summary>
        /// <param name="origion"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private PointD PointZeroRel(PointD origion, PointD p)
        {
            double workpieceMark1X = origion.X;
            double workpieceMark1Y = origion.Y;
            return new PointD(p.X- workpieceMark1X, p.Y- workpieceMark1Y);
        }
        //相机拍到的Mark坐标
        private PointD workPieceMark1;
        private PointD workPieceMark2;
        /// <summary>
        /// 根据拍照得到Workpiece1 和Workpiece2，将patternMarksZero 转换为实际位置
        /// </summary>
        /// <param name="marks"></param>
        public bool TransPoint(List<MarkCmd> marks)
        {
            if (marks.Count!=2)
            {
                MessageBox.Show("校正文件mark必须两个mark点");
                return false;
            }
            try
            {
                this.patternMarksTransed.Clear();
                this.workpieceMark1Transed = (ResultAmphnol)this.workpieceMark1Zero.Clone();
                this.workpieceMark1Transed.position = marks[0].ModelFindPrm.TargetInMachine;

                this.workpieceMark2Transed = (ResultAmphnol)this.workpieceMark2Zero.Clone();
                this.workpieceMark2Transed.position = marks[1].ModelFindPrm.TargetInMachine;

                this.workPieceMark1 = marks[0].ModelFindPrm.TargetInMachine.Clone() as PointD;
                this.workPieceMark2 = marks[1].ModelFindPrm.TargetInMachine.Clone() as PointD;

                CoordinateTransformer transformer = new CoordinateTransformer();
                //根据拍照的workpiece Mark校正所有的 pattern Mark的点位
                transformer.SetMarkPoint(this.workpieceMark1Zero.position, this.workpieceMark2Zero.position, this.workPieceMark1, this.workPieceMark2);

                foreach (ResultAmphnol item in this.patternMarksZero)
                {
                    ResultAmphnol amphnol = (ResultAmphnol)item.Clone();
                    amphnol.position = transformer.Transform(amphnol.position);
                    this.patternMarksTransed.Add(amphnol);
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        /// <summary>
        /// 根据workpiece1 和workpiece1计算出patternMark1相对于workpiece1的方位，用角度和长度定位
        /// </summary>
        /// <param name="worpieceMark1"></param>
        /// <param name="worpieceMark2"></param>
        /// <param name="patternMark1"></param>
        /// <param name="angle"></param>
        /// <param name="lengthW1P"></param>
        public void findPointRelWorkPiece(PointD worpieceMark1, PointD worpieceMark2, PointD patternMark1, out double angle, out double lengthW1P)
        {
            angle = 0;
        
            double angleW1ToW2 = MathUtils.CalculateArc(worpieceMark1, worpieceMark2);
            double angleW1ToP1 = MathUtils.CalculateArc(worpieceMark1, patternMark1);
            //夹角
            angle = angleW1ToP1 - angleW1ToW2;
            //长度
            lengthW1P = worpieceMark1.DistanceTo(patternMark1);

        }
      
        /// <summary>
        /// 根据示教Mark点的坐标找到对应的文件中的Mark点坐标
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataBase GetData(DataBase data)
        {
            try
            {
                if (data is DataAmphnol)
                {
                    DataAmphnol dataAmphnol = data as DataAmphnol;
                    double anglePatternMark1 = 0;
                    double lengthPatternMark1 = 0;

                    this.findPointRelWorkPiece(this.workPieceMark1, this.workPieceMark1, dataAmphnol.MarkInMachine, out anglePatternMark1, out lengthPatternMark1);

                    foreach (ResultAmphnol item in this.patternMarksTransed)
                    {
                        double angleTmp = 0;
                        double lengthTmp = 0;
                        this.findPointRelWorkPiece(this.workpieceMark1Transed.position, this.workpieceMark2Transed.position, item.position, out angleTmp, out lengthTmp);
                        if (Math.Abs(anglePatternMark1 - angleTmp) < this.toleranceAngle && Math.Abs(lengthPatternMark1 - lengthTmp) < this.tolerancePos)
                        {
                            data.IsOk = true;
                            data.MarkFromFile = item.position;
                            return data;
                        }
                    }
                    // 没匹配上可能是跳过的穴位
                    data.IsOk = true;
                    data.MarkFromFile = new PointD(10000, 10000);
                }
            }
            catch (Exception e)
            {
                data.IsOk = false;
                Log.Print(e.Message);
            }
            return data;
        }

        #region data
        public void SaveData(string path)
        {
            
        }

        public void AppendRecored(string dataStr)
        {
            
        }

        public void ClearRecord()
        {
            
        }

        public void SaveOrNot(double value)
        {
            
        }


        #endregion

        #region 生产
        public void ProductionBefore()
        {
            
        }

        public void Production()
        {
            
        }

        public void ProductionAfter()
        {
            
        }

        public void AppendRecored(string name, string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 将跳过的拼版穴位传递出去
        /// </summary>
        /// <param name="SkipBoards"></param>
        public void SkipBoard(List<int> SkipBoards)
        {
            this.SkipBoards.AddRange(this.SkipBoards);
            return;
        }
        #endregion

    }
    
}
