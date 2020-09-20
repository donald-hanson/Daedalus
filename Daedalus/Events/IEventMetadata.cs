using System;

namespace Daedalus.Events
{
    public interface IEventMetadata
    {
        string EventId { get; }
        string EventName { get; }
        int EventVersion { get; }
        DateTimeOffset Timestamp { get; }
        long TimestampEpoch { get; }
        long AggregateSequenceNumber { get; }
        string AggregateId { get; }
        string AggregateName { get; }
    }
}