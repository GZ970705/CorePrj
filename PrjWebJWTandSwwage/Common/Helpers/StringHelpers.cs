using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Common.Helpers
{
    /// <summary>
    /// 字符串辅助类
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// 界面显示文字过长,显示省略符
        /// </summary>
        /// <param name="str">界面显示文字</param>
        /// <param name="sum">截取长度</param>
        /// <param name="Ellipsis">省略符</param>
        /// <returns></returns>
        public static string GetEllipsis(this string str,int sum,string Ellipsis) 
        {
            string txt = str;
            if (str.Length > sum)
                txt = str.Substring(0, sum) + Ellipsis;
            return txt;
        }
    }
}
