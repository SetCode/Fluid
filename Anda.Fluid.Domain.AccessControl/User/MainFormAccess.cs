using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.AccessControl.User
{
    /// <summary>
    /// 主窗体控件权限管理类
    /// </summary>
    [Serializable]
    public class MainFormAccess : IAccess
    {
        /// <summary>
        /// 能否使用加载程序按钮
        /// </summary>
        public bool CanUseBtnLoadPgm { get; set; }
        /// <summary>
        /// 能否进入编程界面按钮
        /// </summary>
        public bool CanUseBtnEditPgm { get; set; }
        /// <summary>
        /// 能否使用全局位置按钮
        /// </summary>
        public bool CanUseBtnLoc1 { get; set; }

        #region Tools下拉菜单相关管控
        public bool CanUseBtnTools1 { get; set; }
        public bool CanEnterIoForm { get; set; }
        public bool CanEnterScaleForm { get; set; }
        public bool CanEnterPurgeForm { get; set; }
        public bool CanEnterHeightForm { get; set; }
        public bool CanEnterHeaterForm { get; set; }
        public bool CanEnterCameraForm { get; set; }

        //bool can
        #endregion

        #region Config下拉菜单管控
        public bool CanUseBtnConfig1 { get; set; }
        public bool CanSwitchLanguage { get; set; }
        public bool CanSwitchUnit { get; set; }
        public bool CanCameraCalib { get; set; }
        public bool CanScriptedCalib { get; set; }
        public bool CanSetupLocations { get; set; }
        public bool CanSetupASV { get; set; }
        public bool CanCalCpk { get; set; }

        #endregion

        #region Setup相关控制
        public bool CanUseBtnAdvanced1 { get; set; }
        public bool CanSetupAccess { get; set; }    //权限分配权限
        public bool CanSetupMotion { get; set; }
        public bool CanSetupAxes { get; set; }
        public bool CanSetupIO { get; set; }
        public bool CanSetupVision { get; set; }
        public bool CanSetupValves { get; set; }
        public bool CanSetupSensors { get; set; }
        public bool CanSetupConveyor { get; set; }
        public bool CanStripMapping { get; set; }
        #endregion
        public bool CanUseCbxRunMode { get; set; }
        public bool CanUseCbxConveyorMode { get; set; }

        public MainFormAccess(RoleType type)
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
        #region 文本汉化操作
        #region 索引器

        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return CanUseBtnLoadPgm;
                    case 1: return CanUseBtnEditPgm;
                    case 2: return CanUseBtnLoc1;
                    case 3: return CanUseBtnTools1;
                    case 4: return CanEnterIoForm;
                    case 5: return CanEnterScaleForm;
                    case 6: return CanEnterPurgeForm;
                    case 7: return CanEnterHeightForm;
                    case 8: return CanEnterHeaterForm;
                    case 9: return CanEnterCameraForm;
                    case 10: return CanUseBtnConfig1;
                    case 11: return CanSwitchLanguage;
                    case 12: return CanSwitchUnit;
                    case 13: return CanCameraCalib;
                    case 14: return CanScriptedCalib;
                    case 15: return CanSetupLocations;
                    case 16: return CanSetupASV;
                    case 17: return CanCalCpk;
                    case 18: return CanUseBtnAdvanced1;
                    case 19: return CanSetupMotion;
                    case 20: return CanSetupAxes;
                    case 21: return CanSetupIO;
                    case 22: return CanSetupVision;
                    case 23: return CanSetupValves;
                    case 24: return CanSetupSensors;
                    case 25: return CanSetupConveyor;
                    case 26: return CanStripMapping;
                    case 27: return CanSetupAccess;
                    case 28: return CanUseCbxRunMode;
                    case 29: return CanUseCbxConveyorMode;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: CanUseBtnLoadPgm = value; break;
                    case 1: CanUseBtnEditPgm = value; break;
                    case 2: CanUseBtnLoc1 = value; break;
                    case 3: CanUseBtnTools1 = value; break;
                    case 4: CanEnterIoForm = value; break;
                    case 5: CanEnterScaleForm = value; break;
                    case 6: CanEnterPurgeForm = value; break;
                    case 7: CanEnterHeightForm = value; break;
                    case 8: CanEnterHeaterForm = value; break;
                    case 9: CanEnterCameraForm = value; break;
                    case 10: CanUseBtnConfig1 = value; break;
                    case 11: CanSwitchLanguage = value; break;
                    case 12: CanSwitchUnit = value; break;
                    case 13: CanCameraCalib = value; break;
                    case 14: CanScriptedCalib = value; break;
                    case 15: CanSetupLocations = value; break;
                    case 16: CanSetupASV = value; break;
                    case 17: CanCalCpk = value; break;
                    case 18: CanUseBtnAdvanced1 = value; break;
                    case 19: CanSetupMotion = value; break;
                    case 20: CanSetupAxes = value; break;
                    case 21: CanSetupIO = value; break;
                    case 22: CanSetupVision = value; break;
                    case 23: CanSetupValves = value; break;
                    case 24: CanSetupSensors = value; break;
                    case 25: CanSetupConveyor = value; break;
                    case 26: CanStripMapping = value; break;
                    case 27: CanSetupAccess = value; break;
                    case 28: CanUseCbxRunMode = value; break;
                    case 29: CanUseCbxConveyorMode = value; break;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
        }
        #endregion

        /// <summary>
        /// 返回定义的除索引器的公共属性个数
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            //去除索引器 -1
            int Count = typeof(MainFormAccess).GetProperties().Length - 1;
            return Count;
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
                case 0: return "Load program file";
                case 1: return "Enter program form";
                case 2: return "Enter Global location form";
                case 3: return "Enter tools menu";
                case 4: return "Enter IO form";
                case 5: return "Enter scale form";
                case 6: return "Enter purge form";
                case 7: return "Enter height form";
                case 8: return "Enter Heater form";
                case 9: return "Enter Camera form";
                case 10: return "Enter configration menu";
                case 11: return "Switch language";
                case 12: return "Switch position Unit";
                case 13: return "Enter camera calibration form";
                case 14: return "Enter scripted valve offsetsform";
                case 15: return "Enter setup locations form";
                case 16: return "Enter setup ASV form";
                case 17: return "Enter calculate CPK form";
                case 18: return "Enter advanced menu";
                case 19: return "Enter setup Motion form";
                case 20: return "Enter setup Axes form";
                case 21: return "Enter setup IO form";
                case 22: return "Enter setup vision form";
                case 23: return "Enter setup valves form";
                case 24: return "Enter setup sensors form";
                case 25: return "Enter setup conveyor form";
                case 26: return "Enter strip mapping form";
                case 27: return "Enter access form";
                case 28: return "Change run mode";
                case 29: return "Change conveyor mode";
                default: return "Undefined item";
            }
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
