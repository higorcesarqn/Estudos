using System.Collections.Generic;
using System.Linq;

namespace Hcqn.Core.Notifications.Abstractions
{
    public interface INotifications
    {
        IReadOnlyList<DomainNotification> GetNotifications();
        bool HasNotifications() => GetNotifications().Any();
    }
}
