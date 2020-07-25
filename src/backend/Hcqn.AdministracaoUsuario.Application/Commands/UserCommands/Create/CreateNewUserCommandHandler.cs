using Hcqn.Core.Commands;
using Hcqn.Core.Notifications.Abstractions;
using Hcqn.Core.Types;
using Hcqn.Infra.CrossCutting.Identity.Entities;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.Create
{
    public class CreateNewUserCommandHandler : CommandHandler<CreateNewUserCommand>
    {
        private readonly IUserManager _userManager;
        private readonly INotifiable _notifiable;

        public CreateNewUserCommandHandler(LoggerFactory loggerFactory, IUserManager userManager,
            ITracer tracer, INotifiable notifiable) : base(loggerFactory,  tracer)
        {
            _userManager = userManager;
            _notifiable = notifiable;
        }

        protected override async Task<Unit> Process(CreateNewUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Id, request.Name, request.Email, request.UserName);

            var createUserIdentityResult = await _userManager.CreateUser(user, request.Password);

            foreach (var erro in createUserIdentityResult?.Errors)
            {
                await _notifiable.Notify(erro.Code, erro.Description);
            }

            if (_notifiable.IsValid())
            {
                var addToRolesIdentityResult = await _userManager.AddToRoles(user, request.Roles);

                foreach (var erro in addToRolesIdentityResult?.Errors)
                {
                    await _notifiable.Notify(erro.Code, erro.Description);
                }
            }

            return Unit.Value;
        }
    }
}
