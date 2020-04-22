using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.DigitalGage
{
    public interface  IDigitalGagable:IConnectable
    {
        int ReadHeight(out double height);
        int ReadTimes(int readTimes, out double height);
        int Read(out double value);
    }
}
