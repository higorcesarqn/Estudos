using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Hcqn.Infra.EntityFramework.Extensions;
using Hcqn.Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcqn.Infra.EntityFramework.PostgreSQL.Mappings.PessoaSchema
{
    public class TelefoneEntityMapping : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable(TableConsts.PessoaSchema.Telefone, TableConsts.PessoaSchema.DefaultSchema);

            builder.ConfigureEntityToNpsql("id_telefone");

            builder.Property(x => x.Numero)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.DDD)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasOne(x => x.Tipo)
                .WithMany()
                .HasForeignKey("id_tipo_telefone")
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("tbl_telefone_tbl_tipo_telefone_fk");

        }
    }
}
