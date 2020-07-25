namespace Hcqn.Core.Events.Abstractions
{
    public interface IEventSerializer
    {
        T Deserialize<T>(string serialization) where T : IEvent;

        string Serialize<T>(T theEvent, bool indented = false) where T : IEvent;
    }
}