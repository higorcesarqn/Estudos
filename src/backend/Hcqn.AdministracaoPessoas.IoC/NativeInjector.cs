using Autofac;
using Hcqn.Core.Commands;
using Hcqn.Core.UnitOfWork.Abstractions;
using Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.PessoaFisicaCommands.Create;
using Hcqn.AdministracaoPessoas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hcqn.AdministracaoPessoas.IoC
{
    public static class NativeInjector
    {
        public static ContainerBuilder RegisterAdministracaoPessoas<TDbContext>(this ContainerBuilder container)
            where TDbContext : DbContext
        {
            container.ConfigureCommandsHandlers<IUnitOfWork<TDbContext>>();
            container.ConfigureAdministracaoPessoasRepositories<TDbContext>();

            return container;
        }

        private static void ConfigureAdministracaoPessoasRepositories<TDbContext>(this ContainerBuilder container)
            where TDbContext : DbContext
        {
            container.RegisterType<PessoaRepository<TDbContext>>()
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }

        private static void ConfigureCommandsHandlers<TUnitOfWork>(this ContainerBuilder container)
            where TUnitOfWork : IUnitOfWork
        {
            container.RegisterCommand<CreateNewPessoaFisicaCommand, CreateNewPessoaFisicaCommandHandler<TUnitOfWork>>();
        }
    }
}
