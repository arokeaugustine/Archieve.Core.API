using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts;
using Archieve.Core.Contracts.Enums;
using Archieve.Core.Contracts.TransferObjects.Auth;
using Archieve.Core.Contracts.TransferObjects.User;
using Archieve.Domain.Helpers;
using Archieve.Domain.Helpers.Authorizations;
using Archieve.Domain.Interfaces;
using Archieve.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Archieve.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly ArchieveContext context;
        private readonly IAccountService userService;
        private readonly IRoleService roleService;
        private readonly ITokenGenerator tokenGenerator;

        public AuthService(ArchieveContext context, 
            IAccountService userService,
            IRoleService roleService,
            ITokenGenerator tokenGenerator  )
        {
            this.context = context;
            this.userService = userService;
            this.roleService = roleService;
            this.tokenGenerator = tokenGenerator;
        }


        public async ValueTask<ResponseModel<Users>> AuthenticateAsync(AuthenticateModel login)
        {
            try
            {
                if (login == null)
                {
                    return new ResponseModel<Users>
                    {
                        IsSuccessful = false,
                        Message = "Invalid login data!"
                    };
                }


                if (string.IsNullOrWhiteSpace(login.EmailAddress) ||
                    string.IsNullOrWhiteSpace(login.Password))
                {
                    return new ResponseModel<Users>
                    {
                        IsSuccessful = false,
                        Message = "Email address and password are required."
                    };
                }

                var user = await this.userService.GetUserByUserName(login.EmailAddress);

                if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
                {
                    return new ResponseModel<Users>
                    {
                        IsSuccessful = false,
                        Message = "Invalid username or password!"
                    };

                }

                var permissions = await GetUserPermissions(user);
                if (!permissions.IsSuccessful){
                    return new ResponseModel<Users>
                    {
                        Message = "Unable to process user permission",
                        IsSuccessful = false
                    };
                }

                var permissionsIdentity = permissions.Data as ClaimsIdentity;

                var token =  this.tokenGenerator.GenerateJwtToken(user, permissionsIdentity);

                if (string.IsNullOrEmpty(token))
                {
                    return new ResponseModel<Users>
                    {
                        IsSuccessful = false,
                        Message = "unable to generate auth token"
                    };
                }

                return new ResponseModel<Users>
                {
                    IsSuccessful = true,
                    Message = token,
                    Data = user
                };


            }
            catch (Exception ex)
            {
                return new ResponseModel<Users>
                {
                    IsSuccessful = false,
                    Message = $"An error occurred"
                };
            }
        }

        private async Task<ResponseModel<object>> GetUserPermissions(Users user)
        {
            var userPermission = await this.userService.GetUserPermissions(user.Uid.ToString());
            var rolePermission = await this.roleService.GetRolePermission(user.UserRoles.Select(x => x.Uid.ToString()).ToList());

            var filteredPermissions = userPermission.Union(rolePermission).Distinct();

            var permissions = filteredPermissions.ToEnums<Permissions>();
            var claims = new List<Claim>();

            claims.Add(new Claim(PermissionConstants.packedPermissionClaimType, permissions.packPermissionIntoString()));

            var permissionsIdentity = new ClaimsIdentity(claims);

            return new ResponseModel<object>
            {
                IsSuccessful = true,
                Message = "successful",
                Data = permissionsIdentity
            };

        }


       
    }
}
