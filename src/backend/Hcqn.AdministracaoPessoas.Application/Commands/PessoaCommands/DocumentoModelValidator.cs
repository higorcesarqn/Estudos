using Hcqn.Core.Models.Extensions.CustomValidators;
using Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.Dtos;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using FluentValidation;

namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands
{
    public class DocumentoModelValidator : AbstractValidator<DocumentoDto>
    {
        private readonly TipoPessoa _tipoPessoa;
        public DocumentoModelValidator(TipoPessoa tipoPessoa)
        {
            _tipoPessoa = tipoPessoa;
            TipoValidator();
        }

        public void TipoValidator()
        {
            //if (_tipoPessoa == default)
            //{
            //    RuleFor(x => x.Tipo)
            //        .IsEnumeration<DocumentoDto, TipoDocumento>("O Tipo de Documento é Inválido");
            //    return;
            //}

            RuleFor(x => x.Tipo)
                .IsEnumeration<DocumentoDto, TipoDocumento>(x => Equals(x.TipoPessoa, _tipoPessoa), "O Tipo de Documento é Inválido");
        }
    }
}
