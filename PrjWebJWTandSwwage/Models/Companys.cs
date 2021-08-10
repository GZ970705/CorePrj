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
    /// 公司
    /// </summary>
    [Serializable, Description("公司")]
    public  class Companys : VersionInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public Companys()
        {
            this.ID = 0;
            this.IdentNo = null;
            this.Name = null;
            this.CreCode = null;
            this.Lnkr = null;
            this.Adr = null;
            this.Tel = null;
            this.Phone = null;
            this.OpnBnk = null;
            this.ActNo = null;
            this.Stt = 1;
            this.Admin = null;
            this.AdminTel = null;
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
        /// 公司10位编码
        /// </summary>

        public string IdentNo { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 公司信用编码
        /// </summary>

        public string CreCode { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>

        public string Lnkr { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>

        public string Adr { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>

        public string Tel { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>

        public string Phone { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>

        public string OpnBnk { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>

        public string ActNo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public decimal Stt { get; set; }
        /// <summary>
        /// 系统管理员
        /// </summary>

        public string Admin { get; set; }
        /// <summary>
        /// 系统管理员电话
        /// </summary>

        public string AdminTel { get; set; }
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
        /// 更新人名称
        /// </summary>
        public string UptrName { get; set; }
        #endregion

        #region 其他属性
        #endregion
    }
}
