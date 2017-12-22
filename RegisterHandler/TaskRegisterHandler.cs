using System;

using CallCenterModel;

using Newtonsoft.Json;

using Repository;

namespace RegisterHandler
{
    public class TaskRegisterHandler
    {
        private readonly ITaskAggregateRepository repository;

        public TaskRegisterHandler() { repository = new TaskAggregateRepository(); }

        public void TaskRegisterHandlerInvoke(string message)
        {
            //Task eventtask
            var task = JsonConvert.DeserializeObject<Task>(message);
            var interval = GetInterval(task.BeginTime);
            var taskAggregate = repository.Get(interval);
            var isNew = taskAggregate == null;

            if (taskAggregate == null)
                taskAggregate = CreateTaskAggregate(interval, task.CorrelationId);

            if (task.EndTime.HasValue)
                TaskRegisterHandlerAggregate(taskAggregate);
            else
                TaskRegisterHandlerIncrement(taskAggregate);

            if (isNew)
                repository.Add(taskAggregate);
            else
                repository.Update(taskAggregate);
        }

        private void ComputeErlang(TaskAggregate taskAggregate)
        {
            taskAggregate.Erlang = taskAggregate.Resolved * new Random().Next();
        }

        private void ComputeLevelService(TaskAggregate taskAggregate)
        {
            taskAggregate.LevelService = (decimal) (taskAggregate.Resolved * 100) / taskAggregate.Quantity;
        }

        private TaskAggregate CreateTaskAggregate(TimeSpan interval, Guid correlationId)
        {
            var taskAggregate = new TaskAggregate
            {
                Interval = interval,
                _id = Guid.NewGuid(),
                CorrelationId = correlationId
            };

            return taskAggregate;
        }

        private TimeSpan GetInterval(TimeSpan beginTimeSpan)
        {
            if (beginTimeSpan.Minutes > 30)
                return new TimeSpan(beginTimeSpan.Hours, 30, 00);

            return new TimeSpan(beginTimeSpan.Hours, 00, 00);
        }

        private void IncrementQuantity(TaskAggregate taskAggregate) { taskAggregate.Quantity++; }

        private void IncrementResolved(TaskAggregate taskAggregate) { taskAggregate.Resolved++; }

        private void TaskRegisterHandlerAggregate(TaskAggregate taskAggregate)
        {
            IncrementResolved(taskAggregate);
            ComputeLevelService(taskAggregate);
            ComputeErlang(taskAggregate);
        }

        private void TaskRegisterHandlerIncrement(TaskAggregate taskAggregate)
        {
            IncrementQuantity(taskAggregate);
            ComputeLevelService(taskAggregate);
        }
    }
}