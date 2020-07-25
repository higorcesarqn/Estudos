using Hcqn.Domain.AggregatesModel.RoleAggregate;
using Hcqn.Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcqn.Infra.EntityFramework.PostgreSQL.Mappings.IdentidadeSchema
{
    public class PermissionEntityMapping : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> entity)
        {
            entity.ToTable(TableConsts.IdentidadeSchema.Permission, TableConsts.IdentidadeSchema.DefaultSchema);

            entity.HasKey(k => k.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(p => p.Titulo)
                .HasMaxLength(256)
                .IsRequired(false);

            entity.Property(x => x.Nome)
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}
