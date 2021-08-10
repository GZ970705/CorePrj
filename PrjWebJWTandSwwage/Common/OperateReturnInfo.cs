using System;

namespace PrjWebJWTandSwwage.Common
{
    /// <summary>
    /// 操作返回
    /// </summary>
    [Serializable]
    public class OperateReturnInfo
    {
        #region 构造
        /// <summary>
        /// 构造函数 
        /// </summary>
        public OperateReturnInfo()
        {
            this.ReturnInfo = string.Empty;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="returnCode">OperateCodeEnum类型</param>
        public OperateReturnInfo(OperateCode returnCode)
            : this()
        {
            this.ReturnCode = returnCode;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="returnCode">OperateCodeEnum类型</param>
        /// <param name="returnInfo">强制返回值</param>
        public OperateReturnInfo(OperateCode returnCode, object returnInfo)
            : this(returnCode)
        {
            this.ReturnInfo = returnInfo ?? string.Empty;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 返回的值类型
        /// </summary>
        public OperateCode ReturnCode { get; set; }
        /// <summary>
        /// 返回的可选信息
        /// </summary>
        public object ReturnInfo { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 构建操作成功信息
        /// </summary>
        /// <param name="returnInfo"></param>
        /// <returns></returns>
        public static OperateReturnInfo Success(object returnInfo)
        {
            return new OperateReturnInfo(OperateCode.Success, returnInfo);
        }
        /// <summary>
        /// 构建操作失败信息
        /// </summary>
        /// <param name="returnInfo"></param>
        /// <returns></returns>
        public static OperateReturnInfo Failed(object returnInfo)
        {
            return new OperateReturnInfo(OperateCode.Failed, returnInfo);
        }
        #endregion
    }
}
