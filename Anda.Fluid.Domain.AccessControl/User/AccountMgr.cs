using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.International.Access;
using Anda.Fluid.Infrastructure.Msg;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using static Anda.Fluid.Infrastructure.International.AccessEnums;

namespace Anda.Fluid.Domain.AccessControl.User
{
    [Serializable]
    public sealed class AccountMgr : IMsgSender
    {
        private static AccountMgr instance = new AccountMgr();
        public AccountMgr()
        {
        }
        public static AccountMgr Instance => instance;
        private string path = SettingsPath.PathBusiness + "\\" + typeof(AccountMgr).Name;
        [NonSerialized]
        private Account currentAccount;
        public Account CurrentAccount
        {
            get { return currentAccount; }
            set { currentAccount = value; }
        }
        private Dictionary<string, Account> AccountMap = new Dictionary<string, Account>();

        /// <summary>
        /// 切换用户操作
        /// </summary>
        /// <param name="user"></param>
        public void SwitchUser(Account user)
        {
            this.CurrentAccount = user;
            RoleMgr.Instance.SwitchRole(this.currentAccount.RoleType);
            AccessControlMgr.Instance.CurrRole = (RoleEnums)(int)user.RoleType;
            //通知主界面和编程界面,更新界面，处理非模态窗口。
            MsgCenter.Broadcast(MsgConstants.SWITCH_USER, this, null);
        }

        #region CRUD
        private void AddDefault()
        {
            this.Add(new Account(RoleType.Developer.ToString(), "4", RoleType.Developer));
            this.Add(new Account(RoleType.Supervisor.ToString(), "3", RoleType.Supervisor));
            this.Add(new Account(RoleType.Technician.ToString(), "2", RoleType.Technician));
            this.Add(new Account(RoleType.Operator.ToString(), "1", RoleType.Operator));
        }
        /// <summary>
        /// 已存在的不覆盖
        /// </summary>
        /// <param name="account"></param>
        /// <returns>返回是否添加成功</returns>
        public bool Add(Account account)
        {
            if (this.AccountMap.ContainsKey(account.NameId))
            {
                return false;
            }
            else
            {
                this.AccountMap.Add(account.NameId, account);
            }
            return true;
        }
        /// <summary>
        /// 根据用户名删除指定用户
        /// 不允许删除4个默认用户
        /// </summary>
        /// <param name="nameId"></param>
        public void Remove(string nameId)
        {
            if (nameId == RoleType.Developer.ToString() || nameId == RoleType.Supervisor.ToString() || nameId == RoleType.Technician.ToString() || nameId == RoleType.Operator.ToString())
            {
                return;
            }
            this.AccountMap.Remove(nameId);
        }
        /// <summary>
        /// 找不到用户的情况下返回默认操作员用户
        /// </summary>
        /// <param name="nameId"></param>
        /// <returns></returns>
        public Account FindBy(string nameId)
        {
            if (this.AccountMap.ContainsKey(nameId))
            {
                return this.AccountMap[nameId];
            }
            else
            {
                return this.AccountMap[RoleType.Operator.ToString()];
            }
        }
        /// <summary>
        /// 查找是否存在指定用户
        /// </summary>
        /// <param name="nameId"></param>
        /// <returns></returns>
        public bool ContainsAccount(string nameId)
        {
            return this.AccountMap.ContainsKey(nameId);
        }

        /// <summary>
        /// 返回指定的默认用户
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Account FindBy(RoleType type)
        {
            return this.FindBy(type.ToString());
        }
        /// <summary>
        /// 返回用户List集合
        /// </summary>
        /// <returns></returns>
        public List<Account> getAccountList()
        {
            List<Account> accountList = new List<Account>();
            foreach (string name in AccountMap.Keys)
            {
                if (name == "Developer")
                {
                    continue;
                }
                accountList.Add(AccountMap[name]);
            }
            return accountList;
        }

        #endregion

        #region save & load
        /// <summary>
        /// 加载用户配置文件
        /// </summary>
        /// <returns>返回是否加载成功</returns>
        public bool Load()
        {
            bool result;
            Stream fstream = null;
            try
            {
                fstream = new FileStream(this.path, FileMode.Open, FileAccess.Read);
                BinaryFormatter binFormat = new BinaryFormatter();
                instance = (AccountMgr)binFormat.Deserialize(fstream);
                result = true;
            }
            catch (Exception)
            {
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
                instance = new AccountMgr();
                result = false;
            }
            instance.AddDefault();
            if (Instance.CurrentAccount == null)
            {
                instance.currentAccount = instance.FindBy(RoleType.Operator);
            }
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
            catch (Exception)
            {
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
