using Hcqn.Core.UnitOfWork.Abstractions;
using Hcqn.Domain.AggregatesModel.UserAggregate;
using Hcqn.Domain.AggregatesModel.UserAggregate.Dtos;
using Hcqn.Infra.CrossCutting.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Infrastructure.Repositories
{
    public class UserRepository<TContext> : IUserRepository
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UserRepository(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public Task<IPagedList<UserListDto>> GetPagedList(int pageIndex = 0, int pageSize = 20)
        {
            var repostory = _repositoryFactory.GetRepository<User>();

            Expression<Func<User, UserListDto>> selector = user => new UserListDto
            {
                Id = user.Id,
                Nome = user.Name,
                Login = user.UserName,
                Email = user.Email,
                Telefone = user.UserName,
                DataFimDoBloqueio = user.LockoutEnd,
                ContadorErroSenha = user.AccessFailedCount
            };

            return repostory.GetPagedListAsync(selector: selector, pageIndex: pageIndex, pageSize: pageSize);
        }

        public Task<UserDetailsDto> GetDetails(Guid idUsuario)
        {
            var repostory = _repositoryFactory.GetRepository<User>();

            Expression<Func<User, bool>> predicate = p => p.Id == idUsuario;

            Expression<Func<User, UserDetailsDto>> selector = user => new UserDetailsDto
            {
                Id = user.Id,
                Nome = user.Name,
                Login = user.UserName,
                Email = user.Email,
                ContadorErroSenha = user.AccessFailedCount,
                DataFimDoBloqueio = user.LockoutEnd,
                Telefone = user.PhoneNumber,
                Grupos = user.UserRoles.Select(x => x.Role.Name),
            };

            return repostory.GetFirstOrDefaultAsync(selector: selector, predicate: predicate, disableTracking: true, include: include);

            static IIncludableQueryable<User, object> include(IQueryable<User> user) => user.Include(x => x.UserRoles).ThenInclude(t => t.Role);
        }
    }
}
