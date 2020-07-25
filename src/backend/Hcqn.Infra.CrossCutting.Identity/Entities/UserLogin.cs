using Microsoft.AspNetCore.Identity;
using System;

namespace Hcqn.Infra.CrossCutting.Identity.Entities
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public User User { get; set; }
    }
}