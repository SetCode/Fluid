using Anda.Fluid.Drive.Motion.ActiveItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.SetupCard
{
    public interface ICardSetupable
    {
        RobotXYZ Setup();
        bool Init();
    }
}
