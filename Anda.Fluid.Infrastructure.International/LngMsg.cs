using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.International
{

    ///<summary>
    /// Description	:语言切换消息类
    /// Author  	:liyi
    /// Date		:2019/05/30
    ///</summary>   
    public class LngMsg
    {
        public const string SWITCH_LNG = "SWITCH_LNG";

        /// <summary>
        /// 传送条码信息
        /// </summary>
        public const string MSG_Barcode_Info = "msg_barcode_info";

        /// <summary>
        /// 传送测宽和测高信息
        /// </summary>
        public const string MSG_WidthAndHeight_Info = "msg_width&height_info";

        /// <summary>
        /// 清空RTV所有显示信息
        /// </summary>
        public const string MSG_Clear_RtvInfo = "msg_clear_RtvInfo";
    }
}
