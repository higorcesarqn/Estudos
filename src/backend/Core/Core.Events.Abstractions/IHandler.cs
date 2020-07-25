namespace Hcqn.Core.Events.Abstractions
{
    public interface IHandler<in T> where T : IMessage
    {
        void Handle(T message);
    }
}