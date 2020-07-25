using Hcqn.Api.Helpers.DbMigrationHelpers.Seeds;
using Hcqn.Infra.EntityFramework.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Hcqn.Api.Helpers.DbMigrationHelpers
{
    public static class DbMigration
    {
        public static async Task EnsureSeedData(IHost host)
        {
            await EnsureSeedData<SitPostgreSqlContext>(host.Services);
        }

        /**
         * dotnet ef migrations add InitialCreate --context SitPostgreSQLContext --output Data/Migrations/SitDb
            dotnet ef migrations script --context SitPostgreSQLContext
         *
         *
        Add-Migration SitDbInit -context SitPostgreSQLContext -output Data/Migrations/SitDb
        Add-Migration EventSourcingDbInit -context EventStoreEventSourcingContext -output Data/Migrations/EventSourcingDb
        **/

        private static async Task EnsureSeedData<TSitContext>(IServiceProvider services)
            where TSitContext : DbContext
        {
            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            //Administração Pessoa Seed.
            //var adminPessoaDbContext = scope.ServiceProvider.GetRequiredService<TSitContext>();
            //await AdministracaoPessoaSeed.EnsureSeedData(adminPessoaDbContext);

            //Identity Seed
            // var userManager = scope.ServiceProvider.GetRequiredService<IUserManager>();
            // var roleManager = scope.ServiceProvider.GetRequiredService<IRoleManager>();
            // var dbContext = scope.ServiceProvider.GetRequiredService<TSitContext>();

            await PermissionSeed.EnsureSeedData<TSitContext>(services);
            await IdentitySeed.EnsureSeedData<TSitContext>(services);
            await PessoaSeed.EnsureSeedData<TSitContext>(services);
        }
    }
}