namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.Dtos
{
    public class TelefoneDto
    {
        public TelefoneDto(int tipo, string ddd, string numero)
        {
            Tipo = tipo;
            DDD = ddd;
            Numero = numero;
        }

        public int Tipo { get; private set; }
        public string DDD { get; private set; }
        public string Numero { get; private set; }
    }
}
