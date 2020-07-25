using Hcqn.Core.Commands;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.Update
{
    public class UpdateUserCommand : UserCommand, ICommand
    {
        public UpdateUserCommand(Guid idUsuario, string name, string email, IEnumerable<string> roles)
        {
            Id = idUsuario;
            Name = name;
            Email = email;
            Roles = roles;
        }

        public ValidationResult ValidationResult { get; set; }
        public Guid AggregateId { get; set; }

        public async Task<bool> IsValid()
        {
            ValidationResult = await new UpdateUserCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
