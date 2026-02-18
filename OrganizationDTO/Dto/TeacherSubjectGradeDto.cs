namespace OrganizationDTO.Dto
{
    public class TeacherSubjectGradeDto
    {
        public string? SubjectName { get; set; }

        public string? StreamGradeName { get; set; }

        public Guid TeacherId { get; set; }

        public Guid GradeStreamId { get; set; }
    }
}
