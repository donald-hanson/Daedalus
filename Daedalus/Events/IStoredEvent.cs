using Daedalus.Domain;

namespace Daedalus.Events
{
    public interface IStoredEvent
    {
        public IAggregateEvent AggregateEvent { get; }
        public IEventMetadata EventMetadata { get; }
    }
}