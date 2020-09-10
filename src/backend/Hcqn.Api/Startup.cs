using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hcqn.AdministracaoPessoas.IoC;
using Hcqn.AdministracaoUsuario.IoC;
using Hcqn.Api.Helpers;
using Hcqn.Api.Infrastructure.Filters;
using Hcqn.Api.Infrastructure.Middleware;
using Hcqn.Caching;
using Hcqn.Core.Bus;
using Hcqn.Core.Notifications;
using Hcqn.Core.UnitOfWork;
using Hcqn.EventSourcing.EntityFramework.PostgreSQL;
using Hcqn.EventSourcing.IoC;
using Hcqn.Infra.CrossCutting.Identity;
using Hcqn.Infra.CrossCutting.Jwt;
using Hcqn.Infra.EntityFramework.PostgreSQL;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace Hcqn.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///
        /// </summary>
        protected IConfigurationRoot Configuration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="container"></param>
        public void ConfigureContainer(ContainerBuilder container)
        {
            container.RegisterPostgreSqlDataBase(Configuration.GetConnectionString("DbConnection"));
            container.RegisterBus();
            container.RegisterNotificatons();

            container.ConfigureEventSourcing<EventStoreEventSourcingContext>();
            container.RegisterUnitOfWork<SitPostgreSqlContext>();
            container.RegisterAdministracaoUsuario<SitPostgreSqlContext>();
            container.RegisterAdministracaoPessoas<SitPostgreSqlContext>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddApiVersioning(
               options =>
               {
                   options.ReportApiVersions = true;
               });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddControllers()
                .AddControllersAsServices()
                .AddMvcOptions(config =>
                {
                    config.CacheProfiles.Add("Default1hr", new Microsoft.AspNetCore.Mvc.CacheProfile()
                    {
                        Duration = 3600,
                        Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.Any,
                        NoStore = true
                    });

                    //Filtros
                    config.Filters.Add(typeof(HttpGlobalExceptionFilter));
                    config.Filters.Add(typeof(ValidateModelStateFilter));

                    config.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(Point)));
                    config.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(LineString)));
                    config.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(MultiLineString)));
                    config.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(MultiPolygon)));
                })
                .AddFluentValidation()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                    foreach (var converter in GeoJsonSerializer.Create(new GeometryFactory(new PrecisionModel())).Converters)
                    {
                        options.SerializerSettings.Converters.Add(converter);
                    }
                });

            services.AddRedisCache(Configuration.GetSection("Redis:ConnectionString").Get<string>());

            services.AddOpenTracingAndJaeger();

            services.AddSwagger();

            AddAuthentication(services);
            RegisterDbContexts(services);

            services.AddMediatR(typeof(Startup).Assembly);
        }

        /// <summary>
        ///
        /// </summary>
        public ILifetimeScope AutofacContainer { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        public virtual void RegisterDbContexts(IServiceCollection services)
        {
            services.ConfigureEventoSourcingPostgreSqlDbContext(Configuration.GetConnectionString("EventStoreDbConnection"));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        public virtual void UseAuthentication(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMiddleware(typeof(AuthenticatedRequestMiddleware));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        public virtual void AddAuthentication(IServiceCollection services)
        {
            //Não Mudar a ordem.
            services.ConfigureIdentity<SitPostgreSqlContext>();
            services.ConfigureJwtAuthorization();

            services.AddScoped<IJwtAutenticationService, JwtAutenticationService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseRouting();

            UseAuthentication(app);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(provider);
        }
    }
}