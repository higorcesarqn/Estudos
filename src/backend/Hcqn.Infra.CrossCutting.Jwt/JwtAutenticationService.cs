using Hcqn.Core.Notifications.Abstractions;
using Hcqn.Core.Tango.Types;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Hcqn.Infra.CrossCutting.Jwt.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Humanizer;
namespace Hcqn.Infra.CrossCutting.Jwt
{
    public class JwtAutenticationService : IJwtAutenticationService
    {
        private readonly INotifiable _notifiable;
        private readonly IAcessManager _userManager;
        private readonly TokenOptions _tokenOptions;

        public JwtAutenticationService(INotifiable notifiable,
            IAcessManager userManager, IOptions<TokenOptions> jwtOptions)
        {
            _notifiable = notifiable;
            _userManager = userManager;
            _tokenOptions = jwtOptions.Value;
        }

        private async Task<JwtToken> MethoWhenSignFail()
        {
            await _notifiable.Notify("usuario", "Usuário e/ou Senha Inválido(s)");
            return default;
        }

        public async Task<JwtToken> Authenticate(string username, string senha)
        {
            Option<Identity.Entities.User> userIdentity = await _userManager.GetUserByUsername(username);

            return await userIdentity.Match(user => MethodWhenUserExist(user, senha), methodWhenNone: MethoWhenSignFail);
        }

      

        private async Task<JwtToken> MethodWhenUserExist(Identity.Entities.User user, string senha)
        {
            var signInResult = await _userManager.ValidateCredentials(user, senha);

            if (signInResult.Succeeded)
            {
                var claims = GetClaims(user);
                var jwtSecurityToken = new JwtSecurityToken(
                            _tokenOptions.Issuer,
                            _tokenOptions.Audience,
                            claims,
                            _tokenOptions.NotBefore,
                            _tokenOptions.Expiration,
                            _tokenOptions.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return new JwtToken
                {
                    Token = encodedJwt,
                    Expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                    User = new UserModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        Username = user.UserName,
                        Permissoes = user.UserRoles
                            .SelectMany(x => x.Role.RoleClaims
                           .Select(s => s.ClaimValue)).Distinct()
                    }
                };
            }

            if (signInResult.IsLockedOut)
            {
                var timespan = user.LockoutEnd - DateTime.Now;
                await _notifiable.Notify("usuario", $"numero máximo de tentativas excedidas! tente novamente em {timespan?.Humanize(2)}");
                return default;
            }

            await MethoWhenSignFail();

            return default;
        }

        public async Task<IEnumerable<Claim>> GetClaimsByUser(string username)
        {
            var permissoes = await _userManager.GetPermissoes(username);

            return permissoes.Select(permissao => new Claim(ClaimTypes.Role, permissao));
        }

        private IEnumerable<Claim> GetClaims(Identity.Entities.User user)
        {
            if (user == default)
            {
                yield break;
            }

            yield return new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName);
            yield return new Claim(JwtRegisteredClaimNames.Email, user.Email);
            yield return new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString());
            yield return new Claim(JwtRegisteredClaimNames.Jti, _tokenOptions.JtiGenerator());
            yield return new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64);
        }

        private static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


    }
}