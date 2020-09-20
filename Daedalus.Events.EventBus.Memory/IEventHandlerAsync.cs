using System.Threading.Tasks;
using Daedalus.Domain;

namespace Daedalus.Events.EventBus.Memory
{
    public interface IEventHandlerAsync<TEvent> where TEvent : IAggregateEvent
    {
        Task HandleAsync(TEvent @event);
    }
}