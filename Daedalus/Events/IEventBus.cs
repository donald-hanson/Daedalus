using System.Threading.Tasks;
using Daedalus.Domain;

namespace Daedalus.Events
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event, IEventMetadata eventMetadata) where TEvent : IAggregateEvent;
        Task PublishAsync<TEvent>(TEvent @event, IEventMetadata eventMetadata) where TEvent : IAggregateEvent;
    }
}
