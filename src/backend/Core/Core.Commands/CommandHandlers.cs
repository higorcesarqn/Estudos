using MediatR;
using Microsoft.Extensions.Logging;
using OpenTracing;
using Unit = Hcqn.Core.Types.Unit;

namespace Hcqn.Core.Commands
{
    public abstract class CommandHandler<TCommand> : CommandHandlerBase<TCommand, Unit>, IRequestHandler<TCommand, Unit>
      where TCommand : ICommand
    {
        protected CommandHandler(ILoggerFactory loggerFactory, ITracer tracer) : base(loggerFactory, tracer)
        {
        }
    }

    public abstract class CommandHandler<TCommand, TResponse> : CommandHandlerBase<TCommand, TResponse>, IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        protected CommandHandler(ILoggerFactory loggerFactory, ITracer tracer) : base(loggerFactory, tracer)
        {
        }
    }
}