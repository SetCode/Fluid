using Anda.Fluid.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.AccessControl.User
{
    /// <summary>
    /// There are only 4 Role Level by default
    /// </summary>
    public enum RoleType
    {
        Operator,
        Technician,
        Supervisor,
        Developer
    }
    /// <summary>
    /// 权限类
    /// 包含:主界面权限/编程界面权限/其他控件权限
    /// </summary>
    [Serializable]
    public class Role
    {
        public RoleType Type { get; private set; } = RoleType.Operator;

        public Role(RoleType type)
        {
            this.Type = type;
            this.MainFormAccess = new MainFormAccess(this.Type);
            this.ProgramFormAccess = new ProgramFormAccess(this.Type);
            this.OtherFormAccess = new OtherFormAccess(this.Type);
        }
        public MainFormAccess MainFormAccess { get; set; }

        public ProgramFormAccess ProgramFormAccess;
        public OtherFormAccess OtherFormAccess { get; set; }

    }
}
