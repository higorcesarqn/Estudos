using Hcqn.Core.Notifications.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.Core.Commands.Behaviors
{
    public class CommandValidatorBehavior<TCommand, TResponse> : CommandPipelineBehaviorBase<TCommand, TResponse>
        where TCommand : ICommandBase
    {
        private readonly INotifiable _notifiable;

        public CommandValidatorBehavior(ILoggerFactory loggerFactory, INotifiable notifiable) : base(loggerFactory)
        {
            _notifiable = notifiable;
        }

        protected override async Task<TResponse> Process(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //se o Command estiver inválido, é notificado os erros.
            if (!await command.IsValid())
            {
                await _notifiable.NotifyValidationErrors(command.ValidationResult);
                return default;
            }

            return await next();
        }

    }
}