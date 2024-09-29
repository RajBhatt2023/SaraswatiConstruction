namespace SaraswatiConstruction.WebApplication.Models
{
    public class UserDetail
    {
        public string? UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string? Password { get; set; }
        public string? Url { get; set; }
    }

    public class UserDetailResult
    {
        public string? userID { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? gender { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public bool? isActive { get; set; }
        public int? resultCode { get; set; }
        public string? resultDescription { get; set; }
    }

}
