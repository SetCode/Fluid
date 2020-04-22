using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Custom.DataCentor
{
    public class DataAmphnol : DataBase
    {
        public string MarkNameInFile = string.Empty;

   

    }

    public class ResultAmphnol:ICloneable
    {
        public string lable = string.Empty;

        public PointD position = new PointD();

        public object Clone()
        {
            ResultAmphnol res = this.MemberwiseClone() as ResultAmphnol;
            res.position = (PointD)this.position.Clone();
            return res;
        }
    }
}
