using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts.TransferObjects.User;

namespace Archieve.Domain.Helpers.Authorizations
{
    public interface ITokenGenerator
    {
        string GenerateJwtToken(Users user, ClaimsIdentity claimsIdentity);
    }
}
