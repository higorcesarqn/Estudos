using Hcqn.Core.Events.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hcqn.EventSourcing
{
    public interface IEventSourcingContext
    {
        DbSet<StoredEvent> StoredEvent { get; set; }
        Task<int> SaveChangesAsync();
    }
}
