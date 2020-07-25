using Hcqn.Caching;
using Hcqn.Infra.CrossCutting.Identity.Entities;
using Hcqn.Infra.CrossCutting.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hcqn.Infra.CrossCutting.Identity.Caching
{
    public class AcessManagerCaching<TAcessManager> : IAcessManager
        where TAcessManager : IAcessManager
    {
        private readonly ICache _distributedCache;
        private readonly TAcessManager _acessManager;
        private readonly ILogger<AcessManagerCaching<TAcessManager>> _logger;

        public AcessManagerCaching(ICache distributedCache, TAcessManager acessManager, ILogger<AcessManagerCaching<TAcessManager>> logger)
        {
            _distributedCache = distributedCache;
            _acessManager = acessManager;
            _logger = logger;
        }

        public async Task<IEnumerable<string>> GetPermissoes(string username)
        {
            var key = GetKeyPermissoesByUsername(username);

            var cache = await _distributedCache.Get<IEnumerable<string>>(key);

            if (cache == null || !cache.Any())
            {
                _logger.LogTrace("Permissões não encontrado no cache com a chave {cacheKey}", key);
                var permissoes = await _acessManager.GetPermissoes(username);

                if (permissoes == null || !permissoes.Any())
                {
                    return Enumerable.Empty<string>();
                }

                await SetPermissoesInCache(key, permissoes);
            }

            return cache ?? await _distributedCache.Get<IEnumerable<string>>(key);
        }


        private static string GetKeyPermissoesByUsername(string UserName) => $"sit:permissoes:{UserName}";

        public async Task<User> GetUserByUsername(string username)
        {
            var result = await _acessManager.GetUserByUsername(username);

            if (result == default)
            {
                return default;
            }

            var permissoes = result
                .UserRoles
                .SelectMany(x => x.Role
                    .RoleClaims
                    .Select(s => s.ClaimValue))
                .Distinct();

            await SetPermissoesInCache(GetKeyPermissoesByUsername(username), permissoes.ToList());

            return result;
        }

        public Task<SignInResult> ValidateCredentials(User user, string password) => _acessManager.ValidateCredentials(user, password);

        private async Task SetPermissoesInCache(string key, IEnumerable<string> permissoes)
        {
            await _distributedCache.Set(key, permissoes, new CacheEntryOptions
            {
                SlidingExpiration = new TimeSpan(0, 0, 30, 0)
            });
        }
    }
}
