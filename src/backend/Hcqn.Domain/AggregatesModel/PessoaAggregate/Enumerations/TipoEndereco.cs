using Hcqn.Core.Models;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations
{
    public class TipoEndereco : Enumeration
    {
        public TipoEndereco(int id, string name) : base(id, name) { }

        protected TipoEndereco() { }

        public static TipoEndereco Residencial = new ResidencialTipoEndereco();
        public static TipoEndereco Comercial = new ComercialTipoEndereco();

        public static implicit operator TipoEndereco(int id) => FromValue<TipoEndereco>(id);

        private class ResidencialTipoEndereco : TipoEndereco
        {
            public ResidencialTipoEndereco() : base(1, "Residencial") { }
        }

        private class ComercialTipoEndereco : TipoEndereco
        {
            public ComercialTipoEndereco() : base(2, "Comercial") { }
        }
    }
}
