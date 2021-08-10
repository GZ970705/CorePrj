using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Common
{
    /// <summary>
    /// 基于Iview的树形类
    /// </summary>
    public class IviewTree
    {
        public IviewTree()
        {
            this.ID = null;
            this.title = null;
            this.expand = false;
            this.disabled = false;
            this.disableCheckbox = false;
            this.selected = false;
            this.@checked = false;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 是否展开直子节点
        /// </summary>
        public bool expand { get; set; }
        /// <summary>
        /// 禁掉响应
        /// </summary>
        public bool disabled { get; set; }
        /// <summary>
        /// 禁掉 checkbox
        /// </summary>
        public bool disableCheckbox { get; set; }
        /// <summary>
        /// 是否选中子节点
        /// </summary>
        public bool selected { get; set; }
        /// <summary>
        /// 是否勾选(如果勾选，子节点也会全部勾选)
        /// </summary>
        public bool @checked { get; set; }
        /// <summary>
        /// 子节点属性数组
        /// </summary>
        public IList<IviewTree> children { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Brf { get; set; }
    }
}
