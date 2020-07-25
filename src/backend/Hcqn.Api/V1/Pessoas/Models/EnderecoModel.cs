using Hcqn.Core.Models;
using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hcqn.Api.V1.Pessoas.Models
{
    public class EnderecoModel
    {
        public string CEP { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public int IdTipoEndereco { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }

    public class EnderecoModelValidation : AbstractValidator<EnderecoModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public EnderecoModelValidation()
        {
            ValidarCEP();
            ValidarUF();
            ValidarCidade();
            ValidarBairro();
            ValidarTipoEndereco();
            ValidarLogradouro();
            ValidarNumero();
        }

        public void ValidarNumero()
        {

        }

        public void ValidarLogradouro()
        {
            RuleFor(x => x.Logradouro)
               .NotEmpty().WithMessage("O Logradouro é obrigatória.");

            RuleFor(x => x.Logradouro)
                .MinimumLength(3).WithMessage("Logradouro Inválido.");

            RuleFor(x => x.Logradouro)
               .MaximumLength(350).WithMessage("Logradouro Inválido.");
        }

        public void ValidarTipoEndereco()
        {
            var tipos = Enumeration.GetAll<TipoEndereco>().Select(x => x.Id); ;

            RuleFor(x => x.IdTipoEndereco).Custom((IdTipoTelefone, context) => {
                if (!tipos.Contains(IdTipoTelefone))
                {
                    context.AddFailure("IdTipoEndereco", "Tipo de Endereço inválido.");
                }
            });
        }

        public void ValidarBairro()
        {
            RuleFor(x => x.Cidade)
               .NotEmpty().WithMessage("O Bairro é obrigatória.");

            RuleFor(x => x.Cidade)
                .MinimumLength(3).WithMessage("Bairro Inválida.");

            RuleFor(x => x.Cidade)
               .MaximumLength(100).WithMessage("Bairro Inválida.");
        }

        public void ValidarCidade()
        {
            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("A Cidade é obrigatória.");

            RuleFor(x => x.Cidade)
                .MinimumLength(3).WithMessage("Cidade Inválida.");

            RuleFor(x => x.Cidade)
               .MaximumLength(50).WithMessage("Cidade Inválida.");
        }

        public void ValidarCEP()
        {
            RuleFor(x => x.CEP)
                .Custom((cep, context) => {
                    cep = cep
                    .Replace(".", "")
                    .Replace("-", "")
                    .Replace(" ", "");

                    Regex Rgx = new Regex(@"^\d{8}$");
                    if (!Rgx.IsMatch(cep))
                    {
                        context.AddFailure("Cep", "O Cep é Inválido.");
                    }
                });
        }

        public void ValidarUF()
        {
            var ufs = new[]
            {
                "AC","AL", "AP","AM","BA",
                "CE","DF","ES","GO","MA",
                "MT","MS", "MG","PA","PB",
                "PR","PE","PI","RJ","RN",
                "RS","RO","RR","SC","SP",
                "SE","TO"
            };

            RuleFor(x => x.Uf).Custom((uf, context) => {

                if(!ufs.Contains(uf))
                {
                    context.AddFailure("UF", "A UF é Inválida.");
                }
            });
        }
    }
}
