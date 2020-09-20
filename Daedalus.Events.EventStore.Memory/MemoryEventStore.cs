using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daedalus.Domain;

namespace Daedalus.Events.EventStore.Memory
{
    public class MemoryEventStore<TIdentity> : IEventStore<TIdentity>
        where TIdentity : IAggregateIdentity
    {
        private readonly List<IStoredEvent> _events = new List<IStoredEvent>();

        public Task Insert(TIdentity identity, IEnumerable<IPendingEvent> events)
        {
            _events.AddRange(events.Select(e => new StoredEvent(e.AggregateEvent, e.EventMetadata)));
            return Task.CompletedTask;
        }

        public Task<IEnumerable<IStoredEvent>> GetEvents(TIdentity identity, long startSequenceNumber = 0)
        {
            var id = identity.AsString();
            var events = _events.Where(e => e.EventMetadata.AggregateId == id && e.EventMetadata.AggregateSequenceNumber > startSequenceNumber);
            return Task.FromResult(events);
        }
    }
}
