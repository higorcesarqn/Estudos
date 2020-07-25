using Hcqn.Core.Events.Abstractions;
using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;
using Unit = Hcqn.Core.Types.Unit;

namespace Hcqn.Core.Commands
{
    public interface ICommand : ICommandBase, IRequest<Unit> { }

    public interface ICommand<TResponse> : ICommandBase, IRequest<TResponse> { }

    public interface ICommandBase : IEvent
    {
        ValidationResult ValidationResult { get; set; }
        Task<bool> IsValid();
    }
}