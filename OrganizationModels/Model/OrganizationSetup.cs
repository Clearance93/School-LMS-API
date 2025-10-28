using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class OrganizationSetup
    {
        [Key]
        public Guid OrganizationSetupId { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationAddress { get; set; }

        public string? TypeOfOrganization { get; set; }

        public string? OrganizationContact { get; set; }

        public string? AdminEmail { get; set; }

        public string? Website { get; set; }

        public string? ServiceDuration { get; set; }

        [MinLength(1, ErrorMessage = "Please select at least one service type")]
        public List<string>? TypeOfService { get; set; }

        public bool? IsDeleted { get; set; } 

        public bool? IsActive { get; set; } 

        public DateTime? Created { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
