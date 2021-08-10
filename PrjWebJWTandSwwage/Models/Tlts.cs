using PrjWebJWTandSwwage.Base;
using PrjWebJWTandSwwage.Common.Helpers;
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
    /// 部门
    /// </summary>
    [Serializable, Description("部门")]
    public class Tlts : VersionInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public Tlts()
        {
            this.ID = 0;
            this.CompanyID = 0;
            this.Name = null;
            this.Describe = null;
            this.Stt = 1;
            this.Brf = null;
            this.Uptr = null;
        }
        #endregion

        #region 映射属性
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 公司编码
        /// </summary>
        public int CompanyID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
    
        public decimal Stt { get; set; }
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
        /// <summary>
        /// 省略部门名称
        /// </summary>
        public string EName {
            get {
                return Name.GetEllipsis(18,"...");
            }
        }
        #endregion
    }
}
