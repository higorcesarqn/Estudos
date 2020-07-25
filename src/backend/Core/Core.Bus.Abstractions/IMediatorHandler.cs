using Hcqn.Core.Commands;
using Hcqn.Core.Events.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Unit = Hcqn.Core.Types.Unit;

namespace Hcqn.Core.Bus.Abstractions
{
    public interface IMediatorHandler
    {
        Task PublishMessage<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : IMessage;

        Task<TResponse> SendCommand<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);

        Task<Unit> SendCommand(ICommand command, CancellationToken cancellationToken = default);

        Task RaiseEvent<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
    }
}