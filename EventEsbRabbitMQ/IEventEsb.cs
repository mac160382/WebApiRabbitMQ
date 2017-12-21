using System;
using System.Collections.Generic;
using System.Text;

namespace EventEsbRabbitMQ
{
    public interface IEventEsb
    {
        void Publish();

        void Suscribe();
    }
}
