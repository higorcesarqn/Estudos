using Hcqn.Teste.Geo.Context;
using Hcqn.Teste.Geo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hcqn.Teste.Geo
{
    public static class NativeInjector
    {
        public static IServiceCollection AddTesteGeo(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddEntityFrameworkNpgsql()
            .AddDbContext<GeoContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("SitDbConnection"), o => { o.UseNetTopologySuite(); o.MigrationsAssembly("Egl.Sit.Api"); }));

            services.AddScoped<ILoteRepository, LoteRepository>();

            return services;
        }
    }
}
