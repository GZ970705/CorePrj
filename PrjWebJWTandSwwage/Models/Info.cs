﻿using PrjWebJWTandSwwage.Base;
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
    /// 信息
    /// </summary>
    [Serializable, Description("信息")]
    public class Info: VersionInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public Info()
        {
            this.ID = "";
            this.InfoTypeID = "";
            this.ZName = null;
            this.EName = null;
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
        /// 类型编号
        /// </summary>

        public string InfoTypeID { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>

        public string ZName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>

        public string EName { get; set; }
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
        /// 更新人名称
        /// </summary>
        public string UptrName { get; set; }
        #endregion
    }
}
