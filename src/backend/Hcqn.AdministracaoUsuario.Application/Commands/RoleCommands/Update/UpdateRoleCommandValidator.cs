namespace Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Update
{
    public class UpdateRoleCommandValidator : RoleCommandValidations<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            NameValidator();
            PermissionsValidator();
            IdValidator();
        }
    }
}
