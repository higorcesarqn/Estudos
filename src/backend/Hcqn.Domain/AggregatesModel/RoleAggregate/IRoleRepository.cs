using Hcqn.Core.UnitOfWork.Abstractions;
using Hcqn.Domain.AggregatesModel.RoleAggregate.Dtos;
using Hcqn.Domain.SeedWork;
using Hcqn.Infra.CrossCutting.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hcqn.Domain.AggregatesModel.RoleAggregate
{
    public interface IRoleRepository : IAggregateRootReposity<Role>
    {
        Task<IPagedList<RoleListDto>> GetPagedList(int pageIndex = 0, int pageSize = 20);

        Task<RoleDetailsDto> Get(Guid idGrupo);

        Task<IEnumerable<PermissaoDto>> ListAllPermissions();
    }
}
