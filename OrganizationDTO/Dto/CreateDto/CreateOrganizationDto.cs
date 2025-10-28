using System.ComponentModel.DataAnnotations;

namespace OrganizationDTO.Dto.CreateDto
{
    public class CreateOrganizationDto
    {
        public string? OrganizationName { get; set; }

        public string? OrganizationAddress { get; set; }

        public string? TypeOfOrganization { get; set; }

        public string? OrganizationContact { get; set; }

        public string? AdminEmail { get; set; }

        public string? Website { get; set; }

        public string? ServiceDuration { get; set; }

        [MinLength(1, ErrorMessage = "Please select at least one service type")]
        public List<string>? TypeOfService { get; set; }
    }
}
