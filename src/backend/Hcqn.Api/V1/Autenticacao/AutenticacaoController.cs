using Hcqn.Core.Notifications.Abstractions;
using Hcqn.Api.Controllers;
using Hcqn.Api.Infrastructure.Filters;
using Hcqn.Api.V1.Autenticacao.Models;
using Hcqn.Infra.CrossCutting.Jwt;
using Hcqn.Infra.CrossCutting.Jwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hcqn.Api.V1.Autenticacao
{
    /// <summary>
    /// Controller de Autenticação
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AutenticacaoController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifications"></param>
        public AutenticacaoController(INotifications notifications) : base(notifications) { }

        /// <summary>
        /// Autentica o usuário. [AllowAnonymous]
        /// </summary>
        /// <param name="jwtAutenticationService"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(JwtToken), 200)]
        [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), 400)]
        [ProducesResponseType(typeof(JsonErrorResponse), 500)]
        public async Task<IActionResult> Autenticacao([FromServices] IJwtAutenticationService jwtAutenticationService, [FromBody] AutenticacaoModel login )
        {
            var token = await jwtAutenticationService.Authenticate(login.Login, login.Senha);
            return Response(token);
        }
    }
}