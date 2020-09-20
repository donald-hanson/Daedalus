using System;
using System.Collections.Generic;
using Daedalus.Events;
using Daedalus.Utility;

namespace Daedalus.Domain
{
    public abstract class AggregateRoot<TAggregate, TIdentity> : IAggregateRoot<TIdentity>
        where TAggregate : AggregateRoot<TAggregate, TIdentity>
        where TIdentity : IAggregateIdentity
    {
        private readonly List<IPendingEvent> _events = new List<IPendingEvent>();

        public long Version { get; private set; }
        public bool IsNew => Version <= 0;
        IReadOnlyList<IPendingEvent> IAggregateRoot<TIdentity>.PendingEvents => _events;
        public TIdentity Id { get; }

        private static readonly IReadOnlyDictionary<Type, Action<TAggregate, IAggregateEvent>> ApplyMethods;
        private static readonly string AggregateName = typeof(TAggregate).Name;

        static AggregateRoot()
        {
            ApplyMethods = typeof(TAggregate).GetAggregateEventApplyMethods<IAggregateEvent, TAggregate>();
        }

        protected AggregateRoot(TIdentity id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (!(this is TAggregate))
            {
                throw new InvalidOperationException($"Aggregate '{GetType().PrettyPrint()}' specifies '{typeof(TAggregate).PrettyPrint()}' as generic argument, it should be its own type");
            }

            Id = id;
        }

        protected void Emit<TEvent>(TEvent aggregateEvent) where TEvent : IAggregateEvent
        {
            var sequenceNumber = Version + 1;

            var eventId = $"{Id.AsString()}-v{sequenceNumber}";

            DateTimeOffset now = DateTimeOffset.UtcNow;

            var metadata = new EventMetadata
            {
                EventId = eventId,
                EventVersion = aggregateEvent.Version,
                EventName = aggregateEvent.Name,
                AggregateId = Id.AsString(),
                AggregateName = AggregateName,
                AggregateSequenceNumber = sequenceNumber,
                Timestamp = now,
                TimestampEpoch = now.ToUnixTimeSeconds()
            };

            var pendingEvent = new PendingEvent(aggregateEvent, metadata);
            ApplyEvent(aggregateEvent);
            _events.Add(pendingEvent);
        }

        void IAggregateRoot<TIdentity>.ApplyEvents(IEnumerable<IStoredEvent> storedEvents)
        {
            foreach (var storedEvent in storedEvents)
            {
                var metadata = storedEvent.EventMetadata;
                var aggregateEvent = storedEvent.AggregateEvent;
                if (metadata.AggregateSequenceNumber != Version + 1)
                    throw new InvalidOperationException(
                        $"Cannot apply aggregate event of type '{aggregateEvent.GetType().PrettyPrint()}' " +
                        $"with SequenceNumber {metadata.AggregateSequenceNumber} on aggregate " +
                        $"with version {Version}");
                
                ApplyEvent(aggregateEvent);
            }
        }

        protected virtual void ApplyEvent(IAggregateEvent aggregateEvent)
        {
            if (aggregateEvent == null)
            {
                throw new ArgumentNullException(nameof(aggregateEvent));
            }

            var eventType = aggregateEvent.GetType();

            if (!ApplyMethods.TryGetValue(eventType, out var applyMethod))
            {
                throw new NotImplementedException($"Aggregate '{AggregateName}' does not have an 'Apply' method that takes aggregate event '{eventType.PrettyPrint()}' as argument");
            }

            applyMethod(this as TAggregate, aggregateEvent);

            Version++;
        }
    }
}