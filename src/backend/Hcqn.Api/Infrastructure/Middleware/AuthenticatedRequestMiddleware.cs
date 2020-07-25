using Hcqn.Infra.CrossCutting.Jwt;
using Microsoft.AspNetCore.Http;
using OpenTracing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hcqn.Api.Infrastructure.Middleware
{
    public class AuthenticatedRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtAutenticationService _jwtAutenticationService;
        private readonly ITracer _tracer;

        public AuthenticatedRequestMiddleware(RequestDelegate next,
            IJwtAutenticationService jwtAutenticationService, ITracer tracer)
        {
            _next = next;
            _jwtAutenticationService = jwtAutenticationService;
            _tracer = tracer;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                using var scope = _tracer.BuildSpan("AuthenticatedRequestMiddleware").StartActive(true);

                var username = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

                //set tag username no Trace
                scope.Span.SetTag("username", username);

                var claims = await _jwtAutenticationService.GetClaimsByUser(username);

                var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
                context.User.AddIdentity(claimsIdentity);
            }

            await _next(context);
        }
    }
}
