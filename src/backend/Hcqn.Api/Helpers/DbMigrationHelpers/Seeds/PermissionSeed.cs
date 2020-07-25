using Hcqn.Api.Configuration.Permission;
using Hcqn.Domain.AggregatesModel.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hcqn.Api.Helpers.DbMigrationHelpers.Seeds
{
    public static class PermissionSeed
    {
        public static async Task EnsureSeedData<TDbContext>(IServiceProvider services)
            where TDbContext : DbContext
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            var permissionDbSet = context.Set<Permissao>();
            await permissionDbSet.AddRangeAsync(Permissions.GetPermissions().Select(x => new Permissao(x)));
            await context.SaveChangesAsync();
        }
    }
}
