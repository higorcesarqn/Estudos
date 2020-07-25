using Hcqn.Core.Commands;
using Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.Dtos;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.PessoaFisicaCommands.Create
{
    public class CreateNewPessoaFisicaCommand : PessoaFisicaCommand, ICommand
    {
        public CreateNewPessoaFisicaCommand(string nome, string email, DateTime? nascimento,
            bool? deficiente, bool? estuda, int? escolaridade, IEnumerable<TelefoneDto> telefones,
            IEnumerable<DocumentoDto> documentos, IEnumerable<EnderecoDto> enderecos)
        {
            Id = Guid.NewGuid();
            AggregateId = Id;
            Nome = nome;
            Email = email;
            Nascimento = nascimento;
            Deficiente = deficiente;
            Estuda = estuda;
            Telefones = telefones;
            Documentos = documentos;
            Enderecos = enderecos;
            Escolaridade = escolaridade;
        }

        public Guid AggregateId { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public async Task<bool> IsValid()
        {
            ValidationResult = await new CreateNewPessoaFisicaCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }

    }
}
