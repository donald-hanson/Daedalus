using Daedalus.Domain;

namespace Daedalus.Events
{
    public interface IPendingEvent
    {
        public IAggregateEvent AggregateEvent { get; }
        public IEventMetadata EventMetadata { get; }
    }
}