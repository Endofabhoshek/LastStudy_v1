using LastStudy.WebHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace LastStudy.Filter
{
    //public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    //{
    //    public bool AllowMultiple => false;

    //    public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
    //    {
    //        var request = context.Request;
    //        var authorization = request.Headers.Authorization;

    //        if (authorization == null || authorization.Scheme != "Bearer")
    //            return;

    //        if (string.IsNullOrEmpty(authorization.Parameter))
    //        {
    //            context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
    //            return;
    //        }

    //        var token = authorization.Parameter;
    //        var principal = await AuthenticateJwtToken(token);

    //        if (principal == null)
    //            context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);

    //        else
    //            context.Principal = principal;
    //    }

    //    public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected Task<IPrincipal> AuthenticateJwtToken(string token)
    //    {
    //        if (ValidateToken(token, out var username))
    //        {
    //            // based on username to get more information from database in order to build local identity
    //            var claims = new List<Claim>
    //            {
    //                new Claim(ClaimTypes.Name, username)
    //                // Add more claims if needed: Roles, ...
    //            };

    //            var identity = new ClaimsIdentity(claims, "Jwt");
    //            IPrincipal user = new ClaimsPrincipal(identity);

    //            return Task.FromResult(user);
    //        }

    //        return Task.FromResult<IPrincipal>(null);
    //    }

    //    private static bool ValidateToken(string token, out string username)
    //    {
    //        username = null;

    //        var simplePrinciple = JWTManager.GetPrincipal(token);
    //        var identity = simplePrinciple?.Identity as ClaimsIdentity;

    //        if (identity == null)
    //            return false;

    //        if (!identity.IsAuthenticated)
    //            return false;

    //        var usernameClaim = identity.FindFirst(ClaimTypes.Name);
    //        username = usernameClaim?.Value;

    //        if (string.IsNullOrEmpty(username))
    //            return false;

    //        // More validate to check whether username exists in system

    //        return true;
    //    }
    //}
}