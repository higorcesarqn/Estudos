using Hcqn.Core.Commands;
using Hcqn.Core.Commands.Behaviors;
using Hcqn.Core.Commands.Extensions;
using Hcqn.Core.Events.Abstractions;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.EventSourcing
{
    public class EventStoreEventHandler<TCommand, TResponse> : CommandPostProcessorBase<TCommand, TResponse>
         where TCommand : ICommandBase
    {
        private readonly IEventStore _eventStore;

        public EventStoreEventHandler(IEventStore eventStore, ILoggerFactory loggerFactory, ITracer tracer)
            : base(loggerFactory, tracer)
        {
            _eventStore = eventStore;
        }

        protected override async Task Process(TCommand request, CancellationToken cancellationToken)
        {
            using var tracing = Tracer.CreateCommandTraceScope(request, "EventStore");

            await _eventStore.Save(request);
        }
    }
}
