using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts.TransferObjects.Roles;

namespace Archieve.Core.Contracts.TransferObjects.User
{
    public class Users
    {
        public int Id { get; set; }

        public Guid Uid { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty ;

        public string EmailAddress { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int Status { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public List<RolesResponse> UserRoles { get; set; }
    }
}
