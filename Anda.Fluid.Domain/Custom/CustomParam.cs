using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Custom
{
    /// <summary>
    /// RTV MES或数据相关参数
    /// </summary>
    [Serializable]
    public class RTVParam
    {
        public string DataMesPathDir { get; set; }
        public string DataLocalPathDir { get; set; }
        public bool IsSaveCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Depart { get; set; }
        /// <summary>
        /// 电脑编号
        /// </summary>
        public string ComputerInfo { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string MachineInfo { get; set; }
        /// <summary>
        /// 生产线编号
        /// </summary>
        public string ProductLineInfo { get; set; }
        /// <summary>
        /// Owk信息
        /// </summary>
        public string OwkInfo { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserInfo { get; set; }
    }
    /// <summary>
    /// AFN MES或数据相关参数
    /// </summary>
    [Serializable]
    public class AmphnolParam
    {
        public string DataMesPathDir { get; set; }
        public string DataLocalPathDir { get; set; }
        public string DataEmarkPathDir { get; set; }
        public string EmarkUserName { get; set; }
        public string EmarkPassword { get; set; }
    }

    /// <summary>
    /// 客户 MES或其他通信数据参数
    /// </summary>
    [Serializable]
    public class CustomParam
    {
        public RTVParam RTVParam { get; set; }

        public AmphnolParam AmphnolParam { get; set; }

        public CustomParam()
        {
            this.RTVParam = new RTVParam();
            this.AmphnolParam = new AmphnolParam();
        }
    }
}
