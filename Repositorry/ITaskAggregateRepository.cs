using System;
using System.Collections.Generic;

using CallCenterModel;

namespace Repository
{
    public interface ITaskAggregateRepository
    {
        void Add(TaskAggregate taskAggregate);

        TaskAggregate Get(TimeSpan intervalTime);

        List<TaskAggregate> GetAll();

        TaskAggregate Update(TaskAggregate taskAggregate);
    }
}