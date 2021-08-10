using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjWebJWTandSwwage.Models;
using PrjWebJWTandSwwage.Tools;

namespace PrjWebJWTandSwwage.Controllers
{
    [EnableCors("cors")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        /// <summary>
        /// redis存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Setredis(string key,string value)
        {
            var json = new JsonResultModels();
            try
            {
                if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                {
                    json.Code = 201;
                    json.Msg = "键值不能为空";
                    return Ok(json);
                }
                else
                {

                    var a = RedisHelperNetCore.Instance.StringSet(key, value);
                    json.Code = 200;
                    json.Msg = "存储成功";
                }

            }
            catch (Exception ex)
            {
                json.Code = 400;
                json.Msg = "失败:" + ex.Message;
            }

            return Ok(json);
        }
        /// <summary>
        /// 存储用户信息
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="users">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SetUserRedis(string key,[FromBody] Users users) {
            var json = new JsonResultModels();
            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    json.Code = 201;
                    json.Msg = "键不能为空";
                    return Ok(json);
                }
                else
                {

                    var a = RedisHelperNetCore.Instance.SetStringKey<Users>(key, users);
                    json.Code = 200;
                    json.Msg = "存储成功";
                }

            }
            catch (Exception ex)
            {
                json.Code = 400;
                json.Msg = "失败:" + ex.Message;
            }
            return Ok(json);

        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]

        public IActionResult GetUserRedis(string key) {
            var json = new JsonResultModels();

            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    json.Code = 201;
                    json.Msg = "键不能为空";
                    return Ok(json);
                }
                else
                {
                    if (RedisHelperNetCore.Instance.KeyExists(key))
                    {
                        var a = RedisHelperNetCore.Instance.GetStringKey<Users>(key);
                        json.Data = a;
                        json.Code = 200;
                        json.Msg = "取值成功";
                    }
                    else
                    {
                        json.Code = 202;
                        json.Msg = "该key值没存储!";
                    }

                }

            }
            catch (Exception ex)
            {
                json.Code = 400;
                json.Msg = "失败:" + ex.Message;
            }
            return Ok(json);

        }
        /// <summary>
        /// redis取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Getredis(string key)
        {
            var json = new JsonResultModels();
            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    json.Code = 201;
                    json.Msg = "键不能为空";
                    return Ok(json);
                }
                else
                {
                    if (RedisHelperNetCore.Instance.KeyExists(key))
                    {
                        var a = RedisHelperNetCore.Instance.StringGet(key);
                        json.Data = a;
                        json.Code = 200;
                        json.Msg = "取值成功";
                    }
                    else
                    {
                        json.Code = 202;
                        json.Msg = "该key值没存储!";
                    }
                    
                }

            }
            catch (Exception ex)
            {
                json.Code = 400;
                json.Msg = "失败:" + ex.Message;
            }

            return Ok(json);
        }
    }
}
