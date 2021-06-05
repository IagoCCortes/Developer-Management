using System;
using System.Text;
using RabbitMQ.Client;

namespace DeveloperManagement.Core.RabbitMQ
{
    public static class Send
    {
        public static void Execute(string message, string queue)
        {
            var factory = new ConnectionFactory() {Uri = new Uri("amqp://guest:guest@192.168.99.100:5672")};
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);
            
            channel.BasicPublish(exchange: "", routingKey: queue, properties, body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}