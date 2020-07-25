using MediatR;
using System;

namespace Hcqn.Core.Events.Abstractions
{
    public interface IMessage : INotification
    {
        public string MessageType => GetType().Name;
        Guid AggregateId { get; set; }
    }
}