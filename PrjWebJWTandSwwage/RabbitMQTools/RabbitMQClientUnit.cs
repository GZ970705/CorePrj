using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.RabbitMQTools
{
    /// <summary>
    /// MQ连接对象
    /// </summary>
    public class RabbitMQClientUnit
    {
        private readonly IConfiguration _configuration;
        public RabbitMQClientUnit(IConfiguration configuration)
        {
            _configuration = configuration;

            ConnectionFactory factory = new ConnectionFactory
            {

                UserName = _configuration.GetConnectionString("MessageMQUserName"),//用户账号
                Password = _configuration.GetConnectionString("MessageMQPassword"),//用户密码
                HostName = _configuration.GetConnectionString("MessageMQHostName"), //IP地址

            };
            Connection = factory.CreateConnection();
            QueueName = _configuration.GetConnectionString("QueueName");//消息队列名称
            Channel = Connection.CreateModel();//连接会话对象
        }
        public IConnection Connection { get; }
        public IModel Channel { get; }
        public string QueueName { get; }
    }
}
