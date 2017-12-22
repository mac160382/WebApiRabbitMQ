// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskAggregateRepositoryTest.cs" company="GBM"> GBM GRUPO BURSÁTIL MEXICANO, S.A. DE C.V. CASA DE BOLSA. ©2017</copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using CallCenterModel;

using FluentAssertions;

using Xunit;

namespace Repository.Integrations.tests
{
    public class TaskAggregateRepositoryTest
    {
        private readonly Guid correlationId;

        public TaskAggregateRepositoryTest()
        {
            this.correlationId = new Guid("9ca7a625-932f-4fda-b752-a46205094cba");
        }

        [Fact]
        public void when_add_new_call_taskaggregate_then_is_created_in_repository()
        {
            ITaskAggregateRepository repository = new TaskAggregateRepository();
            var taskAggregate = new TaskAggregate();
            taskAggregate._id = correlationId;
            taskAggregate.Interval = new TimeSpan(9, 0, 0);
            taskAggregate.Quantity = 1;
            taskAggregate.Resolved = 0;
            taskAggregate.Erlang = 0;
            taskAggregate.LevelService = 0;

            taskAggregate.CorrelationId = correlationId;

            repository.Add(taskAggregate);
        }

        [Fact]
        public void when_search_taskaggregate_with_a_specific_interval()
        {
            ITaskAggregateRepository repository = new TaskAggregateRepository();
            var interval = new TimeSpan(9, 0, 0);
            var taskAggregate = repository.Get(interval);
            taskAggregate.Should().NotBeNull();
            taskAggregate.Interval.Should().Be(interval);
        }

        [Fact]
        public void when_update_taskaggregate_with_a_quantity_resolve_Earlan_and_levelservice()
        {
            ITaskAggregateRepository repository = new TaskAggregateRepository();
            var interval = new TimeSpan(9, 0, 0);
            var taskAggregate = repository.Get(interval);

            taskAggregate.Quantity = 1;
            taskAggregate.Resolved = 1;
            taskAggregate.Erlang = 1;
            taskAggregate.LevelService = 1;

            var result = repository.Update(taskAggregate);

            result.Quantity.Should().Be(taskAggregate.Quantity);
            result.Resolved.Should().Be(taskAggregate.Resolved);
            result.Erlang.Should().Be(taskAggregate.Erlang);
            result.LevelService.Should().Be(taskAggregate.LevelService);
        }
    }
}