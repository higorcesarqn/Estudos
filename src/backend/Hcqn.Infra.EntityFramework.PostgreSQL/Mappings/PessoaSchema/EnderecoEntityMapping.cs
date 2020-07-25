using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Hcqn.Infra.EntityFramework.Extensions;
using Hcqn.Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcqn.Infra.EntityFramework.PostgreSQL.Mappings.PessoaSchema
{
    public class EnderecoEntityMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable(TableConsts.PessoaSchema.Endereco, TableConsts.PessoaSchema.DefaultSchema);

            builder.ConfigureEntityToNpsql("id_endereco");

            builder.Property(x => x.Uf)
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(x => x.Cidade)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Logradouro)
                .HasMaxLength(350)
                .IsRequired();

            builder.Property(x => x.Numero)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.Bairro)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Cep)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Complemento)
                .HasMaxLength(150)
                .IsRequired(false);

            builder.HasOne(x => x.Tipo)
                .WithMany()
                .HasForeignKey("id_tipo_endereco")
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("tbl_endereco_tbl_tipo_endereco_fk");

        }
    }
}
