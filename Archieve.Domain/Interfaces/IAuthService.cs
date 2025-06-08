using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts.TransferObjects.Auth;
using Archieve.Core.Contracts;
using Archieve.Core.Contracts.TransferObjects.User;

namespace Archieve.Domain.Interfaces
{
    public interface IAuthService
    {
        ValueTask<ResponseModel<Users>> AuthenticateAsync(AuthenticateModel login);
    }
}
