using Hcqn.Infra.CrossCutting.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hcqn.Infra.CrossCutting.Identity.Interfaces
{
    public interface IAcessManager
    {
        Task<User> GetUserByUsername(string username);
        Task<SignInResult> ValidateCredentials(User user, string password);

        Task<IEnumerable<string>> GetPermissoes(string username);
    }
}