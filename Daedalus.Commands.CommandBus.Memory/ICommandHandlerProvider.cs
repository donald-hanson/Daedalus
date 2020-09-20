namespace Daedalus.Commands.CommandBus.Memory
{
    public interface ICommandHandlerProvider
    {
        ICommandHandler<TCommand> GetCommandHandler<TCommand>();
        ICommandHandlerAsync<TCommand> GetCommandHandlerAsync<TCommand>();
    }
}
