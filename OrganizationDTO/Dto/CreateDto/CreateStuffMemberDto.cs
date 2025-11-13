namespace OrganizationDTO.Dto.CreateDto
{
    public class CreateStuffMemberDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? StuffmemberEmail { get; set; }

        public string? StuffMemberPosition { get; set; }

        public string? StuffMemberProfilePicture { get; set; }

        public string? Password { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid OrganizationSetupId { get; set; }
    }
}
