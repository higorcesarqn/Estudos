using Hcqn.Core.Types;
using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Hcqn.Infra.EntityFramework.Extensions;
using Hcqn.Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcqn.Infra.EntityFramework.PostgreSQL.Mappings.PessoaSchema
{
    public class PessoaEntityMapping : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable(TableConsts.PessoaSchema.Pessoa, TableConsts.PessoaSchema.DefaultSchema);

            builder.ConfigureEntityToNpsql("id_pessoa");

            builder.Property(x => x.Nome)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasConversion(
                 email => email.ToString(),
                 value => Email.Parse(value)
                );

            builder.Property(x => x.Nascimento)
                .HasColumnType("DATE")
                .IsRequired(false);

            builder.Property(x => x.RazaoSocial)
                .HasMaxLength(512)
                .IsRequired(false);

            builder.Property(x => x.Deficiente)
              .IsRequired(false);

            builder.Property(x => x.Estuda)
              .IsRequired(false);

            builder.HasOne(x => x.Tipo)
                .WithMany()
                .HasForeignKey("id_tipo_pessoa")
                .IsRequired(true)
                .HasConstraintName("tbl_pessoa_tbl_tipo_pessoa_fk");

            builder.HasOne(x => x.Escolaridade)
                .WithMany()
                .HasForeignKey("id_escolaridade")
                .IsRequired(false)
                .HasConstraintName("tbl_pessoa_tbl_escolaridade_fk");

            builder.HasMany(x => x.Telefones)
                .WithOne()
                .HasForeignKey("id_pessoa")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("tbl_telefone_tbl_pessoa_fk");

            builder.HasMany(x => x.Enderecos)
                .WithOne()
                .HasForeignKey("id_pessoa")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("tbl_endereco_tbl_pessoa_fk");
        }
    }
}