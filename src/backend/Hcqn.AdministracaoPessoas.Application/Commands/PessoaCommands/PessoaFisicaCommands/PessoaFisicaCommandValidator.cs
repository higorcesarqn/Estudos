using Hcqn.Core.Models.Extensions.CustomValidators;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using FluentValidation;
using System;

namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.PessoaFisicaCommands
{
    public class PessoaFisicaCommandValidator<TPessoaFisicaCommand> : AbstractValidator<TPessoaFisicaCommand>
        where TPessoaFisicaCommand : PessoaFisicaCommand
    {
        protected void IdValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(default(Guid))
                .WithMessage("Pessoa Inválido.");
        }

        protected void NomeValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O Nome é obrigatorio.");
        }

        protected void EmailValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("O Email é inválido.");
        }

        public void TipoTelefoneValidator()
        {
            RuleForEach(x => x.Telefones).SetValidator(new TelefoneModelValidator()).When(x => x.Telefones != null);
        }

        public void TipoEnderecoValidator()
        {
            RuleForEach(x => x.Enderecos).SetValidator(new EnderecoModelValidator()).When(x => x.Enderecos != null);
        }

        public void TipoDocumentoValidator()
        {
            RuleForEach(x => x.Documentos).SetValidator(new DocumentoModelValidator(TipoPessoa.PessoaFisica)).When(x => x.Documentos != null);
        }

        public void EscolaridadeValidator()
        {
            RuleFor(x => x.Escolaridade).IsEnumeration<TPessoaFisicaCommand, Escolaridade>("A Escolaridade é Inválida.", false);
        }
    }
}
