using System;
using System.Threading.Tasks;
using Daedalus.Events;
using Daedalus.Utility;

namespace Daedalus.Domain
{
    public class AggregateRepository<TAggregate, TIdentity> : IAggregateRepository<TAggregate, TIdentity>
        where TAggregate : IAggregateRoot<TIdentity> 
        where TIdentity : IAggregateIdentity
    {
        private readonly IEventStore<TIdentity> _eventStore;
        private readonly IEventBus _eventBus;

        public AggregateRepository(IEventStore<TIdentity> eventStore, IEventBus eventBus)
        {
            _eventStore = eventStore;
            _eventBus = eventBus;
        }

        public async Task<TAggregate> GetAsync(TIdentity id)
        {
            var instance = Create(id);
            if (instance == null)
            {
                throw new ArgumentException($"Aggregate type '{typeof(TAggregate).PrettyPrint()}' does not have a public constructor that accepts a identity of type '{typeof(TIdentity).PrettyPrint()}'");
            }

            var storedEvents = await _eventStore.GetEvents(id);
            instance.ApplyEvents(storedEvents);
            return instance;
        }

        public async Task CommitAsync(TAggregate instance)
        {
            if (instance.PendingEvents.Count > 0)
            {
                await _eventStore.Insert(instance.Id, instance.PendingEvents);
                foreach (var pendingEvent in instance.PendingEvents)
                {
                    await _eventBus.PublishAsync(pendingEvent.AggregateEvent, pendingEvent.EventMetadata);
                }
            }
        }

        private static TAggregate Create(TIdentity id)
        {
            var constructor = typeof(TAggregate).GetConstructor(new[] {typeof(TIdentity)});
            var instance = constructor?.Invoke(new object[] {id});
            if (instance == null)
                return default;
            return (TAggregate)instance;
        }
    }
}
