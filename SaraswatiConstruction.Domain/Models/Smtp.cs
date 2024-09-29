namespace SaraswatiConstruction.Domain.Models
{
    public class Smtp
    {
        public string? HostName { get; set; }
        public int Port { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
    }
}
