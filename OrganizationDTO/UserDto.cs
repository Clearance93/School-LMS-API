using System.ComponentModel.DataAnnotations;

namespace OrganizationDTO
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public string? Password { get; set; }

        public string? ProfileImage { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? Token { get; set; }

        public DateTime Expiration { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
