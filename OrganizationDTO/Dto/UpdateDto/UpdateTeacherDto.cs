namespace OrganizationDTO.Dto.UpdateDto
{
    public class UpdateTeacherDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? TeacherProfilePicture { get; set; }

        public long PhoneNumber { get; set; }

        public string? Department { get; set; }

        public string? Qualification { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
