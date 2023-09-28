using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Sinacor.Domain;
using System.Text.Json;
using System.Text;
using Sinacor.Interfaces;
using RabbitMQ.Client.Events;

namespace Sinacor.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMQConfig _config;
        private readonly ILogger _logger;

        public RabbitMQService(IOptions<RabbitMQConfig> config, ILogger logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public void SendMessageToQueue<T>(T model)
        {
            using (var connection = CreateConnection())
            using (var channel = connection.CreateModel())
            {
                DeclareQueue(channel);

                string messageTarefa = JsonSerializer.Serialize(model);
                var body = Encoding.UTF8.GetBytes(messageTarefa);

                PublishToQueue(channel, body);
            }
        }

        private IConnection CreateConnection()
        {
            var factory = new ConnectionFactory { HostName = _config.HostName };
            return factory.CreateConnection();
        }

        private void DeclareQueue(IModel channel)
        {
            channel.QueueDeclare(queue: _config.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        private void PublishToQueue(IModel channel, byte[] body)
        {
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: _config.QueueName,
                                 basicProperties: null,
                                 body: body);
        }

        private void ConsumeFromQueue(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var messageTarefa = Encoding.UTF8.GetString(ea.Body.ToArray());
                var tarefa = JsonSerializer.Deserialize<Tarefa>(messageTarefa);               
            };
            channel.BasicConsume(queue: _config.QueueName,
                                autoAck: true,
                                consumer: consumer);
        }

    }
}
