using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Anda.Fluid.Domain.Custom.DataCentor;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive;

namespace Anda.Fluid.Domain.Custom
{
    public class CustomDefault : ICustomary
    {
        public string FileName { get; set; }

        public MachineSelection MachineSelection => MachineSelection.AD16;
     

        //public CustomEnum Custom => CustomEnum.Default;

        public DataBase GetData(DataBase data)
        {
            return default(DataBase);
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
       
        public void AppendRecored(string name, string value)
        {
            
        }
        public void ClearRecord()
        {
            
        }

        public void SaveData(string path)
        {
            return;
        }


        public void SaveOrNot(double value)
        {
            

        }

        #endregion

        #region 生产
        public void ProductionBefore()
        {
            return;
        }

        public void Production()
        {
            return;
        }

        public void ProductionAfter()
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
