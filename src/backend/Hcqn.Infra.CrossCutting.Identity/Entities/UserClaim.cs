using Microsoft.AspNetCore.Identity;
using System;

namespace Hcqn.Infra.CrossCutting.Identity.Entities
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public User User { get; set; }
    }
}