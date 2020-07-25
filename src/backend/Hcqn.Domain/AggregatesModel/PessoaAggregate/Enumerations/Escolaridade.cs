using Hcqn.Core.Models;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations
{
    public class Escolaridade : Enumeration
    {
        protected Escolaridade(int id, string name) : base(id, name) { }

        protected Escolaridade()
        {

        }

        public static Escolaridade Fundamental = new FundamentalEscolaridade();
        public static Escolaridade FundamentalIncompleto = new FundamentalIncompletoEscolaridade();
        public static Escolaridade Medio = new MedioEscolaridade();
        public static Escolaridade MedioIncompleto = new MedioIncompletoEscolaridade();
        public static Escolaridade Superior = new SuperiorEscolaridade();
        public static Escolaridade SuperiorIncompleto = new SuperiorIncompletoEscolaridade();
        public static Escolaridade PosGraduacao = new PosGraduacaoEscolaridade();
        public static Escolaridade Mestrado = new MestradoEscolaridade();
        public static Escolaridade Doutorado = new DoutoradoEscolaridade();
        public static Escolaridade NaoAlfabetizado = new NaoAlfabetizadoEscolaridade();

        public static implicit operator Escolaridade(int id)
        {
            return FromValue<Escolaridade>(id);
        }

        public static implicit operator Escolaridade(int? id)
        {
            return !id.HasValue ? default : FromValue<Escolaridade>(id.Value);
        }

        private class FundamentalEscolaridade : Escolaridade
        {
            public FundamentalEscolaridade() : base(1, "Fundamental") { }
        }

        private class FundamentalIncompletoEscolaridade : Escolaridade
        {
            public FundamentalIncompletoEscolaridade() : base(2, "Fundamental Incompleto") { }
        }

        private class MedioEscolaridade : Escolaridade
        {
            public MedioEscolaridade() : base(3, "Médio") { }
        }

        private class MedioIncompletoEscolaridade : Escolaridade
        {
            public MedioIncompletoEscolaridade() : base(4, "Médio Incompleto") { }
        }

        private class SuperiorEscolaridade : Escolaridade
        {
            public SuperiorEscolaridade() : base(5, "Superior") { }
        }

        private class SuperiorIncompletoEscolaridade : Escolaridade
        {
            public SuperiorIncompletoEscolaridade() : base(6, "Superior Incompleto") { }
        }

        private class PosGraduacaoEscolaridade : Escolaridade
        {
            public PosGraduacaoEscolaridade() : base(7, "Pós-Graduação") { }
        }

        private class MestradoEscolaridade : Escolaridade
        {
            public MestradoEscolaridade() : base(8, "Mestrado") { }
        }

        private class DoutoradoEscolaridade : Escolaridade
        {
            public DoutoradoEscolaridade() : base(9, "Doutorado") { }
        }

        private class NaoAlfabetizadoEscolaridade : Escolaridade
        {
            public NaoAlfabetizadoEscolaridade() : base(10, "Não Alfabetizado") { }
        }

    }
}
