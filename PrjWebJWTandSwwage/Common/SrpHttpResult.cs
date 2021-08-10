using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjWebJWTandSwwage.Common
{
    /// <summary>
    /// 接口访问返回值
    /// </summary>
    public class SrpHttpResult
    {
        /// <summary>
        /// 操作返回值
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 操作返回值名称
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回操作集合
        /// </summary>
        public string result { get; set; }
    }
}
