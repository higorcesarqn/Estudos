using Hcqn.Core.Commands;
using Hcqn.Core.Notifications.Abstractions;
using Hcqn.Core.Types;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Threading;
using System.Threading.Tasks;


namespace Hcqn.AdministracaoUsuario.Application.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordCommandHandler : CommandHandler<ChangePasswordCommand>
    {
        private readonly IUserManager _userManager;
        private readonly INotifiable _notifiable;

        public ChangePasswordCommandHandler(ILoggerFactory loggerFactory, IUserManager userManager, 
            ITracer tracer, INotifiable notifiable) : base(loggerFactory, tracer)
        {
            _userManager = userManager;
            _notifiable = notifiable;
        }

        protected override async Task<Unit> Process(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.GetById(request.Id);

            if (applicationUser == null)
            {
                await _notifiable.Notify("usuario", "usuario inválido.");
                return Unit.Value;
            }

            var changePasswordResult = await _userManager.ChangePassword(applicationUser, request.Password);

            foreach (var erro in changePasswordResult.Errors)
            {
                await _notifiable.Notify(erro.Code, erro.Description);
            }

            return Unit.Value;
        }
    }
}
