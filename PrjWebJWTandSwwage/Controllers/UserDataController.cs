using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrjWebJWTandSwwage.BLL;
using PrjWebJWTandSwwage.JWT;
using PrjWebJWTandSwwage.Models;
using PrjWebJWTandSwwage.SqlTools;
using PrjWebJWTandSwwage.TokenFile;

namespace PrjWebJWTandSwwage.Controllers
{
    [EnableCors("cors")]
    [ServiceFilter(typeof(TokenFilter))]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly ITokenHelper _tokenHelper = null;

        public UserDataController(ITokenHelper tokenHelper)
        {
            this._tokenHelper = tokenHelper;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetDate([FromBody] UserInfoModels user, string token)
        {
            var json = new JsonResultModels();
            if (_tokenHelper.ValiToken(token))
            {

                json.Data = new UserBLL().GetUserDate(user.id.ToString());
                json.Msg = "访问成功";
                json.TnToken = null;
            }
            else
            {
                json.Data = null;
                json.Msg = "访问失败,token验证失败!";
                json.TnToken = null;
            }

            return Ok(json);
        }
    }
}
