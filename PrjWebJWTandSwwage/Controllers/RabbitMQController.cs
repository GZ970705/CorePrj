using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjWebJWTandSwwage.Models;
using PrjWebJWTandSwwage.RabbitMQTools;
using PrjWebJWTandSwwage.TokenFile;

namespace PrjWebJWTandSwwage.Controllers
{
    [EnableCors("cors")]
    [ServiceFilter(typeof(TokenFilter))]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {
        private readonly MessageRepository _message;
        private readonly RabbitListener _rabbit;
        public RabbitMQController(MessageRepository message, RabbitListener rabbit)
        {

            this._message = message;
            this._rabbit = rabbit;
        }
        // <summary>
        // MQ发送消息
        // </summary>
        // <param name = "testmq" >消息</ param >
       // < returns ></ returns >
        [HttpPost]
        public IActionResult SpendMessa(string testmq)
        {
            var json = new JsonResultModels();
            try
            {
                _message.RabbitMQPush(testmq);
                json.Code = 200;
                json.Msg = "成功";
                json.TnToken = null;
            }
            catch (Exception ex)
            {

                json.Code = 400;
                json.Msg = "失败:" + ex.Message;
            }

            return Ok(json);
        }
        /// <summary>
        /// 得到消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMessa()
        {
            _rabbit.Register();
            return Ok("成功");
        }
    }
}
