using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.Custom.DataCentor;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.DeviceType;
using Anda.Fluid.Infrastructure.Alarming;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Anda.Fluid.Domain.FluProgram;

namespace Anda.Fluid.Domain.Custom
{
    public class Custom银宝山 : ICustomary
    {
        public string FileName { get; set; }


        public MachineSelection MachineSelection => MachineSelection.YBSX;
        
        public DataBase GetData(DataBase data)
        {
            return null;
        }

        public bool ParseBarcode(string text)
        {
            return true;
        }
        public bool TransPoint(List<MarkCmd> marks)
        {
            return true;
        }

        #region  Data
        public void AppendRecored(string dataStr)
        {
            return;
        }

        public void ClearRecord()
        {

        }

        public void SaveData(string pathDir)
        {
            
        }

        public void SaveOrNot(double value)
        {
            
        }

        #endregion


        #region 生产
        public void ProductionBefore()
        {
            DoType.阀1胶桶气压控制.Set(false);
        }

        public void Production()
        {
            //气压保持
            DoType.阀1胶桶气压控制.Set(true);
            Debug.WriteLine("阀1胶桶气压控制 开");
        }

        public void ProductionAfter()
        {
            //气压中断
            DoType.阀1胶桶气压控制.Set(false);
            Debug.WriteLine("阀1胶桶气压控制 关闭");
            //声光提示  蜂鸣器  三色灯闪烁  阻塞
            ManualResetEvent stop = new ManualResetEvent(false);
            Machine.Instance.LightTower.SetMessage(new Drive.MachineStates.LightTowerMessage() {
                Type = Drive.MachineStates.LightTowerType.Yellow,
                Shining = true,
                Beep = true,
                MessageType = 3
            });
            Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
            dic.Add(DialogResult.Yes, new Action(()=> 
            {
                stop.Set();
            }));
            dic.Add(DialogResult.No, new Action(()=> { }));            
            List<string> info = new List<string>();
            info.Add("生产完成");
           DialogResult dr= new AlarmForm(dic, info).ShowDialog();
            switch (dr)
            {
                case DialogResult.Yes:

                    break;
                case DialogResult.No:
                    //程序终止
                    Executor.Instance.Stop();
                    break;
            }
            //stop.WaitOne();
        }

        public void AppendRecored(string name, string value)
        {
            return;
        }

        public void SkipBoard(List<int> SkipBoards)
        {
            return;
        }
        #endregion
    }
}
