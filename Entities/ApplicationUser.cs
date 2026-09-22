using Microsoft.AspNetCore.Identity;

namespace daza_store_be.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName {get; set;} = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Gender { get; set; }

        public string? ProfilePicture { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public bool IsAccountVerified { get; set; } = false;

        public bool IsAccountBlocked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}