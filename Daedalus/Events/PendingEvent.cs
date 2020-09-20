using Daedalus.Domain;

namespace Daedalus.Events
{
    internal class PendingEvent : IPendingEvent
    {
        public IAggregateEvent AggregateEvent { get; }
        public IEventMetadata EventMetadata { get; }

        public PendingEvent(IAggregateEvent aggregateEvent, IEventMetadata eventMetadata)
        {
            AggregateEvent = aggregateEvent;
            EventMetadata = eventMetadata;
        }
    }
}