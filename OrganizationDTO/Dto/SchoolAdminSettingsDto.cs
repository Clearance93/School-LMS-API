namespace OrganizationDTO.Dto
{
    public class SchoolAdminSettingsDto
    {
        public Guid OrganizationId { get; set; }

        public string? SchoolName { get; set; }

        public string? SchoolType { get; set; }

        public string? SchoolMotto { get; set; }

        public string? TimeZone { get; set; }

        public string? Locale { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactPhoneNumber { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
