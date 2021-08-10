using PrjWebJWTandSwwage.Base;
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
    /// 角色表
    /// </summary>
    [Serializable, Description("角色表")]
    public class Roles : VersionInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public Roles()
        {
            this.ID = 0;
            this.CompanyID = 0;
            this.Name = null;
            this.Type = null;
            this.Brf = null;
            this.Stt = 1;
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
        /// 公司编号
        /// </summary>
        public int CompanyID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 角色类型
        /// </summary>

        public string Type { get; set; }
        /// <summary>
        /// 备注
        /// </summary>

        public string Brf { get; set; }
        /// <summary>
        /// 状态
        /// </summary>

        public decimal Stt { get; set; }
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
