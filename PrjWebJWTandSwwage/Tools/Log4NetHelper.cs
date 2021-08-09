using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PrjWebJWTandSwwage.Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Tools
{
    public class Log4NetHelper: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                Log4NetUtil.LogError("全局异常", context.Exception);
                if (context.HttpContext.Request.IsHttps)
                {
                    context.Result = new JsonResult(context.Exception.Message);
                }
                else
                {
                    context.Result = new RedirectToActionResult("Error", "Home", new { });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            context.ExceptionHandled = true; // 注意：如果不添加这句代码，程序不会自动断路，会继续向下进行。
        }
    }
}
