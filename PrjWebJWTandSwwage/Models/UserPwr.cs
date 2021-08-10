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
    /// 用户权限表
    /// </summary>
    [Serializable, Description("用户权限表")]
    public class UserPwr : BaseInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public UserPwr()
        {
            this.UserID = 0;
            this.CompanyID = 0;
            this.MdlID = 0;
            this.PwrIDs = null;
            this.Brf = null;
            this.Uptr = null;
        }
        #endregion

        #region 映射属性
        /// <summary>
        /// 用户
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int UserID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int CompanyID { get; set; }
        /// <summary>
        /// 模块编号
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int MdlID { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        
        public string PwrIDs { get; set; }
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
        /// 系统标识码
        /// </summary>
        public string SysGuValue { get; set; }
        /// <summary>
        /// 模块标识码
        /// </summary>
        public string MdlGuValue { get; set; }
        /// <summary>
        /// 模块所有权限
        /// </summary>
        public string MdlFuncs { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string MdlName { get; set; }
        /// <summary>
        /// 更新人名称
        /// </summary>
        public string UptrName { get; set; }
        #endregion

        #region 其他属性
        #endregion
    }
}
