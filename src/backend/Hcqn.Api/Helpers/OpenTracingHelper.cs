using Jaeger.Samplers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;

namespace Hcqn.Api.Helpers
{
    public static class OpenTracingHelper
    {
        public static IServiceCollection AddOpenTracingAndJaeger(this IServiceCollection services)
        {
            services.AddOpenTracing();

            // Adds the Jaeger Tracer.
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                var serviceName = "SIT";
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                var configuration = Jaeger.Configuration.FromEnv(loggerFactory);

                var tracer = configuration.GetTracerBuilder()
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(new ConstSampler(true))
                    .Build();

                GlobalTracer.Register(tracer);

                var config = new Jaeger.Configuration(serviceName, loggerFactory);

                return tracer;
            });

            return services;
        }
    }
}
