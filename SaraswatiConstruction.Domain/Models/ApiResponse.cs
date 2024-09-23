using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
