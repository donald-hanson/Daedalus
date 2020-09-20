using System.Collections.Generic;
using Daedalus.Domain;

namespace Daedalus.Events.EventBus.Memory
{
    public interface IEventHandlerProvider
    {
        IEnumerable<IEventHandler<TEvent>> GetEventHandlers<TEvent>() where TEvent : IAggregateEvent;
        IEnumerable<IEventHandlerAsync<TEvent>> GetEventHandlersAsync<TEvent>() where TEvent : IAggregateEvent;
    }
}
