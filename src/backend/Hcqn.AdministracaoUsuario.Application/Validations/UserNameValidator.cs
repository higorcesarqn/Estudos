using Hcqn.Core.Types;
using FluentValidation;

namespace Hcqn.AdministracaoUsuario.Application.Validations
{
    public class UserNameValidator : AbstractValidator<string>
    {
        public UserNameValidator(string propertyName = "UserName")
        {
            RuleFor(x => x)
                .NotEmpty()
                .OverridePropertyName(propertyName)
                .WithMessage("O Nome de Usuário é obrigatório.");

            RuleFor(x => x)
                .MinimumLength(UserName.MiniumLengh)
                .OverridePropertyName(propertyName)
                .When(x => !string.IsNullOrEmpty(x))
                .WithMessage($"O Nome de Usuário tem que ter {UserName.MiniumLengh} ou mais caracteres.");
        }
    }
}
