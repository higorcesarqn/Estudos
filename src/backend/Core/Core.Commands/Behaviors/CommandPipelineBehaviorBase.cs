using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.Core.Commands.Behaviors
{
    public abstract class CommandPipelineBehaviorBase<TCommand, TResponse>
       : IPipelineBehavior<TCommand, TResponse>
       where TCommand : ICommandBase
    {
        protected CommandPipelineBehaviorBase(ILoggerFactory loggerFactory) 
        {
            Logger = CreateLogger(loggerFactory);
        }

        private ILogger CreateLogger(ILoggerFactory loggerFactory) => loggerFactory.CreateLogger(GetType());

        protected ILogger Logger { get; }

        public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                Logger.LogTrace("Processing Command pipeline request '{command}' ...", command);
                var watch = Stopwatch.StartNew();

                var response = await Process(command, cancellationToken, next).ConfigureAwait(false);

                watch.Stop();
                Logger.LogTrace("Processed Command pipeline request '{command}': {elapsed} ms", command, watch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error handling pipeline request '{command}': {errorMessage}", command, ex.Message);
                throw;
            }
        }

        protected abstract Task<TResponse> Process(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);

    }
}
