using System;
using System.Runtime.Serialization;
using System.Security;

namespace PrjWebJWTandSwwage.Common
{
    /// <summary>
    /// 异常消息提示
    /// </summary>
    [Serializable]
    public class MessageException : Exception
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public MessageException()
            : base()
        {
        }
        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="message"></param>
        public MessageException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecuritySafeCritical]
        protected MessageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MessageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        #endregion
    }
}
