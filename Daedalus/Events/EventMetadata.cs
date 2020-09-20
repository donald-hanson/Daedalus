using System;

namespace Daedalus.Events
{
    internal class EventMetadata : IEventMetadata
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        public int EventVersion { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public long TimestampEpoch { get; set; }
        public long AggregateSequenceNumber { get; set; }
        public string AggregateId { get; set; }
        public string AggregateName { get; set; }
    }
}
