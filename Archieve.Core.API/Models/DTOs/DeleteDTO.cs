using System.Text.Json.Serialization;

namespace Archieve.Core.API.Models.DTOs
{
    public class DeleteDTO
    {
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public DateTime DateDeleted { get; set; }
    }
}
