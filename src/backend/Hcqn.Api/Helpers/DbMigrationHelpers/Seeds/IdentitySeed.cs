using Hcqn.Api.Configuration.Identity;
using Hcqn.Api.Configuration.Permission;
using Hcqn.Infra.CrossCutting.Identity.Configurations;
using Hcqn.Infra.CrossCutting.Identity.Entities;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hcqn.Api.Helpers.DbMigrationHelpers.Seeds
{
    public static class IdentitySeed
    {
        public const string AdminRole = "Administrador";

        public static async Task EnsureSeedData<TContext>(IServiceProvider services)
            where TContext : DbContext
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            var userManager = scope.ServiceProvider.GetRequiredService<IUserManager>();
            var roleManager = scope.ServiceProvider.GetRequiredService<IRoleManager>();
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

            var permissions = Permissions.GetPermissions();

            var user = new User(Guid.NewGuid(), Users.AdminUserName, Users.AdminEmail, Users.AdminUserName);

            await EnsureRoles(context, roleManager, permissions);
            await EnsureAdminUser(context, userManager, user);
            await EnsureRolesAdminUser(context, userManager, user);
        }

        private static async Task EnsureAdminUser<TContext>(TContext dbContext, IUserManager userManager, User user)
             where TContext : DbContext
        {
            var userDbSet = dbContext.Set<User>();
            if (!await userDbSet.AnyAsync())
            {
                await userManager.CreateUser(user, Users.AdminPassword);
            }
        }

        private static async Task EnsureRolesAdminUser<TContext>(TContext dbContext, IUserManager userManager, User user)
             where TContext : DbContext
        {
            var userRoleDbSet = dbContext.Set<UserRole>();
            if (!await userRoleDbSet.AnyAsync())
            {
                await userManager.AddToRoles(user, new List<string> { AdminRole });
            }
        }

        private static async Task EnsureRoles<TContext>(TContext dbContext, IRoleManager roleManager, IEnumerable<string> permissions)
             where TContext : DbContext
        {
            var roleDbSet = dbContext.Set<Role>();

            if (!await roleDbSet.AnyAsync())
            {
                var role = new Role(Guid.NewGuid(), AdminRole);

                await roleManager.CreateRole(role);

                foreach (var permission in permissions)
                {
                    await roleManager.AddClaim(role, new Claim(IdentityConfigurations.DefaultRoleClaim, permission.ToLower()));
                }
            }
        }
    }
}