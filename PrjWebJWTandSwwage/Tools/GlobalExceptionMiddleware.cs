using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PrjWebJWTandSwwage.Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Tools
{
    public class GlobalExceptionMiddleware
    {
        public readonly RequestDelegate Next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception e)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status200OK;

                Log4NetUtil.LogError("全局异常", e);

                if (context.Request.IsHttps)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { Code = 500, Message = e.Message }));
                }
                else
                {
                    context.Response.Redirect("/Home/Error");
                }
            }
        }
    }
}
