using Hcqn.Core.Bus.Abstractions;
using Hcqn.Core.Commands;
using Hcqn.Core.Events.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Unit = Hcqn.Core.Types.Unit;

namespace Hcqn.Core.Bus
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishMessage<T>(T message, CancellationToken cancellationToken = default) where T : IMessage
        {
            return _mediator.Publish(message, cancellationToken);
        }

        public Task RaiseEvent<T>(T @event, CancellationToken cancellationToken = default) where T : IEvent
        {
            return _mediator.Publish(@event, cancellationToken);
        }

        public Task<Unit> SendCommand(ICommand command, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }

        public Task<TResponse> SendCommand<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
         => _mediator.Send(command, cancellationToken);

    }
}