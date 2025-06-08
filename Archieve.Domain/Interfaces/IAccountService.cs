using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts;
using Archieve.Core.Contracts.TransferObjects.User;
using Archieve.Infrastructure.Models;

namespace Archieve.Domain.Interfaces
{
    public interface IAccountService
    {
        ValueTask<ResponseModel<string>> CreateUser(UserRequest userRequest);
        ValueTask<Users> GetUserByUserName(string email);
        ValueTask<List<string>> GetUserPermissions(string uid);
        ValueTask<Users> GetUserAsync(string uid);

    }
}
