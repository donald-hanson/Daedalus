using System.Threading.Tasks;

namespace Daedalus.Commands.CommandBus.Memory
{
    public interface ICommandHandlerAsync<TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}