using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.JWT
{
    /// <summary>
    /// 存放Token 跟过期时间的类
    /// </summary>
    public class TnToken
    {
        /// <summary>
        /// token
        /// </summary>
        public string TokenStr { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// token类型
        /// </summary>
        public enum TokenType
        {
            Ok,
            Fail,
            Expired
        }
    }
}
