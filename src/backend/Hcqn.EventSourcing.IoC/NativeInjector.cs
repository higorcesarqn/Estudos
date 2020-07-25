using Autofac;
using Hcqn.Core.Events;
using Hcqn.Core.Events.Abstractions;
using Hcqn.EventSourcing.EntityFramework.PostgreSQL;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Hcqn.EventSourcing.IoC
{
    public static class NativeInjector
    {
        public static void ConfigureEventSourcing<TContext>(this ContainerBuilder container)
            where TContext : DbContext
        {
            
            container.AddEventSourcing<EventStoreRepository<TContext>>();
        }

        public static void AddEventSourcing<TRepository>(this ContainerBuilder container)
            where TRepository : IEventStoreRepository
        {
            container.RegisterType<TRepository>()
                .As<IEventStoreRepository>()
                .InstancePerLifetimeScope();

            container.RegisterType<EventStore>()
                .As<IEventStore>()
                .InstancePerLifetimeScope();

            container.RegisterGeneric(typeof(EventStoreEventHandler<,>))
                .As(typeof(IRequestPostProcessor<,>))
                .InstancePerLifetimeScope();
        }
    }
}
