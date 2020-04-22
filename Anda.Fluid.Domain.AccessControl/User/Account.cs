using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.AccessControl.User
{
    /// <summary>
    /// 用户类，不同权限级别可以拥有多个用户
    /// </summary>
    [Serializable]
    public class Account
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string NameId { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户权限级别
        /// </summary>
        public RoleType RoleType { get; set; }

        public Account(string name,string password,RoleType type)
        {
            this.NameId = name;
            //加密
            this.Password = password;
            this.RoleType = type;
        }
        public void ChangePassword(string password)
        {
            this.Password = password;
        }
    }
}
