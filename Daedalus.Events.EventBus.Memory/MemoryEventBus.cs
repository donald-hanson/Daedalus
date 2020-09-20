using System.Threading.Tasks;
using Daedalus.Domain;

namespace Daedalus.Events.EventBus.Memory
{
    public class MemoryEventBus : IEventBus
    {
        private readonly IEventHandlerProvider _eventHandlerProvider;

        public MemoryEventBus(IEventHandlerProvider eventHandlerProvider)
        {
            _eventHandlerProvider = eventHandlerProvider;
        }

        public void Publish<TEvent>(TEvent @event, IEventMetadata eventMetadata) where TEvent : IAggregateEvent
        {
            var task = PublishAsync(@event, eventMetadata);
            task.Wait();
        }

        public async Task PublishAsync<TEvent>(TEvent @event, IEventMetadata eventMetadata) where TEvent : IAggregateEvent
        {
            var handlers = _eventHandlerProvider.GetEventHandlers<TEvent>();
            foreach (var handler in handlers)
            {
                handler.Handle(@event);
            }

            var asyncHandlers = _eventHandlerProvider.GetEventHandlersAsync<TEvent>();
            foreach (var handler in asyncHandlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}