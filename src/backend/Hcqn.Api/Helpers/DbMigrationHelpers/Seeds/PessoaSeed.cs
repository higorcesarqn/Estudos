using Hcqn.Core.Models;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Hcqn.Api.Helpers.DbMigrationHelpers.Seeds
{
    public static class PessoaSeed
    {
        public static async Task EnsureSeedData<TDbContext>(IServiceProvider services)
           where TDbContext : DbContext
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

            await EnsureEnumeration<TDbContext, TipoPessoa>(context);
            await EnsureEnumeration<TDbContext, TipoDocumento>(context);
            await EnsureEnumeration<TDbContext, TipoEndereco>(context);
            await EnsureEnumeration<TDbContext, TipoTelefone>(context);
            await EnsureEnumeration<TDbContext, Escolaridade>(context);

            await context.SaveChangesAsync();
        }

        private static async Task EnsureEnumeration<TDbContext, TEnumeration>(TDbContext context)
            where TDbContext : DbContext
            where TEnumeration : Enumeration
        {
            var dbSet = context.Set<TEnumeration>();

            if (!await dbSet.AnyAsync())
            {
                var tipos = Enumeration.GetAll<TEnumeration>();
                await dbSet.AddRangeAsync(tipos);
            }
        }
    }
}
