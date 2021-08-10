
using PrjWebJWTandSwwage;
using PrjWebJWTandSwwage.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Common
{
    /// <summary>
    /// 基于Iview的表格树形类
    /// </summary>
    public class TableTree: VersionInfo
    {
        public TableTree()
        {
            this.ID = null;
            this.ZName = null;
            this.EName = null;
            this.Describe = null;
            this.Stt = 1;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        public string InfoTypeID { get; set; }
        
        /// <summary>
        /// 中文名称
        /// </summary>
        public string ZName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public decimal Stt { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Identification { get; set; }
        /// <summary>
        /// 子节点属性数组
        /// </summary>
        public IList<TableTree> children { get; set; }

    }
}
