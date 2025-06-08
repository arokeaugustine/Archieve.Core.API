using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Books
{
    public class DeleteDTO
    {
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public DateTime DateDeleted { get; set; }
    }
}
