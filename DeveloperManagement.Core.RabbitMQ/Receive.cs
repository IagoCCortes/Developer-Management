using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DeveloperManagement.Core.RabbitMQ
{
    public static class Receive
    {
        public static void Execute(string queue)
        {
            var factory = new ConnectionFactory() {Uri = new Uri("amqp://guest:guest@192.168.99.100:5672")};
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queue, true, consumer);
        }
    }
}