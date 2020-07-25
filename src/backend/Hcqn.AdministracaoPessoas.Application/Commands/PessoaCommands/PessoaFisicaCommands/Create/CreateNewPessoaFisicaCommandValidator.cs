namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.PessoaFisicaCommands.Create
{
    public class CreateNewPessoaFisicaCommandValidator : PessoaFisicaCommandValidator<CreateNewPessoaFisicaCommand>
    {
        public CreateNewPessoaFisicaCommandValidator()
        {
            TipoTelefoneValidator();
            TipoDocumentoValidator();
            TipoEnderecoValidator();
            NomeValidator();
            EmailValidator();
            EscolaridadeValidator();
        }
    }
}
