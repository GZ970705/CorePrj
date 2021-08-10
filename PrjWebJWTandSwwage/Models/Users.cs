
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
    /// 用户
    /// </summary>
    [Serializable, Description("用户")]
    public class Users: VersionInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public Users()
        {
            this.ID = 0;
            this.CompanyID = 0;
            this.LgnName = null;
            this.Name = null;
            this.Tlt = null;
            this.Sex = "M";
            this.Brth = DateTime.Now.Date;
            this.Adr = null;
            this.Mobile = null;
            this.Email = null;
            this.VID = null;
            this.Type = "0";
            this.Pwd = null;
            this.SysFlg = "F";
            this.OnDtt = null;
            this.PwdDtt = null;
            this.Stt = 1;
            this.Photo = null;
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
        /// 公司编号
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int CompanyID { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>

        public string LgnName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 部门
        /// </summary>

        public decimal? Tlt { get; set; }
        /// <summary>
        /// 性别
        /// </summary>

        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>

        public DateTime Brth { get; set; }
        /// <summary>
        /// 住址
        /// </summary>

        public string Adr { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        
        public string Mobile { get; set; }
        /// <summary>
        /// E_mail
        /// </summary>
        
        public string Email { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        
        public string VID { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 用户登录标识
        /// </summary>
        public string SysFlg { get; set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime? OnDtt { get; set; }
        /// <summary>
        /// 最近密码修改时间
        /// </summary>
        public DateTime? PwdDtt { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public decimal Stt { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
       
        public string Photo { get; set; }
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
        /// 公司状态
        /// </summary>
        public int CompanyStt { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string TltName { get; set; }
        /// <summary>
        /// 部门状态
        /// </summary>
        public int TltStt { get; set; }
        /// <summary>
        /// 更新人名称
        /// </summary>
        public string UptrName { get; set; }
        #endregion

        #region 其他属性
        #endregion
    }
}
