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
    /// 模块表
    /// </summary>
    [Serializable, Description("模块表")]
    public class Mdls : VersionInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public Mdls()
        {
            this.ID = 0;
            this.SysPlatformID = null;
            this.Name = null;
            this.TmPath = null;
            this.Describe = null;
            this.MdlType = null;
            this.Icon = null;
            this.PrtID = null;
            this.Lvl = 0;
            this.EndFlg = null;
            this.Seq = 0;
            this.Stt = 0;
            this.Funcs = null;
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
        /// 所属系统ID
        /// </summary>
        public string SysPlatformID { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 唯一标识值
        /// </summary>
        public string GuValue { get; set; }
        /// <summary>
        /// 模块路径
        /// </summary>
        public string TmPath { get; set; }
        /// <summary>
        /// 模块描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 模块类型
        /// </summary>
        public string MdlType { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 上级
        /// </summary>
        public string PrtID { get; set; }
        /// <summary>
        /// 级次
        /// </summary>
        public decimal Lvl { get; set; }
        /// <summary>
        /// 末级
        /// </summary>
        public string EndFlg { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public decimal Seq { get; set; }
        /// 状态
        /// </summary>
        public decimal Stt { get; set; }
        /// <summary>
        /// 功能
        /// </summary>
        public string Funcs { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Uptr { get; set; }
        #endregion

        #region 关联属性
        /// <summary>
        /// 平台系统标识值
        /// </summary>
        public string SysGuValue { get; set; }
        /// <summary>
        /// 平台系统名称
        /// </summary>
        public string SysPlatformName { get; set; }
        /// <summary>
        /// 更新人名称
        /// </summary>
        public string UptrName { get; set; }
        #endregion

        #region 其他属性
        public string SID { get { return ID.ToString(); } set { } }
        #endregion
    }
}
