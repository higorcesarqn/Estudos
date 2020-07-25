using Hcqn.Core.Models;
using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;
using FluentValidation;
using System.Linq;

namespace Hcqn.Api.V1.Pessoas.Models
{
    public class TelefoneModel
    {
        public int IdTipoTelefone { set; get; }
        public string DDD { get; set; }
        public string Numero { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TelefoneModelValidation : AbstractValidator<TelefoneModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public TelefoneModelValidation()
        {
            ValidarDDD();
            ValidarNumero();
            ValidarTipo();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ValidarTipo()
        {
            var tipos = Enumeration.GetAll<TipoTelefone>().Select(x => x.Id); ;

            RuleFor(x => x.IdTipoTelefone).Custom((IdTipoTelefone, context)=> {
                if(!tipos.Contains(IdTipoTelefone))
                {
                    context.AddFailure("IdTipoTelefone", "Tipo de Telefone inválido.");
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ValidarDDD()
        {
            RuleFor(x => x.DDD)
                .NotEmpty().WithMessage(x => $"O DDD {x.DDD} esta inválido.");
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ValidarNumero()
        {
            RuleFor(x => x.Numero)
                .MinimumLength(8)
                .WithMessage(x => $"O Numero {x.Numero}  é inválido.");

            RuleFor(x => x.Numero)
                .NotEmpty()
                .WithMessage(x => $"O Numero tem que ser preenchido.");
        }
    }
}
