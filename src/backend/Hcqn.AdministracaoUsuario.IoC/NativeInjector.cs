using Autofac;
using Hcqn.Core.Commands;
using Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Create;
using Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Update;
using Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.ChangePassword;
using Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.Create;
using Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.Update;
using Hcqn.AdministracaoUsuario.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hcqn.AdministracaoUsuario.IoC
{
    public static class NativeInjector
    {
        public static ContainerBuilder RegisterAdministracaoUsuario<TDbContext>(this ContainerBuilder container)
            where TDbContext : DbContext
        {
            //repositories
            container.RegisterType<UserRepository<TDbContext>>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            container.RegisterType<RoleRepository>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //Commands Handlers
            container.RegisterCommand<CreateNewUserCommand, CreateNewUserCommandHandler>();
            container.RegisterCommand<UpdateUserCommand, UpdateUserCommandHandler>();
            container.RegisterCommand<ChangePasswordCommand, ChangePasswordCommandHandler>();
            container.RegisterCommand<UpdateRoleCommand, UpdateRoleCommandHandler>();
            container.RegisterCommand<CreateNewRoleCommand, CreateNewRoleCommandHandler>();

            return container;
        }
    }
}
