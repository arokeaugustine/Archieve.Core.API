

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Archieve.Core.Contracts.TransferObjects.Auth;
using Archieve.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Archieve.Domain.Helpers.Authorizations
{
    public class JwtMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _jwtConfig;
        public JwtMiddleWare(RequestDelegate next, IOptions<JwtConfig> jwtConfig)
        {
            _next = next;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task Invoke(HttpContext context, IAccountService accountService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await attachUserToContext(context, accountService, token);
            }
            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, IAccountService accountService ,string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // ensure token expires at the exact time
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                var userUid = jwtToken.Claims.First(x => x.Type == "uid").Value;

                // attach user to context on successful jwt validation.
                var response =  await accountService.GetUserAsync(userUid);
                context.Items["User"] = response;
            }
            catch 
            {

            }
        }


    }
}
