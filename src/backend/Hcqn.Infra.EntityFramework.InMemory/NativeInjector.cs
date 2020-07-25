using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hcqn.Infra.EntityFramework.InMemory
{
    public static class NativeInjector
    {
        public static void ConfigureInMemoryDbContext(this IServiceCollection services)
        {
            services
              .AddDbContext<SitInMemoryContext>(
                options =>  options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        }
    }
}
