using Hcqn.Core.Commands;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordCommand : UserCommand, ICommand
    {
        public ChangePasswordCommand(Guid idUser, string password, string confirmPassword)
        {
            Id = idUser;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public ValidationResult ValidationResult { get; set; }
        public Guid AggregateId { get; set; }

        public async Task<bool> IsValid()
        {
            ValidationResult = await new ChangePasswordCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
