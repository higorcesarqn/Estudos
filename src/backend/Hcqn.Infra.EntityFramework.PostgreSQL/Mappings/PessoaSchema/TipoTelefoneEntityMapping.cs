using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using Hcqn.Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcqn.Infra.EntityFramework.PostgreSQL.Mappings.PessoaSchema
{
    public class TipoTelefoneEntityMapping : IEntityTypeConfiguration<TipoTelefone>
    {
        public void Configure(EntityTypeBuilder<TipoTelefone> builder)
        {
            builder.ToTable(TableConsts.PessoaSchema.TipoTelefone, TableConsts.PessoaSchema.DefaultSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasColumnName("nome");
        }
    }
}
