using Hcqn.Core.Models;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate
{
    public class Documento : Entity
    {
        public Documento(TipoDocumento tipo, string valor)
        {
            Tipo = tipo;
            Valor = valor;
        }

        protected Documento() { }

        public Pessoa Pessoa { get; set; }
        public TipoDocumento Tipo { get; set; }
        public string Valor { get; set; }
    }
}
