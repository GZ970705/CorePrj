using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjWebJWTandSwwage.Models;
using PrjWebJWTandSwwage.RabbitMQTools;

namespace PrjWebJWTandSwwage.Controllers
{
    [Route("api/[controller]")]
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
        /// <summary>
        /// MQ发送消息
        /// </summary>
        /// <param name="testmq"></param>
        /// <returns></returns>
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

        public ActionResult GetMessa()
        {
            _rabbit.Register();
            return Ok("成功");
        }
    }
}
