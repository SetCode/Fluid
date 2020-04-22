using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.AccessControl.User
{
    /// <summary>
    /// 编程界面按钮权限管控类
    /// </summary>
    [Serializable]
    public class ProgramFormAccess : IAccess
    {
        /// <summary>
        /// 能否使用所有轨迹按键
        /// </summary>
        public bool CanUseGluePathCmd { get; set; }
        public bool CanUseOtherCmd { get; set; }
        /// <summary>
        /// 能否使用所有程序文件操作按键
        /// </summary>
        public bool CanUseProgramFileOperate { get; set; }
        /// <summary>
        /// 能否使用轨迹程序pattern树形结构控件
        /// </summary>
        public bool CanUseModuleTree { get; set; }
        /// <summary>
        /// 能否编辑程序指令列表
        /// </summary>
        public bool CanUseProgramList { get; set; }
        /// <summary>
        /// 能否进入轨迹程序设置界面
        /// </summary>
        public bool CanEnterProgramSettingForm { get; set; }

        public ProgramFormAccess(RoleType type)
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
        #region 权限说明国际化文本


        /// <summary>
        /// 属性索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return CanUseGluePathCmd;
                    case 1: return CanUseOtherCmd;
                    case 2: return CanUseProgramFileOperate;
                    case 3: return CanUseModuleTree;
                    case 4: return CanUseProgramList;
                    case 5: return CanEnterProgramSettingForm;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: CanUseGluePathCmd = value; break;
                    case 1: CanUseOtherCmd = value; break;
                    case 2: CanUseProgramFileOperate = value; break;
                    case 3: CanUseModuleTree = value; break;
                    case 4: CanUseProgramList = value; break;
                    case 5: CanEnterProgramSettingForm = value; break;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
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
                case 0: return "Use glue path command";
                case 1: return "Use other command";
                case 2: return "Use program file operate";
                case 3: return "Use module tree";
                case 4: return "Use program command list";
                case 5: return "Enter program setting form";
                default: return "Undefined item";
            }
        }
        /// <summary>
        /// 返回除索引器的所有公共属性个数
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            int Count = typeof(ProgramFormAccess).GetProperties().Length - 1;
            return Count;
        }
        #endregion
        /// <summary>
        /// 批量设置当前类所有权限变量
        /// </summary>
        /// <param name="value"></param>
        private void AllSetValue(bool value)
        {
            for (int i = 0; i < this.GetLength(); i++)
            {
                this[i] = value;
            }
        }
    }
}
