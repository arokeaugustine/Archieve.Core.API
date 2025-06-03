using System.Text.Json.Serialization;

namespace Archieve.Core.API.Models.DTOs
{
    public class Books : DeleteDTO
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
