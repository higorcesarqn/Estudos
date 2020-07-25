using Autofac;
using Hcqn.Core.Commands.Behaviors;
using Hcqn.Core.Types;

namespace Hcqn.Core.Commands
{
    public static class CommandAutofacExtensions
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
        /// Adiciona um Novo Comando no IoC.
        /// Todo Commando Adicionado é adicionado para o Commando um CommandValidatorBehavior no pipeline
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TComandHandler"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static ContainerBuilder RegisterCommand<TCommand, TComandHandler>(this ContainerBuilder container)
             where TCommand : ICommand
             where TComandHandler : CommandHandler<TCommand>
        {
            Register<TComandHandler>(container);

            RegisterCommandPipelineBehavior<CommandValidatorBehavior<TCommand, Unit>, TCommand, Unit>(container);

            return container;
        }

        /// <summary>
        /// Adiciona um Novo Comando no IoC.
        /// Todo Commando Adicionado é adicionado para o Commando um CommandValidatorBehavior no pipeline
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TComandHandler"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static ContainerBuilder RegisterCommand<TCommand, TResponse, TComandHandler>(this ContainerBuilder container)
            where TCommand : ICommand<TResponse>
            where TComandHandler : CommandHandler<TCommand, TResponse>
        {
            Register<TComandHandler>(container);

            RegisterCommandPipelineBehavior<CommandValidatorBehavior<TCommand, TResponse>, TCommand, TResponse>(container);

            return container;
        }

        /// <summary>
        /// Adiciona um Novo CommandPipelineBehaviorBase no IoC.
        /// </summary>
        /// <typeparam name="TBehavior"></typeparam>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static ContainerBuilder RegisterCommandPipelineBehavior<TBehavior, TCommand, TResponse>(this ContainerBuilder container)
            where TBehavior : CommandPipelineBehaviorBase<TCommand, TResponse>
            where TCommand : ICommandBase
        {
            Register<TBehavior>(container);

            return container;

        }
    }
}
