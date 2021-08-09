using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.RabbitMQTools
{
    /// <summary>
    /// 发送端
    /// </summary>
    public class MessageRepository
    {
        private readonly RabbitMQClientUnit _rabbitMQClientIUnit;
        private readonly IConfiguration _configuration;

        public MessageRepository(RabbitMQClientUnit rabbitMQClientIUnit, IConfiguration configuration)
        {
            _rabbitMQClientIUnit = rabbitMQClientIUnit;
            _configuration = configuration;

        }
        /// <summary>
        /// MQ下发消息
        /// </summary>
        /// <param name="encryption"></param>
        public void RabbitMQPush(string encryption)
        {
            try
            {
                _rabbitMQClientIUnit.Channel.QueueDeclare(_rabbitMQClientIUnit.QueueName, false, false, false, null);
                var sendBytes = Encoding.UTF8.GetBytes(encryption);
                //发布消息
                _rabbitMQClientIUnit.Channel.BasicPublish("", _configuration.GetConnectionString("QueueName"), null, sendBytes);
                _rabbitMQClientIUnit.Channel.Close();
            }
            catch
            {
                throw new ArgumentException("出现异常MQ推送失败");
            }
        }
    }
}
