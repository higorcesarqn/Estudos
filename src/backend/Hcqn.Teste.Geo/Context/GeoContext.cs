using Hcqn.Teste.Geo.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hcqn.Teste.Geo.Context
{
    public class GeoContext : DbContext
    {
        public GeoContext(DbContextOptions<GeoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("postgis")
                .HasPostgresExtension("tablefunc");

            var entity = modelBuilder.Entity<Lote>();

            entity.Property(x => x.Id)
                .HasColumnName("id_lote")
                .HasDefaultValueSql("gen_random_uuid()");

            entity.ToTable("geo_lote", "public");

            entity.Property(e => e.CodigoPlantaQuadra);

            entity.Property(e => e.Geom)
                .HasColumnName("geom");

            entity.Property(e => e.InscricaoFiscal);

            entity.Property(e => e.LoteProjetado);

            entity.Property(e => e.NumeroLote);

            entity.Property(e => e.CreatedAt)
                .HasColumnName("data_inclusao")
                .HasDefaultValueSql("Now()");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("data_atualizacao")
                .HasDefaultValueSql("Now()");

            modelBuilder.ApplySnakeCaseInColumnName();
        }
    }
}
