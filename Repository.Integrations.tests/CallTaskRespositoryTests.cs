using CallCenterModel;
using System;
using Xunit;

namespace Repository.Integrations.tests
{
    public class CallTaskRespositoryTests
    {
        private Guid correlationId;

        public CallTaskRespositoryTests()
        {
            this.correlationId = new Guid("8ca7a625-932f-4fda-b752-a46205094cba");
        }

        [Fact]
        public void when_add_new_call_task_then_is_created_in_repository()
        {
            ICallTaskRepository repository = new Repository.CallTaskRepository();
            var task = new Task();
            task._id = correlationId;
            task.Date = new DateTime(2017, 12, 01);
            task.BeginTime = new TimeSpan(9, 0, 0);            
            task.User = "Pedro Paramo";
            task.CorrelationId = correlationId;

            repository.Add(task);
        }

        [Fact]
        public void when_update_call_task_then_update_enddate_in_repository()
        {
            ICallTaskRepository repository = new Repository.CallTaskRepository();
            var task = new Task();
            task._id = correlationId;
            task.Date = new DateTime(2017, 12, 01);
            task.BeginTime = new TimeSpan(9, 0, 0);
            task.User = "Pedro Paramo";
            task.CorrelationId = correlationId;
            task.EndTime = new TimeSpan(9, 3, 0);

            var taskUpdate = repository.Update(task);            
        }
    }
}
