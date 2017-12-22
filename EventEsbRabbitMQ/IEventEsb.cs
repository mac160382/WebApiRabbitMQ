namespace EventEsbRabbitMQ
{
    public interface IEventEsb
    {
        void Publish(string message);

        void Subscribe();
    }
}