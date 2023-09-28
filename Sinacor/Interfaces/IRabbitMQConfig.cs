using RabbitMQ.Client;
using Sinacor.Domain;

namespace Sinacor.Interfaces
{
    public interface IRabbitMQService
    {
        void SendMessageToQueue<T>(T model);
    }
}
