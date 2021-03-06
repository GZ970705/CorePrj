using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.JWT
{
    /// <summary>
    /// 配置token信息
    /// </summary>
    public class JWTConfig
    {
        /// <summary>
        /// Token发布者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// oken接受者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string IssuerSigningKey { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public int AccessTokenExpiresMinutes { get; set; }
    }
}
