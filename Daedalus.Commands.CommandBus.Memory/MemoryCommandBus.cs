using System;
using System.Threading.Tasks;
using Daedalus.Utility;

namespace Daedalus.Commands.CommandBus.Memory
{
    public class MemoryCommandBus : ICommandBus
    {
        private readonly ICommandHandlerProvider _commandHandlerProvider;

        public MemoryCommandBus(ICommandHandlerProvider commandHandlerProvider)
        {
            _commandHandlerProvider = commandHandlerProvider;
        }

        public void Publish<TCommand>(TCommand command)
        {
            var task = PublishAsync(command);
            task.Wait();
        }

        public async Task PublishAsync<TCommand>(TCommand command)
        {
            var syncHandler = _commandHandlerProvider.GetCommandHandler<TCommand>();
            if (syncHandler != null)
            {
                syncHandler.Handle(command);
                return;
            }

            var asyncHandler = _commandHandlerProvider.GetCommandHandlerAsync<TCommand>();
            if (asyncHandler != null)
            {
                await asyncHandler.HandleAsync(command);
                return;
            }

            throw new ArgumentException($"Unable to find command handler for command {typeof(TCommand).PrettyPrint()}");
        }
    }
}
