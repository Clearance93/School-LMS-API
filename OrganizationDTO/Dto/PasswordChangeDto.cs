namespace OrganizationDTO.Dto
{
    public class PasswordChangeDto
    {
        public string? CurrentPassword { get; set; }
        
        public string? NewPassword { get; set; }

        public string? Email { get; set; }
    }
}
