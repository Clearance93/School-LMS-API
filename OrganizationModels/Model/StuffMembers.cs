using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model
{
    public class StuffMembers
    {
        [Key]
        public Guid StuffMemberId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? StuffmemberEmail { get; set; }

        public string? StuffMemberPosition { get; set; }

        public string? StuffMemberProfilePicture { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid OrganizationSetupId { get; set; }

        public OrganizationSetup? OrganizationSetup { get; set; }
    }
}
