using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OrganizationModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? ProfileImage { get; set; }

        public string? PasswordResetToken { get; set; }

        public DateTime? TokenExpiration { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
