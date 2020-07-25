using Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.Dtos;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using FluentValidation;
using Hcqn.Core.Models.Extensions.CustomValidators;

namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands
{
    public class TelefoneModelValidator : AbstractValidator<TelefoneDto>
    {

        public TelefoneModelValidator()
        {
            TipoValidator();
        }
        public void TipoValidator()
        {
            RuleFor(x => x.Tipo)
                .IsEnumeration<TelefoneDto, TipoTelefone>("O Tipo de Telefone é Inválido");
        }
    }
}
