using Hcqn.Core.Models;
using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hcqn.Api.V1.Pessoas.Models
{
    public class AdicionarPessoaModel
    {
        public AdicionarPessoaModel()
        {
            //Telefones = Enumerable.Empty<TelefoneModel>();
            //Enderecos = Enumerable.Empty<EnderecoModel>();
            //Documentos = Enumerable.Empty<DocumentoModel>();
        }

        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public DateTime? Nascimento { get; set; }
        public int IdTipoPessoa { get; set; }
        public int? IdEscolaridade { get; set; }

        public bool? Deficiente { get; set; }
        public bool? Estuda { get; set; }

        public IEnumerable<DocumentoModel> Documentos { get; set; }
        public IEnumerable<TelefoneModel> Telefones { get; set; }
        public IEnumerable<EnderecoModel> Enderecos { get; set; }
    }

    public class AdicionarPessoaModelValidation : AbstractValidator<AdicionarPessoaModel>
    {
        public AdicionarPessoaModelValidation()
        {
            ValidarNome();
            ValidarEmail();
            ValidarTipoPessoa();
            ValidarEscolaridade();
            ValidarTelefones();
            ValidarEnderecos();

        }

        public void ValidarEnderecos()
        {
            RuleForEach(x => x.Enderecos).SetValidator(x => new EnderecoModelValidation());
        }

        public void ValidarTelefones()
        {
            RuleForEach(x => x.Telefones).SetValidator(x => new TelefoneModelValidation());
        }

        public void ValidarEscolaridade()
        {
            var tipos = Enumeration.GetAll<Escolaridade>().Select(x => x.Id); ;

            RuleFor(x => x.IdEscolaridade).Custom((IdEscolaridade, context) =>
            {
                if (IdEscolaridade.HasValue &&  !tipos.Contains(IdEscolaridade.Value))
                {
                    context.AddFailure("IdEscolaridade", "Escolaridade inválido.");
                }
            }) ;
        }


        public void ValidarTipoPessoa()
        {
            var tipos = Enumeration.GetAll<TipoPessoa>().Select(x => x.Id);

            RuleFor(x => x.IdTipoPessoa).Custom((IdTipoTelefone, context) => {
                if (!tipos.Contains(IdTipoTelefone))
                {
                    context.AddFailure("IdTipoPessoa", "Tipo de Pessoa inválido.");
                }
            });
        }

        public void ValidarEmail()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email Inválido.");
        }

       

        public void ValidarNome()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome Inválido.");

            RuleFor(x => x.Nome)
                .MinimumLength(3).WithMessage("Nome Inválido");

            RuleFor(x => x.Nome)
                .MaximumLength(250).WithMessage("Nome Inválido");



        }
    }




}
