using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hcqn.EventSourcing.EntityFramework.PostgreSQL
{
    public static class NativeInjector
    {
        public static IServiceCollection ConfigureEventoSourcingPostgreSqlDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddEntityFrameworkNpgsql()
                .AddDbContext<EventStoreEventSourcingContext>(options =>
                    options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly("Hcqn.Api")),
                    contextLifetime: ServiceLifetime.Transient);
        }
    }
}