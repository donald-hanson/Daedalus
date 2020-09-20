using System.Threading.Tasks;

namespace Daedalus.Commands
{
    public interface ICommandBus
    {
        void Publish<TCommand>(TCommand command);
        Task PublishAsync<TCommand>(TCommand command);
    }
}
