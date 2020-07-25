namespace Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands.Create
{
    public class CreateNewRoleCommandValidation : RoleCommandValidations<CreateNewRoleCommand>
    {
        public CreateNewRoleCommandValidation()
        {
            NameValidator();
            PermissionsValidator();
        }
    }
}
