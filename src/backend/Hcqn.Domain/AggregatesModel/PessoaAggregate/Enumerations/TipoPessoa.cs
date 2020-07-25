using Hcqn.Core.Models;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations
{
    public class TipoPessoa : Enumeration
    {
        public static TipoPessoa PessoaFisica = new PessoaFisicaTipo();
        public static TipoPessoa PessoaJuridica = new PessoaJuridicaTipo();

        public TipoPessoa(int id, string name) : base(id, name)
        {
        }

        protected TipoPessoa()
        {

        }

        private class PessoaFisicaTipo : TipoPessoa
        {
            public PessoaFisicaTipo()
                : base(1, "Pessoa Fisica") { }
        }

        private class PessoaJuridicaTipo : TipoPessoa
        {
            public PessoaJuridicaTipo()
                   : base(2, "Pessoa Juridica") { }
        }

        public static implicit operator TipoPessoa(int id)
        {
            return FromValue<TipoPessoa>(id);
        }

        public static implicit operator int(TipoPessoa tipoPessoa)
        {
            return tipoPessoa.Id;
        }
    }
}
