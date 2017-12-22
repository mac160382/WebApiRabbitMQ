using System;
using System.Collections.Generic;
using System.Text;

namespace EventEsbRabbitMQ
{
    public interface IEventEsb
    {
        void Publish(string message);

        void Subscribe();
    }
}
