namespace OrganizationDTO.Dto
{
    public class LearnersDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? LeanerEmail { get; set; }

        public string? LeanerProfilePicture { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid OrganizationSetupId { get; set; }
    }
}
