using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using Hcqn.Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcqn.Infra.EntityFramework.PostgreSQL.Mappings.PessoaSchema
{
    public class EscolaridadeEntityMapping : IEntityTypeConfiguration<Escolaridade>
    {
        public void Configure(EntityTypeBuilder<Escolaridade> builder)
        {
            builder.ToTable(TableConsts.PessoaSchema.Escolaridade, TableConsts.PessoaSchema.DefaultSchema);

            builder.Property(x => x.Name)
                .HasColumnName("nome");

            builder.HasKey(x => x.Id);
        }
    }
}
