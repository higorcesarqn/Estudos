using Hcqn.Core.UnitOfWork.Abstractions;
using System;
using System.Threading.Tasks;

namespace Hcqn.Domain.AggregatesModel.PessoaAggregate
{
    public interface IPessoaRepository
    {
        Task<IPagedList<Pessoa>> GetPagedListAsync(int pageIndex = 0, int pageSize = 20);
        Task<Pessoa> GetDetails(Guid idPessoa);
    }
}
