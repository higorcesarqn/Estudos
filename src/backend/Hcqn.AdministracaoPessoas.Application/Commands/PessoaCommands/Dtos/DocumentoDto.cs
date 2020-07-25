using Hcqn.Domain.AggregatesModel.PessoaAggregate;

namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.Dtos
{
    public class DocumentoDto
    {
        public DocumentoDto(int tipo, string valor)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public int Tipo { get; private set; }
        public string Valor { get; private set; }

        public static implicit operator Documento(DocumentoDto dto)
        {
            return new Documento(dto.Tipo, dto.Valor);
        }

        public static implicit operator DocumentoDto(Documento documento)
        {
            return new DocumentoDto(documento.Tipo, documento.Valor);
        }
    }
}
