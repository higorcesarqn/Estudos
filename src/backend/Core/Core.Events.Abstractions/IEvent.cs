using System;

namespace Hcqn.Core.Events.Abstractions
{
    public interface IEvent : IMessage
    {
        public DateTime Timestamp => DateTime.Now;
    }
}