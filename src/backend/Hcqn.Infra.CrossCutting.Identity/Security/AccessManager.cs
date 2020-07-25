using Hcqn.Infra.CrossCutting.Identity.Entities;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hcqn.Infra.CrossCutting.Identity.Security
{
    public class AccessManager : IAcessManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccessManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userManager
                .Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.RoleClaims)
                .FirstOrDefaultAsync(x => x.UserName == username);
            // return _userManager.FindByNameAsync(username);
        }

        public async Task<IEnumerable<string>> GetPermissoes(string username)
        {
            var result = await _userManager.Users
                 .Where(x => x.UserName == username)
                 .SelectMany(x => x.UserRoles)
                 .Select(s => s.Role)
                 .SelectMany(s => s.RoleClaims)
                 .Select(x => x.ClaimValue).Distinct()
                 .ToListAsync();

            return result;
        }

        public Task<SignInResult> ValidateCredentials(User user, string password)
        {
            return _signInManager.CheckPasswordSignInAsync(user, password, true);
        }
    }
}