using Hcqn.Infra.EntityFramework.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hcqn.Api.Configuration.Test
{
    /// <summary>
    /// 
    /// </summary>
    public class StartupTest : Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public StartupTest(IHostEnvironment env) : base(env)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public override void RegisterDbContexts(IServiceCollection services)
        {
            services.ConfigureInMemoryDbContext();
        }
    }
}
