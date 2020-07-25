using Autofac;
using Microsoft.EntityFrameworkCore;

namespace Hcqn.Core.UnitOfWork
{
    public static class UnitOfWorkAutofacExtensions
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

        /// <summary>
        /// Adiciona um IUnitOfWork<TContext> no IoC
        /// </summary>
        /// <typeparam name="TContext">DbContext para Registar no IUnitOfWork</typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static ContainerBuilder RegisterUnitOfWork<TContext>(this ContainerBuilder container)
            where TContext : DbContext
        {
            Register<UnitOfWork<TContext>>(container);

            return container;
        }
    }
}

