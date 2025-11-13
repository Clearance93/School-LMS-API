namespace OrganizationDTO.Dto.Settings
{
    public class GradeWithStreamDto
    {
        public Guid GradeId { get; set; }

        public string? GradeName { get; set; }

        public Guid StreamId { get; set; }

        public string? TeacherEmail { get; set; }

        public string? StreamName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
