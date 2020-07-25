using OpenTracing;

namespace Hcqn.Core.Commands.Extensions
{
    public static class OpenTracingExtensions
    {
        public static IScope CreateCommandTraceScope(this ITracer tracer, ICommandBase command, string name)
        {
            var trace = tracer.BuildSpan(name).StartActive(true);

            //if (command.AggregateId != default)
            //{
            //    trace.Span.SetTag("aggregate.id", command.AggregateId.ToString());
            //}

            return trace;
        }
    }
}
