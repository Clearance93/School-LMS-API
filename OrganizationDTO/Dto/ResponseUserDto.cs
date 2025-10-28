namespace OrganizationDTO.Dto
{
    public class ResponseUserDto
    {
        public string? Token { get; set; }

        public DateTime Expiration { get; set; }

        public string? Email { get; set; } 
    }
}
