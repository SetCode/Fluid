using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Motion.CardFramework.CardExecutor
{
    public interface IExtMdlExecutable : IIOExecutable
    {
        short Open(short cardId);
        short Reset(short cardId);
        short Close(short cardId);
        short LoadConfig(short cardId, string configFile);
    }
}
