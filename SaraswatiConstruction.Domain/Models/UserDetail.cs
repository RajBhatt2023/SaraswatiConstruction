namespace SaraswatiConstruction.Domain.Models
{
    public class UserDetail
    {
        public string? UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Url { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string? Password { get; set; }
        public int? ResultCode { get; set; }
        public string? ResultDescription { get; set; }
    }
}
