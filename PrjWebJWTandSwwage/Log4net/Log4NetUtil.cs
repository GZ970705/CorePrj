using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Log4net
{
    public class Log4NetUtil
    {
        private static readonly ILog ErrorLog = LogManager.GetLogger(Log4NetConfig.RepositoryName, "Error");

        private static readonly ILog InfoLog = LogManager.GetLogger(Log4NetConfig.RepositoryName, "Info");

        /// <summary>
        /// 全局异常错误记录持久化
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        public static void LogError(string throwMsg, Exception ex)
        {
            var errorMsg =
                $"【抛出信息】：{throwMsg} \r\n【异常类型】：{ex.GetType().Name} \r\n【异常信息】：{ex.Message} \r\n【堆栈调用】：\r\n{ex.StackTrace}";
            ErrorLog.Error(errorMsg);
        }

        public static void LogInfo(string msg)
        {
            InfoLog.Info(msg);
        }
    }
}
