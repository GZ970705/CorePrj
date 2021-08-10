using PrjWebJWTandSwwage.Base;
using PrjWebJWTandSwwage;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Models
{
    /// <summary>
    /// 编号生成表
    /// </summary>
    [Serializable, Description("编号生成表")]
    public class ComCode : BaseInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public ComCode()
        {
            this.Type = null;
            this.CompanyID = 0;
            this.MaxVlu = null;
            this.MinVlu = null;
            this.CurVlu = null;
            this.FixBits = null;
            this.Prefix = null;
            this.Suffix = null;
            this.Brf = null;
            this.Uptr = null;
        }
        #endregion

        #region 映射属性
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int CompanyID { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxVlu { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public string MinVlu { get; set; }
        /// <summary>
        /// 当前值
        /// </summary>
        public string CurVlu { get; set; }
        /// <summary>
        /// 固定位数
        /// </summary>
        public string FixBits { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Brf { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Uptr { get; set; }
        #endregion

        #region 关联属性
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 更新人名称
        /// </summary>
        public string UptrName { get; set; }
        #endregion

        #region 其他属性
        #endregion
    }
}
