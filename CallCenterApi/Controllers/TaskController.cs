using CallCenterModel;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Collections.Generic;

using EventEsbRabbitMQ;

using Newtonsoft.Json;

namespace CallCenterApi.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ICallTaskRepository callTaskRepository;

        private readonly IEventEsb eventEsbRabbitMq;

        public TaskController(ICallTaskRepository callTaskRepository, IEventEsb eventEsb)
        {
            this.callTaskRepository = callTaskRepository;
            this.eventEsbRabbitMq = eventEsb;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(Task task)
        {
            this.callTaskRepository.Add(task);
            /*Mandar mensaje a RabittMQ de creación*/
            var message = JsonConvert.SerializeObject(task);
            this.eventEsbRabbitMq.Publish(message);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, Task task)
        {
            this.callTaskRepository.Update(task);
            /*Mandar mensaje a RabittMQ de Actalizacion*/
            var message = JsonConvert.SerializeObject(task);
            this.eventEsbRabbitMq.Publish(message);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
