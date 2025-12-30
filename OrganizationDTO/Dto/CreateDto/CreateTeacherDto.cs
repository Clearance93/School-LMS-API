namespace OrganizationDTO.Dto.CreateDto
{
    public class CreateTeacherDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Guid RegistrationLinkId { get; set; }

        public string? TeacherEmail { get; set; }

        public string? TeacherProfilePicture { get; set; }

        public string? Password { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid OrganizationSetupId { get; set; }
    }
}
