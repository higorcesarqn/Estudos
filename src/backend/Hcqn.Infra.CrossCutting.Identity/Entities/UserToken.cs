using Microsoft.AspNetCore.Identity;
using System;

namespace Hcqn.Infra.CrossCutting.Identity.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public User User { get; set; }
    }
}