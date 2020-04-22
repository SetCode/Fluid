using Anda.Fluid.Domain.Custom.DataCentor;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Custom
{
    public interface ICustomary
    {
        MachineSelection MachineSelection { get; } 
        DataBase GetData(DataBase data);

        bool ParseBarcode(string text);

        bool TransPoint(List<MarkCmd> marks);

        void SkipBoard(List<int> SkipBoards);

        #region Data 记录        
        string FileName { get; set; }
        void SaveData(string pathDir);
       
        //void AppendRecored(string dataStr);
        void AppendRecored(string name, string value);
        void ClearRecord();
        #endregion

        #region 运行时动作
        void ProductionBefore();

        void Production();

        void ProductionAfter();
        #endregion 
    }
}
