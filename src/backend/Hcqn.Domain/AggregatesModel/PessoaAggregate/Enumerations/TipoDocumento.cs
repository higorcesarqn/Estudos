using Hcqn.Core.Models;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate.Enumerations
{
    public class TipoDocumento : Enumeration
    {
        public TipoDocumento(int id, string name, string descricao, TipoPessoa tipoPessoa) : base(id, name)
        {
            TipoPessoa = tipoPessoa;
            Descricao = descricao;
        }

        protected TipoDocumento(int id, string name) : base(id, name)
        {

        }

        protected TipoDocumento()
        {

        }

        public static TipoDocumento Cpf = new CpfTipoDocumento();
        public static TipoDocumento Rg = new RgTipoDocumento();
        public static TipoDocumento Cnh = new CnhTipoDocumento();
        public static TipoDocumento Ctps = new CtpsTipoDocumento();
        public static TipoDocumento CertidaoNascimento = new CertidaoNascimentoTipoDocumento();
        public static TipoDocumento TituloEleitor = new TituloEleitorTipoDocumento();
        public static TipoDocumento Passaporte = new PassaporteTipoDocumento();
        public static TipoDocumento Crnm = new CrnmTipoDocument();
        public static TipoDocumento Cnpj = new CnpjTipoDocumento();
        public static TipoDocumento Alvara = new AlvaraTipoDocumento();
        public static TipoDocumento InscricaoEstadual = new InscricaoEstadualTipoDocumeto();
        public static TipoDocumento InscricaoMunicipal = new InscricaoMunicipalTipoDocumeto();

        public static implicit operator TipoDocumento(int id)
        {
            return id == 0 ? default : FromValue<TipoDocumento>(id);
        }

        public static implicit operator int(TipoDocumento tipoDocumento)
        {
            return tipoDocumento.Id;
        }

        public string Descricao { get; private set; }

        public TipoPessoa TipoPessoa { get; private set; }

        private class CpfTipoDocumento : TipoDocumento
        {
            public CpfTipoDocumento() : base(1, "CPF", "Cadastro de Pessoas Físicas", TipoPessoa.PessoaFisica) { }
        }

        private class RgTipoDocumento : TipoDocumento
        {
            public RgTipoDocumento() : base(2, "RG", "Registro Geral", TipoPessoa.PessoaFisica) { }
        }

        private class CnhTipoDocumento : TipoDocumento
        {
            public CnhTipoDocumento() : base(3, "CNH", "Carteira Nacional de Habilitação", TipoPessoa.PessoaFisica) { }
        }

        private class CtpsTipoDocumento : TipoDocumento
        {
            public CtpsTipoDocumento() : base(4, "CTPS", "Carteira de Trabalho e Previdência Social", TipoPessoa.PessoaFisica) { }
        }

        private class CertidaoNascimentoTipoDocumento : TipoDocumento
        {
            public CertidaoNascimentoTipoDocumento() : base(5, "Certidão de Casamento", "Certidão de Casamento", TipoPessoa.PessoaFisica) { }
        }

        private class TituloEleitorTipoDocumento : TipoDocumento
        {
            public TituloEleitorTipoDocumento() : base(6, "Título de Eleitor", "Título de Eleitor", TipoPessoa.PessoaFisica) { }
        }

        private class PassaporteTipoDocumento : TipoDocumento
        {
            public PassaporteTipoDocumento() : base(7, "Passaporte", "Passaporte", TipoPessoa.PessoaFisica) { }
        }

        private class CrnmTipoDocument : TipoDocumento
        {
            public CrnmTipoDocument() : base(8, "CRNM ", "Carteira de Registro Nacional Migratório", TipoPessoa.PessoaFisica) { }
        }

        private class CnpjTipoDocumento : TipoDocumento
        {
            public CnpjTipoDocumento() : base(20, "CNPJ", "Cadastro Nacional da Pessoa Jurídica", TipoPessoa.PessoaJuridica) { }
        }

        private class AlvaraTipoDocumento : TipoDocumento
        {
            public AlvaraTipoDocumento() : base(21, "Alvará", "Alvará Municipal", TipoPessoa.PessoaJuridica) { }
        }

        private class InscricaoEstadualTipoDocumeto : TipoDocumento
        {
            public InscricaoEstadualTipoDocumeto() : base(22, "Inscrição Estadual", "Inscrição Estadual", TipoPessoa.PessoaJuridica) { }
        }

        private class InscricaoMunicipalTipoDocumeto : TipoDocumento
        {
            public InscricaoMunicipalTipoDocumeto() : base(23, "Inscrição Municipal", "Inscrição Municipal", TipoPessoa.PessoaJuridica) { }
        }


    }
}
