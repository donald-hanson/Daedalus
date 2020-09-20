using Daedalus.Domain;

namespace Daedalus.Events.EventBus.Memory
{
    public interface IEventHandler<TEvent> where TEvent : IAggregateEvent
    {
        void Handle(TEvent @event);
    }
}
