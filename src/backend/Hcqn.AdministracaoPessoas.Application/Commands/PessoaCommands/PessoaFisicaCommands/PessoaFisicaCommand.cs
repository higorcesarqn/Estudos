using Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.Dtos;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using System;
using System.Collections.Generic;

namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.PessoaFisicaCommands
{
    public abstract class PessoaFisicaCommand 
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public string Email { get; protected set; }
        public DateTime? Nascimento { get; protected set; }
        public bool? Deficiente { get; protected set; }
        public bool? Estuda { get; protected set; }

        public int? Escolaridade { get; protected set; }

        public int TipoPessoaa => TipoPessoa.PessoaFisica;

        public IEnumerable<TelefoneDto> Telefones { get; protected set; }
        public IEnumerable<DocumentoDto> Documentos { get; protected set; }
        public IEnumerable<EnderecoDto> Enderecos { get; protected set; }
    }
}
