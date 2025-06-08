using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Roles
{
    public class RolesDTO
    {
        public int Id { get; set; }

        public Guid Uid { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int CreatedBy { get; set; }
    }
}
