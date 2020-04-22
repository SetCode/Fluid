using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Anda.Fluid.Infrastructure.International.AccessEnums;

namespace Anda.Fluid.Infrastructure.International.Access
{
    public interface IControlAccessable
    {
        //唯一标识
        string Name { get; set; }
        string AccessName { get; set; }

        RoleEnums RoleLevel { get; set; } 

        void Add(IControlAccessable controlAccess);

        void Remove(IControlAccessable controlAccess);

        void Display(int level);
    }
}
