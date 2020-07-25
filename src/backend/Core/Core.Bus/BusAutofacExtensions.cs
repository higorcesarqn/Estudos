using Autofac;

namespace Hcqn.Core.Bus
{
    public static class BusAutofacExtensions
    {
        private static ContainerBuilder Register<TImplementer>(ContainerBuilder container)
        {
            var typeImplementer = typeof(TImplementer);

            container.RegisterType(typeImplementer)
               .AsImplementedInterfaces()
               .IfNotRegistered(typeImplementer)
               .InstancePerLifetimeScope();

            return container;
        }

        public static ContainerBuilder RegisterBus(this ContainerBuilder container)
        {
            Register<MediatorHandler>(container);
            return container;
        }
    }
}