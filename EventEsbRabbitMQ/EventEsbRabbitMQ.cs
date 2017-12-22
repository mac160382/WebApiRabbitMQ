using System;
using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using RegisterHandler;

namespace EventEsbRabbitMQ
{
    public class EventEsbRabbitMq : IEventEsb, IDisposable
    {
        private readonly IModel channel;
        private readonly IConnection connection;

        private readonly TaskRegisterHandler taskRegisterHandler;

        private EventingBasicConsumer consumer;

        public EventEsbRabbitMq()
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            QueueDeclare();
            taskRegisterHandler = new TaskRegisterHandler();
        }

        public void Dispose()
        {
            connection?.Dispose();
            channel?.Dispose();
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

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("",
                                 "hello",
                                 null,
                                 body);
        }

        public void Subscribe()
        {
            EventConsumer();
            BasicConsume();
        }

        private void BasicConsume()
        {
            channel.BasicConsume("hello",
                                 true,
                                 consumer);
        }

        private void QueueDeclare()
        {
            channel.QueueDeclare("hello",
                                 false,
                                 false,
                                 false,
                                 null);
        }
    }
}