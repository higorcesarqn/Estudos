namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.Dtos
{
    public class EnderecoDto
    {
        public EnderecoDto(int tipo, string cEP, string uf, string cidade, string bairro,
            string logradouro, string numero, string complemento)
        {
            CEP = cEP;
            Uf = uf;
            Cidade = cidade;
            Bairro = bairro;
            Tipo = tipo;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
        }

        public string CEP { get; private set; }
        public string Uf { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public int Tipo { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
    }
}
