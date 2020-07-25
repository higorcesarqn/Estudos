using Hcqn.Core.Commands.Extensions;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.Core.Commands.Behaviors
{
    public abstract class CommandPostProcessorBase<TCommand, TResponse> : IRequestPostProcessor<TCommand, TResponse>
        where TCommand : ICommandBase
    {
        protected ITracer Tracer;

        protected ILogger Logger { get; }

        protected CommandPostProcessorBase(ILoggerFactory loggerFactory, ITracer tracer)
        {
            Logger = CreateLogger(loggerFactory);
            Tracer = tracer;
        }

        private ILogger CreateLogger(ILoggerFactory loggerFactory)
           => loggerFactory.CreateLogger(GetType());

        public virtual async Task Process(TCommand command, TResponse response, CancellationToken cancellationToken)
        {
            try
            {
                using var tracerScope = Tracer.CreateCommandTraceScope(command, "CommandPostProcessor");

                //if (command.AggregateId != default)
                //{
                //    tracerScope.Span.SetTag("AggregateId", command.AggregateId.ToString());
                //}

                Logger.LogTrace("Processing Post Processor pipeline request '{command}' ...", command);
                var watch = Stopwatch.StartNew();

                await Process(command, cancellationToken).ConfigureAwait(false);

                watch.Stop();
                Logger.LogTrace("Processed Command pipeline request '{command}': {elapsed} ms", command, watch.ElapsedMilliseconds);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error handling pipeline request '{command}': {errorMessage}", command, ex.Message);
                throw;
            }
        }


        protected abstract Task Process(TCommand request, CancellationToken cancellationToken);
    }
}
