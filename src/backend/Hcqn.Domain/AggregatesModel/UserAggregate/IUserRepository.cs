using Hcqn.Core.UnitOfWork.Abstractions;
using Hcqn.Domain.AggregatesModel.UserAggregate.Dtos;
using Hcqn.Domain.SeedWork;
using Hcqn.Infra.CrossCutting.Identity.Entities;
using System;
using System.Threading.Tasks;

namespace Hcqn.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IAggregateRootReposity<User>
    {
        Task<IPagedList<UserListDto>> GetPagedList(int pageIndex = 0, int pageSize = 20);
        Task<UserDetailsDto> GetDetails(Guid idUsuario);
    }
}