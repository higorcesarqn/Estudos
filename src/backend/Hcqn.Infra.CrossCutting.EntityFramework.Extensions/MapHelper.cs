using Hcqn.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcqn.Infra.EntityFramework.Extensions
{
    public static class MapHelper
    {
        private static void ConfigureCreateAtAndUpdteAt<T>(this EntityTypeBuilder<T> entity) where T : Entity
        {
            entity.Property(e => e.CreatedAt)
                .HasColumnName("data_inclusao")
                .HasDefaultValueSql("Now()");

            entity.Property(e => e.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("data_atualizacao");
        }

        public static void IgnoreCreateAtAndUpdteAt<T>(this EntityTypeBuilder<T> entity) where T : Entity
        {
            entity.Ignore(e => e.CreatedAt);
            entity.Ignore(e => e.UpdatedAt);
        }

        public static void ConfigureEntityToNpsql<T>(this EntityTypeBuilder<T> entity, string keyName, bool ignoreCreateAndUpdateAt = false) where T : Entity
        {
            ConfigureKey(entity, keyName);

            if(ignoreCreateAndUpdateAt)
            {
                IgnoreCreateAtAndUpdteAt(entity);
            }
            else
            {
                ConfigureCreateAtAndUpdteAt(entity);
            }
        }

        public static void ConfigureKey<T>(this EntityTypeBuilder<T> entity, string keyName) where T : Entity
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(e => e.Id)
               .HasColumnName(keyName);
        }

    }
}
