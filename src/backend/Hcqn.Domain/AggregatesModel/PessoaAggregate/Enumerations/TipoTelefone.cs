using Hcqn.Core.Models;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations
{
    public class TipoTelefone : Enumeration
    {
        protected TipoTelefone(int id, string name) : base(id, name) { }
        protected TipoTelefone()
        {

        }
        public static TipoTelefone Residencial = new ResidencialTipoTelefone();
        public static TipoTelefone Comercial = new ComercialTipoTelefon();
        public static TipoTelefone Celular = new CelularTipoTelefone();

        public static implicit operator TipoTelefone(int id)
        {
            return FromValue<TipoTelefone>(id);
        }

        public static implicit operator TipoTelefone(string name)
        {
            return FromDisplayName<TipoTelefone>(name);
        }

        public static implicit operator string(TipoTelefone tipoTelefone)
        {
            return tipoTelefone.Name;
        }

        public static implicit operator int(TipoTelefone tipoTelefone)
        {
            return tipoTelefone.Id;
        }
        private class ResidencialTipoTelefone : TipoTelefone
        {
            public ResidencialTipoTelefone() : base(1, "Residencial") { }
        }

        private class ComercialTipoTelefon : TipoTelefone
        {
            public ComercialTipoTelefon() : base(2, "Comercial") { }
        }

        private class CelularTipoTelefone : TipoTelefone
        {
            public CelularTipoTelefone() : base(3, "Celular") { }
        }
    }
}
