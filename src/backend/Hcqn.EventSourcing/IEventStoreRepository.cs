using Hcqn.Core.Events.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hcqn.EventSourcing
{
    public  interface IEventStoreRepository
    {
        Task Store(StoredEvent theEvent);

        Task<IEnumerable<StoredEvent>> All(Guid aggregateId);
    }
}
