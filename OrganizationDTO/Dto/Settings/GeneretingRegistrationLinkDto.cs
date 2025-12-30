namespace OrganizationDTO.Dto.Settings
{
    public class GeneretingRegistrationLinkDto
    {
        public Guid RegistrationLinkId { get; set; }

        public Guid OrganizationId { get; set; }

        public string? Role { get; set; }

        public int MaxUsers { get; set; }

        public int UserCount { get; set; }

        public bool IsActive { get; set; }

        public string? UrlLink { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
