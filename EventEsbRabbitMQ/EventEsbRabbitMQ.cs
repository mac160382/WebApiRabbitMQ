using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

using System.Text;

using RegisterHandler;

namespace EventEsbRabbitMQ
{
    public class EventEsbRabbitMq : IEventEsb, IDisposable
    {
        private readonly IConnection connection;

        private readonly IModel channel;

        private EventingBasicConsumer consumer;

        private readonly TaskRegisterHandler taskRegisterHandler;

        public EventEsbRabbitMq()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            QueueDeclare();
            taskRegisterHandler = new TaskRegisterHandler();
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
        }

        public void Subscribe()
        {
            EventConsumer();
            BasicConsume();
        }

        private void QueueDeclare()
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        private void BasicConsume()
        {
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void EventConsumer()
        {
             consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);

                taskRegisterHandler.TaskRegisterHandlerInvoke(message);
            };
        }

        public void Dispose()
        {
            connection?.Dispose();
            channel?.Dispose();
        }
    }
}
