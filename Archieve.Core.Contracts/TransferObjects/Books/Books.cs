using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Archieve.Core.Contracts.TransferObjects.Books
{
    public class Books
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        [JsonIgnore]
        public DateTime DateCreated { get; set; }

    }
}
