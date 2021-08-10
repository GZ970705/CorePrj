using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjWebJWTandSwwage.Common
{
    /// <summary>
    /// 操作返回枚举以及对应的返回编码
    /// </summary>
    public enum OperateCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// {0}失败
        /// </summary>
        Failed = -1,
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownError = 10000,
        /// <summary>
        ///  询问
        /// </summary>
        Questioning = 9997,
    }
}
