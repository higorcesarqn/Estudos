using Hcqn.Core.Commands.Extensions;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.Core.Commands
{
    public abstract class CommandHandlerBase<TCommand, TResponse>
        where TCommand : ICommandBase
    {
        protected ITracer Tracer { get; }

        protected ILogger Logger { get; }

        protected CommandHandlerBase(ILoggerFactory loggerFactory, ITracer tracer) 
        {
            Logger = loggerFactory.CreateLogger(GetType());
            Tracer = tracer;
        }

        public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                using var tracerScope = Tracer.CreateCommandTraceScope(command, $"CommandHandler-{typeof(TCommand).Name}");

                Logger.LogTrace($"Processing Command '{command}' ...");
                var watch = Stopwatch.StartNew();

                var response = await Process(command, cancellationToken).ConfigureAwait(false);

                watch.Stop();
                Logger.LogTrace($"Processed Command '{command}': {watch.ElapsedMilliseconds} ms");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error processing Command '{command}': {ex.Message}");
                throw ex;
            }
        }

        protected abstract Task<TResponse> Process(TCommand request, CancellationToken cancellationToken);
    }
}
