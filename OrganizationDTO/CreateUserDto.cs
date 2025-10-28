using System.ComponentModel.DataAnnotations;

namespace OrganizationDTO
{
    public class CreateUserDto
    {
        public string? Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? Role { get; set; }

        [Required]
        public string? ProfileImage { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
