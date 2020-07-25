using Hcqn.Core.Commands;
using Hcqn.Core.Notifications.Abstractions;
using Hcqn.Core.Types;
using Hcqn.Infra.CrossCutting.Identity.Configurations;
using Hcqn.Infra.CrossCutting.Identity.Entities;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Create
{
    public class CreateNewRoleCommandHandler : CommandHandler<CreateNewRoleCommand>
    {
        private readonly IRoleManager _roleManager;
        private readonly INotifiable _notifiable;

        public CreateNewRoleCommandHandler(IRoleManager roleManager, ILoggerFactory loggerFactory,
             ITracer tracer, INotifiable notifiable) : base(loggerFactory, tracer)
        {
            _roleManager = roleManager;
            _notifiable = notifiable;
        }

        protected override async Task<Unit> Process(CreateNewRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role(request.Id, request.Name);

            var createRoleIdentityResult = await _roleManager.CreateRole(role);

            foreach (var erro in createRoleIdentityResult?.Errors)
            {
                await _notifiable.Notify(erro.Code, erro.Description);
            }

            if (_notifiable.IsValid())
            {
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
