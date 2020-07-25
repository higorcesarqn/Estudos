using Hcqn.Core.Commands;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Update
{
    public class UpdateRoleCommand : RoleCommand, ICommand
    {
        public UpdateRoleCommand(Guid idRole ,string nome, IEnumerable<string> permissions) : base(nome, permissions)
        {
            Id = idRole;
        }

        public ValidationResult ValidationResult { get ; set; }
        public Guid AggregateId { get; set; }

        public async Task<bool> IsValid()
        {
            ValidationResult = await new UpdateRoleCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
