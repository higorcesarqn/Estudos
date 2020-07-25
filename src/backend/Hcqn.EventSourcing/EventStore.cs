using Hcqn.Core.Events.Abstractions;
using Hcqn.Core.Identity.Abstractions;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hcqn.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public EventStore(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public Task Save(IEvent theEvent)
        {
            var serializedData = JsonSerializer.Serialize(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Username);

            return _eventStoreRepository.Store(storedEvent);
        }

    }
}
