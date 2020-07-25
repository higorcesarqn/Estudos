using Hcqn.Core.Commands;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.Create
{
    public class CreateNewUserCommand : UserCommand, ICommand
    {
        public CreateNewUserCommand(string nome, string login, string email, string senha, string confirmaSenha, IEnumerable<string> roles)
        {
            Id = Guid.NewGuid();
            Name = nome;
            UserName = login;
            Email = email;
            Password = senha;
            ConfirmPassword = confirmaSenha;
            Roles = roles ?? Enumerable.Empty<string>();
        }

        public ValidationResult ValidationResult { get; set; }
        public Guid AggregateId { get; set; }

        public async Task<bool> IsValid()
        {
            ValidationResult = await new CreateNewUserCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
