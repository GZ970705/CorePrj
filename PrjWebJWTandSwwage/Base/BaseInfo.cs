using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Base
{
    /// <summary>
    /// 模型基类
    /// </summary>
    [Serializable]
    public class BaseInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public BaseInfo()
        {
            this.IsNew = false;
            this.UptDtt = DateTime.Now;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 是否新对象
        /// </summary>
        public virtual bool IsNew { get; set; }
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UptDtt { get; set; }

        #endregion

        #region 分页属性
        public int pageIndex { get; set; }

        public int pageSize { get; set; }

        public int totalCount { get; set; }
        #endregion
    }
}
