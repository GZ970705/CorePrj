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
    /// 系统授权
    /// </summary>
    [Serializable, Description("系统授权")]
    public class SysAuz: VersionInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public SysAuz()
        {
            this.ID =0;
            this.SysPlatformID = null;
            this.CompanyID = 0;
            this.Sdata = null;
            this.Edata = null;
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
        /// 系统编号
        /// </summary>

        public string SysPlatformID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>

        public int CompanyID { get; set; }
        /// <summary>
        /// 有效开始时间
        /// </summary>

        public string Sdata { get; set; }
        /// <summary>
        /// 有效结束时间
        /// </summary>

        public string Edata { get; set; }
        /// <summary>
        /// 描述
        /// </summary>

        public string Describe { get; set; }
        /// <summary>
        /// 状态
        /// </summary>

        public decimal Stt { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Uptr { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Brf { get; set; }
        #endregion

        #region 关联属性
        /// <summary>
        /// 更新人名称
        /// </summary>
        public string UptrName { get; set; }
        /// <summary>
        /// 平台系统名称
        /// </summary>
        public string SysPlatformName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        #endregion
    }
}
