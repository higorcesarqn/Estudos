using Hcqn.Core.Models;
using Hcqn.Core.Types;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using System;
using System.Collections.Generic;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate
{
    public class Pessoa : Entity, IAggregateRoot
    {
        /// <summary>
        /// Construtor de Pessoa Fisica
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="dataAbertura"></param>
        /// <param name="razaoSocial"></param>
        /// <param name="telefones"></param>
        /// <param name="endereco"></param>
        public Pessoa(Guid id, string nome, Email email, DateTime? dataAbertura, string razaoSocial,
            IList<Telefone> telefones, IList<Endereco> endereco, IList<Documento> documentos)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Nascimento = dataAbertura;
            Tipo = TipoPessoa.PessoaJuridica;
            RazaoSocial = razaoSocial;
            Telefones = telefones;
            Enderecos = endereco;
            Documentos = documentos;
        }

        /// <summary>
        /// Construtor de Pessoa Juridica
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="nascimento"></param>
        /// <param name="deficiente"></param>
        /// <param name="estuda"></param>
        /// <param name="telefones"></param>
        /// <param name="enderecos"></param>
        public Pessoa(Guid id, string nome, Email email, DateTime? nascimento, Escolaridade escolaridade,
            bool? deficiente, bool? estuda, IList<Telefone> telefones, IList<Endereco> enderecos, IList<Documento> documentos)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Nascimento = nascimento;
            Tipo = TipoPessoa.PessoaFisica;
            Deficiente = deficiente;
            Estuda = estuda;
            Telefones = telefones;
            Enderecos = enderecos;
            Escolaridade = escolaridade;
            Documentos = documentos;
        }

        protected Pessoa() { }

        public string Nome { get; protected set; }
        public Email Email { get; protected set; }
        public DateTime? Nascimento { get; protected set; }
        public TipoPessoa Tipo { get; protected set; }
        public Escolaridade Escolaridade { get; protected set; }
        public string RazaoSocial { get; protected set; }
        public bool? Deficiente { get; protected set; }
        public bool? Estuda { get; protected set; }

        public IEnumerable<Telefone> Telefones { get; protected set; }
        public IEnumerable<Endereco> Enderecos { get; protected set; }
        public IEnumerable<Documento> Documentos { get; protected set; }
    }
}
