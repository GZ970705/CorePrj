using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PrjWebJWTandSwwage.BLL;
using PrjWebJWTandSwwage.JWT;
using PrjWebJWTandSwwage.Log4net;
using PrjWebJWTandSwwage.Models;
using PrjWebJWTandSwwage.SqlTools;

namespace PrjWebJWTandSwwage
{
    [EnableCors("cors")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ITokenHelper _tokenHelper = null;


        public LoginController(ITokenHelper tokenHelper)
        {
            this._tokenHelper = tokenHelper;

        }

        

        /// <summary>
        /// 登录(获取token)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserInfoModels user)
        {
            var json = new JsonResultModels();
            try
            {
                if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
                {
                    json.Code = 201;
                    json.Msg = "用户名密码不能为空";
                    return Ok(json);
                }
                else
                {
                    var UserDate = new UserBLL().CheckUser(user.UserName, user.Password);
                    if (UserDate == null)
                    {
                        json.Code = 400;
                        json.Msg = "登录失败:用户不存在!";
                    }
                    else
                    {
                        Dictionary<string, string> keyValues = new Dictionary<string, string>{
                            {"UserName", UserDate.UserName}

                        };
                        json.Code = 200;
                        json.Msg = "登录成功";
                        json.TnToken = _tokenHelper.CreateToken(keyValues);
                    }
                   
                }

            }
            catch (Exception ex)
            {
                json.Code = 400;
                json.Msg = "登录失败:" + ex.Message;
                Log4NetUtil.LogError(json.Msg, ex);
            }
            Log4NetUtil.LogInfo(json.Msg);
            return Ok(json);
        }
       

    }
}
