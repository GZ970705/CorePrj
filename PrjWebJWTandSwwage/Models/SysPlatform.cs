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
    /// 平台产品系统表
    /// </summary>
    [Serializable, Description("平台系统表")]
    public class SysPlatform : VersionInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public SysPlatform()
        {
            this.ID = null;
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
        public string ID { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 唯一标识值
        /// </summary>

        public string GuValue { get; set; }
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
        /// F 为未授权
        /// </summary>
        public string isAut { get; set; }
        #endregion

        #region 其他属性
        #endregion
    }
}
