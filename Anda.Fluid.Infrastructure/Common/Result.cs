using System;

namespace Anda.Fluid.Infrastructure.Common
{
    /// <summary>
    /// 用于存储方法返回值，出错时的错误码、错误信息
    /// </summary>
    [Serializable]
    public class Result
    {
        public static readonly Result OK = new Result(true, null);
        public static readonly Result FAILED = new Result(false, null);

        private bool isOk;
        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool IsOk { get { return isOk; } }

        private object param;
        /// <summary>
        /// 返回参数
        /// </summary>
        public object Param { get { return param; } }

        private int errCode;
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrCode { get { return errCode; } }

        private string errMsg;
        /// <summary>
        /// 出错描述信息
        /// </summary>
        public string ErrMsg { get { return errMsg; } }

        public Result(bool isOk, object param) : this(isOk, param, 0, null)
        {
        }

        public Result(bool isOk, object param, string errMsg) : this(isOk, param, 0, errMsg)
        {
        }

        public Result(bool isOk, object param, int errCode) : this(isOk, param, errCode, null)
        {
        }

        public Result(bool isOk, object param, int errCode, string errMsg)
        {
            this.isOk = isOk;
            this.param = param;
            this.errCode = errCode;
            this.errMsg = errMsg;
        }
    }
}