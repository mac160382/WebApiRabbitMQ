using System;

namespace CallCenterModel
{
    public class Task
    {
        public Guid? _id { get; set; }

        public TimeSpan BeginTime { get; set; }

        public Guid CorrelationId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string User { get; set; }
    }
}