using Hcqn.Core.UnitOfWork.Abstractions;
using Hcqn.Domain.AggregatesModel.RoleAggregate;
using Hcqn.Domain.AggregatesModel.RoleAggregate.Dtos;
using Hcqn.Infra.CrossCutting.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public RoleRepository(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }


        public Task<IPagedList<RoleListDto>> GetPagedList(int pageIndex = 0, int pageSize = 20)
        {
            var repositorio = _repositoryFactory.GetRepository<Role>();

            Expression<Func<Role, RoleListDto>> selector = x => new RoleListDto { Id = x.Id, Nome = x.Name };

            return repositorio.GetPagedListAsync(selector: selector, pageIndex: pageIndex, pageSize: pageSize);
        }

        public Task<RoleDetailsDto> Get(Guid idGrupo)
        {
            var repositorio = _repositoryFactory.GetRepository<Role>();

            Expression<Func<Role, bool>> predicate = p => p.Id == idGrupo;

            Expression<Func<Role, RoleDetailsDto>> selector = s => new RoleDetailsDto
            {
                Id = s.Id,
                Nome = s.Name,
                Permissoes = s.RoleClaims.Select(x => x.ClaimValue)
            };

            static IIncludableQueryable<Role, object> include(IQueryable<Role> user) => user.Include(x => x.RoleClaims);

            return repositorio.GetFirstOrDefaultAsync(selector: selector, predicate: predicate, include: include);
        }

        public async Task<IEnumerable<PermissaoDto>> ListAllPermissions()
        {
            var repositorio = _repositoryFactory.GetRepository<Permissao>();

            Expression<Func<Permissao, PermissaoDto>> selector = s
                 => new PermissaoDto
                 {
                     Descricao = s.Descricao,
                     Nome = s.Nome,
                     Titulo = s.Titulo
                 };

            return await repositorio.GetAll(selector).ToListAsync();
        }
    }
}
