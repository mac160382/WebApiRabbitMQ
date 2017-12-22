using System.Collections.Generic;

using CallCenterModel;

using Microsoft.AspNetCore.Mvc;

using Repository;

namespace CallCenterAggregateApi.Controllers
{
    [Route("api/[controller]")]
    public class AggregateController : Controller
    {
        private readonly ITaskAggregateRepository repository;

        public AggregateController(ITaskAggregateRepository repository) { this.repository = repository; }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }

        // GET api/values
        [HttpGet]
        public IEnumerable<TaskAggregate> Get()
        {
            //return new string[] { "value1", "value2" };
            return repository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) { return "value"; }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }
    }
}