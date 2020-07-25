using System.Collections.Generic;
using System.Security.Claims;

namespace Hcqn.Core.Identity.Abstractions
{
    public interface IUser
    {
        string Name { get; }
        string Username { get; }

        bool IsAuthenticated();

        IEnumerable<Claim> GetClaimsIdentity();
    }
}
