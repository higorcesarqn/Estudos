using Microsoft.AspNetCore.Identity;
using System;

namespace Hcqn.Infra.CrossCutting.Identity.Entities
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public Role Role { get; set; }
    }
}