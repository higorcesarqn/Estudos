using System;

namespace Hcqn.Core.Events.Abstractions
{
    public class StoredEvent : IEvent
    {
        public StoredEvent(IMessage theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            Action = theEvent.MessageType;
            Data = data;
            User = user;
        }

        // EF Constructor
        protected StoredEvent() { }

        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
        public Guid AggregateId { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}