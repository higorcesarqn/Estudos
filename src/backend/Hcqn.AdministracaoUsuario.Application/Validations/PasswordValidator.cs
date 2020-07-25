using FluentValidation;

namespace Hcqn.AdministracaoUsuario.Application.Validations
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .OverridePropertyName("Senha")
                .WithMessage("A Senha é obrigatória.");

            RuleFor(x => x)
                .MinimumLength(6)
                .OverridePropertyName("Senha")
                .When(x => !string.IsNullOrEmpty(x))
                .WithMessage($"A Senha tem que ter 6 ou mais caracteres.");
        }
    }
}
