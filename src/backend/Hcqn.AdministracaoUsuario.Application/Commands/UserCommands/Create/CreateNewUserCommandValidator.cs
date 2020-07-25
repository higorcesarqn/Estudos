﻿namespace Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.Create
{
    public class CreateNewUserCommandValidator : UserCommandValidator<CreateNewUserCommand>
    {
        public CreateNewUserCommandValidator()
        {
            NameValidator();
            EmailValidator();
            UsernameValidator();
            ConfirmPasswordValidator();
            PasswordValidator();
        }
    }
}
