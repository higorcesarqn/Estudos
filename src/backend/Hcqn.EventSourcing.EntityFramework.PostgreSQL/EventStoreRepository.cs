using Hcqn.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hcqn.Core.Events.Abstractions;

namespace Hcqn.EventSourcing.EntityFramework.PostgreSQL
{
    public class EventStoreRepository<TContext> : IEventStoreRepository
        where TContext : DbContext
    {
        private readonly DbSet<StoredEvent> _dbSet;
        private readonly TContext _context;

        public EventStoreRepository(TContext context)
        {
            _dbSet = context.Set<StoredEvent>();
            _context = context;
        }

        public async Task<IEnumerable<StoredEvent>> All(Guid aggregateId)
        {
            return await _dbSet.Where(x => x.AggregateId == aggregateId).ToListAsync();
        }

        public async Task Store(StoredEvent theEvent)
        {
            await _dbSet.AddAsync(theEvent);
            await _context.SaveChangesAsync();
        }
    }
}
