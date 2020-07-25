using Hcqn.Infra.CrossCutting.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hcqn.Infra.EntityFramework.PostgreSQL
{
    public class SitPostgreSqlContext : IdentityDbContext<User, Role, Guid,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public SitPostgreSqlContext(DbContextOptions<SitPostgreSqlContext> options) :
         base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .HasPostgresExtension("pgcrypto");

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly(typeof(Mappings.PessoaSchema.PessoaEntityMapping).Assembly);
            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly(typeof(Mappings.IdentidadeSchema.PermissionEntityMapping).Assembly);
            modelBuilder.ApplySnakeCaseInColumnName();

        }
    }
}
