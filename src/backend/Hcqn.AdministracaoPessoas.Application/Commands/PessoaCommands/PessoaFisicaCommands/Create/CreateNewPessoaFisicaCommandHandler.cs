using Hcqn.Core.Commands;
using Hcqn.Core.UnitOfWork.Abstractions;
using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unit = Hcqn.Core.Types.Unit;

namespace Hcqn.AdministracaoPessoas.Application.Commands.PessoaCommands.PessoaFisicaCommands.Create
{
    public class CreateNewPessoaFisicaCommandHandler<TUnitOfWork> : CommandHandler<CreateNewPessoaFisicaCommand>
        where TUnitOfWork : IUnitOfWork
    {
        private readonly IRepository<Pessoa> _respository;
        private readonly TUnitOfWork _unitOfWork;
        public CreateNewPessoaFisicaCommandHandler(TUnitOfWork uow, ILoggerFactory loggerFactory,
            ITracer tracer) : base(loggerFactory,  tracer)
        {
            _respository = uow.GetRepository<Pessoa>();
            _unitOfWork = uow;
        }

        protected override async Task<Unit> Process(CreateNewPessoaFisicaCommand command, CancellationToken cancellationToken)
        {
            var telefones = command.Telefones?.Select(x => new Telefone(x.Tipo, x.DDD, x.Numero));

            var enderecos = command.Enderecos?.Select(x => new Endereco(x.Tipo, x.CEP, x.Uf, x.Cidade,
                x.Bairro, x.Logradouro, x.Numero, x.Complemento));

            var documentos = command.Documentos?.Select(x => new Documento(x.Tipo, x.Valor));

            var pessoa = new Pessoa(command.Id, command.Nome, command.Email, command.Nascimento,
                command.Escolaridade, command.Deficiente, command.Estuda, telefones?.ToList(), enderecos?.ToList(), documentos?.ToList());

            await _respository.InsertAsync(pessoa, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
