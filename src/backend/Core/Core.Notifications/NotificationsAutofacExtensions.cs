using Autofac;

namespace Hcqn.Core.Notifications
{
    public static class NotificationsAutofacExtensions
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

        public static ContainerBuilder RegisterNotificatons(this ContainerBuilder container)
        {
            Register<DomainNotificationHandler>(container);
            Register<Notifiable>(container);

            return container;
        }
    }
}
