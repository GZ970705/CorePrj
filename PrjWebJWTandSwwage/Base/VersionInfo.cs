using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Base
{
    /// <summary>
    ///  版本号模型基类
    /// </summary>
    [Serializable]
    public class VersionInfo : BaseInfo
    {
        public VersionInfo()
            : base()
        {
            Version = 0;
        }
        #region 属性
        /// <summary>
        /// 是否新对象
        /// </summary>
        public override bool IsNew { get { return Version == 0; } }
        /// <summary>
        /// 版本号
        /// </summary>
        public decimal Version { get; set; }
        #endregion
    }
}
