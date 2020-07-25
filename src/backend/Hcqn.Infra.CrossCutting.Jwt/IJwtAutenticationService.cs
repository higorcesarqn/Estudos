using Hcqn.Infra.CrossCutting.Jwt.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hcqn.Infra.CrossCutting.Jwt
{
    public interface IJwtAutenticationService
    {
        Task<JwtToken> Authenticate(string username, string senha);
        Task<IEnumerable<Claim>> GetClaimsByUser(string username);
    }
}
