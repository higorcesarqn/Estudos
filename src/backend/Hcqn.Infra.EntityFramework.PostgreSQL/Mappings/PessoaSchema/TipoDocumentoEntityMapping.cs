using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using Hcqn.Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcqn.Infra.EntityFramework.PostgreSQL.Mappings.PessoaSchema
{
    public class TipoDocumentoEntityMapping : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.ToTable(TableConsts.PessoaSchema.TipoDocumento, TableConsts.PessoaSchema.DefaultSchema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasColumnName("nome")
                .IsRequired(true)
                .HasMaxLength(30);

            builder.Property(x => x.Descricao)
                .IsRequired(true)
                .HasMaxLength(250);

            builder.HasOne(x => x.TipoPessoa)
                .WithMany()
                .HasForeignKey("id_tipo_pessoa");
        }
    }
}
