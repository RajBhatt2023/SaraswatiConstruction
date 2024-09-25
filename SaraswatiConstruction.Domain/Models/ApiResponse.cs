using System.Net;

namespace SaraswatiConstruction.Domain.Models
{
    public class ApiResponse
    {
        public object? Data { get; set; }
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
