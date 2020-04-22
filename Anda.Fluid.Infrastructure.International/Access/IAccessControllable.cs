using Anda.Fluid.Infrastructure.International.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Anda.Fluid.Infrastructure.International.AccessEnums;

namespace Anda.Fluid.Infrastructure.International
{
    public interface IAccessControllable
    {
        int Key { get; set; }
        Control Control{get;}

        ContainerAccess CurrContainerAccess { get; set; }
        ContainerAccess DefaultContainerAccess { get; set; }
        List<AccessObj> UserAccessControls { get; set; }

        void SetupUserAccessControl();
        void SetDefaultAccess();
        void LoadAccess();
       
        
        void UpdateUIByAccess();
        
    }
}
