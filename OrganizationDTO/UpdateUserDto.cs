using System.ComponentModel.DataAnnotations;

namespace OrganizationDTO
{
    public class UpdateUserDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Password { get; set; }

        public string? ProfileImage { get; set; }

        public DateTime UpdatedAt { get; set; }


    }
}
