namespace OrganizationDTO.Dto.CreateDto
{
    public class CreateStudentDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? StudentEmail { get; set; }

        public string? StudentProfilePicture { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid OrganizationSetupId { get; set; }
    }
}
