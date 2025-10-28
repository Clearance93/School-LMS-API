using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class Guests
    {
        [Key]
        public Guid GuestId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? GuestEmail { get; set; }

        public string? GuestProfilePicture { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid OrganizationSetupId { get; set; }

        public OrganizationSetup? OrganizationSetup { get; set; }
    }
}
