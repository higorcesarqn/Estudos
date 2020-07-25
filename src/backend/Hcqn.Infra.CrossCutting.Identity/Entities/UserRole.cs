using Microsoft.AspNetCore.Identity;
using System;

namespace Hcqn.Infra.CrossCutting.Identity.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public Role Role { get; set; }
        public User User { get; set; }
    }
}