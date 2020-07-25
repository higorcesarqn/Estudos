using Hcqn.Core.Models;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate
{
    public class Telefone : Entity
    {
        public Telefone(TipoTelefone tipo, string ddd, string numero)
        {
            Tipo = tipo;
            DDD = ddd;
            Numero = numero;
        }

        protected Telefone() { }
        public TipoTelefone Tipo { set; get; }
        public string DDD { get; set; } 
        public string Numero { get; set; }
    }
}
 