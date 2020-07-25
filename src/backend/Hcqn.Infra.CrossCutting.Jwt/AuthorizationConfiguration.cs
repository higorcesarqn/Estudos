using Hcqn.Core.Identity.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

namespace Hcqn.Infra.CrossCutting.Jwt
{
    /*
     “iss” - O domínio da aplicação geradora do token
     “sub” - É o assunto do token, mas é muito utilizado para guarda o ID do usuário
     “aud” - Define quem pode usar o token
     “exp” - Data para expiração do token
     “nbf” - Define uma data para qual o token não pode ser aceito antes dela
     “iat” - Data de criação do token
     “jti” - O id do token
     */
    public static class AuthorizationConfiguration
    {
        //iss (issuer) = Emissor do token;
        private const string Issuer = "sit3";

        //aud (audience) = Destinatário do token, representa a aplicação que irá usá-lo.
        private const string Audience = "sit3";

       // private const string SecretKey = "d8fd542d-956-5d2d-8fd5-c6ffd5525874";
       // private static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        private static readonly string _jwkLocation = Path.Combine(Environment.CurrentDirectory, "secretkey.json");
       
        public static void ConfigureJwtAuthorization(this IServiceCollection services)
        {
            services.AddTransient<IUser, Models.User>();

            var SigningKey = Loadkey();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,

                ValidateAudience = true,
                ValidAudience = Audience,

                // Valida a assinatura de um token recebido
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SigningKey,

                RequireExpirationTime = true,
                // Verifica se um token recebido ainda é válido
                ValidateLifetime = true,
                
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(
                o =>
                {

                    o.TokenValidationParameters = tokenValidationParameters;
                    o.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                 options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();
            });

            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = Issuer;
                options.Audience = Audience;
                options.SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
            });
        }

        private static SecurityKey Loadkey()
        {
            if (File.Exists(_jwkLocation))
            {
                return JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(_jwkLocation));
            }
                

            var newKey = CreateJWK();
            File.WriteAllText(_jwkLocation, JsonSerializer.Serialize(newKey));
            return newKey;
        }

        private static JsonWebKey CreateJWK()
        {
            var symetricKey = new HMACSHA256(GenerateKey(64));
            var jwk = JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symetricKey.Key));
            jwk.KeyId = Base64UrlEncoder.Encode(GenerateKey(16));
            return jwk;
        }

        private static byte[] GenerateKey(int bytes)
        {
            var rng = RandomNumberGenerator.Create();
            var data = new byte[bytes];
            rng.GetBytes(data);
            return data;
        }
    }
}