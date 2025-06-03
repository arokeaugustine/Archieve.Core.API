namespace Archieve.Core.API.Models.DTOs
{
    public class ResponseModel<T>
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } 
    }
}
