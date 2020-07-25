using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Hcqn.Infra.EntityFramework.Extensions;
using Hcqn.Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcqn.Infra.EntityFramework.PostgreSQL.Mappings.PessoaSchema
{
    public class DocumentoEntityMapping : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable(TableConsts.PessoaSchema.Documento, TableConsts.PessoaSchema.DefaultSchema);

            builder.ConfigureEntityToNpsql("id_documento");

            builder.Property(x => x.Valor)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Tipo)
                .HasColumnType("json");

            builder.HasOne(x => x.Pessoa)
                .WithMany(x => x.Documentos)
                .HasForeignKey("id_pessoa")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("tbl_documento_tbl_pessoa_fk");

            builder.HasIndex(x => x.Valor);
        }
    }
}
