namespace Daedalus.Commands.CommandBus.Memory
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}