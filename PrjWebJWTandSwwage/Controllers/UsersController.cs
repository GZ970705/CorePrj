using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PrjWebJWTandSwwage.BLL;
using PrjWebJWTandSwwage.IBLL;
using PrjWebJWTandSwwage.JWT;
using PrjWebJWTandSwwage.Log4net;
using PrjWebJWTandSwwage.Models;
using PrjWebJWTandSwwage.SqlTools;
using PrjWebJWTandSwwage.TokenFile;

namespace PrjWebJWTandSwwage
{
    [EnableCors("cors")]
    [ServiceFilter(typeof(TokenFilter))]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ITokenHelper _tokenHelper = null;
        private readonly IUserBLL _usersBLL;
        //private readonly ITltsBLL _tltsBLL;
        public UsersController(ITokenHelper tokenHelper, IUserBLL usersBLL)
        {
            this._tokenHelper = tokenHelper;
            this._usersBLL = usersBLL;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetUsers([FromBody] Users user, string token)
        {
            var json = new JsonResultModels();
            if (_tokenHelper.ValiToken(token))
            {
                json.Code = 200;
                json.Data = _usersBLL.GetUsr(user);
                json.Count = _usersBLL.GetUserCount(user);
                json.Msg = "获取数据成功";
            }
            else
            {
                json.Code = 300;
                json.Data = null;
                json.Msg = "获取数据失败,token验证失败!";
            }
            json.TnToken = null;
            return Ok(json);
        }
        /// <summary>
        /// 根据主键更新数据(存在修改,不存在添加)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateUser([FromBody] List<Users> user, string token)
        {

            var json = new JsonResultModels();
            if (_tokenHelper.ValiToken(token))
            {
                if (_usersBLL.UpdateUsr(user))
                {
                    json.Code = 200;
                    json.Msg = "更新数据成功";
                }
                else
                {
                    json.Code = 400;
                    json.Msg = "更新数据失败,请重新填写信息!";
                }
            }
            else
            {
                json.Code = 300;
                json.Msg = "更新数据失败,token验证失败!";
            }
            json.Data = null;
            json.TnToken = null;
            return Ok(json);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteUser([FromBody] Users user, string token) {

            var json = new JsonResultModels();
            if (_tokenHelper.ValiToken(token))
            {
                if (_usersBLL.DeleteUsr(user))
                {
                    json.Code = 200;
                    json.Msg = "数据删除成功";
                }
                else
                {
                    json.Code = 400;
                    json.Msg = "删除数据失败,请检查是否操作有误!";
                }
            }
            else
            {
                json.Code = 300;
                json.Msg = "删除数据失败,token验证失败!";
            }
            json.Data = null;
            json.TnToken = null;
            return Ok(json);
        }
    }
}
