using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.AccessControl.User
{
    [Serializable]
    public class OtherFormAccess :IAccess
    {
        public bool CanJogFormValveEdit { get; set; }
        public bool CanUserMgrForm { get; set; }
        public bool CanUseOutputButton { get; set; }
        public bool CanEditScaleParameter { get; set; }
        public bool CanEditWeightParameter { get; set; }
        public OtherFormAccess(RoleType type)
        {
            bool initValue;
            if (type == RoleType.Developer || type == RoleType.Supervisor)
            {
                initValue = true;
            }
            else
            {
                initValue = false;
            }
            this.AllSetValue(initValue);
        }

        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return CanJogFormValveEdit;
                    case 1: return CanUserMgrForm;
                    case 2: return CanUseOutputButton;
                    case 3: return CanEditScaleParameter;
                    case 4: return CanEditWeightParameter;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: CanJogFormValveEdit = value; break;
                    case 1: CanUserMgrForm = value;break;
                    case 2: CanUseOutputButton = value;break;
                    case 3: CanEditScaleParameter = value;break;
                    case 4: CanEditWeightParameter = value;break;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
        }
        /// <summary>
        /// 返回当前类所有属性个数(除去索引器属性)
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return typeof(OtherFormAccess).GetProperties().Length - 1;
        }
        /// <summary>
        /// 返回指定索引的属性描述
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetDescription(int index)
        {
            switch (index)
            {
                case 0: return "Enter valve Edit form";
                case 1: return "Enter user management form";
                case 2: return "Can use ouput button";
                case 3: return "Can edit Scale Parameter";
                case 4: return "Can edit weight parameter";
                default: return "Undefined item";
            }
        }

        private void AllSetValue(bool value)
        {
            for (int i = 0; i < this.GetLength(); i++)
            {
                this[i] = value;
            }
        }
    }
}
