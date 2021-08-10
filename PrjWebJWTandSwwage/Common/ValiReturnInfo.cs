using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Common
{
    /// <summary>
    /// 校验返回信息
    /// </summary>
    [Serializable]
    public class ValiReturnInfo
    {
        #region 构造
        /// <summary>
        /// 初始化实例
        /// </summary>
        public ValiReturnInfo(bool isVali)
        {
            this.isVali = isVali;
        }
        /// <summary>
        /// 初始化实例
        /// </summary>
        public ValiReturnInfo(bool isVali, string returnInfo)
            : this(isVali)
        {
            this.returnInfo = returnInfo;
        }
        #endregion

        #region 公共属性
        private bool isVali = true;
        /// <summary>
        /// 是否校验通过
        /// </summary>
        public bool IsValidated
        {
            get { return isVali; }
            set { isVali = value; }
        }
        private string returnInfo = null;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ReturnInfo
        {
            get { return returnInfo; }
            set { returnInfo = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 校验成功
        /// </summary>
        /// <param name="returnInfo"></param>
        /// <returns></returns>
        public static ValiReturnInfo Success(string returnInfo = null)
        {
            return new ValiReturnInfo(true, returnInfo);
        }
        /// <summary>
        /// 校验失败
        /// </summary>
        /// <param name="returnInfo"></param>
        /// <returns></returns>
        public static ValiReturnInfo Failed(string returnInfo = null)
        {
            return new ValiReturnInfo(false, returnInfo);
        }
        #endregion
    }
}
