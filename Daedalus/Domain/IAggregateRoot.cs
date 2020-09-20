using System.Collections.Generic;
using Daedalus.Events;

namespace Daedalus.Domain
{
    public interface IAggregateRoot<TIdentity> where TIdentity : IAggregateIdentity
    {
        long Version { get; }
        IReadOnlyList<IPendingEvent> PendingEvents { get; }
        TIdentity Id { get; }
        bool IsNew { get; }
        void ApplyEvents(IEnumerable<IStoredEvent> storedEvents);
    }
}