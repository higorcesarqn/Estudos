using Hcqn.Core.Models;
using Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate
{
    public class Endereco : Entity
    {
        public Endereco(TipoEndereco tipo, string cep, string uf, string cidade, string bairro,
            string logradouro, string numero, string complemento)
        {
            Cep = cep;
            Uf = uf;
            Cidade = cidade;
            Bairro = bairro;
            Tipo = tipo;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
        }

        protected Endereco() { }

        public string Cep { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public TipoEndereco Tipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }

    }
}
