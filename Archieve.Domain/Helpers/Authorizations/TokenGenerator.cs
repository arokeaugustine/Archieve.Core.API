using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts.TransferObjects.Auth;
using Archieve.Core.Contracts.TransferObjects.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Archieve.Domain.Helpers.Authorizations
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtConfig _jwtConfig;
        public TokenGenerator(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;   
        }

        public string GenerateJwtToken(Users user, ClaimsIdentity claimsIdentity)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            claimsIdentity.AddClaims(new[] { new Claim("uid", user.Uid.ToString() )});
            claimsIdentity.AddClaims(new[] {new Claim("sid", user.Id.ToString())});
            claimsIdentity.AddClaims(new[] { new Claim("email-address", user.EmailAddress) });
            claimsIdentity.AddClaims(new[] { new Claim("name", $"{user.FirstName} {user.LastName}") });


            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.TokenValidityPeriod),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);  
        }
    }
}
