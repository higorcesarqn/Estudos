using Hcqn.Core.Events.Abstractions;
using System;

namespace Hcqn.Core.Notifications.Abstractions
{
    public class DomainNotification : IEvent
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public Guid AggregateId { get ; set; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
        }
    }
}