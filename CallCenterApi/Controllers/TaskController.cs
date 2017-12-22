using System.Collections.Generic;

using CallCenterModel;

using EventEsbRabbitMQ;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Repository;

namespace TaskApi.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ICallTaskRepository callTaskRepository;

        private readonly IEventEsb eventEsbRabbitMq;

        public TaskController(ICallTaskRepository callTaskRepository, IEventEsb eventEsb)
        {
            this.callTaskRepository = callTaskRepository;
            eventEsbRabbitMq = eventEsb;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get() { return new[] {"value1", "value2"}; }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) { return "value"; }

        // POST api/values
        [HttpPost]
        public void Post(Task task)
        {
            callTaskRepository.Add(task);
            /*Mandar mensaje a RabittMQ de creación*/
            var message = JsonConvert.SerializeObject(task);
            eventEsbRabbitMq.Publish(message);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, Task task)
        {
            callTaskRepository.Update(task);
            /*Mandar mensaje a RabittMQ de Actalizacion*/
            var message = JsonConvert.SerializeObject(task);
            eventEsbRabbitMq.Publish(message);
        }
    }
}