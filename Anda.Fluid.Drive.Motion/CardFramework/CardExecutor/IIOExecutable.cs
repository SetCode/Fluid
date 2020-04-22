using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.CardExecutor
{
    public interface IIOExecutable
    {
        int DiData { get; set; }
        int DoData { get; set; }
        short GetDi(short cardId, short mdlId, out int data);
        short SetDo(short cardId, short mdlId, int data);
        short GetDo(short cardId, short mdlId, out int data);
        short SetDoBit(short cardId, short mdlId, short doId, short value);
        int GetDiBit(int data, short diId);
        int GetDoBit(int data, short doId);
    }
}
