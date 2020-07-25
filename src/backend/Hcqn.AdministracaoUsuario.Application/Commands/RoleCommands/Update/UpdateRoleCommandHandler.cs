using Hcqn.Core.Commands;
using Hcqn.Core.Notifications.Abstractions;
using Hcqn.Infra.CrossCutting.Identity.Configurations;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Unit = Hcqn.Core.Types.Unit;

namespace Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Update
{
    public class UpdateRoleCommandHandler :  CommandHandler<UpdateRoleCommand>
    {
        private readonly IRoleManager _roleManager;
        private readonly INotifiable _notifiable;

        public UpdateRoleCommandHandler(IRoleManager roleManager, ILoggerFactory loggerFactory,
             ITracer tracer, INotifiable notifiable ) : base( loggerFactory, tracer)
        {
            _roleManager = roleManager;
            _notifiable = notifiable;
        }

        protected override async Task<Unit> Process(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.GetById(request.Id);

            if (role == null)
            {
                await _notifiable.Notify("grupo", "grupo não encontrada.");
                return Unit.Value;
            }

            role.Name = request.Name;

            var createRoleIdentityResult = await _roleManager.Update(role);

            foreach (var erro in createRoleIdentityResult.Errors)
            {
                await _notifiable.Notify(erro.Code, erro.Description);
            }

            if (_notifiable.IsValid())
            {
                await _roleManager.ClearAllClaims(role);
                foreach (var permissao in request.Permissions)
                {
                    _ = await _roleManager
                        .AddClaim(role, new Claim(IdentityConfigurations.DefaultRoleClaim, permissao.ToLower()));
                }
            }

            return Unit.Value;
        }
    }
}
