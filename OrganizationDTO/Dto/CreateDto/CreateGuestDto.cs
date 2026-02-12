using Microsoft.AspNetCore.Http;

namespace OrganizationDTO.Dto.CreateDto
{
    public class CreateGuestDto
    {
        public string? FirstName { get; set; }

        public Guid RegistrationLinkId { get; set; }

        public string? LastName { get; set; }

        public string? GuestEmail { get; set; }

        public string? GuestProfilePicture { get; set; }

        public string? Password { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid OrganizationSetupId { get; set; }
    }
}
