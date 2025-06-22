using System.Linq;
using Archieve.Core.Contracts;
using Archieve.Core.Contracts.TransferObjects.Roles;
using Archieve.Domain.Interfaces;
using Archieve.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Archieve.Domain.Services
{
    public class RolesServices : IRoleService
    {
        ArchieveContext context;
        public RolesServices(ArchieveContext context)
        {
            this.context = context;
        }


        public async ValueTask<ResponseModel<string>> CreateRoleAsync(RolesResponse roles)
        {
            if (roles == null)
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = "Invalid data"
                };
            }

            if (string.IsNullOrWhiteSpace(roles.Name) || string.IsNullOrWhiteSpace(roles.Description))
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = "Name and description is required"
                };
            }
            var newRole = new Role
            {
                Name = roles.Name,
                Description = roles.Description,
                CreatedBy = 1
            };

            await this.context.Roles.AddAsync(newRole);
            await this.context.SaveChangesAsync();

            return new ResponseModel<string>
            {
                IsSuccessful = true,
                Message = "Role created successfully"
            };

        }



        public async ValueTask<List<string>> GetRolePermission(List<string> uids)
        {
            var userPermission = await (from rp in this.context.RolePermissions
                                        join role in this.context.Roles
                                        on rp.RoleId equals role.Id
                                        where uids.Contains(role.Uid.ToString())
                                        select rp.Permission).ToListAsync();

            return userPermission;

        }


        public async ValueTask<List<RolesResponse>> GetRoles()
        {
            var roles = await this.context.Roles.Select(x => new RolesResponse {
                Id = x.Id,
                Uid = x.Uid,
                Description = x.Description,    
                CreatedBy= x.CreatedBy,
                Name = x.Name,
            }).ToListAsync();


            return roles;
        }


    }
}
