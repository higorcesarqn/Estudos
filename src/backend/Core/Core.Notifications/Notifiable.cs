using Hcqn.Core.Bus.Abstractions;
using Hcqn.Core.Notifications.Abstractions;
using System.Threading.Tasks;

namespace Hcqn.Core.Notifications
{
    public class Notifiable : INotifiable
    {
        private readonly IMediatorHandler _bus;
        private readonly INotifications _notifications;

        public Notifiable(IMediatorHandler bus, INotifications notifications)
        {
            _bus = bus;
            _notifications = notifications;
        }

        public Task Notify(DomainNotification notification)
        {
            return _bus.RaiseEvent(notification);
        }

        public bool IsValid()
        {
            return !_notifications.HasNotifications();
        }
    }
}