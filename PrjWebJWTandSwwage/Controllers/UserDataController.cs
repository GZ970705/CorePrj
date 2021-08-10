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
using PrjWebJWTandSwwage.Common;
using PrjWebJWTandSwwage.IBLL;
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
        private readonly IUserModelBLL _userBLL;

        public UserDataController(ITokenHelper tokenHelper, IUserModelBLL userBLL)
        {
            this._tokenHelper = tokenHelper;
            this._userBLL = userBLL;
        }
        /// <summary>
        /// 通过id获取数据
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
                json.Data = _userBLL.GetUserDate(user.id.ToString());
                json.Msg = "获取数据成功";
            }
            else
            {
                json.Data = null;
                json.Msg = "获取数据失败,token验证失败!";
            }
            json.TnToken = null;
            return Ok(json);
        }
        /// <summary>
        /// 通过id更新数据(根据主键更新数据,存在则更新,不存在则添加)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update([FromBody] UserInfoModels user, string token) {

            var json = new JsonResultModels();
            if (_tokenHelper.ValiToken(token))
            {
                //ValiReturnInfo vali = SaveValidate(UsersInfo, "登陆名");
                if (_userBLL.UpdateUser(user))
                {
                    json.Msg = "更新数据成功";
                }
                else
                {
                    json.Msg = "更新数据失败,请重新填写信息!";
                }
            }
            else
            {
                json.Msg = "更新数据失败,token验证失败!";
            }
            json.Data = null;
            json.TnToken = null;
            return Ok(json);
        }
    }
}
