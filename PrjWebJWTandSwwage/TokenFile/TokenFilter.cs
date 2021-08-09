using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PrjWebJWTandSwwage.JWT;
using PrjWebJWTandSwwage.Models;

namespace PrjWebJWTandSwwage.TokenFile
{
    public class TokenFilter : Attribute, IActionFilter
    {
        private readonly ITokenHelper _tokenHelper = null;
        public TokenFilter(ITokenHelper tokenHelper)
        {
            this._tokenHelper = tokenHelper;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
         
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            JsonResultModels ret = new JsonResultModels();
            //获取token
            object tokenobj = context.ActionArguments["token"];

            if (tokenobj == null)
            {
                ret.Code = 201;
                ret.Msg = "token不能为空";
                context.Result = new JsonResult(ret);
                return;
            }

            string token = tokenobj.ToString();

            string userId = "";
            //验证jwt,同时取出来jwt里边的用户ID
            TnToken.TokenType tokenType = _tokenHelper.ValiTokenState(token, a => a["iss"] == "GZ" && a["aud"] == "EveryTestOne", action => { userId = action["UserName"]; });
            if (tokenType == TnToken.TokenType.Fail)
            {
                ret.Code = 202;
                ret.Msg = "token验证失败";
                context.Result = new JsonResult(ret);
                return;
            }
            if (tokenType == TnToken.TokenType.Expired)
            {
                ret.Code = 205;
                ret.Msg = "token已经过期";
                context.Result = new JsonResult(ret);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                //给控制器传递参数(需要什么参数其实可以做成可以配置的，在过滤器里边加字段即可)
                //context.ActionArguments.Add("userId", userId);
            }
        }
    }
}
