using Hcqn.Core.Models.Extensions.CustomValidators;
using Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.Dtos;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using FluentValidation;

namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands
{
    public class EnderecoModelValidator : AbstractValidator<EnderecoDto>
    {
        public EnderecoModelValidator()
        {
            TipoValidator();
        }
        public void TipoValidator()
        {
            RuleFor(x => x.Tipo)
                .IsEnumeration<EnderecoDto, TipoEndereco>("O Tipo de Endereço é Inválido");
        }
    }
}
