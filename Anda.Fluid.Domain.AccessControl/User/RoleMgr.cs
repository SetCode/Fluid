using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Trace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.AccessControl.User
{
    /// <summary>
    /// 职能管理类
    /// </summary>
    [Serializable]
    public sealed class RoleMgr
    {
        private static RoleMgr instance = new RoleMgr();
        private RoleMgr()
        {
        }
        public static RoleMgr Instance => instance;

        private string path = SettingsPath.PathBusiness + "\\" + typeof(RoleMgr).Name;

        [NonSerialized]
        private Role currentRole;
        /// <summary>
        /// 当前用户职能
        /// </summary>
        public Role CurrentRole
        {
            get { return currentRole; }
            private set { currentRole = value; }
        }

        public Role Operator { get; private set; } = new Role(RoleType.Operator);
        public Role Technician { get; private set; } = new Role(RoleType.Technician);
        public Role Supervisor { get; private set; } = new Role(RoleType.Supervisor);
        [NonSerialized]
        private Role developer = new Role(RoleType.Developer);
        public Role Developer
        {
            get { return developer; }
            private set { developer = value; }
        }

        /// <summary>
        /// 切换当前职能
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool SwitchRole(RoleType type)
        {
            if (type == RoleType.Developer)
            {
                currentRole = this.Developer;
            }
            else if (type == RoleType.Supervisor)
            {
                currentRole = this.Supervisor;
            }
            else if (type == RoleType.Technician)
            {
                currentRole = this.Technician;
            }
            else
            {
                currentRole = Operator;
            }
            return true;
        }
        /// <summary>
        /// 是否是开发者
        /// </summary>
        /// <returns></returns>
        public bool IsDeveloper()
        {
            return currentRole.Type == RoleType.Developer;
        }

        #region save & load
        public bool Load()
        {
            bool result;
            Stream fstream = null;
            try
            {
                fstream = new FileStream(this.path, FileMode.Open, FileAccess.Read);
                BinaryFormatter binFormat = new BinaryFormatter();
                instance = (RoleMgr)binFormat.Deserialize(fstream);
                result = true;
            }
            catch (Exception e)
            {
                Log.Dprint("role file load error : " + e);
                result = false;
            }
            finally
            {
                if (fstream != null)
                {
                    fstream.Close();
                }
            }
            if (Instance == null)
            {
                instance = new RoleMgr();
                instance.currentRole = instance.Operator;
                return false;
            }
            if (instance.currentRole == null)
            {
                instance.currentRole = instance.Operator;
            }
            instance.developer = new Role(RoleType.Developer);
            return result;
        }
        /// <summary>
        /// 二进制序列化保存
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            bool result = false;
//             ThreadUtils.Run(() =>
//             {
            Stream fstream = null;
            try
            {
                fstream = new FileStream(this.path, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter binFormat = new BinaryFormatter();
                binFormat.Serialize(fstream, this);
                result = true;
            }
            catch (Exception e)
            {
                Log.Dprint("role file save error : " + e);
                result = false;
            }
            finally
            {
                if (fstream != null)
                {
                    fstream.Close();
                }
            }
/*            });*/
            return result;
        }
        #endregion


    }
}
