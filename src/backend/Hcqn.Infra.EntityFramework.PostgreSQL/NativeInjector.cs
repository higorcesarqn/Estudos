using Autofac;
using Hcqn.Infra.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Hcqn.Infra.EntityFramework.PostgreSQL
{
    public static class NativeInjector
    {
        public static void RegisterPostgreSqlDataBase(this ContainerBuilder container, string connectionString)
        {
            container
              .RegisterContext<SitPostgreSqlContext>(
                options => options.UseNpgsql(connectionString,
                sql => sql.MigrationsAssembly("Hcqn.Api")));
        }
    }
}