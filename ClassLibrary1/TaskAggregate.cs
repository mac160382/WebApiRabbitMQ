using System;

namespace CallCenterModel
{
    public class TaskAggregate
    {
        public Guid? _id { get; set; }

        public Guid CorrelationId { get; set; }

        //AHT
        public int Erlang { get; set; }

        //
        public TimeSpan Interval { get; set; }

        //SVL
        public decimal LevelService { get; set; }

        //NCO
        public int Quantity { get; set; }

        //NCH
        public int Resolved { get; set; }
    }
}