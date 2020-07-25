using Hcqn.Core.Types;
using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hcqn.Infra.EntityFramework.InMemory
{
    public class SitInMemoryContext : DbContext
    {
        public SitInMemoryContext(DbContextOptions<SitInMemoryContext> options) : base(options)
        {
        }

        public SitInMemoryContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var pessoa = modelBuilder.Entity<Pessoa>();

            pessoa.Property(x => x.Email)
                .HasConversion(
                 email => email.ToString(),
                 value => Email.Parse(value)
                );

            pessoa.Property(x => x.Tipo)
                .HasColumnName("tipo")
                .HasConversion(
                    entity => JsonConvert.SerializeObject(entity),
                     value => JsonConvert.DeserializeObject<TipoPessoa>(value))
                .IsRequired();

            pessoa.Property(x => x.Escolaridade)
                .HasColumnName("escolaridade")
                .HasConversion(
                    entity => JsonConvert.SerializeObject(entity),
                     value => JsonConvert.DeserializeObject<Escolaridade>(value))
                .IsRequired(false);

            pessoa.Property(x => x.Documentos)
                .HasColumnName("documentos")
                .HasConversion(
                    entity => JsonConvert.SerializeObject(entity),
                     value => JsonConvert.DeserializeObject<IEnumerable<Documento>>(value));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbName = Guid.NewGuid().ToString();

            optionsBuilder.UseInMemoryDatabase(dbName);
            optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            base.OnConfiguring(optionsBuilder);
        }
    }
}