using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model.Settings
{
    public class SchoolAdminSettings
    {
        [Key]
        public Guid SchoolAdminSettingsId { get; set; }

        public Guid OrganizationId { get; set; }
        public OrganizationSetup? Organization {  get; set; }

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
