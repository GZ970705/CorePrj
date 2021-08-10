
using PrjWebJWTandSwwage.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Common
{

    /// <summary>
    /// Sign签名工具类
    /// 提供功能【1：验证Sign签名是否正确
    /// 2.获取Sign签名】
    /// </summary>
    public class SignTool
    {
        /// <summary>
        /// 验证Sign签名是否正确
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="param">签名参数</param>
        /// <returns>返回是否正确[true:正确;false:失败]</returns>
        public static bool IsSign(string sign, params object[] param)
        {
            //若签名为空，拒绝访问
            if (string.IsNullOrEmpty(sign))
                return false;
            //获取Sign签名
            string signStr = GetSign(param);
            //验证签名是否正确
            if (sign.ToUpper().Equals(signStr.ToUpper()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取Sign签名
        /// </summary>
        /// <param name="param">签名参数</param>
        /// <returns>返回签名</returns>
        public static string GetSign(params object[] param)
        {
            //组织参数签名串
            string signStr = "";
            if (param != null && param.Length > 0)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    signStr += param[i].ToString();
                }
            }
            //计算签名
            signStr = EncryptHelper.EncryptByMD5(signStr, true, 3);
            return signStr;
        }
    }
}
