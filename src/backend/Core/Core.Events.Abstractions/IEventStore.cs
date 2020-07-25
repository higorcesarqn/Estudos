using System.Threading.Tasks;

namespace Hcqn.Core.Events.Abstractions
{
    public interface IEventStore
    {
        Task Save(IEvent theEvent);
    }
}
