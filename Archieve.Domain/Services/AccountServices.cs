using Archieve.Core.Contracts;
using Archieve.Core.Contracts.Enums;
using Archieve.Core.Contracts.TransferObjects.Roles;
using Archieve.Core.Contracts.TransferObjects.User;
using Archieve.Domain.Interfaces;
using Archieve.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;


namespace Archieve.Domain.Services
{
    public partial class AccountServices : IAccountService
    {
        ArchieveContext context;
        private readonly IRoleService roleService;
        public AccountServices(ArchieveContext context, IRoleService roleService)
        {
            this.context = context;
            this.roleService = roleService;
        }


        public async ValueTask<ResponseModel<string>> CreateUser(UserRequest userRequest)
        {
            try
            {
                if (userRequest == null)
                {
                    return new ResponseModel<string>
                    {
                        IsSuccessful = false,
                        Message = "Invalid data!"
                    };
                }

                if (string.IsNullOrWhiteSpace(userRequest.EmailAddress))
                {
                    return new ResponseModel<string>
                    {
                        IsSuccessful = false,
                        Message = "Email address is required."
                    };
                }

                if (string.IsNullOrWhiteSpace(userRequest.FirstName) ||
                   string.IsNullOrWhiteSpace(userRequest.LastName))
                {
                    return new ResponseModel<string>
                    {
                        IsSuccessful = false,
                        Message = "First name and last name are required."
                    };
                }

                if (string.IsNullOrEmpty(userRequest.Password) ||
                   string.IsNullOrWhiteSpace(userRequest.ConfirmPassword))
                {
                    return new ResponseModel<string>
                    {
                        IsSuccessful = false,
                        Message = "Password and confirm password are required."
                    };
                }

                if (string.Equals(userRequest.Password, userRequest.ConfirmPassword) == false)
                {
                    return new ResponseModel<string>
                    {
                        IsSuccessful = false,
                        Message = "Password and confirm password do not match."
                    };
                }

                var checkExistingUser = await this.context.Users.AnyAsync(u => u.EmailAddress == userRequest.EmailAddress);

                if (checkExistingUser)
                {
                    return new ResponseModel<string>
                    {
                        IsSuccessful = false,
                        Message = "User with this email address already exists."
                    };
                }

                var user = new User
                {
                    FirstName = userRequest.FirstName,
                    LastName = userRequest.LastName,
                    PhoneNumber = userRequest.PhoneNumber,
                    EmailAddress = userRequest.EmailAddress,
                    Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password, workFactor: 12),
                    //Status = UserStatus.Active.GetHashCode(),
                   // IsDeleted = false,
                    DateCreated = DateTime.UtcNow
                };

                this.context.Users.Add(user);
                var saveChanges = await this.context.SaveChangesAsync();
                if (saveChanges <= 0)
                {
                    return new ResponseModel<string>
                    {
                        IsSuccessful = false,
                        Message = "An error occurred while trying to save this record."
                    };
                }

                return new ResponseModel<string>
                {
                    IsSuccessful = true,
                    Message = "User created successfully."
                };


            }
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }


        public async ValueTask<Users> GetUserByUserName(string email)
        {
            var userEntity = await this.context.Users
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.EmailAddress == email 
                //&& x.IsDeleted == false
                );

            if (userEntity == null)
                return null;

            var user = new Users
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                EmailAddress = userEntity.EmailAddress,
                Uid = userEntity.Uid,
                //Status = userEntity.Status,
                Password = userEntity.Password,
                UserRoles = await GetUserRoles(userEntity.UserRoles),
            };

            return user;
        }

        public async ValueTask<Users> GetUserAsync(string uid)
        {

            Guid uidString = Guid.Parse(uid);
            var userEntity = await this.context.Users
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(
                x => x.Uid == uidString 
               // && x.IsDeleted == false
                );

            if (userEntity == null)
                return null;

            var user = new Users
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                EmailAddress = userEntity.EmailAddress,
                Uid = userEntity.Uid,
               // Status = userEntity.Status,
                Password = userEntity.Password,
                UserRoles = await GetUserRoles(userEntity.UserRoles),
            };

            return user;
        }


        private async Task<List<RolesResponse>> GetUserRoles(ICollection<UserRole> userRoles)
        {
            if (userRoles == null || !userRoles.Any())
                return new List<RolesResponse>();

            var allRoles = await this.roleService.GetRoles();
            var roleIds = userRoles.Select(ur => ur.RoleId).ToHashSet();
            var matchedRoles = allRoles.Where(r => roleIds.Contains(r.Id)).ToList();

            return matchedRoles;
        }


        public async ValueTask<List<string>> GetUserPermissions(string uid)
        {
            var userPermissions = await (from up in this.context.UserPermissions
                                         join user in this.context.Users
                                         on up.UserId equals user.Id
                                         where user.Uid.ToString() == uid
                                         select up.Permission).ToListAsync();
            return userPermissions;
                                        
        }


       
    }
}
