using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Common
{
    /// <summary>
    /// 树返回结果
    /// </summary>
    [Serializable]
    public class ZTree
    {
        /// <summary>
        /// 唯一编号 必须
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 父级 必须
        /// </summary>
        public string pId { get; set; }
        /// <summary>
        /// 显示值 必须
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool open { get; set; }
        /// <summary>
        /// 是否父级
        /// </summary>
        public bool isParent { get; set; }
        /// <summary>
        /// URL地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 子级图标
        /// </summary>
        public string icon { get; set; }
        //......更多可扩展
    }
}
