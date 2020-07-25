using Hcqn.Infra.CrossCutting.Identity.Caching;
using Hcqn.Infra.CrossCutting.Identity.Entities;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Hcqn.Infra.CrossCutting.Identity.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hcqn.Infra.CrossCutting.Identity
{
    public static class NativeInjector
    {
        public static IServiceCollection ConfigureIdentity<TContext>(this IServiceCollection services)
           where TContext : DbContext
        {
            services.AddScoped<AccessManager>();
            services.AddScoped<IAcessManager, AcessManagerCaching<AccessManager>>();

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IRoleManager, RoleManager>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders();

            services.ConfigureIdentityOptions();

            return services;
        }
    }
}
