namespace Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordCommandValidator : UserCommandValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            PasswordValidator();
            ConfirmPasswordValidator();
        }
    }
}
