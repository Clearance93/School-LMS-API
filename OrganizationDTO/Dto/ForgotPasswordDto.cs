using System.ComponentModel.DataAnnotations;

namespace OrganizationDTO.Dto
{
    public class ForgotPasswordDto
    {
        [Required]
        public string? Email { get; set; }
    }
}
