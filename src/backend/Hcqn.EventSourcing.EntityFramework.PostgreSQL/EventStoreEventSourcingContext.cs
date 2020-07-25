using System.Threading.Tasks;
using Hcqn.Core.Events;
using Hcqn.Core.Events.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Hcqn.EventSourcing.EntityFramework.PostgreSQL
{
    public class EventStoreEventSourcingContext : DbContext, IEventSourcingContext
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }

        public EventStoreEventSourcingContext(DbContextOptions<EventStoreEventSourcingContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("tablefunc");

            modelBuilder.Entity<StoredEvent>(entity =>
            {
                entity.ToTable("tbl_stored_event", "store_event");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Action)
                    .HasColumnName("action");

                entity.Property(e => e.AggregateId)
                    .IsRequired()
                    .HasColumnName("aggregate_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("create_at");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("json");

                entity.Property(e => e.User)
                    .HasColumnName("usuario");
            });
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
