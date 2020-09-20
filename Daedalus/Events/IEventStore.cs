using System.Collections.Generic;
using System.Threading.Tasks;
using Daedalus.Domain;

namespace Daedalus.Events
{
    public interface IEventStore<TIdentity>
        where TIdentity : IAggregateIdentity
    {
        Task Insert(TIdentity identity, IEnumerable<IPendingEvent> events);
        Task<IEnumerable<IStoredEvent>> GetEvents(TIdentity identity, long startSequenceNumber = 0);
    }
}
