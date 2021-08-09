using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.RabbitMQTools
{
    /// <summary>
    /// 接收端
    /// </summary>
    public class RabbitListener: IHostedService
    {
        private readonly RabbitMQClientUnit _rabbitMQClientIUnit;
        private readonly MessageRepository _messageRepository;
        private readonly IConfiguration _configuration;
        public RabbitListener(RabbitMQClientUnit RabbitMQClientIUnit, MessageRepository messageRepository, IConfiguration configuration)
        {
            _rabbitMQClientIUnit = RabbitMQClientIUnit;
            _messageRepository = messageRepository;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            return Task.CompletedTask;
        }


        // 处理消息的方法
        public virtual bool Process(string message)
        {
            throw new NotImplementedException();
        }

        // 注册消费者监听在这里
        public void Register()
        {

            EventingBasicConsumer consumer = new EventingBasicConsumer(_rabbitMQClientIUnit.Channel);
            //接收到消息事件
            consumer.Received += (ch, ea) =>
            {
                //切记在.net core 3.1中无法直接使用ea.Body 
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var result = Process(message);
                if (result)
                {
                    _rabbitMQClientIUnit.Channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            _rabbitMQClientIUnit.Channel.BasicConsume(_configuration.GetConnectionString("QueueName"), false, consumer);
        }

        public void DeRegister()
        {
            _rabbitMQClientIUnit.Connection.Close();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _rabbitMQClientIUnit.Connection.Close();
            return Task.CompletedTask;
        }
    }
}
