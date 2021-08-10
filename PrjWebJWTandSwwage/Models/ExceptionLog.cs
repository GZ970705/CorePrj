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
    /// 异常日志表
    /// </summary>
    [Serializable, Description("异常日志表")]
    public class ExceptionLog : BaseInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public ExceptionLog()
        {
            this.ID = 0;
            this.MethodName = null;
            this.ClassName = null;
            this.Message = null;
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
        /// 报错方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 报错类名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }
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
