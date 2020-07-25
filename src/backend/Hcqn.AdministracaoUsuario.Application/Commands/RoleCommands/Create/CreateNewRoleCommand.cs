using Hcqn.Core.Commands;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Create
{
    public class CreateNewRoleCommand : RoleCommand, ICommand
    {
        public CreateNewRoleCommand(string nome, IEnumerable<string> permissions) : base(nome, permissions) { }

        public ValidationResult ValidationResult { get; set; }
        public Guid AggregateId { get; set; }

        public async Task<bool> IsValid()
        {
            ValidationResult = await new CreateNewRoleCommandValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
