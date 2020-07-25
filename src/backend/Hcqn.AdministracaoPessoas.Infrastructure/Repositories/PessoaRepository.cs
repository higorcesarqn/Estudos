using Hcqn.Core.UnitOfWork;
using Hcqn.Core.UnitOfWork.Abstractions;
using Hcqn.Domain.AggregatesModel.PessoaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoPessoas.Infrastructure.Repositories
{
    public class PessoaRepository<TContext> : Repository<Pessoa>, IRepository<Pessoa>, IPessoaRepository
        where TContext : DbContext
    {
        public PessoaRepository(TContext context) : base(context) { }

        public Task<IPagedList<Pessoa>> GetPagedListAsync(int pageIndex = 0, int pageSize = 20)
        {
            return base.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
        }

        public Task<Pessoa> GetDetails(Guid idPessoa)
        {
            Expression<Func<Pessoa, bool>> predicate = x => x.Id == idPessoa;

            static IIncludableQueryable<Pessoa, object> include(IQueryable<Pessoa> source)
                => source.Include(i => i.Telefones)
                         .Include(i => i.Enderecos);

            return GetFirstOrDefaultAsync(predicate: predicate, include: include);
        }
    }
}
