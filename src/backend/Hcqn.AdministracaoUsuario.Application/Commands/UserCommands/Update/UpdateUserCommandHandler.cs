using Hcqn.Core.Commands;
using Hcqn.Core.Notifications.Abstractions;
using Hcqn.Core.Types;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.Update
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUpdateUserCommand">UserCommand, IRequest</typeparam>
    public class UpdateUserCommandHandler : CommandHandler<UpdateUserCommand>
    {
        private readonly IUserManager _userManager;
        private readonly INotifiable _notifiable;

        public UpdateUserCommandHandler(ILoggerFactory loggerFactory, IUserManager userManager,
             ITracer tracer, INotifiable notifiable)
            : base(loggerFactory, tracer)
        {
            _userManager = userManager;
            _notifiable = notifiable;
        }


        protected override async Task<Unit> Process(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.GetById(request.Id);

            if (applicationUser == null)
            {
                await _notifiable.Notify("usuario", "Usuário não existe!");
                return Unit.Value;
            }

            applicationUser.Name = request.Name;
            applicationUser.Email = request.Email;

            var updateResult = await _userManager.Update(applicationUser);

            if (updateResult.Succeeded)
            {
                var roles = await _userManager.GetRoles(applicationUser);
                await _userManager.RemoveRoles(applicationUser, roles);
                var addRoles = await _userManager.AddToRoles(applicationUser, request.Roles);

                foreach (var erro in addRoles.Errors)
                {
                    await _notifiable.Notify(erro.Code, erro.Description);
                }
            }

            foreach (var erro in updateResult.Errors)
            {
                await _notifiable.Notify(erro.Code, erro.Description);
            }

            return Unit.Value;
        }
    }
}
