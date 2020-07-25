using Hcqn.Core.UnitOfWork;
using Hcqn.Teste.Geo.Context;
using Hcqn.Teste.Geo.Entities;

namespace Hcqn.Teste.Geo.Repositories
{
    public class LoteRepository : Repository<Lote>, ILoteRepository
    {
        public LoteRepository(GeoContext dbContext) : base(dbContext)
        {
        }
    }
}
