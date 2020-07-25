using Hcqn.Core.Identity.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Hcqn.EventSourcing.UnitTests
{
    public class User : IUser
    {
        public string Name => "Usuario Teste";

        public string Username => "usuario.teste";

        public bool IsAuthenticated() => true;

        public IEnumerable<Claim> GetClaimsIdentity() => Enumerable.Empty<Claim>();
    }
}
