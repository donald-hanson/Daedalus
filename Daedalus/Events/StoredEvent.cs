using Daedalus.Domain;

namespace Daedalus.Events
{
    internal class StoredEvent : IStoredEvent
    {
        public IAggregateEvent AggregateEvent { get; }
        public IEventMetadata EventMetadata { get; }

        public StoredEvent(IAggregateEvent aggregateEvent, IEventMetadata eventMetadata)
        {
            AggregateEvent = aggregateEvent;
            EventMetadata = eventMetadata;
        }
    }
}